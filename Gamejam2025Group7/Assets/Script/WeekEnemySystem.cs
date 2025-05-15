using UnityEngine;

public class WeekEnemySystem : MonoBehaviour
{
    [SerializeField] private float interval01 = 1, interval02 = 1;
    [SerializeField] private GameObject enemy01,enemy02;

    private Vector3 newPos;
    private float tim = 0;
    private int i = 0,check,j = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        check = Random.Range(0, 1);
        newPos = this.transform.position;
        InvokeRepeating("enemy01_create", 0, interval01);
    }

    void FixedUpdate()
    {
        tim += Time.deltaTime;
        if (tim > 10 && j == 0)
        {
            CancelInvoke("enemy01_create");
            InvokeRepeating("enemy02_create", 0, interval02);
            j = 1;
        }
        if (tim > 30 && j == 1)
        {
            CancelInvoke("enemy02_create");
            //InvokeRepeating("enemy02_create", 0, interval02);
            j = 2;
        }
    }

    private void enemy01_create()
    {
        newPos.x = Random.Range(-10, 10);
        var b = Instantiate(enemy01, newPos, Quaternion.identity);
        newPos = this.transform.position;
    }

    private void enemy02_create()
    {
        if (i >= 5)
        {
            i = 0;
            check = Random.Range(0, 2);
        }
        if (check == 0)
            newPos.x = 1;
        else
            newPos.x = -1;
        
        var b = Instantiate(enemy02, newPos, Quaternion.identity);
        newPos.y += 2;
        var c = Instantiate(enemy02, newPos, Quaternion.identity);
        newPos = this.transform.position;
        i++;
    }

}
