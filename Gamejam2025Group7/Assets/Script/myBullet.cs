using UnityEngine;

public class myBullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(0, 1f, 0);
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
