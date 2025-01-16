using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float hp;
    float maxHp;
    public float iFrames;
    public float incomingDmg;
    public bool isDamageable;
    public bool canCountdown;
    public float dmgRecievedMult;


    // Start is called before the first frame update
    void Start()
    {
        canCountdown = true;
        isDamageable = true;
        maxHp = hp;
    }

    // Update is called once per frame
    void Update()
    {

        if((incomingDmg > 0f) && (isDamageable))
        {
            TakeDmg();
        }
    
        if (hp <= 0f)
        {
            Destroy(gameObject); 
        }

        if (iFrames > 0)
        {
            if (canCountdown)
            {
            StartCoroutine(IframeCounter());
            }
        }

    }

        

    void TakeDmg()
    {             
        hp -= incomingDmg;
        incomingDmg = 0;
        isDamageable = false;   
    }

    private IEnumerator IframeCounter()
    {
        canCountdown = false;
        isDamageable = false;

        yield return new WaitForSeconds(iFrames);

        iFrames = 0;

        incomingDmg = 0f;

        canCountdown = true;
        isDamageable = true;
    }
}
