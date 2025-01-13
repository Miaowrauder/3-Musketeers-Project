using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCollider : MonoBehaviour
{
    public GameObject self;
    public bool isDestroying;
    public float lifespan;
    public bool canDeflect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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

}
