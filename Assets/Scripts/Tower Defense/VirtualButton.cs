using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARTrackedImage))]
public class VirtualButton : MonoBehaviour
{
    private ARTrackedImage trackedImage;
    [SerializeField] private UnityEvent onTrackedImageLimited;

    // Start is called before the first frame update
    void Start()
    {
        trackedImage = this.GetComponent<ARTrackedImage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Player.Instance.gameOver)
        {
            if (trackedImage.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Limited)
            {
                onTrackedImageLimited.Invoke();
            }
        }
    }
}
