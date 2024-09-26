using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARCore;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit.Samples.ARStarterAssets;
public class PlaygroundManager : MonoBehaviour
{
    public static PlaygroundManager Instance { get; private set; }

    private ARInteractorSpawnTrigger _spawnTrigger;
    private ARPlaneManager _planeManager;
    private ARSession _arSession;

    private CarInput _carInput;

    private ARPlane _plane;
    public ARPlane plane {  get { return _plane; } }

    private bool _isPlaying;

    private FinalLine _finalLine;

    [SerializeField]
    private Collectible _currentCollectible;

    [SerializeField]
    private Material _planeMaterial;

    [SerializeField]
    private int _spawnPointCount;
    public int spawnPointCount { get { return _spawnPointCount; } }

    [SerializeField]
    private GameObject _winPanel;
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
        _carInput = FindObjectOfType<CarInput>();
        _spawnTrigger = FindObjectOfType<ARInteractorSpawnTrigger>();
        _planeManager = FindObjectOfType<ARPlaneManager>();
        _arSession = FindObjectOfType<ARSession>();

        _winPanel.SetActive(false);
        _isPlaying = false;
    }
    public void Enable()
    {
        _finalLine = FindAnyObjectByType<FinalLine>();

        _plane = _spawnTrigger.playgroundPlane;
        _plane.tag = "Plane";

        var meshRenderer = _plane.GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            meshRenderer.material = _planeMaterial;
        }

        Debug.Log("Plane size: " + _plane.size + "\n Plane boundary: " + _plane.boundary);
        _carInput._allowMove = true;
        _isPlaying = true;

        HidePlanes();
        //Turn off finding new planes.
        _planeManager.enabled = false;

        SpawnCollectibles();

        //_plane.transform.localScale = _plane.transform.localScale * 10;
    }

    public void GameOver()
    {
        if (PointManager.Instance.IsPointFilledToMax())
        {
            _winPanel.SetActive(true);
            PauseGame();
            Debug.Log("Game Over!");
        }
    }

    /// <summary>
    /// Pauses game use with time scale.
    /// </summary>
    private void PauseGame()
    {
        Time.timeScale = 0.0f;
    }

    /// <summary>
    /// Hide all of planes without playground plane.
    /// </summary>
    private void HidePlanes()
    {
        foreach (var plane in _planeManager.trackables)
        {
            if(plane != _plane)
                plane.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void SpawnCollectibles()
    {
        int currentObjects = 0;

        // Get the bounds of the plane (assuming the plane has a collider or you know its dimensions)
        Collider planeCollider = _plane.GetComponent<Collider>();

        while (currentObjects < spawnPointCount)
        {
            // Get a random point within the plane's bounds
            Vector3 randomPoint = GetRandomPointOnPlane(_plane);

            // Make sure the y-position is aligned with the plane
            randomPoint.y = _plane.transform.position.y;

            // Check if the random point is within the plane's collider bounds
            if (planeCollider.bounds.Contains(randomPoint))
            {
                Collider[] hitColliders = Physics.OverlapSphere(randomPoint, 10f);

                foreach (var hitCollider in hitColliders)
                {
                    if (!hitCollider.CompareTag("MeshedObject")
                        && !hitCollider.CompareTag("Car")
                        && !hitCollider.CompareTag("PrefabObject"))
                    {
                        if (hitCollider.CompareTag("Plane"))
                        {
                            Debug.Log("Spawned " + _currentCollectible.name + " " + currentObjects+1 + " times.");
                            Instantiate(_currentCollectible, randomPoint, Quaternion.identity);
                            currentObjects++;
                            break;
                        }
                    }
                }
            }
        }
    }


    /// <summary>
    /// Get random point on desired ARPlane.
    /// </summary>
    /// <param name="plane"></param>
    /// <returns></returns>
    Vector3 GetRandomPointOnPlane(ARPlane plane)
    {
        // ARPlane'in sınır noktalarını al
        var boundaryPoints = plane.boundary;

        // Minimum ve maksimum x, y koordinatlarını bul
        float minX = float.MaxValue, maxX = float.MinValue;
        float minY = float.MaxValue, maxY = float.MinValue;

        foreach (var point in boundaryPoints)
        {
            if (point.x < minX)
                minX = point.x;
            if (point.x > maxX)
                maxX = point.x;
            if (point.y < minY)
                minY = point.y;
            if (point.y > maxY)
                maxY = point.y;
        }

        // Rastgele bir x ve y koordinatı seç
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        return new Vector3(randomX, plane.transform.position.y, randomY);
    }
}


