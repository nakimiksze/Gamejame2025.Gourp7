using UnityEngine;
using UnityEngine.UI;

public class WeekEnemySystem : MonoBehaviour
{
    public static WeekEnemySystem instance; //広域型にするぞい

    [SerializeField] private float interval01 = 1, interval02 = 1;
    [SerializeField] private GameObject enemy01,enemy02,boss01,Mboss01;
    [SerializeField] Text spell_text;
    [SerializeField] Slider boss_slider;

    private Vector3 newPos;
    private Vector3[] corPos = new [] { new Vector3(5f,6f,0f), new Vector3(-1f,6f,1f), 
                                        new Vector3(7f,6f,0f), new Vector3(-5f,6f,1f),
                                        new Vector3(1f,6f,0f), new Vector3(-4f,6f,1f),
                                        new Vector3(8f,6f,0f), new Vector3(-3f,6f,1f),
                                        new Vector3(4f,6f,0f), new Vector3(-9f,6f,1f),
                                        new Vector3(6f,6f,0f), new Vector3(-7f,6f,1f),
                                        new Vector3(2f,6f,0f), new Vector3(-2f,6f,1f),
                                        new Vector3(9f,6f,0f), new Vector3(-8f,6f,1f),
                                        new Vector3(3f,6f,0f), new Vector3(10f,6f,1f),
                                        new Vector3(10f,6f,0f), new Vector3(-6f,6f,1f)
                                      }; //雑魚敵01の座標 ※枠次第で値変える必要有

    private int i = 0,check,k = 0,m = 0,n = 0;
    public int j = 0;
    //WeekEnemySystem.instance.j = 0; <-中ボス消滅時に入れtaze

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spell_text.enabled = false;
        boss_slider.gameObject.SetActive(false);

        check = 0;
        newPos = this.transform.position;
        InvokeRepeating("enemy01_create", 0, interval01);
    }

    void FixedUpdate()　//敵召喚の制御
    {
        if (j == 0 && m == 1 && n == 0)
        {
            InvokeRepeating("enemy01_create", 0, interval01);
            n = 1;
        }
        if (i >= 10 && j == 0)
        {
            CancelInvoke("enemy01_create");
            InvokeRepeating("enemy02_create", 0, interval02);
            j = 1;
        }
        if (j == 2)
        {
            InvokeRepeating("enemy01_create", 0, interval01);
            j = 3;
        }
        if (i >= 20 && j == 3)
        {
            CancelInvoke("enemy01_create");
            InvokeRepeating("enemy02_create", 0, interval02);
            j = 4;
        }
        if(j == 5 && m == 0) //ここで中ボス？(現在の指定先はボス)
        {
            i = 0;
            m = 1;
            Invoke("boss01_create", 0);
            j = 7;
        }
        if(j == 5 && m == 1)
        {
            m = 2;
            Invoke("boss01_create", 0);
        }
        /*
        ↑中ボスが消滅時に中ボス側のスクリプトからこちらのスクリプトの変数を変える(j = 0にする)
        二回目のボスの召喚時だと判別する変数を付け加える
        スタート->雑魚敵->中ボス->雑魚敵->ボス->クリア!
        */
    }

    private void enemy01_create() //雑魚敵01召喚！
    {
        newPos.x = corPos[i].x;
        var b = Instantiate(enemy01, newPos, Quaternion.identity);
        newPos = this.transform.position;
        i++;
    }

    private void enemy02_create()　//雑魚敵02召喚！
    {
        if (check == 0) //左右で5回ずつ処理 ※枠次第で値変える必要有
            newPos.x = 1;
        else
            newPos.x = -1;
        
        var b = Instantiate(enemy02, newPos, Quaternion.identity);
        newPos.y += 2;
        var c = Instantiate(enemy02, newPos, Quaternion.identity);
        newPos = this.transform.position;

        k++;
        if (k >= 10)
        {
            k = 0;
            check = 0;
            j = 2;
            if (i == 20) j = 5; 
            CancelInvoke("enemy02_create");
        }
        else if(k >= 5)
        {
            check = 1;
        }
    }

    private void boss01_create() //ボス召喚（仮）
    {
        //newPos.y = 3;
        boss_slider.gameObject.SetActive(true);

        if (m == 1) 
        {
            var b = Instantiate(Mboss01, newPos, Quaternion.identity); 
        }
        else
        {
            var b = Instantiate(boss01, newPos, Quaternion.identity);
        }
        newPos = this.transform.position;
    }
}
