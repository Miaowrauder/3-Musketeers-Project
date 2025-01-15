using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour

{
  public float fuseDelay;
    public GameObject self;
    public GameObject boomPrefab;
    public GameObject shellPrefab;
    public bool isPrimed;
    public float AOEscale;

    // Start is called before the first frame update
    void Start()
    {
        GameObject shell = Instantiate(shellPrefab, self.transform.position, Quaternion.identity);
        shell.transform.parent = self.transform;
    }

    void Update()
    {
        if(isPrimed)
        {          
            isPrimed = false;
            StartCoroutine(FuseAndBoom());
        }
    }

    public IEnumerator FuseAndBoom()
    {
         
        yield return new WaitForSeconds(fuseDelay);

        GameObject boom = Instantiate(boomPrefab, self.transform.position, Quaternion.identity);
        boom.GetComponent<DamageCollider>().scale = AOEscale;

        Destroy(self);
        
    }
}
