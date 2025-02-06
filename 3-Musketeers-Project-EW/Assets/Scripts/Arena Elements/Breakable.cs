using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public float hp, maxHp;
    public float incomingDmg, iFrames;
    bool isDamageable, canCountdown;
    public GameObject item, item2, gm, ui;
    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
        canCountdown = true;
        gm = GameObject.Find("GameManager_DND");
        ui = GameObject.FindWithTag("UImanager");
        item2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if((incomingDmg > 0f) && (isDamageable))
        {
            TakeDmg();
            ui.GetComponent<UImanager>().canHitIndi = true;
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
            item.SetActive(false);
            item2.SetActive(true);
            gm.GetComponent<GameManager>().canBake = true;

            GameObject ui = GameObject.FindWithTag("UImanager");
            GameObject pl = GameObject.FindWithTag("Player");
            ui.GetComponent<UImanager>().musketeerCharge += (6 + (pl.GetComponent<MusketeerAbilities>().musketeerLevel * 2));
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
