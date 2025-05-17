using UnityEngine;

public class lastspell02Bullet : MonoBehaviour
{
    [SerializeField] private GameObject scatterBullet;

    [SerializeField] private float speed = -0.05f;
    private float tim = 0;
    private int j = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Destroy(this.gameObject, 2);
    }

    void FixedUpdate()
    {
        tim += Time.deltaTime;
        if (tim < 2) transform.Translate(0, speed, 0);
        else if (j == 0) //y���W���Q�Ƃ��č��W���Ƃɏ��ł���^�C�~���O�𐧌� ���g����Œl�ς���K�v�L
        {
            if (this.transform.position.y > 2)
            {
                Invoke("boss03Bullets_create", 0f);
                Destroy(this.gameObject, 0);
            }
            else if (this.transform.position.y > 0)
            {
                Invoke("boss03Bullets_create", 0.5f);
                Destroy(this.gameObject, 0.5f);
            }
            else if (this.transform.position.y > -1.5)
            {
                Invoke("boss03Bullets_create", 1f);
                Destroy(this.gameObject, 1);
            }
            else
            {
                Invoke("boss03Bullets_create)", 1.5f);
                Destroy(this.gameObject, 1.5f);
            }
            j = 1;
        }
    }

    private void boss03Bullets_create() //�U���p�^�[��3�̊g�U�e
    {
        for (int i = 0; i < 12; i++)
        {
            var b = Instantiate(scatterBullet, this.transform.position, Quaternion.Euler(0, 0, i * 30f));
        }
    }
}
