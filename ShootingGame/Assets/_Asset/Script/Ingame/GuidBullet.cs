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

public class GuidBullet : MonoBehaviour{
	Rigidbody2D rigid;

	[SerializeField]
	[Range(0, 1)] private float t = 0;
	Vector2[] point = new Vector2[4];
	public Vector2 MuzzlePosition;
	Enemy Enemy;
	public Vector2 enemyPosition;
	float posA = 5.0f;
	float posB = 4.5f;

	[SerializeField] public float Attack;

	void Start(){
		rigid = GetComponent<Rigidbody2D>();
	}

    void FixedUpdate(){
		Clipping();
		t += Time.deltaTime * 0.7f;
		//if (t > 1) return;
		enemyPosition = Enemy.transform.position;
		point[3] = enemyPosition;
		DrawTrajectory();
		CheckEnemy();
	}

	private void OnDisable() {
		//없어졌을때 한번 실행
		rigid.position = Vector2.one * 999999;
		t = 0;
	}

	void CheckEnemy() {
		if (Enemy.gameObject.activeSelf == false) {
			Enemy target = EnemyMng.Instance.GetEnemyPosition(transform.position);
			if (target == null) return;
			Enemy = target;
		}
	}

	void Point(Vector2 target) {
		point[0] = transform.position;
		point[1] = PointSetting(transform.position);
		point[2] = PointSetting(target);
		point[3] = target;
	}

	Vector2 PointSetting(Vector2 pos) {
		float x, y;
		x = posA * Mathf.Cos(Random.Range(0, 360) * Mathf.Deg2Rad) + pos.x;
		y = posB * Mathf.Sin(Random.Range(0, 360) * Mathf.Deg2Rad) + pos.y;
		return new Vector2(x, y);
	}

	void DrawTrajectory() {
		transform.position = new Vector2(
			FourPointBezier(point[0].x, point[1].x, point[2].x, point[3].x),
			FourPointBezier(point[0].y, point[1].y, point[2].y, point[3].y));
	}

	//베지어공식
	private float FourPointBezier(float a, float b, float c, float d) {
		return Mathf.Pow((1 - t), 3) * a + Mathf.Pow((1 - t), 2) * 3 * t * b
			+ Mathf.Pow(t, 2) * 3 * (1 - t) * c + Mathf.Pow(t, 3) * d;
	}

	public void Spawn(Vector2 muzzle, Enemy enemy) {
		gameObject.SetActive(true);
		gameObject.tag = "Bullet";
		MuzzlePosition = muzzle;

		Enemy = enemy;
		enemyPosition = enemy.transform.position;
		transform.position = MuzzlePosition;
		Point(enemyPosition);
	}
	
	void Clipping() {
		if (CameraMng.Instance.MainCamera == null) CameraMng.Instance.SetMainCamera();
		Vector2 view = CameraMng.Instance.MainCamera.WorldToViewportPoint(rigid.position);
		float posX = Mathf.Abs(transform.position.x - point[3].x);
		float posY = Mathf.Abs(transform.position.y - point[3].y);

		if (view.x > 1.0f || view.x < 0 || view.y > 1.0f || view.y < 0) {
			gameObject.tag = "BulletOut";
		}
		else {
			gameObject.tag = "Bullet";
		}

		if (view.x > 1.5f || view.x < -0.5f || view.y > 1.5f || view.y < -0.5f) {
			gameObject.SetActive(false);
		}
		if (posX <= 0.05f && posY <= 0.05f) {
			gameObject.SetActive(false);
		}
	}

}
