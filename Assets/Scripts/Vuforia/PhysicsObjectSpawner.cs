using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PhysicsObjectSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _spawn = new();
    private GameObject _floor;
    [SerializeField]
    private List<GameObject> _spawnList = new List<GameObject>();
    private int _spawnCount = 0;
    [SerializeField]
    private int _objectCount = 5;
    private bool _isSpawning = false;
    private float _range = 0.2f;

    public void OnImageTargetFound()
    {
        this._isSpawning = true;
    }

    public void OnImageTargetLost()
    {
        this._isSpawning = false;
        foreach (GameObject spawn in this._spawnList)
        {
            Destroy(spawn.gameObject);
        }
        this._spawnList.Clear();
        this._spawnCount = 0;
    }

    private void Update()
    {
        if (this._floor == null)
            this._floor = this.transform.parent.gameObject;

        if (this._isSpawning)
        {
            if (this._spawnCount >= this._objectCount)
            {
                this._isSpawning = false;
            }
            else
            {
                Debug.Log("Spawning Object");
                GameObject spawn = Instantiate(this._spawn, this.transform);
                spawn.transform.position = this.transform.position;
                spawn.transform.Translate(Random.Range(-this._range, this._range), 
                                            Random.Range(0.5f, 1.0f), 
                                            Random.Range(-this._range, this._range));
                spawn.GetComponent<PhysicsObject>().Record(this.gameObject);
                this._spawnCount++;
                this._spawnList.Add(spawn);
            }
        }
    }
}
