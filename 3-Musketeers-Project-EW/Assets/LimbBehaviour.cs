using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbBehaviour : MonoBehaviour
{

    public bool isMainhand, isOffhand, isLeg, isActive;
    public GameObject host;
    
    public float hp;
    float maxHp;

    public float iFrames;
    public float incomingDmg;
    public bool isDamageable;
    public bool canCountdown;
    public float dmgRecievedMult;

    public GameObject self;

    public float scale;

    // Start is called before the first frame update
    void Start()
    {
        isActive = true;
        scale = 1f;
        hp = ((host.GetComponent<Health>().hp)/2f);
        dmgRecievedMult = (host.GetComponent<Health>().dmgRecievedMult);
        canCountdown = true;
        isDamageable = true;
        maxHp = hp;
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive)
        {

        if((incomingDmg > 0f) && (isDamageable))
        {
            TakeDmg();
        }
    
        if (hp <= 0f)
        {
            //to be replaced with bleed
            host.GetComponent<Health>().hp -= maxHp/4;
            self.GetComponent<Rigidbody>().isKinematic = false;

            if (isLeg)
            {
                host.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = (host.GetComponent<UnityEngine.AI.NavMeshAgent>().speed*0.4f);
            }
            else if (isMainhand)
            {
                host.GetComponent<EnemyMove>().mainhandEnabled = false;
            }
            else if (isOffhand)
            {
                host.GetComponent<EnemyMove>().offhandEnabled = false;
            }

            self.transform.parent = null;

            isActive = false;
        }

        if (iFrames > 0f)
        {
            if (canCountdown)
            {
            StartCoroutine(IframeCounter());
            }
        }

        }

        if (!isActive)
        {
            transform.localScale = new Vector3(scale, scale, scale);

            scale *= 0.999f;
        
        }

        if (scale <= 0.1f)
        {
            Destroy(self);
        }

    }

        

    void TakeDmg()
    {             
        hp -= incomingDmg;
        incomingDmg = 0f;
        isDamageable = false;      
    }

    private IEnumerator IframeCounter()
    {
        canCountdown = false;
        isDamageable = false;

        yield return new WaitForSeconds(iFrames);

        iFrames = 0f;

        incomingDmg = 0f;

        canCountdown = true;
        isDamageable = true;
    }
}