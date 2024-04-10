using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class AnchorPlacer : MonoBehaviour
{
    ARAnchorManager anchorManager;

    [SerializeField] private List<GameObject> prefabs = new List<GameObject>();
    [SerializeField] private GameObject prefabToAnchor;
    [SerializeField] private float forwardOffset = 2f;
    [SerializeField] private Slider offsetSlider;

    private int currentType = 1;
    
    private List<GameObject> anchorList = new List<GameObject>();
    //private List<GameObject> anchorList1 = new List<GameObject>();
    //private List<GameObject> anchorList2 = new List<GameObject>();
    //private List<GameObject> anchorList3 = new List<GameObject>();

    [SerializeField] private List<Button> buttonTypeList = new();


    // Start is called before the first frame update
    void Start()
    {
        anchorManager = GetComponent<ARAnchorManager>();
    }

    // Update is called once per frame
    void Update()
    {
        this.forwardOffset = offsetSlider.value;

        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Vector3 spawnPos = Camera.main.ScreenPointToRay(Input.GetTouch(0).position).GetPoint(forwardOffset);

                if (!CheckForSpawn(Input.GetTouch(0).position))
                    AnchorObject(spawnPos);
            }
        }

        //this.UpdateObjectType();
        this.UpdateButtons();
    }

    private void UpdateButtons()
    {
        foreach (var button in this.buttonTypeList)
        {
            button.interactable = true;
        }

        this.buttonTypeList[this.currentType - 1].interactable = false;
    }

    private void UpdateObjectType()
    {
        //if (this.currentType == 1)
        //{
        //    if (this.anchorList1.Count > 0)
        //        foreach (GameObject go in this.anchorList1)
        //        {
        //            if (go != null) 
        //                go.SetActive(true);
        //        }
        //    if (this.anchorList2.Count > 0)
        //        foreach (GameObject go in this.anchorList2)
        //        {
        //            if (go != null)
        //                go.SetActive(false);
        //        }
        //    if (this.anchorList3.Count > 0)
        //        foreach (GameObject go in this.anchorList3)
        //        {
        //            if (go != null)
        //                go.SetActive(false);
        //        }
        //}
        //else if (this.currentType == 2)
        //{
        //    if (this.anchorList1.Count > 0)
        //        foreach (GameObject go in this.anchorList1)
        //        {
        //            if (go != null)
        //                go.SetActive(false);
        //        }
        //    if (this.anchorList2.Count > 0)
        //        foreach (GameObject go in this.anchorList2)
        //        {
        //            if (go != null)
        //                go.SetActive(true);
        //        }
        //    if (this.anchorList3.Count > 0)
        //        foreach (GameObject go in this.anchorList3)
        //        {
        //            if (go != null)
        //                go.SetActive(false);
        //        }
        //}
        //else if (this.currentType == 3)
        //{
        //    if (this.anchorList1.Count > 0)
        //        foreach (GameObject go in this.anchorList1)
        //        {
        //            if (go != null)
        //                go.SetActive(false);
        //        }
        //    if (this.anchorList2.Count > 0)
        //        foreach (GameObject go in this.anchorList2)
        //        {
        //            if (go != null)
        //                go.SetActive(false);
        //        }
        //    if (this.anchorList3.Count > 0)
        //        foreach (GameObject go in this.anchorList3)
        //        {
        //            if (go != null)
        //                go.SetActive(true);
        //        }
        //}
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
            this.anchorList.Remove(hitObject);
            Destroy(hitObject.transform.parent.gameObject);
            return true;
        }
        else
            return false;

    }

    public void AnchorObject(Vector3 worldPos)
    {
        GameObject newAnchor = new GameObject("NewAnchor");
        newAnchor.transform.parent = null;
        newAnchor.transform.position = worldPos;
        newAnchor.AddComponent<ARAnchor>();

        GameObject obj = Instantiate(prefabs[this.currentType-1], newAnchor.transform);
        obj.transform.localPosition = Vector3.zero;
        anchorList.Add(obj);

        //GameObject obj1 = Instantiate(prefabs[0], newAnchor.transform);
        //obj1.transform.localPosition = Vector3.zero;
        //anchorList1.Add(obj1);

        //GameObject obj2 = Instantiate(prefabs[1], newAnchor.transform);
        //obj2.transform.localPosition = Vector3.zero;
        //anchorList2.Add(obj2);

        //GameObject obj3 = Instantiate(prefabs[2], newAnchor.transform);
        //obj3.transform.localPosition = Vector3.zero;
        //anchorList3.Add(obj3);
    }

    public void DeleteAllObjects()
    {
        //foreach (GameObject obj in anchorList1)
        //{
        //    Destroy(obj.gameObject);
        //}
        //foreach (GameObject obj in anchorList2)
        //{
        //    Destroy(obj.gameObject);
        //}
        //foreach (GameObject obj in anchorList3)
        //{
        //    Destroy(obj.gameObject);
        //}

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
