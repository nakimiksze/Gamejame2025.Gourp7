using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class last_spell : MonoBehaviour
{
    [SerializeField] private float interval = 0.1f;
    [SerializeField] private GameObject[] bossBullet = new GameObject[7];
    [SerializeField] private GameObject cutin;
    [SerializeField] Text spell_text;

    [SerializeField] Slider boss_slider;
    [SerializeField] private float bossmin_HP = 0, boss01_HP = 4000;//<- HP4000���炢����
    public GameObject pauseMenu;
    [SerializeField] private Canvas canvas;

    private Vector3 newPos, StartPos/*,stanPos*/;

    private float tim = 0,time0 = 0;
    private int i,j = 0,k = 0,cha = 0,c_x = 1,c_y = -1,m = 0;
    private RectTransform text_move;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spell_text = GameObject.Find("lastspell").GetComponent<Text>();
        text_move = GameObject.Find("lastspell").GetComponent<RectTransform>();
        boss_slider = GameObject.Find("BossHp").GetComponent<Slider>();
        cutin = GameObject.FindWithTag("StandingBoss");
        //stanPos = cutin.transform.position;

        boss_slider.maxValue = boss01_HP;
        boss_slider.minValue = bossmin_HP;
        boss_slider.value = bossmin_HP;
        boss_slider.gameObject.SetActive(true);

        newPos = this.transform.position;
        StartPos = newPos;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tim += Time.deltaTime;
        time0 += Time.deltaTime;

        newPos = this.transform.position;

        if (StartPos.y - newPos.y <= 3 && k == 0) transform.Translate(-0.05f, -0.075f, 0); //���̋����i�񂾂�~�܂�
        else if (k == 0) k = 1;

        if (time0 > 5 && k == 1)
        {
            if (cha == 3) 
            { 
                cha = 2;
                c_x = -1;
                c_y = 1;
            }
            else if (cha == 2)
            {
                cha = 1;
                c_x = 1;
                c_y = 1;
            }
            else if (cha == 1)
            {
                cha = 0;
                c_x = 1;
                c_y = -1;
            }
            else
            {
                cha = 3;
                c_x = -1;
                c_y = -1;
            }
            time0 = 0;
        }
        else if (time0 > 4 && k == 1)
        {
            transform.Translate(c_x * 0.075f,c_y  * 0.01f, 0);
        }

        if (k == 1 && m == 0)
        {
            m = 1;
            InvokeRepeating("normal01Bullet_create", 0, interval * 10);
        }
        /* �X�y�J�ҁ@�o������A����j�̒l�ς��Ă�
        if (boss01_HP <= 3000 && j == 0) //�{�X������HP�ɂȂ����u��
        {
            CancelInvoke(); //�ʏ�U���̒�~
            spell_text.enabled = true; //�X�y������\��
            spell_text.text = "�X�y�J"; //text�̕\�L�ύX
            InvokeRepeating("cutin_move01", 0, interval * 0.025f); //���b��e�L�X�g���㕔�Ɉړ�
            InvokeRepeating("text_move01", 3, interval * 0.025f); //���b��e�L�X�g���㕔�Ɉړ�
            j = 1; //��񂵂����Ȃ��悤�ɂ��邽��
        }
        */
        if (boss01_HP <= 2000 && j == 0) //�{�X������HP�ɂȂ����u��
        {
            CancelInvoke(); //�X�y�J��~
            InvokeRepeating("normal02Bullet_create", 0, interval * 20);
            j = 1;
        }

        if (boss01_HP <= 1000 && j == 1) //�{�X������HP�ɂȂ����u��
        {
            CancelInvoke(); //�ʏ�U���̒�~
            spell_text.enabled = true; //�X�y������\��
            spell_text.text = "���X�g�X�y�J"; //text�̕\�L�ύX
            InvokeRepeating("cutin_move01", 0, interval * 0.025f); //���b��e�L�X�g���㕔�Ɉړ�
            InvokeRepeating("text_move01", 3, interval * 0.025f); //���b��e�L�X�g���㕔�Ɉړ�
            j = 2; //��񂵂����Ȃ��悤�ɂ��邽��
        }

        boss_slider.value = Mathf.Clamp(boss01_HP, bossmin_HP, boss01_HP);

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
            boss_slider.gameObject.SetActive(false);
            // var isActive = false;
            // var tmpObject = Instantiate(pauseMenu, canvas.transform);
            // tmpObject.SetActive(!isActive);
            //boss���f�X�g���C�I
            Destroy(this.gameObject);
            SceneManager.LoadScene("Title");
            // Time.timeScale = isActive ? 1 : 0;
            Debug.Log("ゲームクリアおめでとう");
        }
    }

    private void normal01Bullet_create() //�ʏ�U���p�^�[��1
    {
        for (int i = 0; i < 24; i++)
        {
            var a = Instantiate(bossBullet[0], newPos, Quaternion.Euler(0, 0, i * 15f));
            var b = Instantiate(bossBullet[5], newPos, Quaternion.Euler(0, 0, i * 15f));
            var c = Instantiate(bossBullet[6], newPos, Quaternion.Euler(0, 0, i * 15f));
        }
    }

    private void normal02Bullet_create() //�ʏ�U���p�^�[��2
    {
        for (int i = 0; i < 18; i++)
        {
            var b = Instantiate(bossBullet[3], newPos, Quaternion.Euler(0, 0, i * 20f));
        }
        for (int i = 0; i < 18; i++)
        {
            var c = Instantiate(bossBullet[4], newPos, Quaternion.Euler(0, 0, tim * i * 20f));
        }
    }

    private void cutin_move01() //�����G�ړ�1 ���g����Œl�ς���K�v�L
    {   
        Debug.Log("現在位置: " + cutin.transform.position.x);
        cutin.transform.position += new Vector3(-0.02f, 0, 0);
        if (cutin.transform.position.x <= 1) //���̈ʒu�ɒ������u��
        {
            Debug.Log("起動2");
            // InvokeRepeating("cutin_move02", 2.5f, interval * 0.025f);
            while (true)
            {
                Debug.Log("関数が呼ばれました");
                cutin.transform.position += new Vector3(0.1f, 0, 0);
                if (cutin.transform.position.x >= 6) //���̈ʒu�ɒ������u��
                {
                    // CancelInvoke("cutin_move02");
                    break;
                }
            }

            CancelInvoke("cutin_move01");
        }
    }

    private void cutin_move02() //�����G�ړ�2 ���g����Œl�ς���K�v�L
    {
        Debug.Log("関数が呼ばれました");
        cutin.transform.position += new Vector3(0.1f, 0, 0);
        if (cutin.transform.position.x >= 6) //���̈ʒu�ɒ������u��
        {
            // CancelInvoke("cutin_move02");
            return;
        }
    }


    private void text_move01() //�e�L�X�g�ړ� ���g����Œl�ς���K�v�L
    {
        text_move.position += new Vector3(0, 1f, 0);
        if (text_move.position.y >= 320) //���̈ʒu�ɒ������u��
        {
            CancelInvoke(); //�e�L�X�g�̓������~�߂�
            /*if (m == 1) �X�y�J�o������
            {
                InvokeRepeating("�X�y�J��", 0, interval); //�X�y�J����
                m = 2;
            }
            else */
            InvokeRepeating("boss01Bullet_create", 0, interval); //���X�g�X�y�J�p�^�[��1����
        }
    }

    private void boss01Bullet_create() //���X�g�X�y�J�p�^�[��1
    {
        var a = Instantiate(bossBullet[0], newPos, Quaternion.Euler(0, 0, tim * 120f));
        var b = Instantiate(bossBullet[1], newPos, Quaternion.Euler(0, 0, tim * 120f + 90));
        var c = Instantiate(bossBullet[0], newPos, Quaternion.Euler(0, 0, tim * 120f + 180));
        var d = Instantiate(bossBullet[1], newPos, Quaternion.Euler(0, 0, tim * 120f + 270));
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
        var a = Instantiate(bossBullet[1], newPos, Quaternion.Euler(0, 0, -tim * 120f));
        var b = Instantiate(bossBullet[0], newPos, Quaternion.Euler(0, 0, -tim * 120f + 90));
        var c = Instantiate(bossBullet[1], newPos, Quaternion.Euler(0, 0, -tim * 120f + 180));
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
            var a = Instantiate(bossBullet[2], newPos, Quaternion.Euler(0, 0, i * 30f));
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
