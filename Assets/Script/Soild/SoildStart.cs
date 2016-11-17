using UnityEngine;
using System.Collections;

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

	public void Init(int CallAmount){
		for (int i = 0; i < CallAmount; i++) {
			theObjPoor1.Poor2Pull ();
			theObjPoor2.Poor2Pull ();
			theObjPoor3.Poor2Pull ();
			theObjPoor4.Poor2Pull ();
		}
	}
}
