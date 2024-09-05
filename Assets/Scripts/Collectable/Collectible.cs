using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField]
    private GameObject _finalEffect;
    private void OnTriggerEnter(Collider other)
    {
        Collectable collectable = other.GetComponentInParent<Collectable>();

        if (collectable != null)
        {
            //Instantiate(_finalEffect, transform.position, transform.rotation);
            PointManager.Instance.IncreasePoint(1);
            Destroy(gameObject);
        }
    }
}
