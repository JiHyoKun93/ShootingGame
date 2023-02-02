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

public class ItemPower : Item{

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player") || collision.CompareTag("PlayerHit")) {
            collision.GetComponent<PlayerAttack>().SetAttack(1);
            gameObject.SetActive(false);
        }
    }
}
