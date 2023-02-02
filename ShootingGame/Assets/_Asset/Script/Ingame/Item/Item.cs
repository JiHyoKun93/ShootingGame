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

public class Item : MonoBehaviour{
	Rigidbody2D rigid;

	protected Vector2 MoveDir;

	float x, y;

	private void Start() {
		rigid = GetComponent<Rigidbody2D>();
	}

	void Update() {
		x += Time.deltaTime * 5;
		y += Time.deltaTime * 5;
		MoveDir.x = Mathf.Sin(x) * 0.5f;
		MoveDir.y = Mathf.Cos(y) * 0.5f;

		if(gameObject.activeSelf == true) {
			StartCoroutine(Count());
		}

	}

	void FixedUpdate() {
		//rigid.velocity = MoveDir * 1;
		Move();
	}
	
	void Move() {
		Vector2 move = new Vector2(1, 3);
		rigid.velocity = MoveDir * move;
	}

	IEnumerator Count() {
		yield return new WaitForSeconds(15);
		gameObject.SetActive(false);
	}

	public void Spawn(Vector2 pos) {
		gameObject.SetActive(true);
		transform.position = pos;
	}



}
