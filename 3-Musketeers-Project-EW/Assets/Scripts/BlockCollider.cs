using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCollider : MonoBehaviour
{
    public GameObject self, player;
    public bool isDestroying;
    public float lifespan;
    public bool canDeflect;

    public Transform deflectionDirection;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
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
        player.GetComponent<plHealth>().isRangedParrying = false;
        yield return new WaitForSeconds(lifespan);
        Destroy(self);
    }

}
