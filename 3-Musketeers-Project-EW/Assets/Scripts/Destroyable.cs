using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    public float hp, maxHp;
    public float incomingDmg, iFrames;
    bool isDamageable, canCountdown;
    public GameObject breakItem;

    public GameObject[] consumable;
    public Transform[] breakPos;
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
            GameObject shard0 = Instantiate(breakItem, breakPos[0].transform.position, Quaternion.identity);
            GameObject shard1 = Instantiate(breakItem, breakPos[1].transform.position, Quaternion.identity);
            GameObject shard2 = Instantiate(breakItem, breakPos[2].transform.position, Quaternion.identity);

            int i = (Random.Range(0,5));

            if(i == 0)
            {
              
              GameObject pickup = Instantiate(consumable[(Random.Range(0,consumable.Length))], shard0.transform.position, Quaternion.identity);
            }
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
