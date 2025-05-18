using UnityEngine;

public class ZikiDestroying : MonoBehaviour
{
    [SerializeField] private GameObject hitMarker;
    [SerializeField] private float interval = 1;
    [SerializeField] private GameObject myBullet;
    [SerializeField] private SpriteRenderer playerSprite;
    private GameObject hitMarkerActivating;
    bool isShiftPushing = false;
    private int kuraiFrameCounter = 0;
    bool kuraiTriggered = false;
    //bool xTriggered = false;
    bool xCoolTime = false;
    bool zikiInvulnerable = false;
    int xCoolTimeCounter = 0;
    void DestroyAllObjectsWithTag(string tag)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag); // 指定タグのオブジェクトを取得

        foreach (GameObject obj in objects) // 配列内のオブジェクトをすべて削除
        {
            Destroy(obj);
        }
    }


    void MyBullet_create()
    {
        Vector3 newPos = this.transform.position;
        Vector3 offset = new Vector3(0.25f, 0, 0);

        newPos += offset;
        Instantiate(myBullet, newPos, Quaternion.identity);
        newPos -= 2 * offset;
        Instantiate(myBullet, newPos, Quaternion.identity);
    }
    bool IsPlayerExisting(string player)
    {
        return GameObject.Find(player) != null;
    }
    void BombStart()
    {
        zikiInvulnerable = true;
        DestroyAllObjectsWithTag("bullet");
        DestroyAllObjectsWithTag("enemy");
    }
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bullet" && !zikiInvulnerable)
        {
            Destroy(collision.gameObject);
            kuraiTriggered = true;
            Debug.Log("くらいボム受付開始！！！");
        }
    }
    private void Start()
    {
        Vector3 playerPos = transform.position;
        hitMarkerActivating = Instantiate(hitMarker, playerPos, Quaternion.identity);
        hitMarkerActivating.SetActive(false);
        InvokeRepeating("MyBullet_create", 0, interval);
        Debug.Log(hitMarker.transform.position);
    }
    void FixedUpdate()
    {
        //低速移動か否かの処理
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isShiftPushing = true;
        }
        else
        {
            isShiftPushing = false;
        }
        float zikiSpeed = isShiftPushing ? 0.11f : 0.22f; // 低速ならなんとか、高速ならなんちゃら
        //移動の処理
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0, zikiSpeed, 0);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-zikiSpeed, 0, 0);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0, -zikiSpeed, 0);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(zikiSpeed, 0, 0);
        }

        //自機が無敵時自機を半透明にする
        SpriteRenderer sr = GetComponent<SpriteRenderer>(); // SpriteRendererを取得
        if (sr != null)
        {
            Color newColor = sr.color;
            newColor.a = zikiInvulnerable ? 0.5f : 1f; // 透明度を変更
            sr.color = newColor;
        }

        //低速マーカーの座標取得・表示
        Vector3 playerPos = transform.position;
        hitMarkerActivating.transform.position = playerPos;
        //低速移動か否かの処理
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isShiftPushing = true;
        }
        else
        {
            isShiftPushing = false;
        }
        //ボムクールタイムか否かの処理
        if (xCoolTime&& xCoolTimeCounter<=128)
        {
            xCoolTimeCounter++;
        }
        if (xCoolTimeCounter> 128)
        {
            xCoolTime = false;
            xCoolTimeCounter = 0;
            zikiInvulnerable = false;
            Debug.Log("ボム使用可能!!");
        }
        if (IsPlayerExisting("player"))
        {

            if (isShiftPushing) // Shiftキーを押している間
            {
                hitMarkerActivating.SetActive(true);// 白い丸を表示
            }
            else
            {
                hitMarkerActivating.SetActive(false); // 通常時は非表示
            }
            if (Input.GetKey(KeyCode.Z)) // Zキーで弾発射
            {
                MyBullet_create();
            }

            if (Input.GetKey(KeyCode.X)&& !xCoolTime)// Xキーでボム
            {
                BombStart();
                Debug.Log("ボム発動");
                xCoolTime = true;
            }
            if (kuraiTriggered)// くらいボム受付イベントが発生していないなら何もしない
            {
                kuraiFrameCounter++;

                if (kuraiFrameCounter >= 4 && !zikiInvulnerable)
                {
                    // 4フレーム経過＆Xが押されなかった場合
                    Destroy(gameObject);
                    Destroy(hitMarkerActivating);
                    kuraiFrameCounter = 0; // リセット
                    kuraiTriggered = false;
                    Debug.Log("くらいボム失敗！！！！！！！！！！！！！！！！");
                }
                else if (kuraiFrameCounter >= 4 && zikiInvulnerable)
                {
                    //BombStart(); //あとでかえる
                    Debug.Log("くらいボム成功したね！！！！！！！！！！！！！！！！");

                    kuraiFrameCounter = 0; // リセット
                    kuraiTriggered = false;
                }
            }
        }
        


    }
}