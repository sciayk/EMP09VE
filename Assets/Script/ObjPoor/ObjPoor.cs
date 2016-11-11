using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ObjPoor :MonoBehaviour{

	private Queue<GameObject> GameObjPoor = new Queue<GameObject> ();

	private GameObject BronPos;
	private GameObject RePf;
	private string Path;
	private string BornPath;
	private Vector3 QuatV3;
	public int MaxAmount ;
	public int StartAmount;
	protected float[] OffA;

	//計算數量
	private int nowAmount=0;
	public int _GetnowAmount{ get{ return nowAmount;}}

	//input用
	public int needPeople;
	public InputField INSoild;

	public ObjPoor(ModelDataBase Data){
		Path = "Prefab/" + Data.GetModelName;
		MaxAmount = Data.GetMaxAmount;
		StartAmount = Data.GetStartAmount;
		nowAmount = Data.GetnowAmount;
		BornPath = "BornPoint/" + Data.GetBornPointName;
		QuatV3 = Data.GetQuatV3;
	}
		
	public void Awake(){
		
		RePf = Resources.Load (Path) as GameObject;
		BronPos = Resources.Load (BornPath)as GameObject;
		Debug.Log (Path);
		Debug.Log (BornPath);
		for (int i = 0; i < StartAmount; i++) {
			GameObject Soild = Instantiate (RePf);
			Soild.SetActive (false);
			GameObjPoor.Enqueue (Soild);
			}

		}

	public void AutoBorn(){
		if (nowAmount < MaxAmount) {
			if (GameObjPoor.Count > 0) {
				GameObject AgalnPro = GameObjPoor.Dequeue ();
				BornPos (BronPos);
				AgalnPro.transform.position = new Vector3 (OffA [0], 0.5f, OffA [1]);
				AgalnPro.transform.Rotate (QuatV3);
				AgalnPro.SetActive (true);
				nowAmount++;
				//Debug.Log ("使用現有");
			} else {
				BornPos (BronPos);
				GameObject AgalnPro = Instantiate (RePf);
				AgalnPro.transform.position = new Vector3 (OffA [0], 0.5f, OffA [1]);
				AgalnPro.transform.Rotate (QuatV3);
				nowAmount++;
				//Debug.Log ("新創");
			}
		} 
	}

	float[] BornPos(GameObject BornA){
		OffA=new float[2];
		Vector3 BPS=BornA.transform.localScale;
		Vector3 BPP = BornA.transform.position;

		OffA[1]=Random.Range (-BPS.z/2 +BPP.z,BPS.z/2 +BPP.z );
		OffA[0]=Random.Range (-BPS.x/2 +BPP.x,BPS.x/2 +BPP.x );

		return OffA;
	}

	public void RePrefab(GameObject Re)
	{	
		Re.SetActive ( false );
		nowAmount--;
		GameObjPoor.Enqueue (Re);
	}

	//按鈕生小兵
	public void StartBorn(){
		needPeople=int.Parse(INSoild.text)-1;
		if ((nowAmount + needPeople) < MaxAmount) {
			while (needPeople > -1) {
				AutoBorn ();
			}
		} else {
			Debug.Log ("你不能擁有那麼多，人數超過上限");
		}
	}
}



