using UnityEngine;
using UnityEngine.UI;

public class last_spell : MonoBehaviour
{
    [SerializeField] private float interval = 0.1f;
    [SerializeField] private GameObject[] bossBullet = new GameObject[7];
    [SerializeField] private GameObject cutin;
    [SerializeField] Text spell_text;

    [SerializeField] Slider boss_slider;
    [SerializeField] private float bossmin_HP = 0, boss01_HP = 4000;//<- HP4000ぐらいかと

    private Vector3 newPos, StartPos/*,stanPos*/;

    private float tim = 0,time0 = 0;
    private int i,j = 0,k = 0,cha = 0,c_x = 1,c_y = -1,m = 0;
    private RectTransform text_move;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spell_text = GameObject.Find("lastspell").GetComponent<Text>();
        text_move = GameObject.Find("lastspell").GetComponent<RectTransform>();
        boss_slider = GameObject.Find("boss_HP").GetComponent<Slider>();
        cutin = GameObject.Find("StandingPicture_Boss");
        //stanPos = cutin.transform.position;

        boss_slider.maxValue = boss01_HP;
        boss_slider.minValue = bossmin_HP;
        boss_slider.value = bossmin_HP;
        boss_slider.gameObject.SetActive(true);

        newPos = this.transform.position;
        StartPos = newPos;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tim += Time.deltaTime;
        time0 += Time.deltaTime;

        newPos = this.transform.position;

        if (StartPos.y - newPos.y <= 2 && k == 0) transform.Translate(0, -0.075f, 0); //一定の距離進んだら止まる
        else if (k == 0) k = 1;

        if (time0 > 5 && k == 1)
        {
            if (cha == 3) 
            { 
                cha = 2;
                c_x = -1;
                c_y = 1;
            }
            else if (cha == 2)
            {
                cha = 1;
                c_x = 1;
                c_y = 1;
            }
            else if (cha == 1)
            {
                cha = 0;
                c_x = 1;
                c_y = -1;
            }
            else
            {
                cha = 3;
                c_x = -1;
                c_y = -1;
            }
            time0 = 0;
        }
        else if (time0 > 4 && k == 1)
        {
            transform.Translate(c_x * 0.075f,c_y  * 0.015f, 0);
        }

        if (k == 1 && m == 0)
        {
            m = 1;
            InvokeRepeating("normal01Bullet_create", 0, interval * 10);
        }
        /* スペカ待　出来次第、下のjの値変えてな
        if (boss01_HP <= 3000 && j == 0) //ボスが一定のHPになった瞬間
        {
            CancelInvoke(); //通常攻撃の停止
            spell_text.enabled = true; //スペル名を表示
            spell_text.text = "スペカ"; //textの表記変更
            InvokeRepeating("cutin_move01", 0, interval * 0.025f); //一定秒後テキストを上部に移動
            InvokeRepeating("text_move01", 3, interval * 0.025f); //一定秒後テキストを上部に移動
            j = 1; //一回しかやらないようにするため
        }
        */
        if (boss01_HP <= 2000 && j == 0) //ボスが一定のHPになった瞬間
        {
            CancelInvoke(); //スペカ停止
            InvokeRepeating("normal02Bullet_create", 0, interval * 20);
            j = 1;
        }

        if (boss01_HP <= 1000 && j == 1) //ボスが一定のHPになった瞬間
        {
            CancelInvoke(); //通常攻撃の停止
            spell_text.enabled = true; //スペル名を表示
            spell_text.text = "ラストスペカ"; //textの表記変更
            InvokeRepeating("cutin_move01", 0, interval * 0.025f); //一定秒後テキストを上部に移動
            InvokeRepeating("text_move01", 3, interval * 0.025f); //一定秒後テキストを上部に移動
            j = 2; //一回しかやらないようにするため
        }

        boss_slider.value = Mathf.Clamp(boss01_HP, bossmin_HP, boss01_HP);

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
            boss_slider.gameObject.SetActive(false);
            //bossをデストロイ！
            Destroy(this.gameObject);
        }
    }

    private void normal01Bullet_create() //通常攻撃パターン1
    {
        for (int i = 0; i < 24; i++)
        {
            var a = Instantiate(bossBullet[0], newPos, Quaternion.Euler(0, 0, i * 15f));
            var b = Instantiate(bossBullet[5], newPos, Quaternion.Euler(0, 0, i * 15f));
            var c = Instantiate(bossBullet[6], newPos, Quaternion.Euler(0, 0, i * 15f));
        }
    }

    private void normal02Bullet_create() //通常攻撃パターン2
    {
        for (int i = 0; i < 18; i++)
        {
            var b = Instantiate(bossBullet[3], newPos, Quaternion.Euler(0, 0, i * 20f));
        }
        for (int i = 0; i < 18; i++)
        {
            var c = Instantiate(bossBullet[4], newPos, Quaternion.Euler(0, 0, tim * i * 20f));
        }
    }

    private void cutin_move01() //立ち絵移動1 ※枠次第で値変える必要有
    {
        cutin.transform.position += new Vector3(-0.02f, 0, 0);
        if (cutin.transform.position.x <= 0) //一定の位置に着いた瞬間
        {
            CancelInvoke("cutin_move01");
            InvokeRepeating("cutin_move02", 2.5f, interval * 0.025f);
        }
    }

    private void cutin_move02() //立ち絵移動2 ※枠次第で値変える必要有
    {
        cutin.transform.position += new Vector3(0.1f, 0, 0);
        if (cutin.transform.position.x >= 6) //一定の位置に着いた瞬間
        {
            CancelInvoke("cutin_move02");
        }
    }


    private void text_move01() //テキスト移動 ※枠次第で値変える必要有
    {
        text_move.position += new Vector3(0, 1f, 0);
        if (text_move.position.y >= 320) //一定の位置に着いた瞬間
        {
            CancelInvoke(); //テキストの動きを止める
            /*if (m == 1) スペカ出来次第
            {
                InvokeRepeating("スペカ名", 0, interval); //スペカ発動
                m = 2;
            }
            else */
            InvokeRepeating("boss01Bullet_create", 0, interval); //ラストスペカパターン1発動
        }
    }

    private void boss01Bullet_create() //ラストスペカパターン1
    {
        var a = Instantiate(bossBullet[0], newPos, Quaternion.Euler(0, 0, tim * 120f));
        var b = Instantiate(bossBullet[1], newPos, Quaternion.Euler(0, 0, tim * 120f + 90));
        var c = Instantiate(bossBullet[0], newPos, Quaternion.Euler(0, 0, tim * 120f + 180));
        var d = Instantiate(bossBullet[1], newPos, Quaternion.Euler(0, 0, tim * 120f + 270));
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
        var a = Instantiate(bossBullet[1], newPos, Quaternion.Euler(0, 0, -tim * 120f));
        var b = Instantiate(bossBullet[0], newPos, Quaternion.Euler(0, 0, -tim * 120f + 90));
        var c = Instantiate(bossBullet[1], newPos, Quaternion.Euler(0, 0, -tim * 120f + 180));
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
            var a = Instantiate(bossBullet[2], newPos, Quaternion.Euler(0, 0, i * 30f));
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
