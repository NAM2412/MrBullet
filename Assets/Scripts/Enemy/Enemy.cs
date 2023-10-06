using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float damageToGetKilled = 1.5f;
    [SerializeField] AudioClip death;

    private void Die()
    {
        gameObject.tag = "Untagged";
        SoundManager.Instance.PlaySoundEffect(death, 0.75f);

        FindObjectOfType<GameManager>().CheckEnemyCount();

        foreach (Transform obj in transform)
        {
            obj.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        Vector2 direction = transform.position - target.transform.position;
        if (target.tag == "Bullet")
        {
            target.GetComponent<CircleCollider2D>().isTrigger = true;
            if (transform.GetChild(0).GetComponent<Rigidbody2D>().gravityScale < 1)
            {
                Die();
                
            }
            GetComponent<Rigidbody2D>().AddForce(new Vector2((direction.x > 0 ? 1:-1) * 10, 
                                                             (direction.y > 0 ? 0.3f : -0.3f) * 10),
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

   
}
