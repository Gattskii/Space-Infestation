using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject playerShip;
    public GameObject[] planets;
    public GameObject[] enemyShip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private float timerA = 0f;
    private float timerB = 0f;

    // Update is called once per frame
    void Update()
    {
        timerA += Time.deltaTime;
        timerB += Time.deltaTime;

        if (timerA >= 150f) //105
        {
            timerA = 0f;
            // spawn planet
            int randomIndex = Random.Range(0, planets.Length);
            Vector2 position = new Vector2(24, Random.Range(-23,-21));
            GameObject planet = Instantiate(planets[randomIndex].gameObject, position, Quaternion.identity);
            planet.GetComponent<InfestPlanet>().playerShip = playerShip;
        }

        if (timerB >= 4.5f)
        {
            timerB = 0f;
            // spawn waves of enemy ships
            int randomIndex = Random.Range(0, planets.Length);
            int totalShips = Random.Range(1,6);

           for (int i = 1; i < totalShips; i++)
            {
                Vector2 position = new Vector2(17, Random.Range(-20, -9));
                GameObject ship = Instantiate(enemyShip[randomIndex].gameObject, position, Quaternion.identity);
                
                if (Random.Range(1,2) == 1)
                {
                    ship.GetComponent<MoveSine>().inverted = true;
                }
                else
                {
                    ship.GetComponent<MoveSine>().inverted = false;
                }
            }
        }
    }
}
