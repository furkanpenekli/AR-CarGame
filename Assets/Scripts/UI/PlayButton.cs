using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
public class PlayButton : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _objects = new List<GameObject>();

    public bool allowPlay;

    private ObjectSpawner _objectSpawner;
    private PlaygroundManager _playgroundManager;
    [SerializeField]
    private AROcclusionManager occlusionManager;
    private void Start()
    {
        _objectSpawner = FindObjectOfType<ObjectSpawner>();
        _playgroundManager = FindObjectOfType<PlaygroundManager>();

        allowPlay = false;

        SetObjectsIsActive(false);    
    }
    public void Play()
    {
        allowPlay = _objectSpawner.IsCarSpawned();

        if (!allowPlay)
            return;

        _playgroundManager.Enable();

        SetObjectsIsActive(true);
        gameObject.SetActive(false);

        //occlusionManager.requestedEnvironmentDepthMode = UnityEngine.XR.ARSubsystems.EnvironmentDepthMode.Best;
    }

    private void SetObjectsIsActive(bool value)
    {
        for (int i = 0; i < _objects.Count; i++)
        {
            _objects[i].SetActive(value);
        }
    }
}
