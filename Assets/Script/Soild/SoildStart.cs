using UnityEngine;
using System.Collections;


public class SoildStart  {

	BornFactory theBornFactory1=new BornFactory(new ModoData1 ());
	BornFactory theBornFactory2=new BornFactory(new ModoData2 ());
	BornFactory theBornFactory3=new BornFactory(new ModoData3 ());
	BornFactory theBornFactory4=new BornFactory(new ModoData4 ());

	ObjPoor theObjPoor1= new ObjPoor();
	ObjPoor theObjPoor2= new ObjPoor();
	ObjPoor theObjPoor3= new ObjPoor();
	ObjPoor theObjPoor4= new ObjPoor();

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
			theObjPoor4.Push2Poor (theBornFactory4.Born ());
			//Debug.Log (theObjPoor4.GetPoorCount);
		}
	}


	public void Init(int CallAmount,int PoorMember){
		
		switch (PoorMember) {
		case 1:
			if (theObjPoor1.GetPoorCount >= 0) {
				for (int i = 0; i < CallAmount; i++) {
					GameCenter.mGameCenter ().SaveLiveSoildPoor (theObjPoor1.Poor2Pull ());
				}
			} else {
				Debug.Log ("沒人了");}
			break;
		case 2:
			if (theObjPoor2.GetPoorCount >= 0) {
				for (int i = 0; i < CallAmount; i++) {
					GameCenter.mGameCenter ().SaveLiveSoildPoor (theObjPoor2.Poor2Pull ());
				}
			}else {
				Debug.Log ("沒人了");}
			break;
		case 3:
			if (theObjPoor3.GetPoorCount >= 0) {
				for (int i = 0; i < CallAmount; i++) {
					GameCenter.mGameCenter ().SaveLiveSoildPoor (theObjPoor3.Poor2Pull ());
				}
			}else {
				Debug.Log ("沒人了");}
			break;
		case 4:
			if (theObjPoor4.GetPoorCount >= 0) {
				for (int i = 0; i < CallAmount; i++) {
					GameCenter.mGameCenter ().SaveLiveSoildPoor (theObjPoor4.Poor2Pull ());
				}
			}else {
				Debug.Log ("沒人了");}
			break;
		default:
			Debug.Log ("沒有這角色");
			break;
		}
	
	}

	public void ReSoure(GameObject ReSoildGB){

		switch (ReSoildGB.name) {
		case "Cube1":
			theObjPoor1.Push2Poor (ReSoildGB);
			break;
		case "Cube2":
			theObjPoor2.Push2Poor (ReSoildGB);
			break;
		case "Cube3":
			theObjPoor3.Push2Poor (ReSoildGB);
			break;
		case "Cube4":
			theObjPoor4.Push2Poor (ReSoildGB);
			break;
		default:
			Debug.Log ("沒有這物件池");
			break;
		}

	}

	public void ShowPoorCount(){
		Debug.Log ("Poor1: "+theObjPoor1.GetPoorCount);
		Debug.Log ("Poor2: "+theObjPoor2.GetPoorCount);
		Debug.Log ("Poor3: "+theObjPoor3.GetPoorCount);
		Debug.Log ("Poor4: "+theObjPoor4.GetPoorCount);
	}



}
