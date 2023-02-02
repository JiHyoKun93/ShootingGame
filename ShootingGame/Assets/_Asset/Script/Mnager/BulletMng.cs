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


public class BulletMng : MonoBehaviour{
	//싱글톤 패턴
	private static BulletMng instance;
	public static BulletMng Instance {
		get {
			if (instance == null) {
				instance = FindObjectOfType<BulletMng>();
				if (instance == null) {
					GameObject go = new GameObject();
					instance = go.AddComponent<BulletMng>();
					DontDestroyOnLoad(go);
				}
			}
			return instance;
		}
	}
	//

	[SerializeField] GameObject GuidBulletObj;
	[SerializeField] public List<Bullet> BulletPrefabList; // 사용할 Bullet 프리펩들 (총알 단계별)
	[SerializeField] List<Bullet> BulletList; // 재활용할 Bullet 리스트
	[SerializeField] List<GuidBullet> GuidBulletList;

	// 해당하는 이름의 Bullet 가져오기
	GameObject GetNameToGameObject(string name) {
		for (int i = 0; i < BulletPrefabList.Count; i++) {
			if (BulletPrefabList[i].name.Equals(name)) {
				return BulletPrefabList[i].gameObject;
			}
		}
		return null;
	}

	public void CreateBullet(Vector2 pos, BULLET_OWNER owner, Vector2 direction, string name = "Bullet1",float attack = 1, float speed = 3, float angle = 0) {
		bool IsFind = false;
		// BulletList에서 이름이 똑같고, 비활성화되어있는 Bullet을 찾기
		for(int i=0; i<BulletList.Count; i++) {
			if (BulletList[i].gameObject.activeSelf == false && BulletList[i].name.Equals(name)) {
				IsFind = true;
				BulletList[i].Spawn(pos, owner, direction, attack, speed, angle);
				break;
			}
		}
		// 비활성화되어있는것이 없으면 BulletList에 새로 생성
		if(IsFind == false) {
			Bullet bullet = Instantiate(GetNameToGameObject(name)).GetComponent<Bullet>();
			bullet.transform.parent = transform;
			bullet.Spawn(pos, owner, direction, attack, speed, angle);
			bullet.name = name;
			BulletList.Add(bullet);
		}
	}

	public void CreateGuidBullet(Vector2 muzzle, Enemy enemy) {
		if (enemy == null) return;
		bool IsFind = false;
		for (int i = 0; i < GuidBulletList.Count; i++) {
			if (GuidBulletList[i].gameObject.activeSelf == false) {
				IsFind = true;
				GuidBulletList[i].Spawn(muzzle, enemy);
				break;
			}
		}
		if (IsFind == false) {
			GuidBullet bullet = Instantiate(GuidBulletObj).GetComponent<GuidBullet>();
			bullet.transform.parent = transform;
			bullet.Spawn(muzzle, enemy);
			GuidBulletList.Add(bullet);
		}
	}

}
