using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TrackedImageObjectHandler : MonoBehaviour
{
    private List<GameObject> _prefabs;
    [SerializeField]
    private GameObject prefab;

    private List<GameObject> _spawnedObjects = new();
    //private List<GameObject> _spawnedObjects1 = new();
    //private List<GameObject> _spawnedObjects2 = new();
    private List<string> _trackedObjects = new();

    //private int _variant = 1;
    private int _indexToSpawn = 0;

    private float yOffset = 0.05f;

    public void OnTrackedImageChanged(ARTrackablesChangedEventArgs<ARTrackedImage> e) 
    {
        foreach (var image in e.added)
        {
            GameObject obj = Instantiate(this.prefab, image.transform);
            //GameObject obj = Instantiate(this._prefabs[this._indexToSpawn], image.transform);
            obj.transform.localPosition = new Vector3(0f, yOffset, 0f);
            this._spawnedObjects.Add(obj);

            //this._indexToSpawn++;
            //this._spawnedObjects2.Add(Instantiate(this._prefabs[this._indexToSpawn], image.transform));
            //this._spawnedObjects2[this._spawnedObjects1.Count - 1].transform.position = image.transform.position;
            //this._indexToSpawn++;

            if (ARNavigationManager.Instance != null)
                ARNavigationManager.Instance.SetBeaconObject(image.gameObject);

            this._trackedObjects.Add(image.referenceImage.name);

            Debug.Log("Tracked new image: " + image.referenceImage.name);
        }
        foreach (var image in e.updated)
        {
            Debug.Log("Updated image: " + image.referenceImage.name);
            
            //AgentManager.isTrackedImageFound = image.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking;
        }
        foreach (var image in e.removed)
        {
            Debug.Log("Removed image: " + image.referenceImage.name);
        }
    }

    public void ToggleVariant(int num)
    {
        //this._variant = num;
    }

    private void Update()
    {
        //if (this._variant == 1)
        //{
        //    if (this._spawnedObjects2.Count > 0)
        //        foreach (GameObject go in this._spawnedObjects2)
        //            go.SetActive(false);
        //    if (this._spawnedObjects1.Count > 0)
        //        foreach (GameObject go in this._spawnedObjects1)
        //            go.SetActive(true);
            
        //}
        //else if (this._variant == 2)
        //{
        //    foreach (GameObject go in this._spawnedObjects1)
        //        go.SetActive(false);
        //    foreach (GameObject go in this._spawnedObjects2)
        //        go.SetActive(true);
        //}
    }
}
