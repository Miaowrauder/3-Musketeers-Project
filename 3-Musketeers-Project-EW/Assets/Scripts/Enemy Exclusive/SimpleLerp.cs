using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleLerp : MonoBehaviour
{
    GameObject pl;
    public float moveSpeed, lifespan;
    public GameObject self;

    bool isDestroying;
    // Start is called before the first frame update
    void Start()
    {
        pl = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, pl.transform.position, moveSpeed * Time.deltaTime);

        if(lifespan > 0)
        {
            if(!isDestroying)
            {
                isDestroying = true;
                StartCoroutine(DestroySelf());
            }
        }
    }

    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(lifespan);
        Destroy(self);
    }
}
