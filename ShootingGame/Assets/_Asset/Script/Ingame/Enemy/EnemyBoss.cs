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

public class EnemyBoss : Enemy{

	[SerializeField] Transform Muzzle1;
	[SerializeField] Transform Muzzle2;
	[SerializeField] Transform MuzzleCenter;

	float AttackTimer;
	float AttackCheckTimer = 1;

	float SpecialTimer;
	float SpecialCheckTimer = 5;
	bool IsStop;

	bool IsAttack;
	bool IsSpecial;
	int SpecialNumber;

	float BulletInterval = 1.6f;

	protected override void FixedUpdate() {
		StopMove();
		DoubleAttack();
		SpecialAttack();
		DangerAnimator();
		base.FixedUpdate();
	}

	public override void Spawn(Vector2 pos, float hp, float speed = 1) {
		base.Spawn(pos, hp, speed);
		spriteRenderer.sprite = GameManager.Instance.EnemyBoss_Normal;
		MoveDir = Vector2.down;
	}

	void DangerAnimator() {
		float percent = (Hp / MaxHp) * 100;
		if (percent <= 30) animator.SetBool("danger", true);
	}

	void DoubleAttack() {
		AttackTimer += Time.deltaTime;
		if (IsSpecial) AttackTimer = -1;
		if(AttackTimer > AttackCheckTimer && IsStop && !IsSpecial) {
			BulletMng.Instance.CreateBullet(Muzzle1.position, BULLET_OWNER.ENEMY, Vector2.down);
			BulletMng.Instance.CreateBullet(Muzzle2.position, BULLET_OWNER.ENEMY, Vector2.down);
			AttackTimer = 0;
		}
	}

	void SpecialAttack() {
		SpecialTimer += Time.deltaTime;
		if (SpecialTimer > SpecialCheckTimer && IsStop) {
			IsSpecial = true;
			SpecialTimer = 0;
			
			SpecialNumber = Random.Range(0, 3);

			switch (SpecialNumber) {
				case 0:
					StartCoroutine(Special1());
					break;
				case 1:
					StartCoroutine(SpecialConsonantG(RandomPosX(-2.0f, 2.0f, 3)));
					StartCoroutine(SpecialConsonantN(RandomPosX(-2.0f, 2.0f, 3)));
					StartCoroutine(SpecialConsonantD(RandomPosX(-2.0f, 2.0f, 3)));
					StartCoroutine(SpecialConsonantL(RandomPosX(-2.0f, 2.0f, 3)));
					break;
				case 2:
					StartCoroutine(FiveBullet(-3.0f, 3.0f, 3));
					break;
			}
		}
	}

	Vector2 RandomPosX(float x1, float x2, float y) {
		return new Vector2(Random.Range(x1, x2), y);
	}

	IEnumerator Special1() {
		for (int i = 0; i < 10; i++) {
			for (float k = 0.2f; k < 1; k += 0.2f) {
				BulletMng.Instance.CreateBullet(MuzzleCenter.position, BULLET_OWNER.ENEMY, Vector2.down);
				BulletMng.Instance.CreateBullet(MuzzleCenter.position, BULLET_OWNER.ENEMY, new Vector2(k, -1));
				BulletMng.Instance.CreateBullet(MuzzleCenter.position, BULLET_OWNER.ENEMY, new Vector2(-k, -1));
				SpecialTimer = 0;
			}
			yield return new WaitForSeconds(0.2f);
		}
		IsSpecial = false;
	}

