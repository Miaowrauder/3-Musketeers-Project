using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseScaler : MonoBehaviour
{
    public float scale, lifespan;
    public GameObject pl;
    public bool isDestroying, isPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(scale, scale, scale);

        if(isPlayer)
        {
            transform.position = pl.transform.position;
        }
        

        if(!isDestroying)
        {
            isDestroying = true;
            StartCoroutine(Destroy());
        }
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(lifespan);
        Destroy(this.gameObject);
    }
}
