using UnityEngine;
using UnityEngine.UI;

public class WeekEnemySystem : MonoBehaviour
{
    public static WeekEnemySystem instance; //?ｿｽL?ｿｽ?ｿｽ^?ｿｽﾉゑｿｽ?ｿｽ驍ｼ?ｿｽ?ｿｽ

    [SerializeField] private float interval01 = 1, interval02 = 1;
    [SerializeField] private GameObject enemy01,enemy02,boss01,Mboss01;
    [SerializeField] Text spell_text;
    [SerializeField] Slider boss_slider;

    private Vector3 newPos;
    private Vector3[] corPos = new [] { new Vector3(-5f,6f,0f), new Vector3(-1f,6f,1f), 
                                        new Vector3(-7f,6f,0f), new Vector3(-5f,6f,1f),
                                        new Vector3(1f,6f,0f), new Vector3(-4f,6f,1f),
                                        new Vector3(-8f,6f,0f), new Vector3(-3f,6f,1f),
                                        new Vector3(-4f,6f,0f), new Vector3(-9f,6f,1f),
                                        new Vector3(-6f,6f,0f), new Vector3(-7f,6f,1f),
                                        new Vector3(2f,6f,0f), new Vector3(-2f,6f,1f),
                                        new Vector3(-9f,6f,0f), new Vector3(-8f,6f,1f),
                                        new Vector3(-3f,6f,0f), new Vector3(1f,6f,1f),
                                        new Vector3(1f,6f,0f), new Vector3(-6f,6f,1f)
                                      }; //?ｿｽG?ｿｽ?ｿｽ?ｿｽG01?ｿｽﾌ搾ｿｽ?ｿｽW ?ｿｽ?ｿｽ?ｿｽg?ｿｽ?ｿｽ?ｿｽ?ｿｽﾅ値?ｿｽﾏゑｿｽ?ｿｽ?ｿｽK?ｿｽv?ｿｽL

    private int i = 0,check,k = 0,m = 0,n = 0;
    public int j = 0;
    //WeekEnemySystem.instance.j = 0; <-?ｿｽ?ｿｽ?ｿｽ{?ｿｽX?ｿｽ?ｿｽ?ｿｽﾅ趣ｿｽ?ｿｽﾉ難ｿｽ?ｿｽ?ｿｽtaze

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

    void FixedUpdate()//?ｿｽG?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽﾌ撰ｿｽ?ｿｽ?ｿｽ
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
        if(j == 5 && m == 0) //?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽﾅ抵ｿｽ?ｿｽ{?ｿｽX?ｿｽH(?ｿｽ?ｿｽ?ｿｽﾝの指?ｿｽ?ｿｽ?ｿｽﾍボ?ｿｽX)
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
        ?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽ{?ｿｽX?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽﾅ趣ｿｽ?ｿｽﾉ抵ｿｽ?ｿｽ{?ｿｽX?ｿｽ?ｿｽ?ｿｽﾌス?ｿｽN?ｿｽ?ｿｽ?ｿｽv?ｿｽg?ｿｽ?ｿｽ?ｿｽ轤ｱ?ｿｽ?ｿｽ?ｿｽ?ｿｽﾌス?ｿｽN?ｿｽ?ｿｽ?ｿｽv?ｿｽg?ｿｽﾌ変撰ｿｽ?ｿｽ?ｿｽﾏゑｿｽ?ｿｽ?ｿｽ(j = 0?ｿｽﾉゑｿｽ?ｿｽ?ｿｽ)
        ?ｿｽ?ｿｽ?ｿｽﾚのボ?ｿｽX?ｿｽﾌ擾ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽﾆ費ｿｽ?ｿｽﾊゑｿｽ?ｿｽ?ｿｽﾏ撰ｿｽ?ｿｽ?ｿｽt?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽ
        ?ｿｽX?ｿｽ^?ｿｽ[?ｿｽg->?ｿｽG?ｿｽ?ｿｽ?ｿｽG->?ｿｽ?ｿｽ?ｿｽ{?ｿｽX->?ｿｽG?ｿｽ?ｿｽ?ｿｽG->?ｿｽ{?ｿｽX->?ｿｽN?ｿｽ?ｿｽ?ｿｽA!
        */
    }

    private void enemy01_create() //?ｿｽG?ｿｽ?ｿｽ?ｿｽG01?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽI
    {
        newPos.x = corPos[i].x;
        var b = Instantiate(enemy01, newPos, Quaternion.identity);
        newPos = this.transform.position;
        i++;
    }

    private void enemy02_create()//?ｿｽG?ｿｽ?ｿｽ?ｿｽG02?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽI
    {
        if (check == 0) //?ｿｽ?ｿｽ?ｿｽE?ｿｽ?ｿｽ5?ｿｽ?ｸつ擾ｿｽ?ｿｽ?ｿｽ ?ｿｽ?ｿｽ?ｿｽg?ｿｽ?ｿｽ?ｿｽ?ｿｽﾅ値?ｿｽﾏゑｿｽ?ｿｽ?ｿｽK?ｿｽv?ｿｽL
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

    private void boss01_create() //?ｿｽ{?ｿｽX?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽi?ｿｽ?ｿｽ?ｿｽj
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
