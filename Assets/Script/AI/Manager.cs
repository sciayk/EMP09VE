using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Manager : MonoBehaviour {
	private static Manager m_Instance;
	public static Manager Instance () {
		return m_Instance;
	}

	private List<Agent> m_AgentList;
	private List<Agent> m_EnemyList;
	private Obstacle[] m_ArrObstacle;
	private int _agentId = -1;

	void Awake () {
		m_Instance = this;
		GameObject[] gos = GameObject.FindGameObjectsWithTag ("Obstacle");
		m_ArrObstacle = new Obstacle[gos.Length];
		for (int i = 0; i < gos.Length; i++) {
			m_ArrObstacle [i] = gos [i].GetComponent<Obstacle> ();
		}
		
		m_AgentList = new List<Agent> ();
		m_EnemyList = new List<Agent> ();
	}

	public int agentId {
		get {
			_agentId++;
			return _agentId;
		}
	}

	public Obstacle[] GetObstacleArray () {
		return m_ArrObstacle;
	}

	public List<Agent> GetAgentList () {
		return m_AgentList;
	}

	public void AddAgent (Agent agent) {
		m_AgentList.Add (agent);
	}

	public List<Agent> GetEnemyList () {
		return m_EnemyList;
	}

	public void AddEnemy (Agent agent) {
		m_EnemyList.Add (agent);
	}
}
