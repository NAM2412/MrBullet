using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float rotateSpeed = 100f, bulletSpeed = 100f, timeToDestroyBullet = 2f;

    private Transform handPos;
    private Transform fireStartPos, fireEndPos;

    private LineRenderer lineRenderer; // the laser in game

    private void Awake()
    {
        handPos = GameObject.Find("LeftArm").transform;
        fireStartPos = GameObject.Find("FirePos1").transform;
        fireEndPos = GameObject.Find("FirePos2").transform;
        lineRenderer = GameObject.Find("Gun").GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Aim();
        }
        if (Input.GetMouseButtonUp(0))
        {
            Shoot();
        }

    }

    private void Aim()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - handPos.position;
        float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg + 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward); // rotate the hand holds gun.
        handPos.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed + Time.deltaTime);


        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, fireStartPos.position);
        lineRenderer.SetPosition(1, fireEndPos.position);
    }

    private void Shoot()
    {
        lineRenderer.enabled = false;

        GameObject bullet = Instantiate(bulletPrefab, fireStartPos.position, Quaternion.identity);

        if (transform.localScale.x > 0)
        {
            bullet.GetComponent<Rigidbody2D>().AddForce(fireStartPos.right * bulletSpeed, ForceMode2D.Impulse);
        }
        else
        {
            bullet.GetComponent<Rigidbody2D>().AddForce(-fireStartPos.right * bulletSpeed, ForceMode2D.Impulse);
        }    
        Destroy(bullet, timeToDestroyBullet);
    }

}
