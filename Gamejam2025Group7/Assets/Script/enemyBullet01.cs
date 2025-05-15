using System.Security.Permissions;
using UnityEngine;

public class enemyBullet01 : MonoBehaviour
{
    [SerializeField] private float speed = -0.1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(this.gameObject,2);
    }

    void FixedUpdate()
    {
        transform.Translate(0, speed, 0);
    }
}
