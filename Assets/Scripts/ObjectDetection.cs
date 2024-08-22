using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ObjectDetection : MonoBehaviour
{
    public GameObject test;
    void Start()
    {
        if (LoaderUtility
            .GetActiveLoader()?
            .GetLoadedSubsystem<XROcclusionSubsystem>() != null)
        {
            // XROcclusionSubsystem was loaded. The platform supports occlusion.
            test.SetActive(false);
        }
    }
}
