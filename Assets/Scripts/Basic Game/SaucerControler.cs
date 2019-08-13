using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaucerController : MonoBehaviour
{

    public float moveSpeed = 8f;
    public Joystick joystick;
    public Joystick joystick2;

    public GameObject player;
    Rigidbody2D rb;
    public float speed;
    public bool isInJoystickMovementConfig = false;
    Controller playerController;
    public int upgradeSpeed;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerController = player.GetComponent<Controller>();
        if (PlayerPrefs.GetInt("altranateControls") == 0)
        {
            isInJoystickMovementConfig = true;
        }
    }

    private void Update()
    {

        
            Vector3 moveVector = (Vector3.right * joystick.Horizontal + Vector3.up * joystick.Vertical);

            if (moveVector != Vector3.zero)
            {
               // transform.rotation = Quaternion.LookRotation(Vector3.forward, moveVector);

                transform.Translate(moveVector * moveSpeed * Time.deltaTime, Space.World);
            }
           // rb.AddForce(/*transform.up **/ (speed + upgradeSpeed * 5) * moveVector/*Input.GetAxis("Vertical")*/);
        
        
            
        
    }
}
