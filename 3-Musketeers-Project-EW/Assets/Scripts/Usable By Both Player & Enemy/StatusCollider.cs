using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusCollider : MonoBehaviour
{
    public int DotID;
    public float statusDuration, TickDps, statMult, lifespan, scale;
    public GameObject self;
    public bool isDestroying, isEnemy, isGrenade;

    // Start is called before the first frame update
    void Start()
    {
        GameObject gm = GameObject.Find("GameManager_DND");
        GameObject pl = GameObject.FindWithTag("Player");
        GameObject ui = GameObject.FindWithTag("UImanager");
        if(!isEnemy)
        {
        if(pl.GetComponent<OffhandFunctions>().choiceLevel > 0)
        {
        TickDps *= (pl.GetComponent<OffhandFunctions>().choiceLevel);
        }
        if(pl.GetComponent<OffhandFunctions>().choiceLevel > 1)
        {
        scale *= (pl.GetComponent<OffhandFunctions>().choiceLevel - 0.5f);
        }
        if(isGrenade && (ui.GetComponent<UImanager>().hasTrinket[2] == true))
        {
            TickDps *= 1.5f;
            scale *= 1.5f;
        }
        }
        else if(isEnemy)
        {
            TickDps *= (gm.GetComponent<GameManager>().difficultyScaling);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(scale, scale, scale);

        if((lifespan > 0) && (isDestroying == false))
        {
            StartCoroutine(DestroySelf());
        }
    }

    void OnTriggerStay(Collider coll)
    {
        if((coll.tag == "Enemy") || (coll.tag == "Player"))
        {
            if(DotID <= 3)
            {
                if(coll.GetComponent<StatusManager>().DotDuration[DotID] < statusDuration)
                {
                    coll.GetComponent<StatusManager>().DotDuration[DotID] = statusDuration;
                }   
                if(coll.GetComponent<StatusManager>().DotDmg[DotID] < (TickDps/2))
                {
                    coll.GetComponent<StatusManager>().DotDmg[DotID] = (TickDps/2);
                }
            }
            else if(DotID > 3)
            {
                if(coll.GetComponent<StatusManager>().DotDuration[DotID] < statusDuration)
                {
                    coll.GetComponent<StatusManager>().DotDuration[DotID] = statusDuration;
                }   
                if(coll.GetComponent<StatusManager>().DotDmg[DotID] > (statMult))
                {
                    coll.GetComponent<StatusManager>().DotDmg[DotID] = (statMult);
                }
            }
        }
    }

     private IEnumerator DestroySelf()
    {
        isDestroying = true;
        yield return new WaitForSeconds(lifespan);
        Destroy(self);
    }
    
}
