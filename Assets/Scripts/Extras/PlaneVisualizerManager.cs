using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlaneVisualizerManager : MonoBehaviour
{
    [SerializeField] private XROrigin origin;

    private bool isVisible = true;

    private void Start()
    {
        origin = GetComponent<XROrigin>();
    }
    private void Update()
    {
        foreach(var visualizer in origin.GetComponentsInChildren<ARPlaneMeshVisualizer>())
        {
            visualizer.enabled = isVisible;
        }
    }

    public void Toggle()
    {
        if (origin == null) return;

        isVisible = !isVisible;
        foreach (var visualizer in origin.GetComponentsInChildren<ARPlaneMeshVisualizer>())
        {
            visualizer.enabled = isVisible;
        }
    }
}
