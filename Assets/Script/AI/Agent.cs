using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Agent {
	public GameObject m_AgentObj;
	public Transform m_AgentTrans;
	public float m_fProbeLength;
	public float m_fCollisionRadius;
	public float m_fNeighborRange;
	public float m_fAttackRange;
	public float m_fAttackSpeed;
	public float m_fSightAngle;
	public float m_fSpeed;
	public float m_fMaxSpeed;
	public float m_fMaxRot;
	public float m_fRotAcc;
	public float m_fSeparationWeight;
	public float m_fCohesionWeight;
//	public float m_fAlignmentFactor;
	public bool m_bDiscover;
	public bool m_bAttack;
	public string m_sAgentType;
	public int m_iIndex;
	public int m_iTeam;
	public Agent m_TargetAgent;
	public Vector3 m_vTargetPos;
	public Obstacle[] m_arrObstacle;

	private StateContext state;

	public Agent () {
		// 初始化狀態處理的物件
		setStateContext (new Run ());
	}
	// 設定狀態處理的物件
	public void setStateContext(StateContext s)
	{
		state = s;
	}
	// 狀態處理，轉交由 StateContext 物件處理
	public void stateWork()
	{
		state.stateWork(this);
	}
}
