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
public class EnemyA : Enemy{

	[SerializeField] Transform Muzzle;
	const float AttackTime = 3.0f;
	float AttackCheckTime = 0;

	protected override void Update() {
		Attack();
		base.Update();
	}

	public override void Spawn(Vector2 pos, float hp, float speed = 1) {
		base.Spawn(pos, hp, speed);
		spriteRenderer.sprite = GameManager.Instance.EnemyA_Normal;
		MoveDir = Vector2.down;
	}

	protected override IEnumerator HitSpriteSwap() {
		spriteRenderer.sprite = GameManager.Instance.EnemyA_Hit;
		yield return new WaitForSeconds(0.2f);
		spriteRenderer.sprite = GameManager.Instance.EnemyA_Normal;
	}

	void Attack() {
		AttackCheckTime += Time.deltaTime;
		if (AttackCheckTime > AttackTime) {
			BulletMng.Instance.CreateBullet(Muzzle.position, BULLET_OWNER.ENEMY, Vector2.down);
			AttackCheckTime = 0;
		}
	}

	}