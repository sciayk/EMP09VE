using UnityEngine;
using System.Collections;

public class GameCenter : MonoBehaviour {
	SoildStart theSoildStart=new SoildStart();
	void Awake(){
		theSoildStart.AwakeCreat ();
	}
	// Use this for initialization
	void Start () {
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Q)) {
			theSoildStart.Init (10);
		}
	}
		
}
