using UnityEngine;
using System.Collections;

public abstract class StateContext {
	public abstract void stateWork (Agent agent);
}

public class Idle : StateContext {
	public override void stateWork (Agent agent) {
		if (agent.m_fSpeed > 0.0f) {
			agent.setStateContext (new Walk ());
			agent.stateWork ();
		} else if (agent.m_bAttack) {
			agent.setStateContext (new Attack ());
			agent.stateWork ();
		}
	}
}

public class Walk : StateContext {
	Animator anim;
	public override void stateWork (Agent agent) {
		anim = agent.m_AgentObj.GetComponent<Animator> ();
		if (agent.m_fSpeed < 4.0f && agent.m_fSpeed > 0.0f) {
			anim.SetBool ("Walk", true);
		} else if (agent.m_fSpeed == 0.0f) {
			anim.SetBool ("Walk", false);
			agent.setStateContext (new Idle ());
			agent.stateWork ();
		} else {
			anim.SetBool ("Walk", false);
			agent.setStateContext (new Run ());
			agent.stateWork ();
		}
	}
}

public class Run : StateContext {
	Animator anim;
	public override void stateWork (Agent agent) {
		anim = agent.m_AgentObj.GetComponent<Animator> ();
		if (!(agent.m_fSpeed < 4.0f)) {
			anim.SetBool ("Run", true);
		} else {
			anim.SetBool ("Run", false);
			agent.setStateContext (new Walk ());
			agent.stateWork ();
		}
	}
}

public class Attack : StateContext {
	Animator anim;
	float fTime = 100.0f;
	string[] sAttackArr = { "Attack 01", "Attack 02" };
 	public override void stateWork (Agent agent) {
		int idAttack = Random.Range (0, sAttackArr.Length);
		anim = agent.m_AgentObj.GetComponent<Animator> ();
		if (agent.m_bAttack) {
			fTime += Time.deltaTime;
			if (fTime >= agent.m_fAttackSpeed) {
				anim.SetTrigger (sAttackArr [idAttack]);
				fTime = 0.0f;
			}
		} else {
			agent.setStateContext (new Idle ());
			agent.stateWork ();
		}
	}
}