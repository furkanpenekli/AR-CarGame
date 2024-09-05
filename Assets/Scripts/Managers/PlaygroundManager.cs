using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit.Samples.ARStarterAssets;
public class PlaygroundManager : MonoBehaviour
{
    public static PlaygroundManager Instance { get; private set; }

    [SerializeField]
    private ARInteractorSpawnTrigger _spawnTrigger;
    [SerializeField]
    private ARPlaneManager _planeManager;

    private CarInput _carInput;

    private ARPlane _plane;
    public ARPlane plane {  get { return _plane; } }

    private bool _isPlaying;

    private FinalLine _finalLine;

    [SerializeField]
    private Collectible _currentCollectible;

    [SerializeField]
    private int _spawnPointCount;
    public int spawnPointCount { get { return _spawnPointCount; } }

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

        _isPlaying = false;
    }
    public void Enable()
    {
        _finalLine = FindAnyObjectByType<FinalLine>();

        _plane = _spawnTrigger.playgroundPlane;
        _carInput._allowMove = true;
        _isPlaying = true;

        HidePlanes();
        //Turn off finding new planes.
        _planeManager.enabled = false;

        SpawnCollectibles();
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
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


    private void SpawnCollectibles()
    {
        Vector3 planeCenter = _plane.center;
        int attempts = 0;
        int maxAttempts = 100;

        for (int i = 0; i < _spawnPointCount; i++)
        {
            bool pointFound = false;

            while (!pointFound && attempts < maxAttempts)
            {
                Vector3 randomPoint = GetRandomPointOnPlane(_plane);
                randomPoint.y = planeCenter.y;

                // If 
                if (!Physics.CheckSphere(randomPoint, 0.05f))
                {
                    Instantiate(_currentCollectible, randomPoint, Quaternion.identity);
                    pointFound = true;
                }

                attempts++;
            }

            if (attempts >= maxAttempts)
            {
                break;
            }
        }
    }
    private Vector3 GetRandomPointOnPlane(ARPlane plane)
    {
        if (plane.boundary.Length < 3)
        {
            return plane.center;
        }

        Vector3 randomPoint = GetRandomPointInsidePolygon(plane);
        return randomPoint;
    }

    private Vector3 GetRandomPointInsidePolygon(ARPlane plane)
    {
        // Find the centroid of the plane's boundaries (2D)
        Vector2 centroid = Vector2.zero;
        foreach (Vector2 point in plane.boundary)
        {
            centroid += point;
        }
        centroid /= plane.boundary.Length;

        // Select two random boundary points
        int index1 = Random.Range(0, plane.boundary.Length);
        int index2 = (index1 + 1) % plane.boundary.Length;

        Vector2 vertex1 = plane.boundary[index1];
        Vector2 vertex2 = plane.boundary[index2];

        // Calculate a random point within the triangle (2D)
        Vector2 randomPoint2D = GetRandomPointInTriangle(centroid, vertex1, vertex2);

        // Convert 2D point to 3D with the plane's center y-coordinate
        Vector3 randomPoint = new Vector3(randomPoint2D.x, plane.center.y, randomPoint2D.y);

        return randomPoint;
    }

    private Vector2 GetRandomPointInTriangle(Vector2 v1, Vector2 v2, Vector2 v3)
    {
        float u = Random.value;
        float v = Random.value;

        if (u + v > 1)
        {
            u = 1 - u;
            v = 1 - v;
        }

        return v1 + u * (v2 - v1) + v * (v3 - v1);
    }

    /*private Vector3 GetRandomPointOnPlane(ARPlane plane)
    {
        Vector3 randomPoint = Vector3.zero;

        if (plane.boundary.Length > 0)
        {
            int randomIndex = Random.Range(0, plane.boundary.Length);
            randomPoint = plane.boundary[randomIndex];
        }

        return randomPoint;
    }*/
}


