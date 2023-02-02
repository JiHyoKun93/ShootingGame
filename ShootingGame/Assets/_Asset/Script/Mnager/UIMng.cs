using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIMng : MonoBehaviour{

    private static UIMng instance;

    public static UIMng Instance {
        get {
			if (instance == null) {
				instance = FindObjectOfType<UIMng>();
				if (instance == null) {
					GameObject go = new GameObject();
					instance = go.AddComponent<UIMng>();
					DontDestroyOnLoad(go);
				}
			}
			return instance;
        }
    }

	[SerializeField] Image MainBackGround;
	[SerializeField] Canvas canvas;
	[SerializeField] GameObject MainUI;
	[SerializeField] GameObject ManualUI;


	private void Start() {
		Screen.SetResolution(720, 1280, false);

		ManualUI.SetActive(false);
	}

	public void OnStartBtn() {
		StartCoroutine(FadeOutImage(MainBackGround));
	}

	public void OnExitBtn() {
		Application.Quit();

	}

	public void OnManualBtn() {
		MainUI.SetActive(false);
		ManualUI.SetActive(true);
	}

	public void OnManualBackBtn() {
		ManualUI.SetActive(false);
		MainUI.SetActive(true);
	}

	IEnumerator FadeOutImage(Image image) {
		MainUI.gameObject.SetActive(false);
		ManualUI.gameObject.SetActive(false);

		float a = 1;
		while(a > 0.0f) {
			Color c = image.color;
			c.a = a;
			a -= 0.05f;
			yield return new WaitForSeconds(0.1f);
			image.color = c;
			
		}
		if (SceneManager.GetActiveScene().name == "MainScene") {
			SceneManager.LoadScene(1);
		}
	}

}
