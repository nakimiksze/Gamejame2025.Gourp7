using UnityEngine;

public class enemy01_move : MonoBehaviour
{
    [SerializeField] private float interval = 1,x = 0.025f,y = 0.15f;
    [SerializeField] private GameObject enemy01Bullet;

    private Vector3 newPos;
    private Vector3 StartPos;

    private float tim = 0,t01 = 180;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        newPos = this.transform.position;
        StartPos = this.transform.position;
        InvokeRepeating("enemy01Bullet_create", 0, interval);
        Destroy(this.gameObject, 8);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tim += Time.deltaTime;
        newPos = this.transform.position;
        if(StartPos.y - newPos.y <= 4)transform.Translate(0, -0.075f, 0);
        if(tim >= 3)
        {
            t01 += Time.deltaTime;
            if (newPos.x > 0)
            {
                //newPos.x += Mathf.Sin(-t01) * 0.075f;
                //newPos.y += Mathf.Cos(-t01) * 0.1f;
                newPos.x += Mathf.Pow(2, 1.2f) * x;
                newPos.y -= Mathf.Log(2, t01) * y;
                this.transform.position = newPos;
            }
            else
            {
                newPos.x -= Mathf.Pow(2,1.2f) * x;
                newPos.y -= Mathf.Log(2,t01) * y;
                this.transform.position = newPos;
            }
        }
    }

    private void enemy01Bullet_create()
    {
        if(StartPos.y - newPos.y >= 4)
        {
            for (int i = -2; i < 3; i++)
            {
                //this.transform.position = newPos;
                var b = Instantiate(enemy01Bullet, newPos, Quaternion.Euler(0, 0, i * 7.5f));
            }
        }
    }
}
