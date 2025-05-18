using UnityEngine;

public class CollectItem : MonoBehaviour
{
    public enum ItemType { Power, Point, OneUp, Bomb }
    public ItemType itemType;

    public float attractDistance = 2.0f;  // �z���񂹊J�n����
    public float attractSpeed = 5.0f;     // �z���񂹃X�s�[�h

    Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player").transform;
    }

    void Update()
    {
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);
            if (distance < attractDistance)
            {
                // �v���C���[�Ɍ������Ĉړ�
                Vector2 direction = (player.position - transform.position).normalized;
                transform.position += (Vector3)direction * attractSpeed * Time.deltaTime;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {

            switch (itemType)
            {
                case ItemType.Power:
                    // �v���C���[�̃p���[�𑝂₷����
                    break;
                case ItemType.Point:
                    ScoreManager.Instance?.AddScore(100);
                    break;
                case ItemType.OneUp:
                    // �c�@��1���₷
                    break;
                case ItemType.Bomb:
                    // �{����1���₷
                    break;
            }
            Destroy(gameObject);
        }
    }
}