using UnityEngine;

public class ExitButton : MyButton
{
    private void Start()
    {
        base.Start();
    }
    public override void ButtonClicked()
    {
        Exit();
    }

    private void Exit()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
