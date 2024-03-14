using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class BeaconImageObjectHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private GameObject _warningMessage;

    private float yOffset = 0.05f;

    public void OnTrackedImageChanged(ARTrackablesChangedEventArgs<ARTrackedImage> e)
    {
        foreach (var image in e.added)
        {
            GameObject obj = Instantiate(this.prefab, image.transform);
            obj.transform.localPosition = new Vector3(0f, yOffset, 0f);

            if (ARNavigationManager.Instance != null)
                ARNavigationManager.Instance.SetBeaconObject(image.gameObject);

            Debug.Log("Tracked new image: " + image.referenceImage.name);
        }
        foreach (var image in e.updated)
        {
            Debug.Log("Updated image: " + image.referenceImage.name);
            if (image.referenceImage.name == "cobblestone")
            {
                if (image.trackingState != TrackingState.Tracking)
                {
                    this._warningMessage.SetActive(true);
                    ARNavigationManager.Instance.IsBeaconVisible = false;
                }
                else
                {
                    this._warningMessage.SetActive(false);
                    ARNavigationManager.Instance.IsBeaconVisible = true;
                }
            }
        }
        foreach (var image in e.removed)
        {
            Debug.Log("Removed image: " + image.referenceImage.name);
        }
    }
}
