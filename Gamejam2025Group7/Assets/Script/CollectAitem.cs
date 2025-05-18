using UnityEngine;

public class CollectItem : MonoBehaviour
{
    public enum ItemType { Power, Point, OneUp, Bomb }
    public ItemType itemType;

    public float attractDistance = 2.0f;  // 吸い寄せ開始距離
    public float attractSpeed = 5.0f;     // 吸い寄せスピード

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
                // プレイヤーに向かって移動
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
                    // プレイヤーのパワーを増やす処理
                    break;
                case ItemType.Point:
                    ScoreManager.Instance?.AddScore(100);
                    break;
                case ItemType.OneUp:
                    // 残機を1増やす
                    break;
                case ItemType.Bomb:
                    // ボムを1増やす
                    break;
            }
            Destroy(gameObject);
        }
    }
}