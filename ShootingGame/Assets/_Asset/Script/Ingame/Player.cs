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
public class Player : MonoBehaviour{

    Rigidbody2D rigid;
    Animator animator;
    SpriteRenderer sprite;

    Vector2 MoveDir;
    Vector2 Center;

    [SerializeField] float Speed;
    [SerializeField] float MaxHp = 5;
    [SerializeField] float Hp;

    public Image HpImage;
    Coroutine CameraCo;

    void Start(){
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        
        Vector2 max = CameraMng.Instance.MainCamera.WorldToViewportPoint(sprite.bounds.max);
        Vector2 min = CameraMng.Instance.MainCamera.WorldToViewportPoint(sprite.bounds.min);
        Center = (max - min) / 2;

        Hp = MaxHp;
    }

    void Update(){
        Move();
        Clipping();
    }

    void FixedUpdate(){
        //rigid.velocity = new Vector2(Mathf.Sin(100), Mathf.Cos(100));
        rigid.velocity = MoveDir;
    }

    public float GetHp() { return Hp; }
    public float GetMaxHp() { return MaxHp; }

    public void PlusHp() {
        if (Hp >= MaxHp) return;
        Hp++;
    }

	void Move() {
		float hor = Input.GetAxisRaw("Horizontal");
		float ver = Input.GetAxisRaw("Vertical");

		MoveDir.x = hor * Speed;
		MoveDir.y = ver * Speed;

		animator.SetInteger("hor", (int)hor);
	}

    public void TakeDmg(float dmg) {
        Hp -= dmg;
        
        animator.SetTrigger("hit");
        StartCoroutine(PlayerHitTag());

        if (CameraCo != null) StopCoroutine(CameraCo);
        CameraCo = StartCoroutine(CameraMng.Instance.CameraShake(CameraCo ,1, 5));
        
        if(Hp <= 0) {
            StartCoroutine(SetDieAnimation());
        }
    }

    IEnumerator PlayerHitTag() {
        transform.tag = "PlayerHit";
        yield return new WaitForSeconds(1f);
        transform.tag = "Player";
    }

    IEnumerator SetDieAnimation() {
        animator.SetBool("die", true);
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }

    void Clipping() {
        Vector2 PlayerView = CameraMng.Instance.MainCamera.WorldToViewportPoint(transform.position);

        if(PlayerView.x > 1.0f - Center.x) PlayerView.x = 1.0f - Center.x;
        if(PlayerView.x < Center.x) PlayerView.x = Center.x;
        if(PlayerView.y > 1.0f - Center.y) PlayerView.y = 1.0f - Center.y;
        if (PlayerView.y < Center.y) PlayerView.y = Center.y;

        Vector3 pos = CameraMng.Instance.MainCamera.ViewportToWorldPoint(PlayerView);
        transform.position = new Vector3(pos.x, pos.y, 0);
    }
}
