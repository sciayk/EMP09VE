using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AI {

	public static void Move (Vector3 forceVec, ref Agent agent) {
		float fSpeed = Vector3.Dot(forceVec, agent.m_AgentTrans.forward);
		if (fSpeed > agent.m_fMaxSpeed) {
			fSpeed = agent.m_fMaxSpeed;
		} else if (fSpeed < 0.0f) {
			fSpeed = 0.0f;
		}

		if (isAttack (agent)) {
			agent.m_bAttack = true;
			forceVec = agent.m_TargetAgent.m_AgentTrans.position - agent.m_AgentTrans.position;
			fSpeed = 0.0f;
		} else {
			agent.m_bAttack = false;
		}
//		Debug.Log ("Speed: " + fSpeed);
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
//		seekVec.Normalize ();
//		seekVec *= agent.m_fMaxSpeed;
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
			vRot.Normalize ();	if (fProjDist2 == 0.0f) {
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

	public static Vector3 Flocking (ref Agent agent) {
		return SeparationAndCohesion (ref agent);
	}

	public static Agent FindEnemy (ref Agent agent) {
		// find the closest enemy
		Vector3 curPos = agent.m_AgentTrans.position;
		Vector3 curForward = agent.m_AgentTrans.forward;
		Vector3 curVec;
		Agent targetAgent = null;
		float fDist;
		float fMiniDist = 10000.0f;
		var agentList = Manager.Instance ().GetEnemyList ();
		if (agent.m_iTeam == 1) {
			agentList = Manager.Instance ().GetAgentList ();
		}

		foreach (Agent a in agentList) {
			curVec = a.m_AgentTrans.position - curPos;
			fDist = curVec.magnitude;
			if (fDist < agent.m_fNeighborRange) {
				if (fDist < fMiniDist) {
					fMiniDist = fDist;
					targetAgent = a;
				}
			}
		}

		return targetAgent;
	}

	private static bool isAttack (Agent agent) {
		if (agent.m_TargetAgent != null) {
			Vector3 vec = agent.m_AgentTrans.position - agent.m_TargetAgent.m_AgentTrans.position;
			float fDist = vec.magnitude;
			if ((agent.m_fAttackRange + agent.m_TargetAgent.m_fCollisionRadius) >= fDist) {
				return true;
			}
		}
		return false;
	}

	private static Vector3 SeparationAndCohesion (ref Agent agent) {
		Vector3 curPos = agent.m_AgentTrans.position;
		Vector3 curForward = agent.m_AgentTrans.forward;
		Vector3 curVec;
		Vector3 separateVec = new Vector3 (0.0f, 0.0f, 0.0f);
		Vector3 cohesionVec = new Vector3 (0.0f, 0.0f, 0.0f);
		Vector3 centralPos = new Vector3 (0.0f, 0.0f, 0.0f);
		float fDist;
		float fDot;
		int iSepCount = 0;
		int iCohCount = 0;
		var agentList = Manager.Instance ().GetAgentList ();
		// 只找各自隊伍的小兵
		if (agent.m_iTeam == 1) {
			agentList = Manager.Instance ().GetEnemyList ();
		}
		// find neighborhood
		foreach (Agent a in agentList) {
			// 不考慮自己
			if (a.m_iIndex == agent.m_iIndex) {	
				continue;
			}
			curVec = a.m_AgentTrans.position - curPos;
			fDist = curVec.magnitude;
			if (fDist < agent.m_fNeighborRange) {
				curVec.Normalize ();
				fDot = Vector3.Dot (curVec, curForward);
//				if (fDot > Mathf.Cos (agent.m_fSightAngle * Mathf.Deg2Rad)) {	// 不考慮視角外的鄰居
					// Separation
					if (!a.m_sAgentType.Equals (agent.m_sAgentType)) {
//					curVec /= fDist;
//					separateVec -= curVec;
						separateVec -= (curVec / fDist);
						iSepCount++;
					} else {	// Cohesion and Separation
//					curVec /= fDist;
//					separateVec -= curVec;
						separateVec -= (curVec / fDist);
						iSepCount++;
						if (!agent.m_bDiscover) {
							centralPos += a.m_AgentTrans.position;
							iCohCount++;
						}
					}
//				}
			}
		}

		if (iSepCount > 0) {
			separateVec /= iSepCount;
			separateVec.y = 0.0f;
			separateVec *= agent.m_fMaxSpeed;
			separateVec *= agent.m_fSeparationWeight;
		}
		if (iCohCount > 0) {
			// central position of neighborhood
			centralPos /= iCohCount;
			// seek the central position
			cohesionVec = Seek (centralPos, ref agent);
			cohesionVec *= agent.m_fCohesionWeight;
		} 
		// 排斥力
		Debug.DrawLine (agent.m_AgentTrans.position, agent.m_AgentTrans.position + separateVec, Color.magenta);

		return separateVec + cohesionVec;
	}


	//	private static Vector3 Alignment (ref Agent agent) {
	//		Vector3 curPos = agent.m_AgentTrans.position;
	//		Vector3 curForward = agent.m_AgentTrans.forward;
	//		Vector3 curVec;
	//		Vector3 alignVec = Vector3.zero;
	//		float fDist;
	//		float fDot;
	//		int iCount = 0;
	//		var agentList = Manager.Instance ().GetAgentList ();
	//		// find neighborhood
	//		foreach (Agent a in agentList) {
	//			if (a.m_iIndex == agent.m_iIndex || a.m_iTeam != agent.m_iTeam || !a.m_sAgentType.Equals(agent.m_sAgentType)) {	// 不考慮自己與敵隊
	//				continue;
	//			}
	//			curVec = a.m_AgentTrans.position - curPos;
	//			fDist = curVec.magnitude;
	//			if (fDist < agent.m_fNeighborRange) {
	//				alignVec += a.m_AgentTrans.forward;
	//				iCount++;
	//			}
	//		}
	//
	//		if (iCount > 0) {
	//			alignVec /= iCount;
	//			alignVec.y = 0.0f;
	//			float fSpeed = alignVec.magnitude;
	//			if (fSpeed > agent.m_fMaxSpeed) {
	//				fSpeed = agent.m_fMaxSpeed;
	//			}
	//			alignVec.Normalize ();
	//			alignVec *= fSpeed;
	//			return alignVec;
	//		} else {
	//			return Vector3.zero;
	//		}
	//	}

}
