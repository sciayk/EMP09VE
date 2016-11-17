using UnityEngine;
using System.Collections;

public class BornFactory :MonoBehaviour {
	
	Vector3 VBorn;
	GameObject Model;
	GameObject TBornPos;
	protected string ModelPath;
	protected string BornPosPath;
	protected int mMaxAmount=0;
	protected int mStartAmount=0;
	public int GetFMax{get{return mMaxAmount; }}
	public int GetFStart{get{return mStartAmount; }}

	public BornFactory(ModelDataBase theModelData){
		ModelPath = theModelData.GetModelName;
		BornPosPath = theModelData.GetBornPointName;
		mMaxAmount = theModelData.GetMaxAmount;
		mStartAmount = theModelData.GetStartAmount;

	}
	public void AwakeStart(){
		
	}
	public GameObject Born(){
		Model = Resources.Load (ModelPath) as GameObject;
		TBornPos = Resources.Load (BornPosPath) as GameObject;
		BornPos (TBornPos.transform);
		GameObject GB = Instantiate (Model);
		GB.transform.position = VBorn;
		GB.SetActive (false);
		return GB;
	}

	Vector3 BornPos(Transform BornA){
		float[] fBornPos=new float[2];
		Vector3 BPS=  BornA.localScale;
		Vector3 BPP = BornA.position;
		fBornPos[0]=Random.Range ((-BPS.x/2 +BPP.x),(BPS.x/2 +BPP.x) );
		fBornPos[1]=Random.Range ((-BPS.z/2 +BPP.z),(BPS.z/2 +BPP.z) );
		//Debug.Log (fBornPos[0]);
		VBorn = new Vector3 (fBornPos[0],0.5f,fBornPos[1]);
		//Debug.Log (VBorn);
		return VBorn;
	}

	//按鈕生小兵
//	public void StartBorn(){
//		needPeople=int.Parse(INSoild.text)-1;
//		if ((nowAmount + needPeople) < MaxAmount) {
//			while (needPeople > -1) {
//				AutoBorn ();
//			}
//		} else {
//			Debug.Log ("你不能擁有那麼多，人數超過上限");
//		}
//	}
}
