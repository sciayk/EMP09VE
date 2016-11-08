using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Agent {
	public Transform m_AgentTrans;
	public float m_fProbeLength;
	public float m_fCollisionRadius;
	public float m_fNeighborRange;
	public float m_fSightAngle;
	public float m_fSpeed;
	public float m_fMaxSpeed;
	public float m_fAcc;
	public float m_fRot;
	public float m_fMaxRot;
	public float m_fRotAcc;
	public float m_fSeparationFactor;
	public float m_fCohesionFactor;
	public float m_fAlignmentFactor;
	public bool m_bMove;
	public string m_sAgentType;
	public int m_iIndex;
	public int m_iTeam;
	public Vector3 m_vTargetPos;
	public Obstacle[] m_arrObstacle;
}
