using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    public GameObject self;
    public float scale;
    public float meleeDmg;
    public float rangedDmg;
    public float magicDmg;
    public float lifespan;

    public int tempTEST;

    bool isDestroying;

    public bool canBreak, isEnemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(scale, scale, scale);

        if((lifespan > 0) && (isDestroying == false))
        {
            StartCoroutine(DestroySelf());
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
        if(!isEnemy)
        {
        if (coll.tag == "Enemy")
        {
            coll.transform.GetComponent<Health>().incomingDmg = ((meleeDmg*(coll.GetComponent<Health>().dmgRecievedMult)) + (rangedDmg*(coll.GetComponent<Health>().dmgRecievedMult)) + magicDmg);
            coll.transform.GetComponent<Health>().iFrames = (lifespan+0.01f);
        }

        if ((coll.tag == "Limb") && canBreak)
        {
            coll.transform.GetComponent<LimbBehaviour>().incomingDmg = ((meleeDmg*(coll.GetComponent<LimbBehaviour>().dmgRecievedMult)) + (rangedDmg*(coll.GetComponent<LimbBehaviour>().dmgRecievedMult)) + magicDmg);
            coll.transform.GetComponent<LimbBehaviour>().iFrames = (lifespan+0.01f);
        }
        }
        else if(isEnemy && (coll.tag == "Player"))
        {
            if(((coll.transform.GetComponent<plHealth>().rangedIframes) == 0) && (rangedDmg > 0))
            {
            coll.transform.GetComponent<plHealth>().incomingRangedDmg = (rangedDmg);
            coll.transform.GetComponent<plHealth>().rangedIframes = (lifespan+0.01f);
            }
            if(((coll.transform.GetComponent<plHealth>().meleeIframes) == 0) && (meleeDmg > 0))
            {
            coll.transform.GetComponent<plHealth>().incomingMeleeDmg = (meleeDmg);
            coll.transform.GetComponent<plHealth>().meleeIframes = (lifespan+0.01f);
            }
            if(((coll.transform.GetComponent<plHealth>().magicIframes) == 0) && (magicDmg > 0))
            {
            coll.transform.GetComponent<plHealth>().incomingMagicDmg = (magicDmg);
            coll.transform.GetComponent<plHealth>().magicIframes = (lifespan+0.01f);
            }
        }
    }
}
