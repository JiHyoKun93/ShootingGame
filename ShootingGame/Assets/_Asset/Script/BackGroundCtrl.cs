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

public class BackGroundCtrl : MonoBehaviour{

	[SerializeField] Transform BackGround1;
	[SerializeField] Transform BackGround2;

	[SerializeField] float ScrollSpeed;

	float YInterval;

	private void Start() {
		YInterval = BackGround2.position.y - BackGround1.position.y;
	}

	private void Update() {

		BackGround1.Translate(0, ScrollSpeed * Time.deltaTime, 0);
		BackGround2.Translate(0, ScrollSpeed * Time.deltaTime, 0);

		if (BackGround1.position.y < -10) {
			BackGround1.position = BackGround2.position + (Vector3.up * YInterval);
		}

		if (BackGround2.position.y < -10) {
			BackGround2.position = BackGround1.position + (Vector3.up * YInterval);
		}
	}

}
