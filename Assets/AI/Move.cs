using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Move : MonoBehaviour {
	public float m_fProbeLength = 5.0f;
	public float m_fCollisionRadius = 1.0f;
	public float m_fNeighborRange = 10.0f;
	public float m_fSightAngle = 110.0f;
	public float m_fMaxSpeed = 5.0f;
//	public float m_fAcc = 1.0f;
//	public float m_fRot;
	public float m_fForwardForce = 5.0f;
	public float m_fMaxRot = 0.5f;
	public float m_fSeparationFactor = 1.0f;
	public float m_fCohesionFactor = 1.0f;
	public float m_fAlignmentFactor = 1.0f;
	public int m_iTeam = 0;
	public string m_sAgentType = null;
	private Agent m_Agent;
	public Transform m_targetTr;

	void Awake () {
		m_Agent = new Agent ();
		m_Agent.m_AgentTrans = transform;
//		m_Agent.m_fAcc = m_fAcc;
		m_Agent.m_iTeam = m_iTeam;
		m_Agent.m_sAgentType = m_sAgentType;
	}

	void Start () {
		m_Agent.m_arrObstacle = Manager.Instance ().GetObstacleArray ();
		int iIndex = Manager.Instance ().GetAgentList ().Count;
		m_Agent.m_iIndex = iIndex;
		Manager.Instance ().AddAgent (m_Agent);
	}
	
	void Update () {
		// parameters
		m_Agent.m_fProbeLength = m_fProbeLength;
		m_Agent.m_fCollisionRadius = m_fCollisionRadius;
		m_Agent.m_fNeighborRange = m_fNeighborRange;
		m_Agent.m_fMaxRot = m_fMaxRot;
		m_Agent.m_fMaxSpeed = m_fMaxSpeed;
		m_Agent.m_fSightAngle = m_fSightAngle;
		m_Agent.m_fSeparationFactor = m_fSeparationFactor;
		m_Agent.m_fCohesionFactor = m_fCohesionFactor;
		m_Agent.m_fAlignmentFactor = m_fAlignmentFactor;
		// 往前的吸引力
		m_Agent.m_vTargetPos = transform.position + m_targetTr.forward * m_fForwardForce;
		Vector3 seekVec = AI.Seek (m_Agent.m_vTargetPos, ref m_Agent);
		Vector3 turnVec = AI.CollisionAvoid (ref m_Agent);
		Vector3 flockVec = AI.Flocking (ref m_Agent);
		AI.Move (seekVec + turnVec + flockVec, ref m_Agent);

	}

	void OnDrawGizmos () {
		// 探針範圍
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere (transform.position, m_fCollisionRadius);
		Vector3 vRight = transform.position + transform.right * m_fCollisionRadius;
		Vector3 vLeft = transform.position - transform.right * m_fCollisionRadius;
		Gizmos.DrawLine (vRight, vRight + transform.forward * m_fProbeLength);
		Gizmos.DrawLine (vLeft, vLeft + transform.forward * m_fProbeLength);
		Gizmos.DrawLine (vRight + transform.forward * m_fProbeLength, vLeft + transform.forward * m_fProbeLength);
		// 探針
		Gizmos.color = Color.blue;
		Gizmos.DrawLine (transform.position, transform.position + transform.forward * m_fProbeLength);
		// the range of neighborhood
		Gizmos.color = Color.grey;
		Gizmos.DrawWireSphere (transform.position, m_fNeighborRange);
	}
}
