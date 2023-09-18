using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Die()
    {
        gameObject.tag = "Untagged";

        foreach (Transform obj in transform)
        {
            obj.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        Vector2 force = GetForceVector(target.gameObject);
        if (target.tag == "Bullet")
        {
            if (transform.GetChild(0).GetComponent<Rigidbody2D>().gravityScale < 1)
            {
                Die();
                
            }
            GetComponent<Rigidbody2D>().AddForce(new Vector2(force.x, force.y),
                                                       ForceMode2D.Impulse);

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
