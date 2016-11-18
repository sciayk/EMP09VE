using UnityEngine;
using System.Collections;

public class GameCenter : MonoBehaviour {
	SoildStart theSoildStart=new SoildStart();
	public GameObject GB;

	void Awake(){
		theSoildStart.AwakeCreat ();
	}

	// Use this for initialization
	void Start () {
		Debug.Log (GB.GetComponent<Main> ().m_iTeam);
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.Q)) {
			theSoildStart.Init (10,1);
			theSoildStart.Init (10,2);
		}
	}
}
