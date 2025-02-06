using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainCollider : MonoBehaviour
{
    public float regenPerSec, statusDuration;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void OnTriggerStay(Collider coll)
    {
        if(coll.tag == "Player")
        {
            
            if(coll.GetComponent<StatusManager>().DotDuration[3] < statusDuration)
            {
                coll.GetComponent<StatusManager>().DotDuration[3] = statusDuration;
            }   
            if(coll.GetComponent<StatusManager>().DotDmg[3] < (regenPerSec/2))
            {
                    coll.GetComponent<StatusManager>().DotDmg[3] = (regenPerSec/2);
            }
        }
        else if(coll.tag == "Enemy")
        {
            coll.GetComponent<StatusManager>().DotDuration[6] = (statusDuration*3);
        }
    }
}
