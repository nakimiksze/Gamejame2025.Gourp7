using UnityEngine;

public class lastspell02Bullet : MonoBehaviour
{
    [SerializeField] private GameObject scatterBullet;

    [SerializeField] private float speed = -0.05f;
    private float tim = 0;
    private int j = 0;

    private Vector3 StartPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartPos = this.transform.position;
    }

    void FixedUpdate()
    {
        tim += Time.deltaTime;
        if (tim < 2) transform.Translate(0, speed, 0);
        else if (j == 0) //y座標を参照して座標ごとに消滅するタイミングを制御 ※枠次第で値変える必要有
        {
            if (StartPos.y - this.transform.position.y > 4.5f)
            {
                Invoke("boss03Bullets_create", 1.5f);
                Destroy(this.gameObject, 1.5f);
            }
            else if (StartPos.y - this.transform.position.y > 3f)
            {
                Invoke("boss03Bullets_create", 1f);
                Destroy(this.gameObject, 1f);
            }
            else if (StartPos.y - this.transform.position.y > 0f)
            {
                Invoke("boss03Bullets_create", 0.5f);
                Destroy(this.gameObject, 0.5f);
            }
            else
            {
                Invoke("boss03Bullets_create", 0f);
                Destroy(this.gameObject, 0f);
            }
            /*
            if (StartPos.y - this.transform.position.y > 4.5f)
            {
                Debug.Log("1.5");
                Invoke("boss03Bullets_create", 0f);
                Destroy(this.gameObject, 0);
            }
            else if (StartPos.y - this.transform.position.y > 3f)
            {
                Debug.Log("1");
                Invoke("boss03Bullets_create", 0.5f);
                Destroy(this.gameObject, 0.5f);
            }
            else if (StartPos.y - this.transform.position.y > 0f)
            {
                Debug.Log("0.5");
                Invoke("boss03Bullets_create", 1f);
                Destroy(this.gameObject, 1);
            }
            else
            {
                Debug.Log("0");
                Invoke("boss03Bullets_create)", 1.5f);
                Destroy(this.gameObject, 1.5f);
            }
            */
            j = 1;
        }
    }

    private void boss03Bullets_create() //攻撃パターン3の拡散弾
    {
        for (int i = 0; i < 12; i++)
        {
            var b = Instantiate(scatterBullet, this.transform.position, Quaternion.Euler(0, 0, i * 30f));
        }
    }
}
