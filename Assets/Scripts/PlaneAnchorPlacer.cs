using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaneAnchorPlacer : MonoBehaviour
{
    ARRaycastManager raycastManager;

    [SerializeField] private GameObject prefabToAnchor;
    [SerializeField] private List<GameObject> prefabs = new();
    [SerializeField] private float posOffset = 0.1f;

    private int currentType = 1;

    private List<GameObject> anchorList = new();
    private List<ARRaycastHit> hits = new();

    [SerializeField] private List<Button> buttonTypeList = new();

    // Start is called before the first frame update
    void Start()
    {
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

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);

            if (raycastManager.Raycast(ray, hits, TrackableType.PlaneWithinPolygon))
            {
                this.HandleRaycastHit(hits, mousePos);
            }
        }
        this.UpdateButtons();
    }

    private void HandleRaycastHit(List<ARRaycastHit> hits, Vector3 touchPos)
    {
        // Determine if it is a plane
        foreach (ARRaycastHit hit in hits)
        {
            if (hit.trackable is ARPlane plane &&
                plane.alignment == PlaneAlignment.HorizontalUp)
            {
                if (!CheckForSpawn(touchPos))
                    AnchorObject(hit.pose.position);
                Debug.Log($"Hit a plane with alignment {plane.alignment}");
            }
            else
                Debug.Log($"Raycast hit a {hit.hitType}");
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

    private bool CheckForSpawn(Vector3 worldPos)
    {
        GameObject hitObject = GetHitObject(worldPos);

        if (hitObject != null)
        {
            ARAnchor handler = hitObject.GetComponentInParent<ARAnchor>();

            if (handler != null)
            {
                Debug.Log($"AR Anchor hit.");
                this.anchorList.Remove(hitObject);
                Destroy(hitObject.transform.parent.gameObject);
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
    }

    public void DeleteAllObjects()
    {
        this.anchorList.RemoveAll(x => x == null);
        foreach (GameObject obj in anchorList)
        {
            if (obj != null)
                Destroy(obj.transform.parent.gameObject);
        }
        this.anchorList.Clear();
    }

    public void SetType(int type)
    {
        this.currentType = type;
    }
}
