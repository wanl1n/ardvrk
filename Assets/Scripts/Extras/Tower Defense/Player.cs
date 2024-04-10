using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player Instance;

    [SerializeField] private int _hp = 3;
    [SerializeField] private float _speed = 1;
    public int HP { get { return _hp; } set {  _hp = value; } }

    public bool gameOver = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (this.transform.position.z >= -1.21f)
                this.transform.Translate(0, 0, -_speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (this.transform.position.z <= -0.354f)
                this.transform.Translate(0, 0, _speed * Time.deltaTime);
        }

        if (this._hp <= 0)
            this.gameOver = true;
    }
}
