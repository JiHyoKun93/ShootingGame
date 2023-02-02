using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class IngameUI : MonoBehaviour{

	[SerializeField] GameObject Pause;
	[SerializeField] Image BackGround;
	[SerializeField] Image BackGround2;

	int ChildIndex = 5;
	bool IsEscape;

	private void Start() {
		Pause.SetActive(false);
		BackGround2.gameObject.SetActive(false);
	}

	private void Update() {
	
		if (Input.GetKeyDown(KeyCode.Escape)) {
			PauseEscape();
		}
	}

	void PauseEscape() {
		if (IsEscape) {
			IsEscape = false;
			Pause.SetActive(false);
			Time.timeScale = 1.0f;
		}
		else {
			IsEscape = true;
			Pause.SetActive(true);
			Time.timeScale = 0;
		}
	}

	public void OnContinueBtn() {
		Time.timeScale = 1.0f;
		Pause.SetActive(false);
		IsEscape = false;
	}

	public void OnHomeBtn() {
		Time.timeScale = 1.0f;
		BackGround.gameObject.SetActive(true);
		BackGround2.gameObject.SetActive(true);
		StartCoroutine(FadeOutImage());
	}

	IEnumerator FadeOutImage() {
		
		for (int i = 1; i <= ChildIndex; i++) {
			Pause.transform.GetChild(i).gameObject.SetActive(false);
		}

		float a = 1;
		while (a > 0.0f) {
			Color c = BackGround.color;
			c.a = a;
			a -= 0.05f;
			yield return new WaitForSeconds(0.1f);
			BackGround.color = c;

		}
		SceneManager.LoadScene(0);
		
	}
}
