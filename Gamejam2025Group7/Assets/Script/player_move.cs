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

    void FixedUpdate()
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

    private void myBullet_create()
    {
        Vector3 newPos = this.transform.position;
        Vector3 offset = new Vector3(0.25f, 0, 0);

        newPos += offset;
        var b = Instantiate(myBullet, newPos, Quaternion.identity);
        newPos -= 2 * offset;
        var c = Instantiate(myBullet, newPos, Quaternion.identity);
    }
}
