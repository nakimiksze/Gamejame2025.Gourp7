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
        //低速移動か否かの処理
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isShiftPushing = true;
        }
        else
        {
            isShiftPushing = false;
        }
        float zikiSpeed = isShiftPushing ? 0.11f : 0.22f; // 低速ならなんとか、高速ならなんちゃら
        //移動の処理
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
        //低速マーカーの表示
        Vector3 playerPos = transform.position;
        gameObj.transform.position = playerPos;
        Debug.Log(gameObj.transform.position);

        if (isShiftPushing) // Shiftキーを押している間
        {

            gameObj.SetActive(true);// 白い丸を表示
            Debug.Log("低速マーカー");
        }
        else
        {
            gameObj.SetActive(false); // 通常時は非表示
            //Debug.Log("低速マーカーOFF");
        }
        */
        if (Input.GetKey(KeyCode.Z)) // Zキーで弾発射
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