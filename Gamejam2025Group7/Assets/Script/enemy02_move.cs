using UnityEngine;

public class enemy02_move : MonoBehaviour
{
    [SerializeField] private float interval = 1, x = 0.025f, y = 0.15f;
    [SerializeField] private GameObject enemy02Bullet;

    [SerializeField] private float speed = 5;
    [SerializeField] GameObject player;

    private Vector3 newPos;
    private Vector3 StartPos;

    private float t01 = 180,tim = 0;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        newPos = this.transform.position;
        StartPos = this.transform.position;
        InvokeRepeating("enemy02Bullet_create", 0, interval);
        Destroy(this.gameObject, 6);
    }

    void FixedUpdate()
    {
        tim += Time.deltaTime;

        newPos = this.transform.position;
        
        t01 += Time.deltaTime * 0.15f;
        if (newPos.x > 0)
        {
            newPos.x += Mathf.Sin(-t01) * x;
            newPos.y += Mathf.Cos(-t01) * y;
            this.transform.position = newPos;
        }
        else
        {
            newPos.x += Mathf.Sin(t01) * x;
            newPos.y += Mathf.Cos(t01) * y;
            this.transform.position = newPos;
        }
    }

    private void enemy02Bullet_create()
    {
        if (tim > 0.5f)
        {
            var b = Instantiate(enemy02Bullet, newPos, Quaternion.identity);

            Vector3 vec = player.transform.position - b.transform.position;
            Quaternion rotation = Quaternion.FromToRotation(Vector3.down, vec);

            b.transform.rotation = rotation;
            b.GetComponent<Rigidbody2D>().linearVelocity = vec.normalized * speed;
        }
    }

}
