using UnityEngine;
using System.Collections;

public class ModoDataBase  {
	public string mModeName;
	public string mBornPointName;
	public int mMaxAmount;
	public int mStartAmount;
	public int mnowAmount;
	public int mneedPeople;
}
public class ModoData1:ModoDataBase{
	public ModoData1(){
		mModeName="Cube1";
		mBornPointName="BornPoint1";
		mMaxAmount =120;
		mStartAmount=10;
		mnowAmount=0;
		mneedPeople=0;
	}
}

public class ModoData2:ModoDataBase{
	public ModoData2(){
		mModeName="Cube3";
		mBornPointName="BornPoint1";
		mMaxAmount =50;
		mStartAmount=10;
		mnowAmount=0;
		mneedPeople=0;
	}
}
public class ModoData3:ModoDataBase{
	public ModoData3(){
		mModeName="Cube2";
		mBornPointName="BornPoint2";
		mMaxAmount =50;
		mStartAmount=10;
		mnowAmount=0;
		mneedPeople=0;
	}
}
public class ModoData4:ModoDataBase{
	public ModoData4(){
		mModeName="Cube4";
		mBornPointName="BornPoint2";
		mMaxAmount =100;
		mStartAmount=30;
		mnowAmount=0;
		mneedPeople=0;
	}
}