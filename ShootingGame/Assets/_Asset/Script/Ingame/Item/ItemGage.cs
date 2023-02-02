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

public class ItemGage : Item {

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.CompareTag("Player") || collision.CompareTag("PlayerHit")) {
			collision.GetComponent<PlayerAttack>().SetGage(5);
			gameObject.SetActive(false);
		}
	}

}
