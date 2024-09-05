using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
public class InstructionManager : MonoBehaviour
{
    public static InstructionManager Instance { get; private set; }

    [SerializeField]
    private GameObject instructionObject;

    [SerializeField]
    private Text instructionText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ShowInstruction(string text)
    {
        instructionText.text = text;
    }

    public void HideInstruction()
    {
        instructionObject.SetActive(false);
    }
}