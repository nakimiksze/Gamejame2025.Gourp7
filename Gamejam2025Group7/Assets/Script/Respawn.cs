using Unity.VisualScripting;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] private GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame

    void Update()
    {
        if (!GameObject.FindGameObjectWithTag("Player"))
        {
            Vector3 position = new Vector3(0f, -2.3f, 0f);

            GameObject newPlayer = Instantiate(player, position, Quaternion.identity);
            
            
            //newPlayer.AddComponent<ZikiDestroying>();
            
        }
    }
}
