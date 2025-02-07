using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagCollider : MonoBehaviour
{
    public GameObject self;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider coll)
    {
        if(coll.tag == "Player")
        {
            if(coll.GetComponent<plHealth>().currentTags < 12)
            {
                coll.GetComponent<plHealth>().currentTags += 1;
            }

            Destroy(self);
        }
    }
}
