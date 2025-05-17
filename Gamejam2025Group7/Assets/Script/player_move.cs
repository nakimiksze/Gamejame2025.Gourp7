using System;
using UnityEngine;

public class player_move : MonoBehaviour
{
    [SerializeField] private float interval = 1;
    [SerializeField] private GameObject myBullet;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("myBullet_create", 0, interval);
    }

    void FixedUpdate() //プレイヤーwasdで移動制御
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, 0.1f, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-0.1f, 0, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, -0.1f, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(0.1f, 0, 0);
        }
    }

    private void myBullet_create() //弾を前方で発射
    {
        Vector3 newPos = this.transform.position;
        Vector3 offset = new Vector3(0.25f, 1.25f, 0);

        newPos += offset;
        var b = Instantiate(myBullet, newPos, Quaternion.identity);
        newPos.x -= 2 * offset.x;
        var c = Instantiate(myBullet, newPos, Quaternion.identity);
    }
}
