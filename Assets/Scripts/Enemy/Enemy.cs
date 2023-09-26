using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float damageToGetKilled = 1.5f;

    private void Die()
    {
        gameObject.tag = "Untagged";

        FindObjectOfType<GameManager>().CheckEnemyCount();

        foreach (Transform obj in transform)
        {
            obj.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Bullet")
        {
            Vector2 force = GetForceVector(target.gameObject);
            if (transform.GetChild(0).GetComponent<Rigidbody2D>().gravityScale < 1)
            {
                Die();
                
            }
            GetComponent<Rigidbody2D>().AddForce(new Vector2(force.x, force.y),
                                                       ForceMode2D.Impulse);

        }



        if (target.tag == "Plank")
        {
            if (target.GetComponent<Rigidbody2D>().velocity.magnitude > damageToGetKilled)
            {
                Die();
            }
        }

        if (target.tag == "Ground")
        {
            if (this.GetComponent<Rigidbody2D>().velocity.magnitude > damageToGetKilled)
            {
                Die();
            }
        }
    }

    private Vector2 GetForceVector(GameObject target)
    {
        Vector2 force = Vector2.zero;
        Vector2 direction = transform.position - target.transform.position;

        // Get x
        if (direction.x > 0)
        {
            force.x = 1;
        }
        else force.x = -1;


        //Get y
        if (direction.y > 0)
        {
            force.y = 0.3f;
        }
        else force.y = -0.3f;

        return force;
    }
}
