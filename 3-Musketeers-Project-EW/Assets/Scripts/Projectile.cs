using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody rb;
    public bool isEnemy, isDestroying, isHoming;
    public float lifespan;
    public float moveSpeed;
    public float rangedDmg, magicDmg;
    public GameObject self, pl;

    public float appliedIframes;
    // Start is called before the first frame update
    void Start()
    {
        pl = GameObject.FindWithTag("Player");
        if(!isHoming)
        {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * moveSpeed, ForceMode.Impulse);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(lifespan > 0 && !isDestroying)
        {
            StartCoroutine(DestroySelf());
        }

        if(isHoming)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, (pl.GetComponent<PlayerInteractions>().target).transform.position, moveSpeed * Time.deltaTime);
        }
    }

    private IEnumerator DestroySelf()
    {

        isDestroying = true;
        yield return new WaitForSeconds(lifespan);
        Destroy(self);
    }

    void OnTriggerEnter(Collider coll)
    {
        if (isEnemy)
        {
        if (coll.tag == "Player")
        {
            if(rangedDmg > 0)
            {
            if((coll.transform.GetComponent<plHealth>().rangedIframes) == 0)
            {
            coll.transform.GetComponent<plHealth>().incomingRangedDmg = (rangedDmg);
            coll.transform.GetComponent<plHealth>().rangedIframes = (appliedIframes);
            }
            }
            if(magicDmg > 0)
            {
            if((coll.transform.GetComponent<plHealth>().magicIframes) == 0)
            {
            coll.transform.GetComponent<plHealth>().incomingMagicDmg = (magicDmg);
            coll.transform.GetComponent<plHealth>().magicIframes = (appliedIframes);
            }
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
                rangedDmg *= 2f;
                magicDmg *= 2f;
            }
        }
        }

        if(!isEnemy)
        {
            if(coll.tag == "Enemy")
            {
                if((coll.transform.GetComponent<Health>().iFrames) <= 0f)
                {
                coll.transform.GetComponent<Health>().incomingDmg = ((rangedDmg*(coll.GetComponent<Health>().dmgRecievedMult)) + magicDmg);
                coll.transform.GetComponent<Health>().iFrames = (appliedIframes);
                }
            Destroy(self);
            }
        }
    }
}
