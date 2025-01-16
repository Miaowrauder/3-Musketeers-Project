using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    public GameObject self;
    public float[] DotDmg, DotDuration;
    public bool isTicking, isEnemy;
    // Start is called before the first frame update
    void Start()
    {
        isTicking = false;
        DotDmg[0] = 0f;
        DotDmg[1] = 0f;
        DotDmg[2] = 0f;

        DotDuration[0] = 0f;
        DotDuration[1] = 0f;
        
    }

    // Update is called once per frame
    void Update()
    {
        if( (((DotDuration[0]) > 0f) || ((DotDuration[1]) > 0f) || ((DotDmg[2]) > 0f)) && (isTicking == false))
        {
            StartCoroutine(tickDown());
        }

        if(DotDuration[0] <= 0f)
        {
            DotDmg[0] = 0f;
        }

        if(DotDuration[1] <= 0f)
        {
            DotDmg[1] = 0f;
        }

        // poison wearoff
        if(!isEnemy)
        {
        if (self.GetComponent<OffhandFunctions>().isConsuming == true)
        {
            DotDuration[1] = 0f;
        }
        }
    }

    private IEnumerator tickDown()
    {
        isTicking = true;

        if(DotDuration[0] >= 0.5f)
        {
            DotDuration[0] -= 0.5f;

            if(isEnemy)
            {
                self.GetComponent<Health>().hp -= DotDmg[0];
            }
            else if (!isEnemy)
            {
                self.GetComponent<plHealth>().health -= DotDmg[0];

                if (self.GetComponent<PlayerController>().isSprinting == true)
                {
                    DotDuration[0] -= 0.5f;
                }
            }
        }

        if(DotDuration[1] >= 0.5f)
        {
            DotDuration[1] -= 0.5f;

            if(isEnemy)
            {
                self.GetComponent<Health>().hp -= DotDmg[1];
            }
            else if (!isEnemy)
            {
                self.GetComponent<plHealth>().health -= DotDmg[1];

            }
        }

        //bleed, enemy exclusive
        if(DotDmg[2] > 0)
        {    
             self.GetComponent<Health>().hp -= DotDmg[2];         
        }

        yield return new WaitForSeconds(0.5f);

         isTicking = false;

    }
}
