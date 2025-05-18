using UnityEngine;

public class ZikiDestroying : MonoBehaviour
{
    [SerializeField] private GameObject hitMarker;
    [SerializeField] private float interval = 1;
    [SerializeField] private GameObject myBullet;
    [SerializeField] private SpriteRenderer playerSprite;
    private GameObject hitMarkerActivating;
    bool isShiftPushing = false;
    private int kuraiFrameCounter = 0;
    bool kuraiTriggered = false;
    //bool xTriggered = false;
    bool xCoolTime = false;
    bool zikiInvulnerable = false;
    int xCoolTimeCounter = 0;
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
        Vector3 newPos = this.transform.position;
        Vector3 offset = new Vector3(0.25f, 0, 0);

        newPos += offset;
        Instantiate(myBullet, newPos, Quaternion.identity);
        newPos -= 2 * offset;
        Instantiate(myBullet, newPos, Quaternion.identity);
    }
    bool IsPlayerExisting(string player)
    {
        return GameObject.Find(player) != null;
    }
    void BombStart()
    {
        zikiInvulnerable = true;
        DestroyAllObjectsWithTag("bullet");
        DestroyAllObjectsWithTag("enemy");
    }
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bullet" && !zikiInvulnerable)
        {
            Destroy(collision.gameObject);
            kuraiTriggered = true;
            Debug.Log("���炢�{����t�J�n�I�I�I");
        }
    }
    private void Start()
    {
        Vector3 playerPos = transform.position;
        hitMarkerActivating = Instantiate(hitMarker, playerPos, Quaternion.identity);
        hitMarkerActivating.SetActive(false);
        InvokeRepeating("MyBullet_create", 0, interval);
        Debug.Log(hitMarker.transform.position);
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
        if (xCoolTime&& xCoolTimeCounter<=128)
        {
            xCoolTimeCounter++;
        }
        if (xCoolTimeCounter> 128)
        {
            xCoolTime = false;
            xCoolTimeCounter = 0;
            zikiInvulnerable = false;
            Debug.Log("�{���g�p�\!!");
        }
        if (IsPlayerExisting("player"))
        {

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

            if (Input.GetKey(KeyCode.X)&& !xCoolTime)// X�L�[�Ń{��
            {
                BombStart();
                Debug.Log("�{������");
                xCoolTime = true;
            }
            if (kuraiTriggered)// ���炢�{����t�C�x���g���������Ă��Ȃ��Ȃ牽�����Ȃ�
            {
                kuraiFrameCounter++;

                if (kuraiFrameCounter >= 4 && !zikiInvulnerable)
                {
                    // 4�t���[���o�߁�X��������Ȃ������ꍇ
                    Destroy(gameObject);
                    Destroy(hitMarkerActivating);
                    kuraiFrameCounter = 0; // ���Z�b�g
                    kuraiTriggered = false;
                    Debug.Log("���炢�{�����s�I�I�I�I�I�I�I�I�I�I�I�I�I�I�I�I");
                }
                else if (kuraiFrameCounter >= 4 && zikiInvulnerable)
                {
                    //BombStart(); //���Ƃł�����
                    Debug.Log("���炢�{�����������ˁI�I�I�I�I�I�I�I�I�I�I�I�I�I�I�I");

                    kuraiFrameCounter = 0; // ���Z�b�g
                    kuraiTriggered = false;
                }
            }
        }
        


    }
}