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

public class EnemyMng : MonoBehaviour{

    private static EnemyMng instance;
    public static EnemyMng Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<EnemyMng>();
                if (instance == null) {
                    GameObject go = new GameObject();
                    instance = go.AddComponent<EnemyMng>();
                    DontDestroyOnLoad(go);
                }
            }
            return instance;
        }
    }

    [SerializeField] public Transform MuzzleCenter;
    [SerializeField] public Transform MuzzleLeft;
    [SerializeField] public Transform MuzzleRight;

    [SerializeField] List<Enemy> EnemyList;
    [SerializeField] List<Enemy> EnemyPrefabList;
    float CreateEnemyTimer1;
    float CreateEnemyTimer2;
    float CreateBossTimer;
    bool IsBoss;
    public int EnemyB_Muzzle;

    float EnemyATimeSpeed;

    private void Update() {
        CreateEnemyTimer1 += Time.deltaTime;
        CreateEnemyTimer2 += Time.deltaTime;
        CreateBossTimer += Time.deltaTime;

        // EnemyA
        EnemyATimeSpeed = Random.Range(1.0f, 2.1f);

		if (CreateEnemyTimer1 > EnemyATimeSpeed) {
			Vector2 pos = CameraMng.Instance.MainCamera.ViewportToWorldPoint(new Vector3(Random.Range(0.05f, 0.95f), 1.25f, 0));
			CreateEnemy(pos, "EnemyA", 1, 1.5f);
            CreateEnemyTimer1 = 0;
        }
		// EnemyB
		if (CreateEnemyTimer2 > 15.0f) {
            EnemyB_Muzzle = Random.Range(0, 2);
			StartCoroutine(CreateEnemy2());
			CreateEnemyTimer2 = 0;
		}
		// Enemy Boss
		if (CreateBossTimer > 100 && !IsBoss) {
            IsBoss = true;
			CreateEnemy(MuzzleCenter.position, "EnemyBoss", 300);
			StopCoroutine(CreateEnemy2());
		}
        if (IsBoss) {
            CreateEnemyTimer1 = 0;
            CreateEnemyTimer2 = 0;
        }
    }

    IEnumerator CreateEnemy2() {
        for (int i = 0; i < 5; i++) {
            CreateEnemy(MuzzleLeft.position, "EnemyB", 1, 2);
            yield return new WaitForSeconds(0.3f);
        }
	}

    GameObject GetNameToGameObject(string name) {
        for(int i=0; i<EnemyPrefabList.Count; i++) {
            if (EnemyPrefabList[i].name.Equals(name)) {
                return EnemyPrefabList[i].gameObject;
            }
        }
        return null;
    }

    void CreateEnemy(Vector2 pos, string name, float hp , float speed = 1) {
        bool IsFind = false;
        for (int i = 0; i < EnemyList.Count; i++) {
            if (EnemyList[i].gameObject.activeSelf == false && EnemyList[i].name.Equals(name)) {
                IsFind = true;
                EnemyList[i].Spawn(pos, hp, speed);
                break;
            }
        }
        if (IsFind == false) {
            Enemy enemy = Instantiate(GetNameToGameObject(name)).GetComponent<Enemy>();
            enemy.transform.parent = transform;
            enemy.Spawn(pos, hp, speed);
            enemy.name = name;
            EnemyList.Add(enemy);
        }
    }

    public Enemy GetEnemy(int index) {
        return EnemyList[index];
    }
    public Enemy GetEnemyPosition(Vector2 playerPos) {
        Enemy enemy = new Enemy();
        float distance;
        float Mindistance = 999999;
        for (int i = 0; i < EnemyList.Count; i++) {
            if (EnemyList[i].gameObject.activeSelf == false) {
                continue;
            }
            distance = Vector3.Distance(playerPos, EnemyList[i].transform.position);
            if (distance < Mindistance) {
                Mindistance = distance;
                enemy = EnemyList[i];
            }
        }
        return enemy;
    }
}

