using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

	public float m_fRadius = 1.0f;

	void OnDrawGizmos () {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, m_fRadius);
	}
}
