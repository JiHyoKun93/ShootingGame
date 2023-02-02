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

    public BULLET_OWNER Owner; // �߻��� ���
    
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
        //���������� �ѹ� ����
        rigid.position = Vector2.one * 999999;
	}

    void Clipping() {
        // ī�޶� ���� ��� ��ġ
		Vector2 view = CameraMng.Instance.MainCamera.WorldToViewportPoint(rigid.position);

        if(view.x > 1.0f || view.x < 0 || view.y > 1.0f || view.y < 0) {
			// ī�޶� ������ ����� tag�� �����Ͽ� �������� ���� �ʰ� ����
			gameObject.tag = "BulletOut";
        }

        if(view.x > 1.5f || view.x < -0.5f || view.y > 1.5f || view.y < -0.5f) {
			//ī�޶� ������ ����� �ش� ������Ʈ ��Ȱ��ȭ
			gameObject.SetActive(false);
        }
    }

    public void Spawn(Vector2 pos, BULLET_OWNER owner, Vector2 direction, float attack =1, float speed = 3, float angle = 0 ) {
        gameObject.SetActive(true);
        gameObject.tag = "Bullet"; 
		Speed = speed;
		Owner = owner; // �߻��� ���
		Attack = attack;
        Angle = angle + Mathf.Deg2Rad; // ���Ȱ�

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