using UnityEngine;
using UnityEngine.SceneManagement;
using static Unity.Burst.Intrinsics.X86.Avx;

public class ZikiDestroying : MonoBehaviour
{
    [SerializeField] private GameObject hitMarker;
    [SerializeField] private int interval;
    [SerializeField] private GameObject myBullet;
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private GameObject bomb;
    [SerializeField] private AudioClip deathSe;
    [SerializeField] private AudioClip bombSe;
    [SerializeField] private float bombVolume = 1.0f;
    //ステータスの定義
    [Header("ステータス")]
    [SerializeField] private int playerHP = 3;
    [SerializeField] private int bombCount = 3;
    [SerializeField] private int shotPower = 1;
    private AudioSource audioSource;
    private GameObject[] bombs = new GameObject[3];
    private GameObject hitMarkerActivating;
    bool isShiftPushing = false;
    private int kuraiFrameCounter = 0;
    bool kuraiTriggered = false;
    //bool xTriggered = false;
    bool xCoolTime = false;
    bool zikiInvulnerable = false;
    int xCoolTimeCounter = 0;
    float StartExplosion = 0;
    int mainShotCooldown = 0;
    void DestroyAllObjectsWithTag(string tag)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag); // �w��^�O�̃I�u�W�F�N�g���擾

        foreach (GameObject obj in objects) // �z����̃I�u�W�F�N�g�����ׂč폜
        {
            Destroy(obj);
        }
    }


    void MyBullet_create()
    {
        if (mainShotCooldown == 0)
        {
            Vector3 newPos = this.transform.position;
            Vector3 offset = new Vector3(0.25f, 0, 0);

            GameObject bullet1 = Instantiate(myBullet, newPos + offset, Quaternion.identity);
            GameObject bullet2 = Instantiate(myBullet, newPos - offset, Quaternion.identity);

            // 弾にパワーを伝える（Bullet スクリプトが必要）
            myBullet bulletScript1 = bullet1.GetComponent<myBullet>();
            myBullet bulletScript2 = bullet2.GetComponent<myBullet>();
            //if (bulletScript1 != null) bulletScript1.SetPower(shotPower);
            //if (bulletScript2 != null) bulletScript2.SetPower(shotPower);

            mainShotCooldown = interval;
        }
        else
        {
            mainShotCooldown --;
        }
    }
    void BombStart()
    {
        zikiInvulnerable = true;
        DestroyAllObjectsWithTag("bullet");
        DestroyAllObjectsWithTag("enemy");
        DestroyAllObjectsWithTag("enemyBullet");
        for (int i = 0; i < 3; i++)
        {
            float angle = i * 120f; // 120�x���z�u
            bombs[i] = Instantiate(bomb, transform.position, Quaternion.identity, transform); // ���@�̎q�I�u�W�F�N�g�ɐݒ�
            StartExplosion++;
        }
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemyBullet"))
        {
            Destroy(collision.gameObject);
                kuraiTriggered = true;
        }
    }

    private void Start()
    {
        Vector3 playerPos = transform.position;
        hitMarkerActivating = Instantiate(hitMarker, playerPos, Quaternion.identity);
        hitMarkerActivating.SetActive(false);
        //InvokeRepeating("MyBullet_create", 0, interval);
        Debug.Log(hitMarker.transform.position);
        audioSource = GetComponent<AudioSource>();
    }
    void FixedUpdate()
    {
        //�ᑬ�ړ����ۂ��̏���
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isShiftPushing = true;
        }
        else
        {
            isShiftPushing = false;
        }
        float zikiSpeed = isShiftPushing ? 0.11f : 0.22f; // �ᑬ�Ȃ�Ȃ�Ƃ��A�����Ȃ�Ȃ񂿂��
        //�ړ��̏���
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0, zikiSpeed, 0);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-zikiSpeed, 0, 0);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0, -zikiSpeed, 0);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(zikiSpeed, 0, 0);
        }

        if (StartExplosion>=1f&&StartExplosion <= 180f)
        {
            StartExplosion++;
        }
        if (StartExplosion > 180f)
        {
            StartExplosion = 0f;
            DestroyAllObjectsWithTag("bullet");
            DestroyAllObjectsWithTag("enemy");
            DestroyAllObjectsWithTag("enemyBullet");
        }
            //���@�����G�����@�𔼓����ɂ���
            SpriteRenderer sr = GetComponent<SpriteRenderer>(); // SpriteRenderer���擾
        if (sr != null)
        {
            Color newColor = sr.color;
            newColor.a = zikiInvulnerable ? 0.5f : 1f; // �����x��ύX
            sr.color = newColor;
        }

        //�ᑬ�}�[�J�[�̍��W�擾�E�\��
        Vector3 playerPos = transform.position;
        hitMarkerActivating.transform.position = playerPos;
        //�ᑬ�ړ����ۂ��̏���
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isShiftPushing = true;
        }
        else
        {
            isShiftPushing = false;
        }
        //�{���N�[���^�C�����ۂ��̏���
        if (xCoolTime&& xCoolTimeCounter<=250)
        {
            xCoolTimeCounter++;
        }
        if (xCoolTimeCounter> 250)
        {
            xCoolTime = false;
            xCoolTimeCounter = 0;
            zikiInvulnerable = false;
        }


        if (isShiftPushing) // Shift�L�[�������Ă����
        {
            hitMarkerActivating.SetActive(true);// �����ۂ�\��
        }
        else
        {
            hitMarkerActivating.SetActive(false); // �ʏ펞�͔�\��
        }
        if (Input.GetKey(KeyCode.Z)) // Z�L�[�Œe����
        {
            MyBullet_create();
        }
        if (!Input.GetKey(KeyCode.Z))
        {
            mainShotCooldown = 0;
        }
        if (Input.GetKey(KeyCode.X) && !xCoolTime && bombCount >0)// X�L�[�Ń{��
        {
            BombStart();
            xCoolTime = true;
            bombCount--;
            zikiInvulnerable = true;
            audioSource.PlayOneShot(bombSe);
        }
        if (kuraiTriggered)// ���炢�{����t�C�x���g���������Ă��Ȃ��Ȃ牽�����Ȃ�
        {
            kuraiFrameCounter++;

            if (kuraiFrameCounter >= 8 && !zikiInvulnerable)
            {
                // 8�t���[���o�߁�X��������Ȃ������ꍇ
                Vector3 position = new Vector3(-2.78f, -3.25f, 0f);
                transform.position = position;
                kuraiFrameCounter = 0; // ���Z�b�g
                kuraiTriggered = false;
                xCoolTime = true;
                zikiInvulnerable = true;
                xCoolTimeCounter += 180;
                Debug.Log("死亡");
                playerHP--;

                if (playerHP <= 0)
                {
                    Debug.Log("ゲームオーバー！");
                    //残機がなくなるとタイトルにもどる
                    SceneManager.LoadScene("Title");
                }

                var tmp = audioSource.volume;
                audioSource.volume = bombVolume;
                audioSource.PlayOneShot(deathSe);
                audioSource.volume = tmp;
            }
            else if (kuraiFrameCounter >= 8 && zikiInvulnerable)
            {

                kuraiFrameCounter = 0; // ���Z�b�g
                kuraiTriggered = false;

                var tmp = audioSource.volume;
                audioSource.volume = bombVolume;
                audioSource.PlayOneShot(deathSe);
                audioSource.volume = tmp;
                Debug.Log("くらいボム成功");
            }
        }
        
    }
}