using UnityEngine;

public class enemy02_move : MonoBehaviour
{
    [SerializeField] private float interval = 1, x = 0.025f, y = 0.15f;
    [SerializeField] private GameObject enemy02Bullet;

    [SerializeField] private float speed = 5;
    [SerializeField] GameObject player;

    private Vector3 newPos;
    private Vector3 StartPos;

    private float t01 = 180,tim = 0;
    private int enemy02_HP = 30;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        newPos = this.transform.position;
        StartPos = this.transform.position;
        InvokeRepeating("enemy02Bullet_create", 0, interval); //�U���p�^�[��1�X�^�[�g
        Destroy(this.gameObject, 6); //6�b��Ɏ�������
    }

    void FixedUpdate()
    {
        tim += Time.deltaTime;

        newPos = this.transform.position;
        
        t01 += Time.deltaTime * 0.15f;
        if (newPos.x > 0) //��ʉE�ɂ���Ȃ�A��������E���֌ʂ�`���ē��� ���g����Œl�ς���K�v�L
        {
            newPos.x += Mathf.Sin(-t01) * x;
            newPos.y += Mathf.Cos(-t01) * y;
            this.transform.position = newPos;
        }
        else //��ʉE�ɂ���Ȃ�A��������E���֌ʂ�`���ē��� ���g����Œl�ς���K�v�L
        {
            newPos.x += Mathf.Sin(t01) * x;
            newPos.y += Mathf.Cos(t01) * y;
            this.transform.position = newPos;
        }

        if (enemy02_HP <= 0) Destroy(this.gameObject); //HP��0�ȉ��ŏ���
    }

    private void enemy02Bullet_create() //�U���p�^�[��1
    {
        if (tim > 0.5f)//���̎��Ԍ�ŃX�^�[�g
        {
            var b = Instantiate(enemy02Bullet, newPos, Quaternion.identity);

            //�v���C���[�Ɍ�������
            Vector3 vec = player.transform.position - b.transform.position;
            Quaternion rotation = Quaternion.FromToRotation(Vector3.down, vec);

            b.transform.rotation = rotation;
            b.GetComponent<Rigidbody2D>().linearVelocity = vec.normalized * speed;
        }
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "myBullet") //myBullet�ƃ^�O�̂����I�u�W�F�N�g�ɓ������HP����
        {
            enemy02_HP -= 5;
        }
    }
}
