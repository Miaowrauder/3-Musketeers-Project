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
    public GameObject cam, noiseZone;
    public GameObject playerHead;

    bool gravityActive;
    GameObject ui;

    // Start is called before the first frame update
    void Start()
    {
        ui = GameObject.FindWithTag("UImanager");
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
            if(ui.GetComponent<UImanager>().hasTrinket[10] == true) //slow invert
            {
                controller.Move(((movement * (moveSpeed + (this.GetComponent<plHealth>().currentTags/12))) / this.GetComponent<StatusManager>().DotDmg[4]) * Time.deltaTime);
            }
            else
            {
                controller.Move(((movement * (moveSpeed + (this.GetComponent<plHealth>().currentTags/12))) * this.GetComponent<StatusManager>().DotDmg[4]) * Time.deltaTime);
            }
            
        }
        else if(speedyTicks > 0)
        {  
            if(ui.GetComponent<UImanager>().hasTrinket[10] == true) //slow invert
            {
                controller.Move(((movement * ((moveSpeed + (this.GetComponent<plHealth>().currentTags/12))*(speedyTicks/2))) / this.GetComponent<StatusManager>().DotDmg[4]) * Time.deltaTime); 
            }
            else
            {
                controller.Move(((movement * ((moveSpeed + (this.GetComponent<plHealth>().currentTags/12))*(speedyTicks/2))) * this.GetComponent<StatusManager>().DotDmg[4]) * Time.deltaTime); 
            }
            
            speedyTicks -= 1;
        }

        if(!isSprinting && !isCrouching)
        {
            noiseZone.GetComponent<NoiseScaler>().scale = 6f;
            moveSpeed = baseSpeed;
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
            noiseZone.GetComponent<NoiseScaler>().scale += 3f;
            gravity = Mathf.Sqrt(jumpForce);
            noiseZone.GetComponent<NoiseScaler>().scale -= 3f;
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
            noiseZone.GetComponent<NoiseScaler>().scale += 7f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
            moveSpeed = baseSpeed;
            noiseZone.GetComponent<NoiseScaler>().scale -= 7f;
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
            noiseZone.GetComponent<NoiseScaler>().scale += 1f;
            gravity = Mathf.Sqrt(jumpForce * 1f);
            noiseZone.GetComponent<NoiseScaler>().scale -= 1f;
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
        if(Input.GetKeyDown(KeyCode.C))
        {
            noiseZone.GetComponent<NoiseScaler>().scale -= 3f;
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
        if(Input.GetKeyUp(KeyCode.C))
        {
            noiseZone.GetComponent<NoiseScaler>().scale += 3f;
            speedyTicks = 0;
            isCrouching = false;
            moveSpeed = baseSpeed;
            this.GetComponent<CharacterController>().height = 2f;
            transform.localScale = new Vector3(0.8f, 0.8f, 0.8f); 
        }
    }

}
