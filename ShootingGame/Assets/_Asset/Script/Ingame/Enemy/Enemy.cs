using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;

public class Enemy : MonoBehaviour{

    protected Rigidbody2D rigid;
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;

    protected Vector2 MoveDir;
    BULLET_OWNER Owner;
    int Number;
    float Angle;

    [SerializeField] protected float MaxHp;
    [SerializeField] protected float Hp;
    [SerializeField] float Speed;
    [SerializeField] protected float Attack;

    public int EnemyB_Count;

    string[] ItemName = { "Gage", "Plus", "Power", "Hp" };
    int ItemNameIndex;

	Coroutine CameraCo;

	virtual protected void Update(){
        Clipping();
    }

	virtual protected void FixedUpdate() {
        rigid.velocity = MoveDir * Speed;
    }

    void Clipping() {
		Vector2 veiw = CameraMng.Instance.MainCamera.WorldToViewportPoint(transform.position);
        if (veiw.x > 1.8f || veiw.y > 1.5f || veiw.x < -1.8f || veiw.y < -0.1f) {
            gameObject.SetActive(false);
        }
    }

    virtual public void Spawn(Vector2 pos, float hp, float speed = 1) {
        gameObject.SetActive(true);
        Speed = speed;
        MaxHp = hp;
		Hp = hp;

		if (rigid == null)
			rigid = GetComponent<Rigidbody2D>();

		if (spriteRenderer == null)
			spriteRenderer = GetComponent<SpriteRenderer>();
        if (animator == null)
            animator = GetComponent<Animator>();

		transform.position = pos;
    }

    public void TakeDmg(float dmg) {
        Hp -= dmg;

        if(Hp <= 0) {
            GameManager.Instance.score += Random.Range(1, 11);
            
            int dropItem = Random.Range(0, 10);
            if (dropItem == 3) {
                ItemNameIndex = Random.Range(0, ItemName.Length);
                ItemMng.Instance.CreateItem(transform.position, "Item" + ItemName[ItemNameIndex]);
            }

			//transform.position = Vector2.one * 999999;
			StartCoroutine(SetDieAnimation());
            if(gameObject.name == "EnemyBoss") {
				if (CameraCo != null) StopCoroutine(CameraCo);
				CameraCo = StartCoroutine(CameraMng.Instance.CameraShake(CameraCo, 1, 5));
			}
		}
    }

	IEnumerator SetDieAnimation() {
		animator.SetBool("die", true);
		yield return new WaitForSeconds(0.4f);
		gameObject.SetActive(false);
	}

	virtual protected void OnTriggerPlayer(Collider2D collision, Player player) {
        player.TakeDmg(Attack);
        if (!gameObject.name.Equals("EnemyBoss")) gameObject.SetActive(false);
    }

    virtual protected void OnTriggerBullet(Collider2D collision, Bullet bullet) {
        TakeDmg(bullet.Attack);
        collision.gameObject.SetActive(false);
    }

    virtual protected void OnTriggerGuidBullet(Collider2D collision, GuidBullet bullet) {
        TakeDmg(bullet.Attack);
        collision.gameObject.SetActive(false);
    }

    // virtual 을 사용하면 자식클래스에서 override 가능
    virtual protected void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            OnTriggerPlayer(collision, collision.GetComponent<Player>());
        }
        if (collision.CompareTag("Bullet")) {
            if(collision.GetComponent<Bullet>() != null && collision.GetComponent<Bullet>().Owner == BULLET_OWNER.PLAYER)
                OnTriggerBullet(collision, collision.GetComponent<Bullet>());
            if(collision.GetComponent<GuidBullet>() != null) // GuidBullet은 Player만 발사 가능이어서 Owner 체크 안해도 될듯?
                OnTriggerGuidBullet(collision, collision.GetComponent<GuidBullet>());
        }
    }

    virtual protected IEnumerator HitSpriteSwap() {
        yield return null;
    }
}