	IEnumerator SpecialConsonantG(Vector2 pos) {
		for(int i=0; i<5; i++) {
			BulletMng.Instance.CreateBullet(pos, BULLET_OWNER.ENEMY, Vector2.down);
			SpecialTimer = 0;
			yield return new WaitForSeconds(0.1f);
		}
		for(float i= 0; i < BulletInterval; i += 0.2f) {
			BulletMng.Instance.CreateBullet(new Vector2(pos.x - i, pos.y), BULLET_OWNER.ENEMY, Vector2.down);
		}
	}
	IEnumerator SpecialConsonantN(Vector2 pos) {
		yield return new WaitForSeconds(1.0f);
		for (float i = 0; i < BulletInterval; i += 0.2f) {
			BulletMng.Instance.CreateBullet(new Vector2(pos.x - i, pos.y), BULLET_OWNER.ENEMY, Vector2.down);
		}
		yield return new WaitForSeconds(0.1f);
		for (int i = 0; i < 5; i++) {
			BulletMng.Instance.CreateBullet(new Vector2(pos.x - 1.4f, pos.y), BULLET_OWNER.ENEMY, Vector2.down);
			SpecialTimer = 0;
			yield return new WaitForSeconds(0.1f);
		}
	}
	IEnumerator SpecialConsonantD(Vector2 pos) {
		yield return new WaitForSeconds(2.0f);
		for (float i = 0; i < BulletInterval; i += 0.2f) {
			BulletMng.Instance.CreateBullet(new Vector2(pos.x + i, pos.y), BULLET_OWNER.ENEMY, Vector2.down);
		}
		yield return new WaitForSeconds(0.1f);
		for (int i = 0; i < 5; i++) {
			BulletMng.Instance.CreateBullet(pos, BULLET_OWNER.ENEMY, Vector2.down);
			SpecialTimer = 0;
			yield return new WaitForSeconds(0.1f);
		}
		for (float i = 0; i < BulletInterval; i += 0.2f) {
			BulletMng.Instance.CreateBullet(new Vector2(pos.x + i, pos.y), BULLET_OWNER.ENEMY, Vector2.down);
		}
	}

	IEnumerator SpecialConsonantL(Vector2 pos) {
		yield return new WaitForSeconds(3.0f);
		for (float i = 0; i <= BulletInterval; i += 0.2f) {
			BulletMng.Instance.CreateBullet(new Vector2(pos.x + i, pos.y), BULLET_OWNER.ENEMY, Vector2.down);
		}
		yield return new WaitForSeconds(0.1f);
		for (int i = 0; i < 3; i++) {
			BulletMng.Instance.CreateBullet(pos, BULLET_OWNER.ENEMY, Vector2.down);
			SpecialTimer = 0;
			yield return new WaitForSeconds(0.1f);
		}
		for (float i = 0; i < BulletInterval; i += 0.2f) {
			BulletMng.Instance.CreateBullet(new Vector2(pos.x + i, pos.y), BULLET_OWNER.ENEMY, Vector2.down);
		}
		yield return new WaitForSeconds(0.1f);
		for (int i = 0; i < 3; i++) {
			BulletMng.Instance.CreateBullet(new Vector2(pos.x + 1.4f, pos.y), BULLET_OWNER.ENEMY, Vector2.down);
			SpecialTimer = 0;
			yield return new WaitForSeconds(0.1f);
		}
		for (float i = 0; i < BulletInterval; i += 0.2f) {
			BulletMng.Instance.CreateBullet(new Vector2(pos.x + i, pos.y), BULLET_OWNER.ENEMY, Vector2.down);
		}
	}

	IEnumerator FiveBullet(float randomX1, float randomX2, float y) {

		for (int i = 0; i < 10; i++) {
			Vector2 pos = RandomPosX(randomX1, randomX2, y);
			for (float k = 0; k < BulletInterval; k += 0.2f) {
				BulletMng.Instance.CreateBullet(new Vector2(pos.x + k, pos.y), BULLET_OWNER.ENEMY, Vector2.down);
			}
			SpecialTimer = 0;
			yield return new WaitForSeconds(0.3f);
		}
	}

	void StopMove() {
		if(transform.position.y <= 3.75f) {
			IsStop = true;
			MoveDir = Vector2.zero;
		}
	}

	protected override void OnTriggerPlayer(Collider2D collision, Player player) {
		player.TakeDmg(1);
		base.OnTriggerPlayer(collision, player);
	}
}