using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInstantiator : MonoBehaviour
{
    [SerializeField] private GameObject _mapCopy;

    public void OnFound()
    {
        GameObject map = GameObject.FindGameObjectWithTag("SpawnedMap");
        if (map == null)
        {
            GameObject newCopy = this._mapCopy;
            newCopy.SetActive(true);
            newCopy.tag = "SpawnedMap";
            Instantiate(newCopy.gameObject, this._mapCopy.transform);
            newCopy.transform.position = this.transform.position;
            
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        this._mapCopy.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
