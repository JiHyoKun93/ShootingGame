using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;

#if UNITY_EDITOR
using UnityEditor;
#endif

//[AddComponentMenu("ComponentCategory/ComponentName")] // Add ComponentMenu to this script
//[ExecuteAlways]
//[RequireComponent(typeof(SpriteRenderer))]
public class FileMng : MonoBehaviour{

	private void Awake() {
		Dictionary<string, int> keyValuePairs = new Dictionary<string, int>();
		keyValuePairs.Add("체력", 10);
		keyValuePairs.Add("공격력", 2);
		keyValuePairs.Add("속도", 5);

		foreach(KeyValuePair<string, int> pair in keyValuePairs) {
			Debug.Log(pair.Key + " / " + pair.Value);
		}

	}

	public void LoadFile<T>(ref Dictionary<string, T> datas, string path) where T : Object {
		string log = path + "에서 불러온 파일\n";
		datas = new Dictionary<string, T>();
		T[] files = Resources.LoadAll<T>(path);
		foreach (T file in files) {
			datas.Add(file.name, file);
			log += file.name + "\n";
		}
		Debug.Log(log);
	}
}