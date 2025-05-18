using UnityEngine;

public class ZikiDestroying : MonoBehaviour
{
    [SerializeField] private GameObject hitMarker;
    [SerializeField] private int interval;
    [SerializeField] private GameObject myBullet;
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private GameObject bomb;
    private GameObject[] bombs = new GameObject[3];
    private GameObject hitMarkerActivating;
    bool isShiftPushing = false;
    private int kuraiFrameCounter = 0;
    bool kuraiTriggered = false;
    //bool xTriggered = false;
    bool xCoolTime = false;
    bool zikiInvulnerable = false;
    int xCoolTimeCounter = 0;
    float StartExplosion = 0;
    int mainShotCooldown = 0;
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
        if (mainShotCooldown == 0)
        {
            Vector3 newPos = this.transform.position;
            Vector3 offset = new Vector3(0.25f, 0, 0);

            newPos += offset;
            Instantiate(myBullet, newPos, Quaternion.identity);
            newPos -= 2 * offset;
            Instantiate(myBullet, newPos, Quaternion.identity);
            mainShotCooldown = interval;
        }
        else
        {
            mainShotCooldown --;
        }
    }
    void BombStart()
    {
        zikiInvulnerable = true;
        DestroyAllObjectsWithTag("bullet");
        DestroyAllObjectsWithTag("enemy");
        for (int i = 0; i < 3; i++)
        {
            float angle = i * 120f; // 120度ずつ配置
            bombs[i] = Instantiate(bomb, transform.position, Quaternion.identity, transform); // 自機の子オブジェクトに設定
            StartExplosion++;
        }
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullet" && !zikiInvulnerable)
        {
            Destroy(collision.gameObject);
            kuraiTriggered = true;
        }
        if (collision.gameObject.tag == "bullet" && zikiInvulnerable)
        {
            Destroy(collision.gameObject);
        }
    }

    private void Start()
    {
        Vector3 playerPos = transform.position;
        hitMarkerActivating = Instantiate(hitMarker, playerPos, Quaternion.identity);
        hitMarkerActivating.SetActive(false);
        //InvokeRepeating("MyBullet_create", 0, interval);
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

        if (StartExplosion>=1f&&StartExplosion <= 180f)
        {
            StartExplosion++;
        }
        if (StartExplosion > 180f)
        {
            StartExplosion = 0f;
            DestroyAllObjectsWithTag("bullet");
            DestroyAllObjectsWithTag("enemy");
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
        if (xCoolTime&& xCoolTimeCounter<=250)
        {
            xCoolTimeCounter++;
        }
        if (xCoolTimeCounter> 250)
        {
            xCoolTime = false;
            xCoolTimeCounter = 0;
            zikiInvulnerable = false;
        }


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
        if (!Input.GetKey(KeyCode.Z))
        {
            mainShotCooldown = 0;
        }
        if (Input.GetKey(KeyCode.X)&& !xCoolTime)// Xキーでボム
        {
            BombStart();
            xCoolTime = true;
        }
        if (kuraiTriggered)// くらいボム受付イベントが発生していないなら何もしない
        {
            kuraiFrameCounter++;

            if (kuraiFrameCounter >= 8 && !zikiInvulnerable)
            {
                // 8フレーム経過＆Xが押されなかった場合
                Vector3 position = new Vector3(-2.78f, -3.25f, 0f);
                transform.position = position;
                kuraiFrameCounter = 0; // リセット
                kuraiTriggered = false;
                xCoolTime = true;
                zikiInvulnerable = true;
                xCoolTimeCounter += 180;

            }
            else if (kuraiFrameCounter >= 8 && zikiInvulnerable)
            {

                kuraiFrameCounter = 0; // リセット
                kuraiTriggered = false;
            }
        }
        
    }
}