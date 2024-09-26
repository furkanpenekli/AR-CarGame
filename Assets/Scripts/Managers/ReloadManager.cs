using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
public class ReloadManager : MonoBehaviour
{
    public static ReloadManager Instance { get; private set; }

    private ARSession _arSession;

    private List<GameObject> m_objects;
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

    private void Start()
    {
        _arSession = FindObjectOfType<ARSession>();
    }

    /// <summary>
    /// Reload current game scene.
    /// </summary>
    public void ResetGame()
    {
        _arSession.Reset();
    }
}
