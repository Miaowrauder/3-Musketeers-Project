using System.Collections;
using System.Collections.Generic;
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
                moveSpeed -= (moveSpeed*2);
            }
        }
        }

        if(!isEnemy)
        {
            if(coll.tag == "Enemy")
            {
            coll.transform.GetComponent<Health>().incomingDmg = (rangedDmg*(coll.GetComponent<Health>().dmgRecievedMult));
            Destroy(self);
            }
        }
    }
}
