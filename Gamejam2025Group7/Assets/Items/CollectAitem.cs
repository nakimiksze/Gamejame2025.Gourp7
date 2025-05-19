using UnityEngine;

public class CollectItem : MonoBehaviour
{
    public enum ItemType { Power, Point, OneUp, Bomb }
    public ItemType itemType;

    public float attractDistance = 2.0f;  // 吸い寄せ開始距離
    public float attractSpeed = 5.0f;     // 吸い寄せスピード
    public float minY = -6f;              // アイテムの消える位置

    Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        if (player != null)
        {   
            //距離を図る
            float distance = Vector2.Distance(transform.position, player.position);
            if (distance < attractDistance)
            {
                OnAbsorbed();
            }
            // プレイヤーに向かって移動
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)(direction * attractSpeed * Time.deltaTime);
        }
        
        //一定距離に近づいたらアイテムを取得
        if (transform.position.y < minY)
        {
            Destroy(gameObject);
        }
    }
    void OnAbsorbed()
    {
        Debug.Log("アイテム獲得");
        switch (itemType)
        {
            case ItemType.Power:
                //パワーの増加
                break;
            case ItemType.Point:
                //スコアの増加
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