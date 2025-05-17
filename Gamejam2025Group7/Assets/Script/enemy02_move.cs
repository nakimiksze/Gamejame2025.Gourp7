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
    private int enemy02_HP = 30;

    void Start()
    {
        player = GameObject.Find("player"); //ヒエラルキー上のplayerを入れる
        newPos = this.transform.position;
        StartPos = this.transform.position;
        InvokeRepeating("enemy02Bullet_create", 0, interval); //攻撃パターン1スタート
        Destroy(this.gameObject, 6); //6秒後に自動消滅
    }

    void FixedUpdate()
    {
        tim += Time.deltaTime;

        newPos = this.transform.position;
        
        t01 += Time.deltaTime * 0.15f;
        if (newPos.x > 0) //画面右にいるなら、中央から右下へ弧を描いて動く ※枠次第で値変える必要有
        {
            newPos.x += Mathf.Sin(-t01) * x;
            newPos.y += Mathf.Cos(-t01) * y;
            this.transform.position = newPos;
        }
        else //画面右にいるなら、中央から右下へ弧を描いて動く ※枠次第で値変える必要有
        {
            newPos.x += Mathf.Sin(t01) * x;
            newPos.y += Mathf.Cos(t01) * y;
            this.transform.position = newPos;
        }

        if (enemy02_HP <= 0) Destroy(this.gameObject); //HPが0以下で消滅
    }

    private void enemy02Bullet_create() //攻撃パターン1
    {
        if (tim > 0.5f)//一定の時間後でスタート
        {
            var b = Instantiate(enemy02Bullet, newPos, Quaternion.identity);

            //プレイヤーに向かって
            Vector3 vec = player.transform.position - b.transform.position;
            Quaternion rotation = Quaternion.FromToRotation(Vector3.down, vec);

            b.transform.rotation = rotation;
            b.GetComponent<Rigidbody2D>().linearVelocity = vec.normalized * speed;
        }
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "myBullet") //myBulletとタグのついたオブジェクトに当たるとHP減少
        {
            enemy02_HP -= 5;
        }
    }
}
