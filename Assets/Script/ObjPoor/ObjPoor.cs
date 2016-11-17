using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ObjPoor {
	
	private int nowAmount;
	public int GetNowAmount{get{ return nowAmount;}}
	private int MaxPoorCount=0;
	private Queue<GameObject> GameObjPoor = new Queue<GameObject> ();
	public int GetMaxPoorCount{get{return MaxPoorCount; }}
	public int GetPoorCount{get{return GameObjPoor.Count;}}

	//推進物件池
	public void Push2Poor(GameObject Gb){
		Gb.SetActive (false);
		GameObjPoor.Enqueue(Gb);
		MaxPoorCount++;
	}

	public void DiePush2Poor(GameObject Gb){
		Gb.SetActive (false);
		GameObjPoor.Enqueue(Gb);
		nowAmount--;
	}
	//拉出物件池
	public void Poor2Pull(){
		if (GameObjPoor.Count > 0) {
			GameObject AgalnPro = GameObjPoor.Dequeue ();
			AgalnPro.SetActive (true);
			nowAmount++;
		} 
	}

	public void ReCallSoild(){
		GameObject AgalnPro = GameObjPoor.Dequeue ();
		AgalnPro.SetActive (true);
		nowAmount++;
	}

}



