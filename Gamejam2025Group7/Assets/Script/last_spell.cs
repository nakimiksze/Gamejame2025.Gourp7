using UnityEngine;
using UnityEngine.UI;

public class last_spell : MonoBehaviour
{
    [SerializeField] private float interval = 0.1f;
    [SerializeField] private GameObject[] bossBullet = new GameObject[3];
    [SerializeField] Text spell_text;

    private Vector3 newPos;

    private float tim = 0;
    private int boss01_HP = 1800,i,j = 0;
    private RectTransform text_move;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spell_text = GameObject.Find("lastspell").GetComponent<Text>();
        text_move = GameObject.Find("lastspell").GetComponent<RectTransform>();
        newPos = this.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tim += Time.deltaTime;
        if (boss01_HP <= 1500 && j == 0) //ボスが一定のHPになった瞬間
        {
            InvokeRepeating("normal01Bullet_create", 0, interval * 20);
            j = 1;
        }
        if (boss01_HP <= 1000 && j == 1) //ボスが一定のHPになった瞬間
        {
            CancelInvoke(); //通常攻撃の停止
            spell_text.enabled = true; //スペル名を表示
            InvokeRepeating("text_move01", 2, interval * 0.025f); //一定秒後テキストを上部に移動
            j = 2; //一回しかやらないようにするため
        }
        if (boss01_HP <= 0)//bossを倒した！
        {
            //enemyBulletのタグがついているobjを消す
            GameObject[] objects = GameObject.FindGameObjectsWithTag("enemyBullet");
            foreach (GameObject obj in objects)
            {
                Destroy(obj);
            }
            //ラストスペルのテキスト非表示
            spell_text.enabled = false;
            //bossをデストロイ！
            Destroy(this.gameObject);
        }
    }

    private void normal01Bullet_create() //通常攻撃パターン1
    {
        for (int i = 0; i < 12; i++)
        {
            var b = Instantiate(bossBullet[2], newPos, Quaternion.Euler(0, 0, i * 30f));
        }
        for (int i = 0; i < 12; i++)
        {
            var c = Instantiate(bossBullet[0], newPos, Quaternion.Euler(0, 0, tim * i * 30f));
        }
    }

    private void text_move01() //テキスト移動 ※枠次第で値変える必要有
    {
        text_move.position += new Vector3(0, 1f, 0);
        if (text_move.position.y >= 320) //一定の位置に着いた瞬間
        {
            CancelInvoke(); //テキストの動きを止める
            InvokeRepeating("boss01Bullet_create", 0, interval); //ラストスペカパターン1発動
        }
    }

    private void boss01Bullet_create() //ラストスペカパターン1
    {
        var a = Instantiate(bossBullet[0], newPos, Quaternion.Euler(0, 0, tim * 120f));
        var b = Instantiate(bossBullet[0], newPos, Quaternion.Euler(0, 0, tim * 120f + 90));
        var c = Instantiate(bossBullet[0], newPos, Quaternion.Euler(0, 0, tim * 120f + 180));
        var d = Instantiate(bossBullet[0], newPos, Quaternion.Euler(0, 0, tim * 120f + 270));
        i++;
        if (i == 60) //一定回数行った瞬間
        {
            i = 0;
            CancelInvoke("boss01Bullet_create"); //ラストスペカパターン1停止
            InvokeRepeating("boss02Bullet_create", 0, interval); //ラストスペカパターン2発動
        }
    }

    private void boss02Bullet_create() //ラストスペカパターン2
    {
        var a = Instantiate(bossBullet[0], newPos, Quaternion.Euler(0, 0, -tim * 120f));
        var b = Instantiate(bossBullet[0], newPos, Quaternion.Euler(0, 0, -tim * 120f + 90));
        var c = Instantiate(bossBullet[0], newPos, Quaternion.Euler(0, 0, -tim * 120f + 180));
        var d = Instantiate(bossBullet[0], newPos, Quaternion.Euler(0, 0, -tim * 120f + 270));
        i++;
        if (i == 60)
        {
            i = 0;
            CancelInvoke("boss02Bullet_create");
            Invoke("boss03Bullet_create", 0.5f);
        }
    }

    private void boss03Bullet_create() //ラストスペカパターン3
    {
        for (int i = 3; i > -4; i--)
        {
            var a = Instantiate(bossBullet[1], newPos, Quaternion.Euler(0, 0, i * 30f));
        }
        InvokeRepeating("boss01Bullet_create", 4, interval); //ラストスペカパターン1発動
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "myBullet") //myBulletとタグのついたオブジェクトに当たるとHP減少
        {
            boss01_HP -= 5;
        }
    }

}
