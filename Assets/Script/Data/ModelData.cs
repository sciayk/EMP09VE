using UnityEngine;
using System.Collections;

public class ModelDataBase  {
	//生產資料
	protected string mModelName; 
	protected string mBornPointName;
	protected int mMaxAmount;
	protected int mStartAmount;
	protected int mnowAmount;
	protected Vector3 mQuatV3;


	public string GetModelName{get{return mModelName;}} 
	public string GetBornPointName{get{return mBornPointName;}}
	public int GetMaxAmount{get{return mMaxAmount;}}
	public int GetStartAmount{get{return mStartAmount;}}
	public int GetnowAmount{get{return mnowAmount;}}
	public Vector3 GetQuatV3{get{return mQuatV3;}}
}
public class ModoData1:ModelDataBase{
	
	public ModoData1(){
		mModelName="Cube1";
		mBornPointName="BornPoint1";
		mMaxAmount =50;
		mStartAmount=20;
		mnowAmount=0;
		mQuatV3=new Vector3(0.0f,0.0f,0.0f);
	}
}

public class ModoData2:ModelDataBase{
	
	public ModoData2(){
		mModelName="Cube3";
		mBornPointName="BornPoint1";
		mMaxAmount =50;
		mStartAmount=10;
		mnowAmount=0;
		mQuatV3=new Vector3(0.0f,0.0f,0.0f);
	}
}
public class ModoData3:ModelDataBase{
	
	public ModoData3(){
		mModelName="Cube2";
		mBornPointName="BornPoint2";
		mMaxAmount =50;
		mStartAmount=10;
		mnowAmount=0;
		mQuatV3=new Vector3(0.0f,0.0f,0.0f);
	}
}
public class ModoData4:ModelDataBase{
	
	public ModoData4(){
		mModelName="Cube4";
		mBornPointName="BornPoint2";
		mMaxAmount =50;
		mStartAmount=10;
		mnowAmount=0;
		mQuatV3=new Vector3(0.0f,0.0f,0.0f);
	}
}