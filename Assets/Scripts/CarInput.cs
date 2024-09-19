using UnityEngine;
public class CarInput : MonoBehaviour
{
    public float horizontalInput;
    public float verticalInput;

    [SerializeField]
    private Joystick _joystick;

    public bool _allowMove;

    private void Start()
    {
        _allowMove = false;
    }

    private void Update()
    {
        if (!_allowMove)
            return;

        verticalInput = _joystick.Vertical;
        horizontalInput = _joystick.Horizontal;
    }

    public void ForwardButtonDown()
    {
        verticalInput = 1f;
    }
    public void ForwardButtonUp()
    {
        verticalInput = 0f;
    }
    public void BackwardButtonDown()
    {
        verticalInput = -1f;
    }
    public void BackwardButtonUp()
    {
        verticalInput = 0f;
    }
    public void LeftButtonDown()
    {
        horizontalInput = -1f;
    }
    public void LeftButtonUp()
    {
        horizontalInput = 0f;
    }
    public void RightButtonDown()
    {
        horizontalInput = 1f;
    }
    public void RightButtonUp()
    {
        horizontalInput = 0f;
    }
}
