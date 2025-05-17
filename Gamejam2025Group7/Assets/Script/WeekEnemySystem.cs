using UnityEngine;
using UnityEngine.UI;

public class WeekEnemySystem : MonoBehaviour
{
    [SerializeField] private float interval01 = 1, interval02 = 1;
    [SerializeField] private GameObject enemy01,enemy02,boss01;
    [SerializeField] Text spell_text;

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

    private int i = 0,check,j = 0,k = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spell_text.enabled = false;
        check = 0;
        newPos = this.transform.position;
        InvokeRepeating("enemy01_create", 0, interval01);
    }

    void FixedUpdate()　//敵召喚の制御
    {
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
            i = 0;
            j = 4;
        }
        if(j == 5) //ここで中ボス？(現在の指定先はボス)
        {
            Invoke("boss01_create", 0);
            j = 7;
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
        newPos.y = 3;
        var b = Instantiate(boss01, newPos, Quaternion.identity);
        newPos = this.transform.position;
    }
}
