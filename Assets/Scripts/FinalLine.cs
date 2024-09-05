using UnityEngine;

public class FinalLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlaygroundManager.Instance.GameOver();
    }
}
