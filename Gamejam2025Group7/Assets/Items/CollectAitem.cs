using UnityEngine;

public class CollectAitem : MonoBehaviour
{
    public enum ItemType { Power, Point, OneUp, Bomb }
    public ItemType itemType;

    public float attractDistance = 2.0f;  // �z���񂹊J�n����
    public float attractSpeed = 5.0f;     // �z���񂹃X�s�[�h
    public float minY = -6f;              // �A�C�e���̏�����ʒu

    Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        if (player != null)
        {   
            //������}��
            float distance = Vector2.Distance(transform.position, player.position);
            if (distance < attractDistance)
            {
                OnAbsorbed();
            }
            // �v���C���[�Ɍ������Ĉړ�
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)(direction * attractSpeed * Time.deltaTime);
        }
        
        //��苗���ɋ߂Â�����A�C�e�����擾
        if (transform.position.y < minY)
        {
            Destroy(gameObject);
        }
    }
    void OnAbsorbed()
    {
        Debug.Log("�A�C�e���l��");
        switch (itemType)
        {
            case ItemType.Power:
                //�p���[�̑���
                break;
            case ItemType.Point:
                //�X�R�A�̑���
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