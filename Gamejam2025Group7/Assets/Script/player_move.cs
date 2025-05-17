using System;
using UnityEngine;
using UnityEngine.UI;


public class player_move : MonoBehaviour
{
    [SerializeField] private float interval = 1;
    [SerializeField] private GameObject myBullet;
    //[SerializeField] private GameObject hitMarker;
    /*private GameObject gameObj;
     */
    bool isShiftPushing = false;
    void MyBullet_create()
    {
        Vector3 newPos = this.transform.position;
        Vector3 offset = new Vector3(0.25f, 0, 0);

        newPos += offset;
        Instantiate(myBullet, newPos, Quaternion.identity);
        newPos -= 2 * offset;
        Instantiate(myBullet, newPos, Quaternion.identity);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("MyBullet_create", 0, interval);
        /*Vector3 playerPos = transform.position;
        gameObj = Instantiate(hitMarker, playerPos, Quaternion.identity);
        gameObj.SetActive(false);
        Debug.Log(hitMarker.transform.position);
        */
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
        /*
        //�ᑬ�}�[�J�[�̕\��
        Vector3 playerPos = transform.position;
        gameObj.transform.position = playerPos;
        Debug.Log(gameObj.transform.position);

        if (isShiftPushing) // Shift�L�[�������Ă����
        {

            gameObj.SetActive(true);// �����ۂ�\��
            Debug.Log("�ᑬ�}�[�J�[");
        }
        else
        {
            gameObj.SetActive(false); // �ʏ펞�͔�\��
            //Debug.Log("�ᑬ�}�[�J�[OFF");
        }
        */
        if (Input.GetKey(KeyCode.Z)) // Z�L�[�Œe����
        {
            MyBullet_create();
        }
    }
}

/*private void myBullet_create()
{
    Vector3 newPos = this.transform.position;
    Vector3 offset = new Vector3(0.25f, 0, 0);

    newPos += offset;
    var b = Instantiate(myBullet, newPos, Quaternion.identity);
    newPos -= 2 * offset;
    var c = Instantiate(myBullet, newPos, Quaternion.identity);
}
*/