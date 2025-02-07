using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float hp;
    public float maxHp;
    public float iFrames;
    public float incomingDmg;
    public bool isDamageable, isBoss;
    public bool canCountdown;
    public float dmgRecievedMult;

    public GameObject alertPrefab;

    GameObject gm, ui;


    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager_DND");
        hp *= (0.5f + gm.GetComponent<GameManager>().difficultyScaling/2f);
        canCountdown = true;
        isDamageable = true;
        maxHp = hp;
        ui = GameObject.FindWithTag("UImanager");

        if(isBoss)
        {
            ui.GetComponent<UImanager>().bossName.text = "Cardinal's Elementalist";
            ui.GetComponent<UImanager>().bossBar.maxValue = maxHp;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isBoss)
        {
            ui.GetComponent<UImanager>().bossBar.value = hp;
            this.GetComponent<EnemyMove>().canSeePlayer = true;
        }

        if((incomingDmg > 0f) && (isDamageable))
        {
            TakeDmg();
            ui.GetComponent<UImanager>().canHitIndi = true;
        }
    
        if (hp <= 0f)
        {
            if(isBoss)
            {
                ui.GetComponent<UImanager>().bossBits.transform.position = new Vector3 (ui.GetComponent<UImanager>().bossBits.transform.position.x, ui.GetComponent<UImanager>().bossBits.transform.position.y+120, ui.GetComponent<UImanager>().bossBits.transform.position.z);
                GameObject gm = GameObject.Find("GameManager_DND");
                gm.GetComponent<GameManager>().themeReroll = true;
                gm.GetComponent<GameManager>().canEnd = true;
            }
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
        if(this.GetComponent<EnemyMove>().canSeePlayer == false)
        {
            incomingDmg *= 2f;
            GameObject pl = GameObject.FindWithTag("Player");
            ui.GetComponent<UImanager>().musketeerCharge += (4f + (pl.GetComponent<MusketeerAbilities>().musketeerLevel*2));
        } 

        if(!isBoss)
        {
            GameObject noise = Instantiate(alertPrefab, this.transform.position, Quaternion.identity); 
        }
        
        hp -= incomingDmg;
        incomingDmg = 0;
        isDamageable = false;   

        this.GetComponent<EnemyMove>().canSeePlayer = true;   
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
