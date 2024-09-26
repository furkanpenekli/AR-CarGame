using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
public class PlayButton : MyButton
{
    [Tooltip("These objects will be hidden before the game starts.")]
    [SerializeField]
    private List<GameObject> _objects = new List<GameObject>();

    public bool allowPlay;

    private ObjectSpawner _objectSpawner;
    private PlaygroundManager _playgroundManager;

    private void Start()
    {
        base.Start();

        _objectSpawner = FindObjectOfType<ObjectSpawner>();
        _playgroundManager = FindObjectOfType<PlaygroundManager>();

        allowPlay = false;
    }

    public override void ButtonClicked()
    {
        Play();
    }

    private void Play()
    {
        allowPlay = _objectSpawner.IsReadyGame();

        if (!allowPlay)
            return;

        _playgroundManager.Enable();

        SetObjectsIsActive(true);
        gameObject.SetActive(false);
    }

    private void SetObjectsIsActive(bool value)
    {
        for (int i = 0; i < _objects.Count; i++)
        {
            _objects[i].SetActive(value);
        }
    }
}
