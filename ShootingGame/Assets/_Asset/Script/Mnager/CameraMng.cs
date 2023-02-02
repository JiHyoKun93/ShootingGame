using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;
using Unity.VisualScripting;

#if UNITY_EDITOR
using UnityEditor;
#endif

//[AddComponentMenu("ComponentCategory/ComponentName")] // Add ComponentMenu to this script
//[ExecuteAlways]
//[RequireComponent(typeof(SpriteRenderer))]
public class CameraMng : MonoBehaviour{

    private static CameraMng instance;

    public static CameraMng Instance {
        get {
            if(instance == null) {
                instance = FindObjectOfType<CameraMng>();
                if(instance == null) {
					GameObject go = new GameObject();
                    instance = go.AddComponent<CameraMng>();
                    DontDestroyOnLoad(go);
                }
            }
            return instance;
        }
    }

    Vector3 OriginalPos;
    public Camera MainCamera;

    void Awake() {
        MainCamera = Camera.main;
        OriginalPos = MainCamera.transform.position;
    }

    private void Update() {
        if(MainCamera == null) SetMainCamera();
    }

    public void SetMainCamera() {
		MainCamera = Camera.main;
		OriginalPos = MainCamera.transform.position;
	}

    public IEnumerator CameraShake(Coroutine co ,float time, float power) {
        MainCamera.transform.position = OriginalPos;

        while(time > 0) {
            float dtPower = power * Time.deltaTime;
            MainCamera.transform.position += new Vector3(Random.Range(-dtPower, dtPower), Random.Range(-dtPower, dtPower), 0);
            time -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        Vector3 prevPos = MainCamera.transform.position;
        float LTime = 0;
        while(LTime < 1.0f) {
            MainCamera.transform.position = Vector3.Lerp(prevPos, OriginalPos, LTime);
            LTime += Time.deltaTime * 10;
            yield return new WaitForEndOfFrame();
        }

        MainCamera.transform.position = OriginalPos;
        co = null;
    }


}