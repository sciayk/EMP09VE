using UnityEngine;
using System.Collections;

public class SoildTerr  {
	
	protected int mTeam;  // 1. 自己人 2.敵人
	protected int mAtk;
	protected int mDef;
	protected int mNowHp;
	protected int mMaxHp;
	protected float mNowSpeed;
	protected float mMaxSpeed;
	protected float mLookRange;

	public int GetTeam{get{return mTeam;}}
	public int GetAtk{get{ return mAtk; }}
	public int GetDef{get{ return mDef; }}
	public int SetGetNowHp{set{ if (value > mMaxHp)
			mNowHp = mMaxHp;
			else {
				mNowHp = value;}} get{ return mNowHp;}}
	public int GetMaxHp{get{ return mMaxHp; }}
	public float SetGetNowSpeed{set{if (value > mMaxSpeed)
				mNowSpeed = mMaxSpeed;
			else {
				mNowSpeed = value;} } get{ return mNowSpeed;}}
	public float GetLookRange{get{ return mLookRange; }}
	public float GetMaxSpeed{get{return mMaxSpeed;}}


}

public class MySoildTerr:SoildTerr{

	MySoildTerr(){
		mTeam = 1;
		mAtk=10;
		mDef=5;
		mNowHp=100;
		mMaxHp=100;
		mNowSpeed=5.0f;
		mMaxSpeed = 5.0f;
		mLookRange=2.0f;
	}



}

public class EmemySoildTerr:SoildTerr{

	EmemySoildTerr(){
		mTeam = 2;
		mAtk=15;
		mDef=5;
		mNowHp=100;
		mMaxHp=100;
		mNowSpeed=5.0f;
		mMaxSpeed = 5.0f;
		mLookRange=2.0f;
	}

}
