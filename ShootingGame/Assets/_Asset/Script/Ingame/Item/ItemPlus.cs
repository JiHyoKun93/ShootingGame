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
public class ItemPlus : Item{

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.CompareTag("Player") || collision.CompareTag("PlayerHit")) {
			collision.GetComponent<PlayerAttack>().SetBulletIndex(1);
			gameObject.SetActive(false);
		}
	}
}
