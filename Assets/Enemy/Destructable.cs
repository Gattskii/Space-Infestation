using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Destructable : MonoBehaviour
{


    bool dead = false;
    public int scoreValue = 1;
    
    public AudioSource audioSrc;
    public GameObject ourShip;
    public GameObject gun;
    public GameObject normalSprite;
    public GameObject explosionSprite;
    public GameObject afterExplosion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null & dead == false)
        {
            if (!bullet.isEnemy)   
            {
                if (bullet)
                {
                    Destroy(bullet.gameObject);
                }

                audioSrc.Play();
                dead = true;

                gun.GetComponent<Gun>().canShoot = false;
                ourShip.GetComponent<MoveRightLeft>().moveSpeed = 0;
                normalSprite.GetComponent<SpriteRenderer>().enabled = false;
                explosionSprite.GetComponent<SpriteRenderer>().enabled = true;
                Level.instance.AddShipDestroyed(scoreValue);

                StartCoroutine(AfterExplosion());

                // Add a coroutine to wait for 0.5 seconds before destroying the game objects
                StartCoroutine(DelayDestruction(bullet));
            }
        }
    }
    private IEnumerator AfterExplosion()
    {
        yield return new WaitForSeconds(.28f);
        explosionSprite.GetComponent<SpriteRenderer>().enabled = false;
        afterExplosion.GetComponent<SpriteRenderer>().enabled = true;
    }

    private IEnumerator DelayDestruction(Bullet bullet)
    {
        yield return new WaitForSeconds(.95f);

        Destroy(gameObject);
    }
}
