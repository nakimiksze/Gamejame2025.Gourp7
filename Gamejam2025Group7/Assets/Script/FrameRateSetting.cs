using UnityEngine;

public class FrameRateSetting : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

}
