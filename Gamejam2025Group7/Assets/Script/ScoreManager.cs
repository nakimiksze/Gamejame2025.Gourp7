using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    //インスタンスの作成
    public static ScoreManager Instance { get; private set; }

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

    public void AddScore(int amount)
    {
        Debug.Log(amount + "点を追加");
        score += amount;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = score.ToString("N0");
    }
}
