using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT : MonoBehaviour
{
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] float radiusOfExplosion = 1.5f;
    [SerializeField] float power = 10f;
    private float timeToDestroy = 0.8f;

    void Explode()
    {
        Vector2 explosionPositionn = this.transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPositionn, radiusOfExplosion);

        foreach (Collider2D hit in colliders)
        {
            if (hit.GetComponent<Rigidbody2D>() != null)
            {
                Vector2 explodeDirection = hit.GetComponent<Rigidbody2D>().position - explosionPositionn;

                hit.GetComponent<Rigidbody2D>().gravityScale = 1;

                hit.GetComponent<Rigidbody2D>().AddForce(power * explodeDirection, ForceMode2D.Impulse);
            }
            if (hit.tag == "Enemy")
            {
                hit.tag = "Untagged";
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.tag == "Bullet")
        {
            GameObject explosionEffect = Instantiate(explosionPrefab);
            explosionEffect.transform.position = this.transform.position;
            Explode();
            Destroy(explosionEffect, timeToDestroy);
            Destroy(this.gameObject);
        }
    }
}
