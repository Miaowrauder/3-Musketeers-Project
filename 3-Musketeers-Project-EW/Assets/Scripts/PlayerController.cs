using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float baseSpeed;
    public float moveSpeed;
    public float sprintSpeed;
    public float cameraSpeed;
    public float gravity;
    public float gravityLimit;
    public float gravityMultiplier;
    public float jumpForce;
    public int extraJumps;
    public int jumpLimit;
    public bool isSprinting;
    Vector2 inputs;

    public CharacterController controller;
    public GameObject cam;
    public GameObject playerHead;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = baseSpeed;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        Rotation();
        Sprint();
    }

    void Move()
    {
        inputs = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector3 movement = new Vector3(inputs.x, gravity, inputs.y);
        movement = Quaternion.Euler(0, cam.transform.eulerAngles.y, 0) * movement;
        controller.Move(movement * moveSpeed * Time.deltaTime);
    }

    void Rotation()
    {
        playerHead.transform.rotation = Quaternion.Slerp(playerHead.transform.rotation, cam.transform.rotation, cameraSpeed * Time.deltaTime);

    }

    void Jump()
    {
        if (gravity < gravityLimit)
        {
            gravity = gravityLimit;
        }
        else
        {
            gravity -= Time.deltaTime * gravityMultiplier;
        }

        if (controller.isGrounded && Input.GetButtonDown("Jump"))
        {
            gravity = Mathf.Sqrt(jumpForce);
        }
        //else if (controller.isGrounded && Input.GetButtonDown("Jump") && isSprinting)
        //{
        //    gravity = Mathf.Sqrt(jumpForce * (baseSpeed / sprintSpeed));
        //}


        if (extraJumps > 0 && Input.GetButtonDown("Jump") && controller.isGrounded == false)
        {
            gravity = Mathf.Sqrt(jumpForce);
            extraJumps -= 1;
        }
        //else if  (extraJumps > 0 && Input.GetButtonDown("Jump") && controller.isGrounded == false)
        //{
        //    gravity = Mathf.Sqrt(jumpForce * (baseSpeed / sprintSpeed));
        //    extraJumps -= 1;
        //}

        if (controller.isGrounded)
        {
            extraJumps = jumpLimit;
        }
    }

    void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && controller.isGrounded)
        {
            isSprinting = true;
            moveSpeed = sprintSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
            moveSpeed = baseSpeed;
        }

    }

}
