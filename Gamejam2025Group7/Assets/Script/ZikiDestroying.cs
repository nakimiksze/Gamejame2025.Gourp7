using UnityEngine;

public class ZikiDestroying : MonoBehaviour
{
    [SerializeField] private GameObject hitMarker;
    private GameObject hitMarkerActivating;
    bool isShiftPushing = false;
    private int kuraiFrameCounter = 0;
    bool kuraiTriggered = false;
    /*void BombStart()
    {
        Destroy()
    }
    */
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bullet")
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
        //�ᑬ�}�[�J�[�̕\��
        Vector3 playerPos = transform.position;
        hitMarkerActivating.transform.position = playerPos;
        Debug.Log(hitMarkerActivating.transform.position);

        if (isShiftPushing) // Shift�L�[�������Ă����
        {

            hitMarkerActivating.SetActive(true);// �����ۂ�\��
            Debug.Log("�ᑬ�}�[�J�[");
        }
        else
        {
            hitMarkerActivating.SetActive(false); // �ʏ펞�͔�\��
            Debug.Log("�ᑬ�}�[�J�[OFF");
        }
        if (!kuraiTriggered) return; // A�C�x���g���������Ă��Ȃ��Ȃ牽�����Ȃ�

        kuraiFrameCounter++;

        if (kuraiFrameCounter >= 4 && !(Input.GetKey(KeyCode.X)))
        {
            // 4�t���[���o�߁�X��������Ȃ������ꍇ
            Destroy(gameObject);
            Destroy(hitMarkerActivating);
            kuraiFrameCounter = 0; // ���Z�b�g
            kuraiTriggered = false;
        }
        else if (kuraiFrameCounter >= 4 && (Input.GetKey(KeyCode.X)))
        {
            //BombStart(); //���Ƃł�����
            Destroy(gameObject);
            Destroy(hitMarkerActivating);
            kuraiFrameCounter = 0; // ���Z�b�g
            kuraiTriggered = false;
        }
        


    }
}