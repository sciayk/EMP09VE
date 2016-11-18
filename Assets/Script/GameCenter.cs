using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameCenter : MonoBehaviour {
	private static GameCenter theGameCenter;
	public static GameCenter mGameCenter(){
		return theGameCenter; 
	}


	protected List<GameObject> InitMySoild = new List<GameObject>();
	protected List<GameObject> InitEmemySoild = new List<GameObject> ();

	SoildStart theSoildStart=new SoildStart();


	void Awake(){
		theGameCenter = this;

	}

	// Use this for initialization
	void Start () {
		theSoildStart.AwakeCreat ();
	}

	// Update is called once per frame
	void Update () {
		
		if (Input.GetKey (KeyCode.Q)) {
			theSoildStart.Init (10,1);
			theSoildStart.Init (10,2);
		}
	}
	public void SaveLiveSoildPoor(GameObject GB){
		if (GB.GetComponent<Main> ().mTeam == 0) {
			InitMySoild.Add (GB);
		} else if (GB.GetComponent<Main> ().mTeam == 1) {
			InitEmemySoild.Add (GB);
		}
	}

	public List<GameObject> GetMyListMember(){
		return InitMySoild;
	}

	public List<GameObject> GetEmemySoildMember(){
		return InitEmemySoild;
	}
}
