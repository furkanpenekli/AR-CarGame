using UnityEngine;
using UnityEngine.UI;

public class PointManager : MonoBehaviour
{
    public static PointManager Instance { get; private set; }

    private PlaygroundManager _playgroundManager;

    private int _maxPoint;
    public int MaxPoint { get => _maxPoint; }

    private int _currentPoint;

    [SerializeField]
    private Text _text;

    private void Awake()
    {
        // Singleton pattern implementation
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keeps the instance between scenes
        }
        else
        {
            Destroy(gameObject); // Ensures there is only one instance
        }
    }

    private void Start()
    {
        _playgroundManager = GetComponent<PlaygroundManager>();

        _currentPoint = 0;
        _maxPoint = _playgroundManager.spawnPointCount;
        UpdateText();
    }

    /// <summary>
    /// Increases the current point by a specified value and updates the UI text.
    /// </summary>
    /// <param name="value">The value to increase the current point by.</param>
    public void IncreasePoint(int value)
    {
        _currentPoint += value;
        UpdateText();
    }

    /// <summary>
    /// Updates the UI text to display the current point out of the maximum points in the format "currentPoint/maxPoint".
    /// </summary>
    public void UpdateText()
    {
        _text.text = _currentPoint.ToString() + "/" + _maxPoint.ToString();
    }

}
