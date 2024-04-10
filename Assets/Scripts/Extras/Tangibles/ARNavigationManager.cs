using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARNavigationManager : MonoBehaviour
{
    public static ARNavigationManager Instance;

    [SerializeField] private GameObject levelPrefab;
    [SerializeField] private Vector3 levelOffset = new Vector3(0, 0.05f, 0);

    private ARRaycastManager raycastManager;
    private List<ARRaycastHit> hits;

    public bool HasLevelPlaced {  get; private set; }
    public bool IsBeaconVisible {  get; set; }

    private GameObject beaconObject;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        hits = new List<ARRaycastHit>();
        HasLevelPlaced = false;
        IsBeaconVisible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!HasLevelPlaced)
            CheckPlanePlacement();
    }

    public void CheckPlanePlacement()
    {
        if (Input.GetMouseButton(0))
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (raycastManager.Raycast(r, hits,UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
            {
                foreach (ARRaycastHit hit in hits)
                {
                    if (hit.trackable is ARPlane plane && plane.alignment == UnityEngine.XR.ARSubsystems.PlaneAlignment.HorizontalUp)
                    {
                        AnchorLevel(hit.pose.position);
                        break;
                    }
                }
            }
        }
    }

    private void AnchorLevel(Vector3 worldPos)
    {
        GameObject newAnchor = new GameObject("Anchor");
        newAnchor.transform.parent = null;
        newAnchor.transform.position = worldPos;
        newAnchor.AddComponent<ARAnchor>();

        GameObject content = Instantiate(levelPrefab, newAnchor.transform);
        content.transform.localPosition = levelOffset;

        HasLevelPlaced = true;
    }

    public Vector3 GetBeaconPosition()
    {
        if (beaconObject != null)
            return beaconObject.transform.position;
        else
            return Vector3.zero;
    }

    public void SetBeaconObject(GameObject obj)
    {
        this.beaconObject = obj;
        IsBeaconVisible = true;
    }
}
