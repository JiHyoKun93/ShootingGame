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

public class PlayerAttack : MonoBehaviour{

    Player player;
    [SerializeField] Transform Muzzle;
    [SerializeField] Enemy Enemy;
    float CreateBulletTime;

	float MaxGage = 10;
	float Gage;
    [SerializeField] float Attack = 1;
    int bulletIndex = 1;

	void Start(){
        player = GetComponent<Player>();
    }

    void Update(){
        CreateBulletTime += Time.deltaTime;
		Charging();

		if (CreateBulletTime > 0.1f) {
            if (Input.GetKey(KeyCode.Space)) {
                BulletMng.Instance.CreateBullet(Muzzle.position, BULLET_OWNER.PLAYER, Vector2.up, "Bullet" + bulletIndex, Attack);
                
            }
            if (Gage > 0) {
                if (Input.GetKey(KeyCode.C)) {
                    BulletMng.Instance.CreateGuidBullet(Muzzle.position, Enemy);
                }
            }

            CreateBulletTime = 0;
        }
        CheckEnemyDistance();
    }

    void CheckEnemyDistance() {
        Enemy target = EnemyMng.Instance.GetEnemyPosition(transform.position);
        if (target == null) return;
		Enemy = target;
    }

	public float GetGage() { return Gage; }
	public float GetMaxGage() { return MaxGage; }
    public void SetGage(float value) {
        if (Gage >= MaxGage) return;
        Gage += value; 
    }

	void Charging() {
        if (Gage > MaxGage) {
            Gage = MaxGage; 
            return;
        }
        if (Input.GetKey(KeyCode.C)) Gage -= Time.deltaTime;
		else if(Gage < MaxGage) Gage += Time.deltaTime;
	}

    public void SetAttack(float attack) {
        Attack += attack;
    }

    public void SetBulletIndex(int index) {
        if (bulletIndex >= BulletMng.Instance.BulletPrefabList.Count) return;
        bulletIndex += index;
    }

}
