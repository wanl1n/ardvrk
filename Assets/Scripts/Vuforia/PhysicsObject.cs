using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    private Vector3 _initPosition;
    private GameObject _floor;
    private float _range = 0.01f;

    public void Record(GameObject floor)
    {
        this._initPosition = this.transform.position;
        this._floor = floor;
    }

    private void Update()
    {
        if (this.transform.position.y <= -500.0f)
        {
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            this.transform.position = this._floor.transform.position;
            //this.transform.Translate(Random.Range(-this._range, this._range),
            //                        Random.Range(0.5f, 2.0f),
            //                        Random.Range(-this._range, this._range));
            Debug.Log(transform.position);
        }
    }
}
