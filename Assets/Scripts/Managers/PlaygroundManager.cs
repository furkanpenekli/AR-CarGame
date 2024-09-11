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

        _winPanel.SetActive(false);
        _isPlaying = false;
    }
    public void Enable()
    {
        _finalLine = FindAnyObjectByType<FinalLine>();

        _plane = _spawnTrigger.playgroundPlane;
        _plane.tag = "Plane";

        Debug.Log("Plane size: " + _plane.size + "\n Plane boundary: " + _plane.boundary);
        _carInput._allowMove = true;
        _isPlaying = true;

        HidePlanes();
        //Turn off finding new planes.
        _planeManager.enabled = false;

        SpawnCollectibles();
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


    private void SpawnCollectibles()
    {
        int currentObjects = 0;
        while (currentObjects < spawnPointCount)
        {
            // Get a random point within the plane's bounds
            Vector3 randomPoint = GetRandomPointOnPlane(_plane);
            // Make sure the y-position is aligned with the plane
            randomPoint.y = _plane.transform.position.y;

            // Get the bounds of the plane (assuming the plane has a collider or you know its dimensions)
            Collider planeCollider = _plane.GetComponent<Collider>();

            // Check if the random point is within the plane's collider bounds
            if (planeCollider.bounds.Contains(randomPoint))
            {
                // Optionally use Physics.CheckSphere to check for overlap with other objects
                if (Physics.CheckSphere(randomPoint, 0.5f) != gameObject.CompareTag("MeshedObject"))
                {
                    // Instantiate the object if the point is valid and no other objects are overlapping
                    Instantiate(_currentCollectible, randomPoint, Quaternion.identity);
                    currentObjects++;
                }
            }
        }

    }
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
    private Vector3 GetRandomPointOnPlane2(ARPlane plane)
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
        int index1 = (int)Random.Range(0, plane.size.y);
        int index2 = (index1 + 1) % (int)plane.size.x;

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

    private Vector3 GetRandomPointOnPlane3(ARPlane plane)
    {
        Vector3 randomPoint = Vector3.zero;

        if (plane.boundary.Length > 0)
        {
            int randomIndex = Random.Range(0, plane.boundary.Length);
            randomPoint = plane.boundary[randomIndex];
        }

        return randomPoint;
    }
}


