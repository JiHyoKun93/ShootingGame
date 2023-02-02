using UnityEngine;

#if UNITY_EDITOR
#endif

public enum BULLET_OWNER { PLAYER, ENEMY }

public class Bullet : MonoBehaviour{

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;

	[SerializeField] public float Attack;
	[SerializeField] float Speed;
    [SerializeField] Vector2 MoveDir;

    public BULLET_OWNER Owner; // 발사한 대상
    
    float Angle;

    private void Start() {
        rigid = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

    private void FixedUpdate() {
        rigid.velocity = MoveDir * Speed;
        Clipping();
    }

    private void OnDisable() {
        //없어졌을때 한번 실행
        rigid.position = Vector2.one * 999999;
	}

    void Clipping() {
        // 카메라 안의 대상 위치
		Vector2 view = CameraMng.Instance.MainCamera.WorldToViewportPoint(rigid.position);

        if(view.x > 1.0f || view.x < 0 || view.y > 1.0f || view.y < 0) {
			// 카메라 범위를 벗어나면 tag를 변경하여 데미지를 주지 않게 설정
			gameObject.tag = "BulletOut";
        }

        if(view.x > 1.5f || view.x < -0.5f || view.y > 1.5f || view.y < -0.5f) {
			//카메라 범위를 벗어나면 해당 오브젝트 비활성화
			gameObject.SetActive(false);
        }
    }

    public void Spawn(Vector2 pos, BULLET_OWNER owner, Vector2 direction, float attack =1, float speed = 3, float angle = 0 ) {
        gameObject.SetActive(true);
        gameObject.tag = "Bullet"; 
		Speed = speed;
		Owner = owner; // 발사한 대상
		Attack = attack;
        Angle = angle + Mathf.Deg2Rad; // 라디안값

        transform.position = pos;

        MoveDir = direction;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            if(Owner != BULLET_OWNER.PLAYER) {
                Player player = collision.GetComponent<Player>();
                player.TakeDmg(Attack);
                gameObject.SetActive(false);
            }
        }
    }
}