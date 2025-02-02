using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float cameraSpeed, sprintSpeed, moveSpeed, baseSpeed, crouchSpeed;
    public float gravityMultiplier, jumpForce, gravityLimit, gravity;
    public int extraJumps, jumpLimit, speedyTicks;
    public bool isSprinting, canMove, isCrouching;
    Vector2 inputs;

    public CharacterController controller;
    public GameObject cam;
    public GameObject playerHead;

    bool gravityActive;

    // Start is called before the first frame update
    void Start()
    {
        gravityActive = true;
        moveSpeed = baseSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
        Move();
        if(gravityActive)
        {
            Jump();
        }
        Rotation();
        Sprint();
        Crouch();
        }
        else if(!canMove)
        {
            StartCoroutine(MoveLock());
        }
    }

    void Move()
    {
        inputs = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector3 movement = new Vector3(inputs.x, gravity, inputs.y);
        movement = Quaternion.Euler(0, cam.transform.eulerAngles.y, 0) * movement;

        if(speedyTicks < 1)
        {
        controller.Move(movement * moveSpeed * Time.deltaTime);
        }
        else if(speedyTicks > 0)
        {  
            controller.Move(movement * (moveSpeed*(speedyTicks/2)) * Time.deltaTime); 
            speedyTicks -= 1;
        }
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
        if (Input.GetKeyDown(KeyCode.LeftShift) && controller.isGrounded && !isCrouching)
        {
            isSprinting = true;
            moveSpeed = sprintSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && !isCrouching)
        {
            isSprinting = false;
            moveSpeed = baseSpeed;
        }

    }

    public IEnumerator MoveLock()
    {
        yield return new WaitForSeconds(0.1f);
        canMove = true;
    }

    //3 triggers responsible for mantling and grabbing
    void OnTriggerEnter(Collider coll)
    {
        if(coll.CompareTag("MantleCollider"))
        {
            gravityActive = false;
            gravity = 0;
        }
    }

    void OnTriggerStay(Collider coll)
    {
        if(coll.CompareTag("MantleCollider") && (Input.GetKey(KeyCode.Space)))
        {
            gravity = Mathf.Sqrt(jumpForce * 1f);
        }
        
        
    }

    void OnTriggerExit(Collider coll)
    {
        if(coll.CompareTag("MantleCollider"))
        {
            gravityActive = true;
        }
    }

    void Crouch()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            if(isSprinting && (this.GetComponent<cooldownManager>().dodgeCd <= 0))
            {
                speedyTicks = 45;
                this.GetComponent<cooldownManager>().dodgeCd = 2;
            }
            isCrouching = true;
            moveSpeed = crouchSpeed;
            this.GetComponent<CharacterController>().height = 0.6f;
            transform.localScale = new Vector3(0.8f, 0.4f, 0.8f);  
        }
        if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            speedyTicks = 0;
            isCrouching = false;
            moveSpeed = baseSpeed;
            this.GetComponent<CharacterController>().height = 2f;
            transform.localScale = new Vector3(0.8f, 0.8f, 0.8f); 
        }
    }

}
