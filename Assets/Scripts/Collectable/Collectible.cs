using UnityEngine;

public class Collectible : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Collectable collectable = other.GetComponentInParent<Collectable>();

        if (collectable != null)
        {
            PointManager.Instance.IncreasePoint(1);
            Destroy(gameObject);
        }
    }
}
