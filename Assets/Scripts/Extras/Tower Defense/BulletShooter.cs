using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class BulletShooter : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float spawnInterval = 0.5f;

    private bool canSpawn = true;

    private float ticks = 0;

    private void Update()
    {
        this.ticks += Time.deltaTime;

        if (this.ticks > this.spawnInterval)
        {
            canSpawn = true;
        }
    }

    public void ShootBullet()
    {
        if (this.canSpawn) 
        this.AnchorBullet();
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

    private GameObject CheckForSpawn(Vector3 screenPos)
    {
        GameObject hitObject = GetHitObject(screenPos);

        if (hitObject != null)
        {
            if (hitObject.gameObject.tag == "Turret")
            {
                return hitObject;
            }
            else return null;
        }
        else
            return null;
    }

    private void AnchorBullet()
    {
        if (this.bullet != null)
        {
            GameObject bulletSpawn = Instantiate(this.bullet, this.transform);
            bulletSpawn.transform.localScale = new Vector3(1.1f, 1.1f, 2.6f);

            this.ticks = 0;
            this.canSpawn = false;
        }
    }
}
