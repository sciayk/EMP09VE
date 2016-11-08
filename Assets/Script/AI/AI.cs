using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AI : MonoBehaviour {
	public static void Move (Vector3 forceVec, ref Agent agent) {
		float fSpeed = Vector3.Dot(forceVec, agent.m_AgentTrans.forward);
		if (fSpeed > agent.m_fMaxSpeed) {
			fSpeed = agent.m_fMaxSpeed;
		} else if (fSpeed < 0.0f) {
			fSpeed = 0.0f;
		}
		Debug.Log ("Speed: " + fSpeed);
		agent.m_fSpeed = fSpeed;

		Vector3 vDir = forceVec.normalized;
		Vector3 vForward = agent.m_AgentTrans.forward;
		Vector3 vRot = vDir - vForward;
		float fRotMag = vRot.magnitude;
		if (fRotMag > agent.m_fMaxRot) {
			fRotMag = agent.m_fMaxRot;
		} 
		if (vDir != -agent.m_AgentTrans.forward) {
			agent.m_AgentTrans.forward = vForward + vRot * fRotMag;
		} else {	// 避免目標點在正背面
			agent.m_AgentTrans.Rotate (0.0f, 1.0f, 0.0f);
		}
		// 合力
		Debug.DrawLine (agent.m_AgentTrans.position, agent.m_AgentTrans.position + forceVec, Color.cyan);
		agent.m_AgentTrans.position += agent.m_AgentTrans.forward * agent.m_fSpeed * Time.deltaTime;
	}

	public static Vector3 Seek (Vector3 targetPos, ref Agent agent) {
		Vector3 seekVec = targetPos - agent.m_AgentTrans.position;
		seekVec.y = 0.0f;
//		float fSpeed = seekVec.magnitude;
//		if (fSpeed > agent.m_fMaxSpeed) {
//			fSpeed = agent.m_fMaxSpeed;
//		}
//		seekVec.Normalize ();
//		seekVec *= fSpeed;
		// seek方向
		Debug.DrawLine (agent.m_AgentTrans.position, agent.m_AgentTrans.position + seekVec, Color.red);
		return seekVec;
	}

	public static Vector3 CollisionAvoid (ref Agent agent) {
		Vector3 curPos = agent.m_AgentTrans.position;
		Vector3 curForward = agent.m_AgentTrans.forward;
		Vector3 curVec;
		float fDist;
		float fDot;
		float fProjDist;
		float fProjDist2 = 1.0f;
		float fMiniDist = 10000.0f;
		Obstacle closeObstacle = null;	// 最靠近的障礙物
		int iNumObstacle = agent.m_arrObstacle.Length;
		for (int i = 0; i < iNumObstacle; i++) {
			curVec = agent.m_arrObstacle [i].transform.position - curPos;
			fDist = curVec.magnitude;
			if (fDist < agent.m_fProbeLength) {
				fDot = Vector3.Dot (curVec.normalized, curForward);
				if (fDot > Mathf.Cos (agent.m_fSightAngle * Mathf.Deg2Rad)) {	// 不考慮視角外的障礙物
					fProjDist = fDist * fDot;
					fProjDist = Mathf.Sqrt(curVec.magnitude * curVec.magnitude - fProjDist * fProjDist);
					if (fProjDist > agent.m_arrObstacle [i].m_fRadius + agent.m_fCollisionRadius) {
						continue;
					}
					if (fDist < fMiniDist) {
						fMiniDist = fDist;
						fProjDist2 = fProjDist;
						closeObstacle = agent.m_arrObstacle [i];
					}
				}
			} 
		}
		if (closeObstacle != null) {
			Vector3 vec = closeObstacle.transform.position - agent.m_AgentTrans.position;
			Vector3 vForward = curForward * Vector3.Dot (vec, curForward);
			Vector3 vRot = vForward - vec;
			vRot.y = 0.0f;
			vRot.Normalize ();
			if (fProjDist2 == 0.0f) {
				vRot = Vector3.right;
			}
			// 距離越近轉向力越大
			vRot *= 10.0f * (1.0f - fProjDist2 / (closeObstacle.m_fRadius + agent.m_fCollisionRadius));
			// 轉向力
			Debug.DrawLine (agent.m_AgentTrans.position, agent.m_AgentTrans.position + vRot, Color.grey);
			return vRot;
		} else {
			return Vector3.zero;
		}
	}

	private static Vector3 Separation (ref Agent agent) {
		Vector3 curPos = agent.m_AgentTrans.position;
		Vector3 curForward = agent.m_AgentTrans.forward;
		Vector3 curVec;
		Vector3 separateVec = Vector3.zero;
		float fDist;
		float fDot;
		int iCount = 0;
		var agentList = Manager.Instance ().GetAgentList ();
		// find neighborhood
		foreach (Agent a in agentList) {
			if (a.m_iIndex == agent.m_iIndex || a.m_iTeam != agent.m_iTeam) {	// 不考慮自己與敵隊
				continue;
			}
			curVec = a.m_AgentTrans.position - curPos;
			fDist = curVec.magnitude;
			if (fDist < 3) {
				curVec.Normalize ();
				fDot = Vector3.Dot (curVec, curForward);
//				if (fDot > Mathf.Cos (agent.m_fSightAngle * Mathf.Deg2Rad)) {	// 不考慮視角外的鄰居
					curVec /= fDist;
					separateVec -= curVec;
					iCount++;
//				}
			}
		}
		if (iCount > 0) {
			separateVec /= iCount;
			separateVec.y = 0.0f;
//			float fSpeed = separateVec.magnitude;
//			if (fSpeed > agent.m_fMaxSpeed) {
//				fSpeed = agent.m_fMaxSpeed;
//			}
//			separateVec.Normalize ();
//			separateVec *= fSpeed;
		} 

		return separateVec;
	}

	private static Vector3 Cohesion (ref Agent agent) {
		Vector3 curPos = agent.m_AgentTrans.position;
		Vector3 curForward = agent.m_AgentTrans.forward;
		Vector3 curVec;
		Vector3 centralPos = Vector3.zero;
		float fDist;
		float fDot;
		int iCount = 0;
		var agentList = Manager.Instance ().GetAgentList ();
		// find neighborhood
		foreach (Agent a in agentList) {
			if (a.m_iIndex == agent.m_iIndex || a.m_iTeam != agent.m_iTeam || !a.m_sAgentType.Equals(agent.m_sAgentType)) {	// 不考慮自己與敵隊
				continue;
			}
			curVec = a.m_AgentTrans.position - curPos;
			fDist = curVec.magnitude;
			if (fDist < agent.m_fNeighborRange) {
				curVec.Normalize ();
				fDot = Vector3.Dot (curVec, curForward);
//				if (fDot > Mathf.Cos (agent.m_fSightAngle * Mathf.Deg2Rad)) {	// 不考慮視角外的鄰居
					centralPos += a.m_AgentTrans.position;
					iCount++;
//				}
			}
		}

		if (iCount > 0) {
			// central position of neighborhood
			centralPos /= iCount;
			// seek the central position
			return Seek (centralPos, ref agent);
		} else {
			return Vector3.zero;
		}
	}

	private static Vector3 Alignment (ref Agent agent) {
		Vector3 curPos = agent.m_AgentTrans.position;
		Vector3 curForward = agent.m_AgentTrans.forward;
		Vector3 curVec;
		Vector3 alignVec = Vector3.zero;
		float fDist;
		float fDot;
		int iCount = 0;
		var agentList = Manager.Instance ().GetAgentList ();
		// find neighborhood
		foreach (Agent a in agentList) {
			if (a.m_iIndex == agent.m_iIndex || a.m_iTeam != agent.m_iTeam || !a.m_sAgentType.Equals(agent.m_sAgentType)) {	// 不考慮自己與敵隊
				continue;
			}
			curVec = a.m_AgentTrans.position - curPos;
			fDist = curVec.magnitude;
			if (fDist < agent.m_fNeighborRange) {
				alignVec += a.m_AgentTrans.forward;
				iCount++;
			}
		}

		if (iCount > 0) {
			alignVec /= iCount;
			alignVec.y = 0.0f;
			float fSpeed = alignVec.magnitude;
			if (fSpeed > agent.m_fMaxSpeed) {
				fSpeed = agent.m_fMaxSpeed;
			}
			alignVec.Normalize ();
			alignVec *= fSpeed;
			return alignVec;
		} else {
			return Vector3.zero;
		}
	}

	public static Vector3 Flocking (ref Agent agent) {
		Vector3 separateVec = Separation (ref agent);
		Vector3 cohesionVec = Cohesion (ref agent);
		Vector3 alignVec = Alignment (ref agent);

		separateVec *= agent.m_fSeparationFactor;
		cohesionVec *= agent.m_fCohesionFactor;
		alignVec *= agent.m_fAlignmentFactor;
		// 排斥力
		Debug.DrawLine (agent.m_AgentTrans.position, agent.m_AgentTrans.position + separateVec, Color.magenta);
		// alignment 
		Debug.DrawLine (agent.m_AgentTrans.position, agent.m_AgentTrans.position + alignVec, Color.yellow);
		return separateVec + cohesionVec + alignVec;
	}
}
