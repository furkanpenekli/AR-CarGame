using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class UtilityChecker : MonoBehaviour
{
    void Start()
    {
        var isMeshingSupported = FindObjectOfType<ARMeshManager>().subsystem;
        if (isMeshingSupported == null)
        {
            Application.Quit();
        }
    }
}
