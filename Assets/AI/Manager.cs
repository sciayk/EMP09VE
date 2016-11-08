using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Manager : MonoBehaviour {
	private static Manager m_Instance;
	public static Manager Instance () {
		return m_Instance;
	}

	private List<Agent> m_agentList;
	private Obstacle[] m_arrObstacle;

	void Awake () {
		m_Instance = this;
		GameObject[] gos = GameObject.FindGameObjectsWithTag ("Obstacle");
		m_arrObstacle = new Obstacle[gos.Length];
		for (int i = 0; i < gos.Length; i++) {
			m_arrObstacle [i] = gos [i].GetComponent<Obstacle> ();
		}

		m_agentList = new List<Agent> ();
	}

	void Start () {

	}

	public Obstacle[] GetObstacleArray () {
		return m_arrObstacle;
	}

	public List<Agent> GetAgentList () {
		return m_agentList;
	}

	public void AddAgent (Agent agent) {
		m_agentList.Add (agent);
	}
}
