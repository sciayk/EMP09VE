using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Collections.Generic;
using System.Linq;
using System;

public class KeywordManager : MonoBehaviour {
	KeywordRecognizer keywordRecognizer;
	Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();


	// Use this for initialization
	void Start () {
		//初始化关键词
		keywords.Add("火球", () =>
			{
				Debug.Log("火球");// 想执行的行为
			});
		keywords.Add("fire", () =>
			{
				Debug.Log("fire");// 想执行的行为
			});
		keywords.Add("water", () =>
			{
				Debug.Log("water");// 想执行的行为
			});
		keywords.Add("stone", () =>
			{
				Debug.Log("water");// 想执行的行为
			});

		keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());
		keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
		//开始识别
		keywordRecognizer.Start();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
	{
		System.Action keywordAction;
		// 如果识别到关键词就调用
		if (keywords.TryGetValue(args.text, out keywordAction))
		{
			keywordAction.Invoke();
		}
	}

}
