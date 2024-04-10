using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NavigationSystem : MonoBehaviour
{
    [SerializeField] private GameObject _location;
    [SerializeField] private GameObject _destination;
    [SerializeField] private GameObject _scale;
    private int _locationValue;
    private int _destinationValue; //0 - RL , 1 - GT , 2 - MRR
    [SerializeField] private List<GameObject> _RLPaths = new List<GameObject>();
    [SerializeField] private List<GameObject> _GTPaths = new List<GameObject>();
    [SerializeField] private List<GameObject> _MRRPaths = new List<GameObject>();

    public void UpdateLocations()
    {
        this._locationValue = this._location.GetComponent<TMP_Dropdown>().value;
        this._destinationValue = this._destination.GetComponent<TMP_Dropdown>().value;

        if (GameObject.FindWithTag("SpawnedMap"))
            this.UpdateNavigation();
    }

    public void UpdateNavigation()
    {
        //Reset Paths
        foreach (GameObject o in this._RLPaths)
        {
            o.SetActive(false);
        }

        foreach (GameObject o in this._GTPaths)
        {
            o.SetActive(false);
        }

        foreach (GameObject o in this._MRRPaths)
        {
            o.SetActive(false);
        }

        //Check for new path
        if (this._locationValue == 0)
        {
            if (this._destinationValue == 1)
            {
                this._RLPaths[0].SetActive(true);
            } 
            else if (this._destinationValue == 2)
            {
                this._RLPaths[1].SetActive(true);
            }
        }

        if (this._locationValue == 1)
        {
            if (this._destinationValue == 0)
            {
                this._GTPaths[0].SetActive(true);
            }
            else if (this._destinationValue == 2)
            {
                this._GTPaths[1].SetActive(true);
            }
        }

        if (this._locationValue == 2)
        {
            if (this._destinationValue == 0)
            {
                this._MRRPaths[0].SetActive(true);
            }
            else if (this._destinationValue == 1)
            {
                this._MRRPaths[1].SetActive(true);
            }
        }
    }

    public void UpdateScale()
    {
        float scale = this._scale.GetComponent<Slider>().value;
        this.gameObject.transform.localScale = new Vector3(scale, scale, scale);
    }

    public void SetLocationRL()
    {
        this._location.GetComponent<TMP_Dropdown>().value = 0;
    }

    public void SetLocationGT()
    {
        this._location.GetComponent<TMP_Dropdown>().value = 1;
    }

    public void SetLocationMRR()
    {
        this._location.GetComponent<TMP_Dropdown>().value = 2;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log( this._location.GetComponent<TMP_Dropdown>().value.ToString());
    }
}
