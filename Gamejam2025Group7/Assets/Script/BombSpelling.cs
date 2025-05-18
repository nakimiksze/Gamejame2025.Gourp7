using UnityEngine;

public class BombSpelling : MonoBehaviour
{
    private float rotationSpeed = 100f; // 初期回転速度
    private float angleOffset = 0f;
    private float StartExplosion = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    private void FixedUpdate()
    {
        rotationSpeed += 1000 * Time.deltaTime; // 回転速度を徐々に増加
        angleOffset += rotationSpeed * Time.deltaTime;  // 時間経過で角度を進める

        for (int i = 0; i < 3; i++)
        {
            //angle += angleOffset; // 120度ずつずらす
            float x = 3.0f * Mathf.Cos(angleOffset * Mathf.Deg2Rad);
            float y = 3.0f * Mathf.Sin(angleOffset * Mathf.Deg2Rad);
            transform.localPosition = new Vector3(x, y, 0);
        }
        if (StartExplosion <= 180f)
        {
            StartExplosion++;
        }
        if (StartExplosion > 180f)
        {
            StartExplosion = 0f;
            Destroy(gameObject);
        }

    }

}
