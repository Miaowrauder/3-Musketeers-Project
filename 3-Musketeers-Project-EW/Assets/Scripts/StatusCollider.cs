using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusCollider : MonoBehaviour
{
    public int DotID;
    public float statusDuration, TickDps, lifespan, scale;
    public GameObject self;
    public bool isDestroying;

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

    void OnTriggerStay(Collider coll)
    {
        if((coll.tag == "Enemy") || (coll.tag == "Player"))
        {
        if(coll.GetComponent<StatusManager>().DotDuration[DotID] < statusDuration)
        {
            coll.GetComponent<StatusManager>().DotDuration[DotID] = statusDuration;
        }
        if(coll.GetComponent<StatusManager>().DotDmg[DotID] < (TickDps/2))
        {
            coll.GetComponent<StatusManager>().DotDmg[DotID] = (TickDps/2);
        }
        }
    }

     private IEnumerator DestroySelf()
    {
        isDestroying = true;
        yield return new WaitForSeconds(lifespan);
        Destroy(self);
    }
    
}
