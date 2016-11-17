using UnityEngine;
using System.Collections;

public class TimeClock : MonoBehaviour {
	protected float T1 = 0;
	public  bool TimeCheck (float needTime){
		if ((Time.time-T1) > needTime) {
			T1 = Time.time;
			return true;
		}
		return false;
	}
}
