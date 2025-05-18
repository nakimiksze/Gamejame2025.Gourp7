using UnityEngine;

public class enemy01_move : MonoBehaviour
{
    [SerializeField] private float interval = 1;
    [SerializeField] private GameObject enemy01Bullet;

    private Vector3 newPos;
    private Vector3 StartPos;

    private float tim = 0;
    private int enemy01_HP = 30;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        newPos = this.transform.position;
        StartPos = this.transform.position;
        InvokeRepeating("enemy01Bullet_create", 0, interval); //攻撃パターン1スタート
        Destroy(this.gameObject, 8); //8秒後に自動消滅
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tim += Time.deltaTime;
        newPos = this.transform.position;
        if(StartPos.y - newPos.y <= 3)transform.Translate(0, -0.075f, 0); //一定の距離進んだら止まる
        if(tim >= 3) //一定の時間後でまた動く
        {
            if (newPos.x > 0) //画面右にいるなら、右下へ ※枠次第で値変える必要有
            {
                transform.Translate(0.025f, -0.05f, 0);
            }
            else　//画面左にいるなら、左下へ
            {
                transform.Translate(-0.025f, -0.075f, 0);
            }
        }

        if (enemy01_HP <= 0) Destroy(this.gameObject); //HPが0以下で消滅
    }

    private void enemy01Bullet_create() //攻撃パターン1
    {
        if(StartPos.y - newPos.y >= 3) //一定の位置になったらスタート ※枠次第で値変える必要有
        {
            if (newPos.x > 0) //画面右にいるなら、左下に向けて攻撃 ※枠次第で値変える必要有
            {
                for (int i = -5; i < 4; i++)
                {
                    //this.transform.position = newPos;
                    var b = Instantiate(enemy01Bullet, newPos, Quaternion.Euler(0, 0, i * 15f));
                }
            }
            else //画面左にいるなら、右下に向けて攻撃
            {
                for (int i = -3; i < 6; i++)
                {
                    //this.transform.position = newPos;
                    var b = Instantiate(enemy01Bullet, newPos, Quaternion.Euler(0, 0, i * 15f));
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "myBullet") //myBulletとタグのついたオブジェクトに当たるとHP減少
        {
            enemy01_HP -= 5;
        }
    }

}
