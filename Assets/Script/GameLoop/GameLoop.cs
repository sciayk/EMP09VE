using UnityEngine;
using System.Collections;

public abstract class  GameLoop : MonoBehaviour {
	void Awake(){
		AwakeAction ();
	}
	// Use this for initialization
	void Start () {
		StartAction ();
	}
	
	// Update is called once per frame
	void Update () {
		UpdataStart ();
	}
	public virtual void AwakeAction (){}
	public virtual void StartAction (){}
	public virtual void UpdataStart (){}
	public virtual void Release(){}
}

