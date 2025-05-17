using UnityEngine;

public class normal01Bullet : MonoBehaviour
{
    [SerializeField] private float speed = -0.1f,sp01 = 5;
    [SerializeField] GameObject player;

    private int i = 0;
    private float tim = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("player"); //ƒqƒGƒ‰ƒ‹ƒL[ã‚Ìplayer‚ğ“ü‚ê‚é
        Destroy(this.gameObject, 6); //6•bŒã‚É©“®Á–Å
    }

    void FixedUpdate()
    {
        tim += Time.deltaTime;
        if (tim < 0.75f) transform.Translate(0, speed, 0);
        else if (i == 0)
        {
            Vector3 vec = player.transform.position - this.transform.position;
            Quaternion rotation = Quaternion.FromToRotation(Vector3.down, vec);

            this.transform.rotation = rotation;
            this.GetComponent<Rigidbody2D>().linearVelocity = vec.normalized * sp01;
            i = 1;
        }
    }
}
