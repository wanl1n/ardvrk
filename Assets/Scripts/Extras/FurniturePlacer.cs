using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class FurniturePlacer : MonoBehaviour
{
    // Managers
    ARPlaneManager planeManager;
    ARRaycastManager raycastManager;

    // Spawning of Furnitures
    [SerializeField] private List<GameObject> prefabs = new();
    [SerializeField] private float posOffset = 0.1f;

    // Changing Object Type
    private int currentType = 1;
    [SerializeField] private List<Button> buttonTypeList = new();

    // Keeps track of all spawned objects.
    private List<GameObject> anchorList = new();
    private GameObject heldObject;
    private Vector3 heldScreenPos;

    // Keeps track of the hits of a raycast.
    private List<ARRaycastHit> hits = new();
    private List<ARRaycast> rays = new();

    // Holding Item
    private bool _holding = false;

    // Start is called before the first frame update
    void Start()
    {
        planeManager = GetComponent<ARPlaneManager>();
        raycastManager  = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Vector3 touchPos = Input.GetTouch(0).position;

                if (raycastManager.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon))
                    this.HandleRaycastHit(hits, touchPos);
            }
        }


        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (!this._holding)
            {
                Vector3 mousePos = Input.mousePosition;
                Ray rayCheck = Camera.main.ScreenPointToRay(mousePos);

                if (raycastManager.Raycast(rayCheck, hits, TrackableType.PlaneWithinPolygon))
                    this.HandleRaycastHit(hits, mousePos);
            }
            else
                this._holding = false;
        }

        this.UpdateHeldItem();
        this.UpdateButtons();
    }

    private void UpdateHeldItem()
    {
        if (this._holding)
        {
            Ray r = Camera.main.ScreenPointToRay(this.heldScreenPos);

            // Update Position
            if (raycastManager.Raycast(r, hits, TrackableType.PlaneWithinPolygon))
            {
                foreach (ARRaycastHit hit in hits)
                {
                    if (hit.trackable is ARPlane plane &&
                        plane.alignment == PlaneAlignment.HorizontalUp)
                    {
                        this.heldObject.transform.position = hit.pose.position;
                    }
                }
            }

            // Update Rotation
            if (Input.mouseScrollDelta.y != 0)
            {
                this.heldObject.transform.Rotate(Vector3.up, Input.mouseScrollDelta.y * 10.0f);
            }
        }
    }

    private void HandleRaycastHit(List<ARRaycastHit> hits, Vector3 touchPos)
    {
        foreach (ARRaycastHit hit in hits)
        {
            if (hit.trackable is ARPlane plane &&
                plane.alignment == PlaneAlignment.HorizontalUp)
            {
                if (!CheckForSpawn(touchPos))
                { 
                    this.heldScreenPos = touchPos;
                    AnchorObject(hit.pose.position);
                    break;
                }
            }
        }
    }

    private void UpdateButtons()
    {
        foreach (var button in this.buttonTypeList)
        {
            button.interactable = true;
        }

        this.buttonTypeList[this.currentType - 1].interactable = false;
    }

    private GameObject GetHitObject(Vector2 position)
    {
        GameObject hitObject = null;
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            hitObject = hit.collider.gameObject;
        }

        return hitObject;
    }

    private bool CheckForSpawn(Vector3 screenPos)
    {
        GameObject hitObject = GetHitObject(screenPos);

        if (hitObject != null)
        {
            ARAnchor handler = hitObject.GetComponentInParent<ARAnchor>();

            if (handler != null)
            {
                Debug.Log($"AR Anchor hit.");
                this.heldObject = hitObject.gameObject;
                this.heldScreenPos = screenPos;
                this._holding = true;
                return true;
            }
            else return false;
        }
        else
            return false;
    }

    public void AnchorObject(Vector3 worldPos)
    {
        GameObject newAnchor = new GameObject("NewAnchor");
        newAnchor.transform.parent = null;
        newAnchor.transform.position = worldPos;
        newAnchor.transform.Translate(0, posOffset, 0);
        newAnchor.AddComponent<ARAnchor>();

        GameObject obj = Instantiate(prefabs[this.currentType - 1], newAnchor.transform);
        obj.transform.localPosition = Vector3.zero;
        anchorList.Add(obj);

        this._holding = true;
        this.heldObject = obj;
    }

    public void SetType(int type)
    {
        this.currentType = type;
        this._holding = false;  
    }
}
