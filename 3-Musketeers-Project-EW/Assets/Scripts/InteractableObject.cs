using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool isActivated, canActivate;
    public float delay;
    public Transform[] shootPos;
    public GameObject shotObject;
    public GameObject proj;

    bool canDelay;
    // Start is called before the first frame update
    void Start()
    {
        canDelay = true;
        canActivate = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isActivated && (shotObject != null) && canActivate)
        {
            canActivate = false;
            
            if(delay == 0)
            {
                ShootBased();
            }
            else if(delay > 0)
            {
                if(canDelay)
                {
                    StartCoroutine(DelayHandler());
                }
            }
        }
    }

    private IEnumerator DelayHandler()
    {
        canDelay = false;
        yield return new WaitForSeconds(delay);

        if(shotObject != null)
        {
            ShootBased();
        }
    }

    void ShootBased()
    {
        for (int i = 0; i < shootPos.Length; i++)
        {
            proj = Instantiate(shotObject, shootPos[i].transform.position, shootPos[i].transform.rotation);

            proj.GetComponent<Grenade>().isPrimed = true;
            proj.gameObject.transform.parent = null;
            proj.gameObject.GetComponent<Rigidbody>().AddForce(proj.transform.forward * 5, ForceMode.Impulse);    
            proj.gameObject.GetComponent<Rigidbody>().AddForce(proj.transform.up * (5), ForceMode.Impulse);
        }
    }
}
