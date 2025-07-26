using TMPro;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    public static PointManager Instance { get; private set; }

    public int score = 0;
    public TMP_Text scoreText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateScore();
    }

    public void AddPoint(int amount)
    {
        Debug.Log(amount + "�_��ǉ�");
        score += amount;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = score.ToString("N0");
    }
}
