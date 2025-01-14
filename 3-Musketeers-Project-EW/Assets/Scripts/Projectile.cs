using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEditor.Callbacks;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody rb;
    public bool isEnemy;
    public float lifespan;
    public float moveSpeed;
    public float rangedDmg;
    public GameObject self;

    public float appliedIframes;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * moveSpeed, ForceMode.Impulse);
        DestroySelf();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(lifespan);
        Destroy(self);
    }

    void OnTriggerEnter(Collider coll)
    {
        if (isEnemy)
        {
        if (coll.tag == "Player")
        {
            if((coll.transform.GetComponent<plHealth>().rangedIframes) == 0)
            {
            coll.transform.GetComponent<plHealth>().incomingRangedDmg = (rangedDmg);
            coll.transform.GetComponent<plHealth>().rangedIframes = (appliedIframes);
            }

            Destroy(self);
        }
        if (coll.tag == "Blocker")
        {
            if(coll.transform.GetComponent<BlockCollider>().canDeflect == false)
            {
                Destroy(self);
            }
            else if(coll.transform.GetComponent<BlockCollider>().canDeflect == true)
            {
                isEnemy = false;
                self.transform.position = coll.transform.position;
                self.transform.LookAt(coll.GetComponent<BlockCollider>().deflectionDirection.transform.position);
                rb.velocity = Vector3.zero;
                rb.AddForce(transform.forward * (moveSpeed*2), ForceMode.Impulse);
            }
        }
        }

        if(!isEnemy)
        {
            if(coll.tag == "Enemy")
            {
            coll.transform.GetComponent<Health>().incomingDmg = (rangedDmg*(coll.GetComponent<Health>().dmgRecievedMult));
            coll.transform.GetComponent<Health>().iFrames = (appliedIframes);
            Destroy(self);
            }
        }
    }
}
