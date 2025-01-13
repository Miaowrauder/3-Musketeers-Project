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

    public bool canBreak;
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
}
