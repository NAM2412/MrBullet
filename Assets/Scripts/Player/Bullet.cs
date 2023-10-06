using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Sound FX")]
    [SerializeField] AudioClip boxHit;
    [SerializeField] AudioClip plankHit;
    [SerializeField] AudioClip groundHit;
    [SerializeField] AudioClip explodeHit;
    [Range(0f, 1f)]
    [SerializeField] float soundEffectVolume;
    private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.tag == "Enemy")
        {
            this.gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
        }
        if (target.gameObject.tag == "Box")
        {
            SoundManager.Instance.PlaySoundEffect(boxHit, soundEffectVolume);
            Destroy(target.gameObject);
        }

        if (target.gameObject.tag == "Plank")
        {
            SoundManager.Instance.PlaySoundEffect(plankHit, soundEffectVolume);
        }

        if (target.gameObject.tag == "Ground")
        {
            SoundManager.Instance.PlaySoundEffect(groundHit, soundEffectVolume);
        }

        if (target.gameObject.tag == "TNT")
        {
            SoundManager.Instance.PlaySoundEffect(explodeHit, soundEffectVolume);
            Destroy(target.gameObject);
        }

        this.gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
    }
}
