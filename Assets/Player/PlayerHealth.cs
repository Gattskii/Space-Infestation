using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public AudioSource bloodSplat;
    public AudioSource damagedGrowl;
    public GameObject sprite;
    public GameObject dead1;
    public GameObject dead2;

    public GameObject gun;

    public int Health = 10;

    Color damagedColor;
    Color originalColor;

    // Start is called before the first frame update
    void Start()
    {
        originalColor = sprite.GetComponent<SpriteRenderer>().color;
        ColorUtility.TryParseHtmlString("#D75656", out damagedColor);
        Level.instance.updateHealth(Health);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null && bullet.isEnemy)
        {
            if (Health > 0)
            {
                // lower health
            Health -= 1;
            Level.instance.updateHealth(Health);
            Destroy(bullet.gameObject);

            damagedGrowl.Play();

            sprite.GetComponent<SpriteRenderer>().color = damagedColor;
            //add wait by 0.15 seconds here
            StartCoroutine(RestoreOriginalColor());

            // if health is 0 then die
            if (Health <= 0)
            {
                Level.instance.updateHealth(0);

                gun.GetComponent<Gun>().canShoot = false;
                gameObject.GetComponent<Ship>().moveSpeed = 0;

               // explosion effects
                sprite.GetComponent<SpriteRenderer>().enabled = false;
                dead1.GetComponent<SpriteRenderer>().enabled = true;

                bloodSplat.Play();

                // Add a coroutine to wait for 0.4 seconds before enabling dead2
                StartCoroutine(DelayEnableDead2());

                // Add a coroutine to wait for 0.5 seconds before destroying the gameObject
                StartCoroutine(DelayDestroy());
            }
            }
        }
    }

    private IEnumerator DelayEnableDead2()
    {
        yield return new WaitForSeconds(0.28f);
        dead1.GetComponent<SpriteRenderer>().enabled = false;
        dead2.GetComponent<SpriteRenderer>().enabled = true;
    }

    private IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(1.2f);
        Destroy(gameObject);
    }

    private IEnumerator RestoreOriginalColor()
    {
        yield return new WaitForSeconds(0.3f);
        sprite.GetComponent<SpriteRenderer>().color = originalColor;
    }
}
