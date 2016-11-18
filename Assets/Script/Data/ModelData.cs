using UnityEngine;
using System.Collections;

public class ModelDataBase  {
	//生產資料
	protected string mName;
	protected string mModelName; 
	protected string mBornPointName;
	protected int mMaxAmount;
	protected int mStartAmount;

	public string GetName{get{return mName;}}
	public string GetModelName{get{return mModelName;}} 
	public string GetBornPointName{get{return mBornPointName;}}
	public int GetMaxAmount{get{return mMaxAmount;}}
	public int GetStartAmount{get{return mStartAmount;}}
}
public class ModoData1:ModelDataBase{
	public ModoData1(){
		mName="Cube1";
		mModelName="Prefab/Cube1";
		mBornPointName="BornPoint/BornPoint1";
		mMaxAmount =200;
		mStartAmount=40;
	}
}

public class ModoData2:ModelDataBase{
	public ModoData2(){
		mName="Cube3";
		mModelName="Prefab/Cube3";
		mBornPointName="BornPoint/BornPoint1";
		mMaxAmount =200;
		mStartAmount=40;
	}
}
public class ModoData3:ModelDataBase{
	public ModoData3(){
		mName="Cube2";
		mModelName="Prefab/Cube2";
		mBornPointName="BornPoint/BornPoint2";
		mMaxAmount =200;
		mStartAmount=40;
	}
}
public class ModoData4:ModelDataBase{
	public ModoData4(){
		mName="Cube4";
		mModelName="Prefab/Cube4";
		mBornPointName="BornPoint/BornPoint2";
		mMaxAmount =200;
		mStartAmount=40;
	}
}