using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ObjPoor  :MonoBehaviour{

	private GameObject BronPos;
	private GameObject RePf;
	private string Path;
	private string BornPath;
	private Vector3 QuatV3;
	public int MaxAmount ;
	public int StartAmount;

	//計算數量
	private int nowAmount;
	public int needPeople;
	public InputField INSoild;
	public int _GetnowAmount{ get{ return nowAmount;}}

	public ObjPoor(ModoDataBase Data){
		Path = "Prefab/" + Data.mModeName;
		MaxAmount = Data.mMaxAmount;
		StartAmount = Data.mStartAmount;
		nowAmount = Data.mnowAmount;
		needPeople = Data.mneedPeople;
		BornPath = "BornPoint/" + Data.mBornPointName;
		QuatV3 = Data.mQuatV3;
	}


	float[] OffA;
	private Queue<GameObject> m_ObjPoor = new Queue<GameObject> ();



	public void Awake(){
		RePf = Resources.Load (Path) as GameObject;
		BronPos = Resources.Load (BornPath)as GameObject;

		for (int i = 0; i < StartAmount; i++) {
			GameObject Soild = Instantiate (RePf);
			m_ObjPoor.Enqueue (Soild);
			Soild.SetActive (false);
		}
	}


	// Update is called once per frame
	public  void Updata () {
		AutoBorn ();
	}

	void InstancePeople(int needAmount){
		while (needAmount > -1) {
			if (m_ObjPoor.Count > 0) {
				GameObject AgalnPro = m_ObjPoor.Dequeue ();
				BornPos (BronPos);
				AgalnPro.transform.position = new Vector3 (OffA [0], 0.5f, OffA [1]);
				//AgalnPro.transform.Rotate (QuatV3);
				AgalnPro.SetActive (true);
				nowAmount++;
				needAmount --;
				Debug.Log ("使用現有");
			} else {
				BornPos (BronPos);
				Instantiate (RePf, new Vector3 (OffA [0], 0.5f, OffA [1]),Quaternion.Euler(QuatV3));
				nowAmount++;
				needAmount --;
				Debug.Log ("新創");
			}
		}
	}


	float[] BornPos(GameObject BornA){
		OffA=new float[2];
		Vector3 BPS=BornA.transform.localScale;
		Vector3 BPP = BornA.transform.position;

		OffA[1]=Random.Range (-BPS.z/2 +BPP.z  ,BPS.z/2 +BPP.z );
		OffA[0]=Random.Range (-BPS.x/2 +BPP.x,BPS.x/2+BPP.x);

		return OffA;
	}

	public void RePrefab(GameObject Re)
	{
		m_ObjPoor.Enqueue ( Re );
		Re.SetActive ( false );
		nowAmount--;
	}

	public void AutoBorn(){
		if (nowAmount < MaxAmount) {
			if (m_ObjPoor.Count > 0) {
				GameObject AgalnPro = m_ObjPoor.Dequeue ();
				BornPos (BronPos);
				AgalnPro.transform.position = new Vector3 (OffA [0], 0.5f, OffA [1]);
				AgalnPro.transform.Rotate (QuatV3);
				AgalnPro.SetActive (true);
				nowAmount++;
				Debug.Log ("使用現有");
			} else {
				BornPos (BronPos);
				GameObject AgalnPro=Instantiate (RePf);
				AgalnPro.transform.position = new Vector3 (OffA [0], 0.5f, OffA [1]);
				AgalnPro.transform.Rotate (QuatV3);
				nowAmount++;
				Debug.Log ("新創");
			}
		}else {
			Debug.Log ("滿人");
		}

	}
//	public void StartBorn(){
//		needPeople=int.Parse(INSoild.text)-1;
//		if ((nowAmount + needPeople) < MaxAmount) {
//			InstancePeople (needPeople);
//		} else {
//			Debug.Log ("你不能擁有那麼多，人數超過上限");
//		}
//	}
}



