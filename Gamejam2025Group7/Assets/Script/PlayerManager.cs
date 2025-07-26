using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject hitMarker;
    [SerializeField] private GameObject shot;
    [SerializeField] private GameObject bomb;
    [SerializeField] private AudioClip deathSe;
    [SerializeField] private AudioClip bombSe;
    [SerializeField] private float playerSpeed = 10f;
    [SerializeField] private float delayDeathTime = 1f;
    [SerializeField] private int hp = 3;
    [SerializeField] private int bombs = 3;


    private AudioSource audioSource;
    private Vector2 startPos;
    private float deathTime;
    private bool isDeath;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        startPos = transform.position;
    }

    void FixedUpdate()
    {
        PlayerMove();

        hitMarker.SetActive(Input.GetKey(KeyCode.LeftShift));

        if (!isDeath && Input.GetKeyDown(KeyCode.X))
        {
            OnBomb();
        }
        else if (isDeath && Input.GetKeyDown(KeyCode.X) && IsPressXBeforeDeath())
        {
            OnBomb();
            isDeath = false;
        }
        else if (isDeath)
        {
            OnDeath();
        }
    }

    void OnDeath()
    {
        hp -= 1;
            if (hp < 0)
            {
                Debug.Log("ゲームオーバー");
            }
            transform.position = startPos;
            audioSource.PlayOneShot(deathSe);
    }
    void PlayerShot()
    {
        Vector2 playerPos = this.transform.position;
        Instantiate(shot, playerPos, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("enemyBullet"))
        {
            deathTime = Time.deltaTime;
            isDeath = true;
        }
    }

    bool IsPressXBeforeDeath()
    {
        return Time.deltaTime - deathTime < delayDeathTime;
    }

    void OnBomb()
    {
        if (bombs > 0)
        {
            audioSource.PlayOneShot(bombSe);
            bombs -= 1;
        }
        else if (isDeath && bombs <= 0)
        {
            OnDeath();
        }
        else Debug.Log("ボムがありません");
    }

    void PlayerMove()
    {
        KeyCode? keyPressed = null;
        Vector2 playerMove = Vector2.zero;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            keyPressed = KeyCode.W;
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            keyPressed = KeyCode.S;
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            keyPressed = KeyCode.A;
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            keyPressed = KeyCode.D;

        if (keyPressed != null)
        {
            switch (keyPressed)
            {
                case KeyCode.W:
                    playerMove = new Vector2(0, playerSpeed);
                    break;
                case KeyCode.S:
                    playerMove = new Vector2(0, -playerSpeed);
                    break;
                case KeyCode.A:
                    playerMove = new Vector2(-playerSpeed, 0);
                    break;
                case KeyCode.D:
                    playerMove = new Vector2(playerSpeed, 0);
                    break;
            }

            transform.Translate(playerMove * Time.fixedDeltaTime);
        }
    }
}
