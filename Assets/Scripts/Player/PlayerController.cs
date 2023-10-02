using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float rotateSpeed = 100f, bulletSpeed = 100f, timeToDestroyBullet = 2f;
    [SerializeField] GameObject crosshair;
    public int numberOfBullets = 4;

    private Transform handPos;
    private Transform fireStartPos, fireEndPos;

    private LineRenderer lineRenderer; // the laser in game

    
    private void Awake()
    {
        crosshair.SetActive(false);

        handPos = GameObject.Find("LeftArm").transform;
        fireStartPos = GameObject.Find("FirePos1").transform;
        fireEndPos = GameObject.Find("FirePos2").transform;
        lineRenderer = GameObject.Find("Gun").GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    private void Update()
    {
        if (!IsMouseOverUI())
        {
            if (Input.GetMouseButton(0))
            {
                Aim();
               
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (numberOfBullets > 0)
                {
                    Shoot();
                }
                else
                {
                    lineRenderer.enabled = false;
                    crosshair.SetActive(false);
                }
                
            }
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


        crosshair.SetActive(true);
        crosshair.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + (Vector3.forward * 10);
    }

    private void Shoot()
    {
        lineRenderer.enabled = false;
        crosshair.SetActive(false);

        GameObject bullet = Instantiate(bulletPrefab, fireStartPos.position, Quaternion.identity);

        if (transform.localScale.x > 0)
        {
            bullet.GetComponent<Rigidbody2D>().AddForce(fireStartPos.right * bulletSpeed, ForceMode2D.Impulse);
        }
        else
        {
            bullet.GetComponent<Rigidbody2D>().AddForce(-fireStartPos.right * bulletSpeed, ForceMode2D.Impulse);
        }

        numberOfBullets--;

        FindObjectOfType<GameManager>().CheckBullets();
        Destroy(bullet, timeToDestroyBullet);
    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

}
