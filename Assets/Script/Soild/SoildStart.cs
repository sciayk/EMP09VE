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
			theObjPoor3.Push2Poor (theBornFactory4.Born ());
			//Debug.Log (theObjPoor4.GetPoorCount);
		}
	}


	public void Init(int CallAmount,int PoorMember){

		switch (PoorMember) {
		case 1:
			for (int i = 0; i < CallAmount; i++) {
				GameCenter.mGameCenter().SaveLiveSoildPoor(theObjPoor1.Poor2Pull ());
			}
			break;
		case 2:
			for (int i = 0; i < CallAmount; i++) {
				GameCenter.mGameCenter().SaveLiveSoildPoor(theObjPoor2.Poor2Pull ());
			}
			break;
		case 3:
			for (int i = 0; i < CallAmount; i++) {
				GameCenter.mGameCenter().SaveLiveSoildPoor(theObjPoor3.Poor2Pull ());
			}
			break;
		case 4:
			for (int i = 0; i < CallAmount; i++) {
				GameCenter.mGameCenter().SaveLiveSoildPoor(theObjPoor4.Poor2Pull ());
			}
			break;
		default:
			Debug.Log ("沒有這角色");
			break;
		}
	
	}





}
