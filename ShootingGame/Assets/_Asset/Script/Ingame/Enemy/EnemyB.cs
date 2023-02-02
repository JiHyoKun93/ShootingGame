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
public class EnemyB : Enemy{

	float MoveTime;
	public Transform[] PointObj;
	Vector2[] point = new Vector2[4];
	float posA = 5.0f;
	float posB = 10f;

	private void Start() {
		Attack = 1;
	}

	protected override void Update() {
		MoveTime += Time.deltaTime * 0.1f;
		DrawTrajectory();
		base.Update();
	}

	private void OnDisable() {
		MoveTime = 0;
	}

	public override void Spawn(Vector2 pos, float hp, float speed = 1) {
		base.Spawn(pos, hp, speed);
		spriteRenderer.sprite = GameManager.Instance.EnemyB_Normal;
		if (EnemyMng.Instance.EnemyB_Muzzle == 0) PointLeft();
		else if (EnemyMng.Instance.EnemyB_Muzzle == 1) PointRight();
	}
	
	Vector2 PointSetting(Vector2 pos) {
		float x, y;
		float angle = 90;
		x = posA * -(Mathf.Cos(angle * Mathf.Deg2Rad)) + pos.x;
		y = posB * -(Mathf.Sin(angle * Mathf.Deg2Rad)) + pos.y;
		return new Vector2(x, y);
	}

	void PointLeft() {
		point[0] = EnemyMng.Instance.MuzzleLeft.position;
		point[1] = PointSetting(EnemyMng.Instance.MuzzleLeft.position);
		point[2] = PointSetting(EnemyMng.Instance.MuzzleRight.position);
		point[3] = EnemyMng.Instance.MuzzleRight.position;
	}

	void PointRight() {
		point[0] = EnemyMng.Instance.MuzzleRight.position;
		point[1] = PointSetting(EnemyMng.Instance.MuzzleRight.position);
		point[2] = PointSetting(EnemyMng.Instance.MuzzleLeft.position);
		point[3] = EnemyMng.Instance.MuzzleLeft.position;
	}

	private float FourPointBezier(float a, float b, float c, float d) {
		return Mathf.Pow((1 - MoveTime), 3) * a + Mathf.Pow((1 - MoveTime), 2) * 3 * MoveTime * b
			+ Mathf.Pow(MoveTime, 2) * 3 * (1 - MoveTime) * c + Mathf.Pow(MoveTime, 3) * d;
	}

	void DrawTrajectory() {
		transform.position = new Vector2(
			FourPointBezier(point[0].x, point[1].x, point[2].x, point[3].x),
			FourPointBezier(point[0].y, point[1].y, point[2].y, point[3].y));
	}

	protected override IEnumerator HitSpriteSwap() {
		spriteRenderer.sprite = GameManager.Instance.EnemyB_Hit;
		yield return new WaitForSeconds(0.2f);
		spriteRenderer.sprite = GameManager.Instance.EnemyB_Normal;
	}
}