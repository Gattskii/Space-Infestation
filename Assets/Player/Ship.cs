using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{

    Gun[] guns;

    public float moveSpeed;
    public float shootDelaySeconds;

    bool moveUp, moveDown, moveLeft, moveRight, speedUp,
    shoot;

    bool debounceShoot = false;

    // Start is called before the first frame update
    void Start()
    {
        guns = transform.GetComponentsInChildren<Gun>();
    }

    // Update is called once per frame
    void Update()
    {

        moveUp = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        moveDown = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
        moveLeft = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        moveRight = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        speedUp = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        shoot = Input.GetKeyDown(KeyCode.Space);
        if (shoot & debounceShoot == false)
        {
            shoot = false;
            debounceShoot = true;
            foreach(Gun gun in guns)
            {
                gun.Shoot();
            }
            StartCoroutine(ShootDelayFunc());
        }
    }

    private IEnumerator ShootDelayFunc()
    {
        yield return new WaitForSeconds(shootDelaySeconds);
        debounceShoot = false;
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        float moveAmount = moveSpeed * Time.fixedDeltaTime;
        if (speedUp)
        {
            moveAmount *= 2;
        }

        Vector2 move = Vector2.zero;

        if (moveUp)
        {
            move.y += moveAmount;
        }
        if (moveDown)
        {
            move.y -= moveAmount;
        }
        if (moveLeft)
        {
            move.x -= moveAmount;
        }
        if (moveRight)
        {
            move.x += moveAmount;
        }

        pos += move;

        transform.position = pos;
    }

}
