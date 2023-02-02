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

public class GameManager : MonoBehaviour{

    private static GameManager instance;
    public static GameManager Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<GameManager>();
                if (instance == null) {
                    GameObject go = new GameObject();
                    instance = go.AddComponent<GameManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return instance;
        }
    }

    public Sprite EnemyA_Normal;
    public Sprite EnemyA_Hit;
    public Sprite EnemyB_Normal;
    public Sprite EnemyB_Hit;
    public Sprite EnemyBoss_Normal;
    public Sprite EnemyBoss_Hit;

	[SerializeField] Image HpImage;
	[SerializeField] Image GageImage;
	[SerializeField] TextMeshProUGUI Score;

	Player player;
	PlayerAttack playerAttack;

	public double score;

	private void Start() {
		player = FindObjectOfType<Player>();
		playerAttack = FindObjectOfType<PlayerAttack>();
	}

	void Update() {
		HpFill();
		GageFill();
		ScoreUpdate();
	}

	void HpFill() {
		HpImage.fillAmount = player.GetHp() / player.GetMaxHp();
	}

	void GageFill() {
		GageImage.fillAmount = playerAttack.GetGage() / playerAttack.GetMaxGage();
	}

	void ScoreUpdate() {
		Score.text = "Score : " + score;
	}

}