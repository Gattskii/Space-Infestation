using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InfestPlanet : MonoBehaviour
{

    private bool infested = false;

    public GameObject gun;

    public AudioSource panicAudio;
    public AudioSource infestingAudio;
    public AudioSource panicFire;

    public GameObject playerShip;

    public InputAction planetControl;

    public GameObject sprite1;
    public GameObject sprite2;
    public Text textLabel; 

    private SpriteRenderer sprite1Renderer;
    private SpriteRenderer sprite2Renderer;

    private float distance;

    private void OnEnable()
    {
        planetControl.Enable();
    }
    private void OnDisable()
    {
        planetControl.Disable();
    }

     private bool textVisible;

    // Start is called before the first frame update
    void Start()
    {
        textVisible = false;
        textLabel.enabled = false;

        sprite1Renderer = sprite1.GetComponent<SpriteRenderer>();
        sprite2Renderer = sprite2.GetComponent<SpriteRenderer>();

        planetControl.started += context => {
            if (infested == false & textLabel.enabled == true)
            {
                infestingAudio.Play();
            }
        };

        planetControl.canceled += context => {
            if (infested == false & textLabel.enabled == true)
            {
                infestingAudio.Stop();
            }
        };

        planetControl.performed += context => {
            if (infested == false & textLabel.enabled == true)
            {
                OnDisable();

                gun.GetComponent<Gun>().autoShoot = true;

                panicAudio.Play();
                panicFire.Play();
                Level.instance.AddPlanetInfested(1);

                infested = true;

                sprite1Renderer.enabled = false;
                sprite2Renderer.enabled = true;

                textLabel.enabled = false;
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (playerShip)
        {
            distance = Vector2.Distance(playerShip.transform.position, gameObject.transform.position);
            if (distance <= 11 & infested == false & textVisible == false )
            {
                textVisible = true;
                textLabel.enabled = true;
            }
            else if (distance > 11 & infested == false & textVisible == true)
            {
                textVisible = false;
                textLabel.enabled = false;
            }
        }
    }
}
