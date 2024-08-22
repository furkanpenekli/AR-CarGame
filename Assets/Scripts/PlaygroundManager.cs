using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit.Samples.ARStarterAssets;
public class PlaygroundManager : MonoBehaviour
{
    [SerializeField]
    private ARInteractorSpawnTrigger _spawnTrigger;
    private CarInput _carInput;

    private ARPlane _plane;
    public ARPlane plane {  get { return _plane; } }

    private bool _isPlaying;
    private void Start()
    {
        _carInput = FindObjectOfType<CarInput>();

        _isPlaying = false;
    }
    public void Enable()
    {
        
        _plane = _spawnTrigger.playgroundPlane;
        _carInput._allowMove = true;
        _isPlaying = true;
    }
}
