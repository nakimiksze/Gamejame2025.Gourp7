using Unity.Mathematics;
using UnityEngine;

public class enemyBase : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private float bulletInterval = 1f;
    [SerializeField] private float speed = 0.0f;
    [SerializeField] private int hp = 0;

    private AudioSource audioSource;
    private GameObject player;
    private Vector2 currentPos;

    void Start()
    {
        player = GameManager.Instance.player;
        currentPos = this.transform.position;
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        OnDeath();
    }


    void ThreeWay_Bullet()
    {
        float angle = 30f;
        for (float i = 0, newAngle = -angle; i < 3; i++, newAngle += angle)
        {
            bulletCreate(angle);
        }
    }

    void TwoWay_Bullet()
    {
        float angle = 15f;
        for (float i = 0, newAngle = -angle; i < 2 || newAngle >= angle + 360f; i++, newAngle += angle * 2f)
        {
            bulletCreate(angle);
        }
    }

    protected void bulletCreate(float angle = 0f)
    {
        Vector2 playerPos = player.transform.position;
        currentPos = this.transform.position;

        Vector2 ToPlayerVec = playerPos - currentPos;
        float ToPlayerAngle = Mathf.Atan2(ToPlayerVec.y, ToPlayerVec.x) * Mathf.Rad2Deg;

        var createdBullet = Instantiate(bullet, currentPos, quaternion.Euler(0f, 0f, ToPlayerAngle + angle));
    }
    
    private void OnDeath()
    {
        if (hp <= 0)
        {
            audioSource.PlayOneShot(deathSound);
            Destroy(this.gameObject);
        }
    }
}
