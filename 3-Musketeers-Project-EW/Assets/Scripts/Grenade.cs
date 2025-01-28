using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour

{
  public float fuseDelay;
    public GameObject self;
    public GameObject boomPrefab;
    public GameObject shellPrefab;
    public bool isPrimed, isEnemy, hasBeenDeflected, isMagic, isStatus;
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

        if(isStatus)
        {
            boom.GetComponent<StatusCollider>().scale = AOEscale;
        }
        else
        {
        boom.GetComponent<DamageCollider>().scale = AOEscale;
        }

        if(hasBeenDeflected)
        {
            boom.GetComponent<DamageCollider>().isEnemy = false;
            boom.GetComponent<DamageCollider>().rangedDmg *=3f;
            boom.GetComponent<DamageCollider>().meleeDmg *=3f;
            boom.GetComponent<DamageCollider>().magicDmg *=3f;
        }

        Destroy(self);
        
    }

    void OnTriggerEnter(Collider coll)
    {
        if ((coll.tag == "Blocker") && isEnemy && !isMagic)
        {
            if(coll.transform.GetComponent<BlockCollider>().canDeflect == false)
            {
                Destroy(self);
            }
            else if(coll.transform.GetComponent<BlockCollider>().canDeflect == true)
            {
                isEnemy = false;
                self.transform.position = coll.transform.position;
                self.transform.LookAt(coll.GetComponent<BlockCollider>().deflectionDirection.transform.position);
                self.gameObject.GetComponent<Rigidbody>().AddForce(self.transform.forward * 20f, ForceMode.Impulse);
                hasBeenDeflected = true;
            }
        }
    }

}
