using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoildStart  {

	TimeClock Time1 = new TimeClock ();

	BornFactory theBornFactory1=new BornFactory(new ModoData1 ());
	BornFactory theBornFactory2=new BornFactory(new ModoData2 ());
	BornFactory theBornFactory3=new BornFactory(new ModoData3 ());
	BornFactory theBornFactory4=new BornFactory(new ModoData4 ());

	ObjPoor theObjPoor1= new ObjPoor();
	ObjPoor theObjPoor2= new ObjPoor();
	ObjPoor theObjPoor3= new ObjPoor();
	ObjPoor theObjPoor4= new ObjPoor();

	protected List<GameObject> InitMySoild = new List<GameObject>();
	protected List<GameObject> InitEmemySoild = new List<GameObject> ();


	public void AwakeCreat(){
		for(int i =0;i<theBornFactory1.GetFMax;i++){
			theObjPoor1.Push2Poor (theBornFactory1.Born ());
		//	Debug.Log (theObjPoor1.GetPoorCount);
		}
		for(int i =0;i<theBornFactory2.GetFMax;i++){
			theObjPoor2.Push2Poor (theBornFactory2.Born ());
		//	Debug.Log (theObjPoor2.GetPoorCount);
		}
		for(int i =0;i<theBornFactory3.GetFMax;i++){
			theObjPoor3.Push2Poor (theBornFactory3.Born ());
		//	Debug.Log (theObjPoor3.GetPoorCount);
		}
		for(int i =0;i<theBornFactory4.GetFMax;i++){
			theObjPoor3.Push2Poor (theBornFactory4.Born ());
			//Debug.Log (theObjPoor4.GetPoorCount);
		}
	}
	// Use this for initialization
	public void Start () {
		
			
	}
	
	// Update is called once per frame
	public void UpdateCreat () {
		if (Time1.TimeCheck (0.2f)) {
			if (theObjPoor1.GetMaxPoorCount < theBornFactory1.GetFMax) {
				for (int i = theBornFactory1.GetFStart; i < theBornFactory1.GetFMax; i++) {
					theObjPoor1.Push2Poor (theBornFactory1.Born ());
				}
			}
			if (theObjPoor2.GetMaxPoorCount < theBornFactory2.GetFMax) {
				for (int i = theBornFactory2.GetFStart; i < theBornFactory2.GetFMax; i++) {
					theObjPoor2.Push2Poor (theBornFactory2.Born ());
				}
			}
			if (theObjPoor4.GetMaxPoorCount < theBornFactory3.GetFMax) {
				for (int i = theBornFactory3.GetFStart; i < theBornFactory3.GetFMax; i++) {
					theObjPoor3.Push2Poor (theBornFactory3.Born ());
				}
			}
			if (theObjPoor4.GetMaxPoorCount < theBornFactory4.GetFMax) {
				for (int i = theBornFactory4.GetFStart; i < theBornFactory4.GetFMax; i++) {
					theObjPoor4.Push2Poor (theBornFactory4.Born ());
				}
			}
		}
	}

	public void Init(int CallAmount,int PoorMember){

		switch (PoorMember) {
		case 1:
			for (int i = 0; i < CallAmount; i++) {
				SaveLiveSoildPoor(theObjPoor1.Poor2Pull ());
			}
			break;
		case 2:
			for (int i = 0; i < CallAmount; i++) {
				SaveLiveSoildPoor(theObjPoor2.Poor2Pull ());
			}
			break;
		case 3:
			for (int i = 0; i < CallAmount; i++) {
				SaveLiveSoildPoor(theObjPoor3.Poor2Pull ());
			}
			break;
		case 4:
			for (int i = 0; i < CallAmount; i++) {
				SaveLiveSoildPoor(theObjPoor4.Poor2Pull ());
			}
			break;
		default:
			Debug.Log ("沒有這角色");
			break;
		}
	
	}

	void SaveLiveSoildPoor(GameObject GB){
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
