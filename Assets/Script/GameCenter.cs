using UnityEngine;
using System.Collections;

public class GameCenter : MonoBehaviour {
	
	ObjPoor m_ObjPoor1=new ObjPoor(new ModoData1());
	ObjPoor m_ObjPoor2=new ObjPoor(new ModoData2());
	ObjPoor m_ObjPoor3=new ObjPoor(new ModoData3());
	ObjPoor m_ObjPoor4=new ObjPoor(new ModoData4());

	// Use this for initialization
	void Start () {
		m_ObjPoor1.Awake ();
		m_ObjPoor2.Awake ();
		m_ObjPoor3.Awake ();
		m_ObjPoor4.Awake ();
	}

	
	// Update is called once per frame
	void Update () {
		if (TimeClock (0.5f)) {
			m_ObjPoor1.AutoBorn ();
			m_ObjPoor2.AutoBorn ();
			m_ObjPoor3.AutoBorn ();
			m_ObjPoor4.AutoBorn ();
		}
	}



	protected float T1 = 0;
	public  bool TimeClock (float needTime){
		if ((Time.time-T1) > needTime) {
			T1 = Time.time;
			return true;
		}
		return false;
	}
}
