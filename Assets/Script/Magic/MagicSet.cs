using UnityEngine;
using System.Collections;


//user Controal
public class Context{

	MagicBaseState m_MagicState=null;

	public void Request(int Value){
		m_MagicState.Handle (Value);
	}
	//Set GetMagic
	public void SetState(MagicBaseState theState){
		Debug.Log ("ConText.SetState:" + theState);
		m_MagicState = theState;
	}
}


//Magic base
public abstract class MagicBaseState{

	protected Context m_Context = null;

	public MagicBaseState(Context theContext){
		m_Context = theContext;
	}

	public abstract void Handle (int Value);

}
