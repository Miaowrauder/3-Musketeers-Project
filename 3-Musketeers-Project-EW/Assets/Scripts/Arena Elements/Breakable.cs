using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public float hp, maxHp;
    public float incomingDmg, iFrames;
    bool isDamageable, canCountdown;
    public GameObject item;
    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
        canCountdown = true;
    }

    // Update is called once per frame
    void Update()
    {
        if((incomingDmg > 0f) && (isDamageable))
        {
            TakeDmg();
        }

         if (iFrames > 0f)
        {
            if (canCountdown)
            {
            StartCoroutine(IframeCounter());
            }
        }

        if (hp <= 0f)
        {
            item.GetComponent<Rigidbody>().isKinematic = false;
            Destroy(this.gameObject);
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
