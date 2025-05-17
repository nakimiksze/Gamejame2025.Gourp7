using UnityEngine;
using UnityEngine.UI;

public class last_spell : MonoBehaviour
{
    [SerializeField] private float interval = 0.1f;
    [SerializeField] private GameObject[] bossBullet = new GameObject[3];
    [SerializeField] Text spell_text;

    private Vector3 newPos;

    private float tim = 0;
    private int boss01_HP = 1800,i,j = 0;
    private RectTransform text_move;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spell_text = GameObject.Find("lastspell").GetComponent<Text>();
        text_move = GameObject.Find("lastspell").GetComponent<RectTransform>();
        newPos = this.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tim += Time.deltaTime;
        if (boss01_HP <= 1500 && j == 0) //�{�X������HP�ɂȂ����u��
        {
            InvokeRepeating("normal01Bullet_create", 0, interval * 20);
            j = 1;
        }
        if (boss01_HP <= 1000 && j == 1) //�{�X������HP�ɂȂ����u��
        {
            CancelInvoke(); //�ʏ�U���̒�~
            spell_text.enabled = true; //�X�y������\��
            InvokeRepeating("text_move01", 2, interval * 0.025f); //���b��e�L�X�g���㕔�Ɉړ�
            j = 2; //��񂵂����Ȃ��悤�ɂ��邽��
        }
        if (boss01_HP <= 0)//boss��|�����I
        {
            //enemyBullet�̃^�O�����Ă���obj������
            GameObject[] objects = GameObject.FindGameObjectsWithTag("enemyBullet");
            foreach (GameObject obj in objects)
            {
                Destroy(obj);
            }
            //���X�g�X�y���̃e�L�X�g��\��
            spell_text.enabled = false;
            //boss���f�X�g���C�I
            Destroy(this.gameObject);
        }
    }

    private void normal01Bullet_create() //�ʏ�U���p�^�[��1
    {
        for (int i = 0; i < 12; i++)
        {
            var b = Instantiate(bossBullet[2], newPos, Quaternion.Euler(0, 0, i * 30f));
        }
        for (int i = 0; i < 12; i++)
        {
            var c = Instantiate(bossBullet[0], newPos, Quaternion.Euler(0, 0, tim * i * 30f));
        }
    }

    private void text_move01() //�e�L�X�g�ړ� ���g����Œl�ς���K�v�L
    {
        text_move.position += new Vector3(0, 1f, 0);
        if (text_move.position.y >= 320) //���̈ʒu�ɒ������u��
        {
            CancelInvoke(); //�e�L�X�g�̓������~�߂�
            InvokeRepeating("boss01Bullet_create", 0, interval); //���X�g�X�y�J�p�^�[��1����
        }
    }

    private void boss01Bullet_create() //���X�g�X�y�J�p�^�[��1
    {
        var a = Instantiate(bossBullet[0], newPos, Quaternion.Euler(0, 0, tim * 120f));
        var b = Instantiate(bossBullet[0], newPos, Quaternion.Euler(0, 0, tim * 120f + 90));
        var c = Instantiate(bossBullet[0], newPos, Quaternion.Euler(0, 0, tim * 120f + 180));
        var d = Instantiate(bossBullet[0], newPos, Quaternion.Euler(0, 0, tim * 120f + 270));
        i++;
        if (i == 60) //���񐔍s�����u��
        {
            i = 0;
            CancelInvoke("boss01Bullet_create"); //���X�g�X�y�J�p�^�[��1��~
            InvokeRepeating("boss02Bullet_create", 0, interval); //���X�g�X�y�J�p�^�[��2����
        }
    }

    private void boss02Bullet_create() //���X�g�X�y�J�p�^�[��2
    {
        var a = Instantiate(bossBullet[0], newPos, Quaternion.Euler(0, 0, -tim * 120f));
        var b = Instantiate(bossBullet[0], newPos, Quaternion.Euler(0, 0, -tim * 120f + 90));
        var c = Instantiate(bossBullet[0], newPos, Quaternion.Euler(0, 0, -tim * 120f + 180));
        var d = Instantiate(bossBullet[0], newPos, Quaternion.Euler(0, 0, -tim * 120f + 270));
        i++;
        if (i == 60)
        {
            i = 0;
            CancelInvoke("boss02Bullet_create");
            Invoke("boss03Bullet_create", 0.5f);
        }
    }

    private void boss03Bullet_create() //���X�g�X�y�J�p�^�[��3
    {
        for (int i = 3; i > -4; i--)
        {
            var a = Instantiate(bossBullet[1], newPos, Quaternion.Euler(0, 0, i * 30f));
        }
        InvokeRepeating("boss01Bullet_create", 4, interval); //���X�g�X�y�J�p�^�[��1����
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "myBullet") //myBullet�ƃ^�O�̂����I�u�W�F�N�g�ɓ������HP����
        {
            boss01_HP -= 5;
        }
    }

}
