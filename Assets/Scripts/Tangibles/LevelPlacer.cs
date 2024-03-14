using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class LevelPlacer : MonoBehaviour
{
    ARRaycastManager raycastManager;

    [SerializeField] private GameObject prefabToAnchor;
    [SerializeField] private float posOffset = 0.1f;

    private List<ARRaycastHit> hits = new();

    private bool _placedLevel = false;

    // Start is called before the first frame update
    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);

            if (raycastManager.Raycast(ray, hits, TrackableType.PlaneWithinPolygon))
            {
                this.HandleRaycastHit(hits);
            }
        }
    }

    private void HandleRaycastHit(List<ARRaycastHit> hits)
    {
        // Determine if it is a plane
        foreach (ARRaycastHit hit in hits)
        {
            if (!this._placedLevel &&
                hit.trackable is ARPlane plane &&
                plane.alignment == PlaneAlignment.HorizontalUp)
            {
                AnchorObject(hit.pose.position);
                this._placedLevel = true;
            }
        }
    }
    public void AnchorObject(Vector3 worldPos)
    {
        GameObject newAnchor = new GameObject("Level");
        newAnchor.transform.parent = null;
        newAnchor.transform.position = worldPos;
        newAnchor.transform.Translate(0, posOffset, 0);
        newAnchor.AddComponent<ARAnchor>();

        GameObject obj = Instantiate(prefabToAnchor, newAnchor.transform);
        obj.transform.localPosition = Vector3.zero;
    }
}
