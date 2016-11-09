using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Move : MonoBehaviour {
	public float m_fProbeLength = 10.0f;
	public float m_fCollisionRadius = 1.0f;
	public float m_fNeighborRange = 10.0f;
	public float m_fSightAngle = 100.0f;
	public float m_fMaxSpeed = 10.0f;
//	public float m_fAcc = 1.0f;
//	public float m_fRot;
	public float m_fForwardForce = 5.0f;
	public float m_fMaxRot = 0.1f;
	public float m_fSeparationFactor = 3.0f;
	public float m_fCohesionFactor = 0.3f;
	public float m_fAlignmentFactor = 0.0f;
	public int m_iTeam = 0;
	public string m_sAgentType = null;
	private Agent m_Agent;
//	public Transform m_targetTr;
	private Vector3 m_vForward;
	private Vector3 seekVec;
	private Vector3 turnVec;
	private Vector3 flockVec;

	private bool go = false;
	void Awake () {
		m_Agent = new Agent ();
		m_Agent.m_AgentTrans = transform;
//		m_Agent.m_fAcc = m_fAcc;
		m_Agent.m_iTeam = m_iTeam;
		m_Agent.m_sAgentType = m_sAgentType;
		m_vForward = transform.forward;
	}

	void Start () {
		m_Agent.m_arrObstacle = Manager.Instance ().GetObstacleArray ();
		int iIndex = Manager.Instance ().GetAgentList ().Count;
		m_Agent.m_iIndex = iIndex;
		Manager.Instance ().AddAgent (m_Agent);
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

//		StartCoroutine (SteeringBehavior ());
	}
	
	void Update () {
		if (Input.GetKeyDown (KeyCode.A)) {
			go = true;
		}

		if (go) {
			// 往前的吸引力
			m_Agent.m_vTargetPos = transform.position + m_vForward * m_fForwardForce;
			seekVec = AI.Seek (m_Agent.m_vTargetPos, ref m_Agent);
			turnVec = AI.CollisionAvoid (ref m_Agent);
			flockVec = AI.Flocking (ref m_Agent);
			AI.Move (seekVec + turnVec + flockVec, ref m_Agent);
		}
	}

	IEnumerator SteeringBehavior () {
		while (true) {
			// 往前的吸引力
			m_Agent.m_vTargetPos = transform.position + m_vForward * m_fForwardForce;
			seekVec = AI.Seek (m_Agent.m_vTargetPos, ref m_Agent);
			turnVec = AI.CollisionAvoid (ref m_Agent);
			flockVec = AI.Flocking (ref m_Agent);
			AI.Move (seekVec + turnVec + flockVec, ref m_Agent);

			yield return new WaitForSeconds (0.02f);
		}
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
