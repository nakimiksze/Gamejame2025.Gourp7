using UnityEngine;

public class SC2Bullet : MonoBehaviour
{
    [SerializeField] private GameObject scatterBullet;

    [SerializeField] private float speed = -0.05f;
    float tim = 0;
    private Vector3 newPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tim += Time.deltaTime;
        if (tim < 1) transform.Translate(0, speed, 0);
        else
        {
            Invoke("boss11Bullets_create", 0f);
            Destroy(this.gameObject, 0f);
        }
    }
    void boss11Bullets_create()
    {
        for (float i = 0; i <= 2; i += 0.5f)
        {
            newPos.x = this.transform.position.x - i;
            newPos.y = this.transform.position.y + i * 1.73f;
            var b = Instantiate(scatterBullet, newPos, Quaternion.Euler(0, 0, 30));
            newPos = this.transform.position;
        }
    }
}
