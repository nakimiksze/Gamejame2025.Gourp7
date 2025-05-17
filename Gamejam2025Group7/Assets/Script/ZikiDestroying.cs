using UnityEngine;

public class ZikiDestroying : MonoBehaviour
{
    [SerializeField] private GameObject hitMarker;
    private GameObject hitMarkerActivating;
    bool isShiftPushing = false;
    private int kuraiFrameCounter = 0;
    bool kuraiTriggered = false;
    /*void BombStart()
    {
        Destroy()
    }
    */
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            Destroy(collision.gameObject);
            kuraiTriggered = true;
        }
    }
    private void Start()
    {
        Vector3 playerPos = transform.position;
        hitMarkerActivating = Instantiate(hitMarker, playerPos, Quaternion.identity);
        hitMarkerActivating.SetActive(false);
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
        //低速マーカーの表示
        Vector3 playerPos = transform.position;
        hitMarkerActivating.transform.position = playerPos;
        Debug.Log(hitMarkerActivating.transform.position);

        if (isShiftPushing) // Shiftキーを押している間
        {

            hitMarkerActivating.SetActive(true);// 白い丸を表示
            Debug.Log("低速マーカー");
        }
        else
        {
            hitMarkerActivating.SetActive(false); // 通常時は非表示
            Debug.Log("低速マーカーOFF");
        }
        if (!kuraiTriggered) return; // Aイベントが発生していないなら何もしない

        kuraiFrameCounter++;

        if (kuraiFrameCounter >= 4 && !(Input.GetKey(KeyCode.X)))
        {
            // 4フレーム経過＆Xが押されなかった場合
            Destroy(gameObject);
            Destroy(hitMarkerActivating);
            kuraiFrameCounter = 0; // リセット
            kuraiTriggered = false;
        }
        else if (kuraiFrameCounter >= 4 && (Input.GetKey(KeyCode.X)))
        {
            //BombStart(); //あとでかえる
            Destroy(gameObject);
            Destroy(hitMarkerActivating);
            kuraiFrameCounter = 0; // リセット
            kuraiTriggered = false;
        }
        


    }
}