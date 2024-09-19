using UnityEngine;
public class ReplayButton : MonoBehaviour
{
    /// <summary>
    /// Reset game variables.
    /// </summary>
    public void ReplayGame()
    {
        PlaygroundManager.Instance.ResetGame();
    }
}
