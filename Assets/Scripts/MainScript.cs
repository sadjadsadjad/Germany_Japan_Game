using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MainScript : MonoBehaviour {

	public GameObject Abutton;
	public GameObject Bbutton;
	public GameObject QuestionObj;
	public GameObject MessageObj;
	public GameObject DateObj;

	public GameObject LoveText;
	public GameObject MoneyText;
	public GameObject FriendText;

	public GameObject AnswerScene;
	public GameObject MessageScene;

	public int id;
	public int month;
	public int day;
	public int answer;
	public int sceneNum;
	public int loveGauge;
	public int moneyGauge;
	public int friendGauge;

	bool[,,] sch;//month,day,who

	public static bool setFlag;

	// Use this for initialization
	void Start () {
		id = 1;

		month = 2;
		day = 8;

		answer = 0;
		sceneNum = 0;
		loveGauge = 0;
		friendGauge = 0;
		moneyGauge = 0;

		sch = new bool[12,31,3];
		for (int i = 0; i < 12; i++) {
			for(int j = 0; j < 31; j++){
				for(int k = 0; k < 3; k++){
					sch [i, j, k] = false;
				}
			}
		}

		GaugeSet ();
		setFlag = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (setFlag && sceneNum == 0) {
			if (ReadFileScript.csvDatas [id] [3] == "N") {
				DateSet ();
				QuestionSet ();
			} 
			if(ReadFileScript.csvDatas [id] [3] == "S") {
				if (sch [month, day, 0]) {
					if (sch [month, day, 1]) {
						id += 3;
					} else {
					}
				} else if (sch [month, day, 1]) {
					id += 2;
				} 
				if(sch [month, day, 0] == false && sch [month, day, 1] == false){
					id += 1;
				}
				DateSet ();
				SpecialMessageSet ();
				AnswerScene.SetActive (false);
				MessageScene.SetActive (true);
				sceneNum = 1;
				setFlag = false;
			}
		}
	}

	void DateSet(){
		DateObj.GetComponent<Text>().text = ReadFileScript.csvDatas [id] [2] + "/" + ReadFileScript.csvDatas [id] [1];
	}

	void QuestionSet(){
		Abutton.GetComponentInChildren<Text>().text = 
			ReadFileScript.csvDatas[id][6];
		Bbutton.GetComponentInChildren<Text>().text = 
			ReadFileScript.csvDatas[id][7];
		QuestionObj.GetComponent<Text> ().text = 
			ReadFileScript.csvDatas [id] [5];
		ImageSet (int.Parse(ReadFileScript.csvDatas[id][4]));
		setFlag = false;
	}

	void MessageSet(){
		if(answer == 1){
			MessageObj.GetComponentInChildren<Text>().text = 
			ReadFileScript.csvDatas[id][14];
			ImageSet (int.Parse(ReadFileScript.csvDatas[id][13]));
			loveGauge += int.Parse(ReadFileScript.csvDatas[id][10]);
			moneyGauge += int.Parse(ReadFileScript.csvDatas[id][11]);
			friendGauge += int.Parse(ReadFileScript.csvDatas[id][12]);
			if(ReadFileScript.csvDatas[id][8] != ""){//予定を入れる場合
				sch[int.Parse(ReadFileScript.csvDatas[id][8]), int.Parse(ReadFileScript.csvDatas[id][9]),
					int.Parse(ReadFileScript.csvDatas[id][4])] = true;
			}
		}
		else if(answer == 2){
			MessageObj.GetComponentInChildren<Text>().text = 
			ReadFileScript.csvDatas[id][21];
			ImageSet (int.Parse(ReadFileScript.csvDatas[id][20]));
			loveGauge += int.Parse(ReadFileScript.csvDatas[id][17]);
			moneyGauge += int.Parse(ReadFileScript.csvDatas[id][18]);
			friendGauge += int.Parse(ReadFileScript.csvDatas[id][19]);
			if(ReadFileScript.csvDatas[id][15] != ""){//予定を入れる場合
				sch[int.Parse(ReadFileScript.csvDatas[id][15]), int.Parse(ReadFileScript.csvDatas[id][16]),
					int.Parse(ReadFileScript.csvDatas[id][4])] = true;
			}
		}
	}

	void SpecialMessageSet(){
		MessageObj.GetComponentInChildren<Text> ().text = 
			ReadFileScript.csvDatas [id] [23];

		ImageSet (int.Parse(ReadFileScript.csvDatas[id][22]));
		loveGauge += int.Parse(ReadFileScript.csvDatas[id][24]);
		moneyGauge += int.Parse(ReadFileScript.csvDatas[id][25]);
		friendGauge += int.Parse(ReadFileScript.csvDatas[id][26]);
		GaugeSet ();
	}

	void GaugeSet(){
		LoveText.GetComponent<Text> ().text = loveGauge.ToString();
		MoneyText.GetComponent<Text> ().text = moneyGauge.ToString();
		FriendText.GetComponent<Text> ().text = friendGauge.ToString();
	}

	void ImageSet(int num){
		if (num == 0) {
			Texture2D texture = Resources.Load ("00") as Texture2D;
			Image img = GameObject.Find ("Canvas/HumanImage").GetComponent<Image> ();
			img.sprite = Sprite.Create (texture, new Rect (0, 0, texture.width, texture.height), Vector2.zero);
		}
		if (num == 1) {
			Texture2D texture = Resources.Load ("01") as Texture2D;
			Image img = GameObject.Find ("Canvas/HumanImage").GetComponent<Image> ();
			img.sprite = Sprite.Create (texture, new Rect (0, 0, texture.width, texture.height), Vector2.zero);
		}
		if (num == 2) {
			Texture2D texture = Resources.Load ("02") as Texture2D;
			Image img = GameObject.Find ("Canvas/HumanImage").GetComponent<Image> ();
			img.sprite = Sprite.Create (texture, new Rect (0, 0, texture.width, texture.height), Vector2.zero);
		}
	}

	//-----------button
	public void Answer_A_Button(){
		answer = 1;
		sceneNum = 1;
		AnswerScene.SetActive (false);
		MessageScene.SetActive (true);
		MessageSet ();
		GaugeSet ();
	}

	public void Answer_B_Button(){
		answer = 2;
		sceneNum = 1;
		AnswerScene.SetActive (false);
		MessageScene.SetActive (true);
		MessageSet ();
		GaugeSet ();
	}

	public void NextButton(){
		answer = 0;
		sceneNum = 0;
		while (int.Parse(ReadFileScript.csvDatas [id] [1]) == month && int.Parse(ReadFileScript.csvDatas [id] [2]) == day){
			id++;
		}
		month = int.Parse(ReadFileScript.csvDatas[id][1]);
		day = int.Parse(ReadFileScript.csvDatas[id][2]);
		AnswerScene.SetActive (true);
		MessageScene.SetActive (false);
		setFlag = true;
	}
}
