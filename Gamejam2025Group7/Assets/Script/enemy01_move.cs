using UnityEngine;

public class enemy01_move : MonoBehaviour
{
    [SerializeField] private float interval = 1;
    [SerializeField] private GameObject enemy01Bullet;

    private Vector3 newPos;
    private Vector3 StartPos;

    private float tim = 0;
    private int enemy01_HP = 30;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        newPos = this.transform.position;
        StartPos = this.transform.position;
        InvokeRepeating("enemy01Bullet_create", 0, interval); //�U���p�^�[��1�X�^�[�g
        Destroy(this.gameObject, 8); //8�b��Ɏ�������
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tim += Time.deltaTime;
        newPos = this.transform.position;
        if(StartPos.y - newPos.y <= 3)transform.Translate(0, -0.075f, 0); //���̋����i�񂾂�~�܂�
        if(tim >= 3) //���̎��Ԍ�ł܂�����
        {
            if (newPos.x > 0) //��ʉE�ɂ���Ȃ�A�E���� ���g����Œl�ς���K�v�L
            {
                transform.Translate(0.025f, -0.05f, 0);
            }
            else//��ʍ��ɂ���Ȃ�A������
            {
                transform.Translate(-0.025f, -0.075f, 0);
            }
        }

        if (enemy01_HP <= 0) Destroy(this.gameObject); //HP��0�ȉ��ŏ���
    }

    private void enemy01Bullet_create() //�U���p�^�[��1
    {
        if(StartPos.y - newPos.y >= 3) //���̈ʒu�ɂȂ�����X�^�[�g ���g����Œl�ς���K�v�L
        {
            if (newPos.x > 0) //��ʉE�ɂ���Ȃ�A�����Ɍ����čU�� ���g����Œl�ς���K�v�L
            {
                for (int i = -5; i < 4; i++)
                {
                    //this.transform.position = newPos;
                    var b = Instantiate(enemy01Bullet, newPos, Quaternion.Euler(0, 0, i * 15f));
                }
            }
            else //��ʍ��ɂ���Ȃ�A�E���Ɍ����čU��
            {
                for (int i = -3; i < 6; i++)
                {
                    //this.transform.position = newPos;
                    var b = Instantiate(enemy01Bullet, newPos, Quaternion.Euler(0, 0, i * 15f));
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "myBullet") //myBullet�ƃ^�O�̂����I�u�W�F�N�g�ɓ������HP����
        {
            enemy01_HP -= 5;
        }
    }

}
