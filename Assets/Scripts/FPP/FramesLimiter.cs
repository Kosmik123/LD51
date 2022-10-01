using UnityEngine;

public class FramesLimiter : MonoBehaviour
{
    [SerializeField]
    private int maxFrames = 60;
        
    private void OnValidate()
    {
        Application.targetFrameRate = maxFrames;
    }
}
