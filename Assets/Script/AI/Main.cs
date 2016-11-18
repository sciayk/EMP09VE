using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour {
	
	public float m_fProbeLength = 10.0f;
	public float m_fCollisionRadius = 0.7f;
	public float m_fNeighborRange = 8.0f;
	public float m_fAttackRange = 1.0f;
	public float m_fAttackSpeed = 1.5f;
	public float m_fSightAngle = 100.0f;
	public float m_fMaxSpeed = 5.0f;
	public float m_fForwardForce = 5.0f;
	public float m_fMaxRot = 0.1f;
	public float m_fSeparationFactor = 2.5f;
	public float m_fCohesionFactor = 0.8f;
//	public float m_fAlignmentFactor = 0.0f;
	public int m_iTeam = 0;
	public int mTeam {
		get { return m_iTeam; }
	}
	public string m_sAgentType = null;
//	public bool m_bFight = false;
//	public bool m_bDiscover = false;
	private Agent m_Agent;
	private Vector3 m_vForward;
	private Vector3 seekVec;
	private Vector3 turnVec;
	private Vector3 flockVec;
//	private Vector3? targetPos;

	void Awake () {
		m_Agent = new Agent ();
		m_Agent.m_AgentObj = this.gameObject;
		m_Agent.m_AgentTrans = transform;
		m_Agent.m_iTeam = m_iTeam;
		m_Agent.m_sAgentType = m_sAgentType;
		m_Agent.m_bDiscover = false;
		m_Agent.m_bAttack = false;
		m_vForward = transform.forward;
	}

	void Start () {
		m_Agent.m_arrObstacle = Manager.Instance ().GetObstacleArray ();
		m_Agent.m_iIndex = Manager.Instance ().agentId;

		if (m_Agent.m_iTeam == 0) {
			Manager.Instance ().AddAgent (m_Agent);
		} else {
			Manager.Instance ().AddEnemy (m_Agent);
		}

		StartCoroutine (SteeringBehavior ());
//		m_Agent.stateWork ();
	}
	
	void Update () {
		// parameters
		m_Agent.m_fProbeLength = m_fProbeLength;
		m_Agent.m_fCollisionRadius = m_fCollisionRadius;
		m_Agent.m_fNeighborRange = m_fNeighborRange;
		m_Agent.m_fAttackRange = m_fAttackRange;
		m_Agent.m_fAttackSpeed = m_fAttackSpeed;
		m_Agent.m_fMaxRot = m_fMaxRot;
		m_Agent.m_fMaxSpeed = m_fMaxSpeed;
		m_Agent.m_fSightAngle = m_fSightAngle;
		m_Agent.m_fSeparationWeight = m_fSeparationFactor;
		m_Agent.m_fCohesionWeight = m_fCohesionFactor;
//		m_Agent.m_bFight = m_bFight;
//		m_Agent.m_bDiscover = m_bDiscover;
	}

	IEnumerator SteeringBehavior () {
		while (true) {
			// 往前的吸引力
			m_Agent.m_vTargetPos = transform.position + m_vForward * m_fForwardForce;
			if (!m_Agent.m_bAttack) {
				m_Agent.m_TargetAgent = AI.FindEnemy (ref m_Agent);
				flockVec = AI.Flocking (ref m_Agent);
			}
			if (m_Agent.m_TargetAgent != null) {
				m_Agent.m_bDiscover = true;
				Vector3 vec = m_Agent.m_TargetAgent.m_AgentTrans.position - m_Agent.m_AgentTrans.position;
				vec.Normalize ();
				m_Agent.m_vTargetPos = m_Agent.m_AgentTrans.position + vec * m_Agent.m_fMaxSpeed;
//				m_Agent.m_vTargetPos = m_Agent.m_TargetAgent.m_AgentTrans.position;
			}
			seekVec = AI.Seek (m_Agent.m_vTargetPos, ref m_Agent);
			turnVec = AI.CollisionAvoid (ref m_Agent);
			AI.Move (seekVec + turnVec + flockVec, ref m_Agent);
			m_Agent.stateWork ();
			yield return new WaitForSeconds (0.02f);
		}
	}



	void OnDrawGizmos () {
//		// 探針範圍
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere (transform.position, m_fCollisionRadius);
//		Vector3 vRight = transform.position + transform.right * m_fCollisionRadius;
//		Vector3 vLeft = transform.position - transform.right * m_fCollisionRadius;
//		Gizmos.DrawLine (vRight, vRight + transform.forward * m_fProbeLength);
//		Gizmos.DrawLine (vLeft, vLeft + transform.forward * m_fProbeLength);
//		Gizmos.DrawLine (vRight + transform.forward * m_fProbeLength, vLeft + transform.forward * m_fProbeLength);
//		// 探針
//		Gizmos.color = Color.blue;
//		Gizmos.DrawLine (transform.position, transform.position + transform.forward * m_fProbeLength);
//		// the range of neighborhood
//		Gizmos.color = Color.grey;
//		Gizmos.DrawWireSphere (transform.position, m_fNeighborRange);
//		// attack range
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, m_fAttackRange);
	}
}
