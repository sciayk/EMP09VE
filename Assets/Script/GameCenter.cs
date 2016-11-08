using UnityEngine;
using System.Collections;

public class GameCenter : MonoBehaviour {
	ObjPoor m_ObjPoor1=new ObjPoor(new ModoData1());
	ObjPoor m_ObjPoor2=new ObjPoor(new ModoData3());
	ObjPoor m_ObjPoor3=new ObjPoor(new ModoData2());
	ObjPoor m_ObjPoor4=new ObjPoor(new ModoData4());
	// Use this for initialization
	void Start () {
		m_ObjPoor1.Awake();
		m_ObjPoor2.Awake();
		m_ObjPoor3.Awake();
		m_ObjPoor4.Awake();
	}
	
	// Update is called once per frame
	void Update () {
		m_ObjPoor1.Updata();
		m_ObjPoor2.Updata();
		m_ObjPoor3.Updata();
		m_ObjPoor4.Updata();
	}
}
