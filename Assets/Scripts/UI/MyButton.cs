using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MyButton : MonoBehaviour
{
    public void Start()
    {
        Debug.Log("Button event added.");
        GetComponent<Button>().onClick.AddListener(ButtonClicked);
    }

    /// <summary>
    /// This method calls when button clicked
    /// </summary>
    public virtual void ButtonClicked()
    {

    }
}
