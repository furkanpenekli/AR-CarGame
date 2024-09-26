using UnityEngine;
public class ReplayButton : MyButton
{
    private void Start()
    {
        base.Start();
    }
    public override void ButtonClicked()
    {
        ReplayGame();
    }

    /// <summary>
    /// Reset game variables.
    /// </summary>
    private void ReplayGame()
    {
        ReloadManager.Instance.ResetGame();
    }
}
