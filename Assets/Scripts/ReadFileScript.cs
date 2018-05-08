using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ReadFileScript : MonoBehaviour {

	private string fileName; // 読み込むファイルの名前
	private TextAsset csvFile; // CSVファイル
	public static List<string[]> csvDatas = new List<string[]>(); // CSVの中身を入れるリスト
	private int height = 0; // CSVの行数

	void Start(){
		StartCoroutine (ReadFile());
	}

	IEnumerator ReadFile(){
		fileName = "prototype"; // ファイル名
		csvFile = Resources.Load(fileName) as TextAsset; /* Resouces/CSV下のCSV読み込み */
		StringReader reader = new StringReader(csvFile.text);

		while(reader.Peek() > -1) {
			string line = reader.ReadLine();
			csvDatas.Add(line.Split(',')); // リストに入れる
			height++; // 行数加算
		}
		bool repflag = false;
		for(int i = 0; i < 8; i++){
			repflag = false;
			for(int j = 0; j < csvDatas[i].Length; j++){
				Debug.Log ("test"+i);
				if(repflag == true && j != csvDatas[i].Length - 1){
					csvDatas [i] [j] = csvDatas [i] [j + 1];
					Debug.Log (i + "," + j);
				}
				if (0 <= csvDatas[i][j].IndexOf ("\"")) {
					csvDatas [i] [j] = csvDatas [i] [j] + "," + csvDatas [i] [j + 1];
					repflag = true;
					Debug.Log ("test");
				}
			}
		}
		//Debug.Log (csvDatas[1][5]);
		yield return new WaitForSeconds(1);
		MainScript.setFlag = true;
	}
}
