using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PointCloudVisualizerManager : MonoBehaviour
{
    private bool trackablesActive = true;
    public void Toggle()
    {
        GetComponent<ARPointCloudManager>().SetTrackablesActive(!trackablesActive);
        trackablesActive = !trackablesActive;
    }
}
