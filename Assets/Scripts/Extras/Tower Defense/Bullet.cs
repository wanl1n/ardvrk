using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.activeSelf)
            this.transform.Translate(0, this.speed * Time.deltaTime, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "Spawner")
        {
            Destroy(this.gameObject);
        }
    }
}
