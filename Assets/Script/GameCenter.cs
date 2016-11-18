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
			theSoildStart.Init (10,3);
			theSoildStart.Init (10,4);
			ShowListCount ();
			theSoildStart.ShowPoorCount();

		}
		if (Input.GetKeyUp (KeyCode.W)) {
			GameObject a = GameObject.Find ("Cube1(Clone)");
			GameCenter.mGameCenter().SoildDie (a);
			 a = GameObject.Find ("Cube2(Clone)");
			GameCenter.mGameCenter().SoildDie (a);
			 a = GameObject.Find ("Cube3(Clone)");
			GameCenter.mGameCenter().SoildDie (a);
			 a = GameObject.Find ("Cube4(Clone)");
			GameCenter.mGameCenter().SoildDie (a);

			ShowListCount ();
			theSoildStart.ShowPoorCount();
		}
	}
	void ShowListCount(){
		Debug.Log ("MyCount: "+GameCenter.mGameCenter().GetMyListMember().Count);
		Debug.Log ("EmemyCount: "+GameCenter.mGameCenter().GetEmemySoildMember().Count);
	}

	public void SaveLiveSoildPoor(GameObject GB){
		if (GB.GetComponent<Main> ().mTeam == 0) {
			InitMySoild.Add (GB);
		} else if (GB.GetComponent<Main> ().mTeam == 1) {
			InitEmemySoild.Add (GB);
		}
	}

	public void SoildDie(GameObject GG){
		if (GG.GetComponent<Main>().mTeam == 0) {
			InitMySoild.Remove (GG);
		}else if (GG.GetComponent<Main>().mTeam == 1) {
			InitEmemySoild.Remove (GG);
		}
		theSoildStart.ReSoure (GG);
	}

	public List<GameObject> GetMyListMember(){
		return InitMySoild;
	}

	public List<GameObject> GetEmemySoildMember(){
		return InitEmemySoild;
	}
}
