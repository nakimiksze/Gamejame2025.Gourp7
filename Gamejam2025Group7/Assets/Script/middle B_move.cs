using UnityEngine;
using UnityEngine.UI;

public class middleB_move : MonoBehaviour
{
    [SerializeField] private float interval = 0.1f;
    [SerializeField] private GameObject[] MbossBullet = new GameObject[4];

    [SerializeField] Slider boss_slider;
    [SerializeField] private float bossmin_HP = 0, boss01_HP = 2000;//, current_HP = 0;

    private Vector3 newPos;
    private Vector3 StartPos;

    private float tim = 0, time0 = 0;
    private int i, k = 0, cha = 0, c_x = 1, c_y = -1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boss_slider = GameObject.Find("BossHp").GetComponent<Slider>();

        boss_slider.maxValue = boss01_HP;
        boss_slider.minValue = bossmin_HP;
        boss_slider.value = bossmin_HP;

        newPos = this.transform.position;
        StartPos = newPos;

        //InvokeRepeating("normal01Bullet_create", 0, interval * 20);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tim += Time.deltaTime;
        time0 += Time.deltaTime;

        newPos = this.transform.position;

        if (StartPos.y - newPos.y <= 3 && k == 0) transform.Translate(-0.05f, -0.075f, 0); //���̋����i�񂾂�~�܂�
        else if(k == 0)
        {
            InvokeRepeating("normal01Bullet_create", 0, interval * 20);
            k = 1;
        }

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
            transform.Translate(c_x * 0.075f, c_y * 0.01f, 0);
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
            //�X���C�_�[��\��
            boss_slider.gameObject.SetActive(false);
            WeekEnemySystem.instance.j = 0;
            //��boss���f�X�g���C�I
            Destroy(this.gameObject);
        }
    }

    private void normal01Bullet_create() //�ʏ�U���p�^�[��1
    {
        for (int i = 5; i <= 8; i++)
        {
            var a = Instantiate(MbossBullet[1], newPos, Quaternion.Euler(0, 0, i * 15f));
            var b = Instantiate(MbossBullet[2], newPos, Quaternion.Euler(0, 0, i * 15f));
            var c = Instantiate(MbossBullet[3], newPos, Quaternion.Euler(0, 0, i * 15f));

            var d = Instantiate(MbossBullet[1], newPos, Quaternion.Euler(0, 0, -i * 15f));
            var e = Instantiate(MbossBullet[2], newPos, Quaternion.Euler(0, 0, -i * 15f));
            var f = Instantiate(MbossBullet[3], newPos, Quaternion.Euler(0, 0, -i * 15f));
        }

        i++;
        if (i == 10) //���񐔍s�����u��
        {
            i = 0;
            CancelInvoke("normal01Bullet_create"); //�ʏ�U���p�^�[��1��~
            InvokeRepeating("normal02Bullet_create", 0, interval * 5); //�ʏ�U���p�^�[��2����
        }
    }

    private void normal02Bullet_create() //�ʏ�U���p�^�[��2
    {
        for (int i = 0; i <= 12; i++)
        {
            var a = Instantiate(MbossBullet[0], newPos, Quaternion.Euler(0, 0, i * 15f * -tim));
            var b = Instantiate(MbossBullet[0], newPos, Quaternion.Euler(0, 0, i * 15f * tim));
        }

        i++;
        if (i == 20) //���񐔍s�����u��
        {
            i = 0;
            CancelInvoke("normal02Bullet_create"); //�ʏ�U���p�^�[��2��~
            InvokeRepeating("normal01Bullet_create", 0, interval * 20); //�ʏ�U���p�^�[��1����->���[�v
        }
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "myBullet") //myBullet�ƃ^�O�̂����I�u�W�F�N�g�ɓ������HP����
        {
            boss01_HP -= 5;
        }
    }
}
