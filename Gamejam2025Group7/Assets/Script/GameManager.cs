using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject player; // プレイヤー参照をここにセット

    private void Awake()
    {
        // シングルトンのセット
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // 二重生成防止
        }
    }
}
