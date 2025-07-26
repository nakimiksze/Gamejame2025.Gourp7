using System.Drawing;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            Debug.Log(gameObject.name);
            ScoreManager.Instance?.AddScore(100);
            if (gameObject.name == "Point(Clone)")
                PointManager.Instance?.AddPoint(100);
            Destroy(gameObject);
        }
    }
}
