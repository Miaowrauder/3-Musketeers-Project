using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class plHealth : MonoBehaviour
{
    [Header("Incoming Melee")]
    public bool isMeleeable, isMeleeParrying, meleeParryEnabled;
    public float incomingMeleeDmg, meleeParryCd;
    public int meleeParryLevel; // used as both unlock condition >1 and upgrade condition >2
    public float meleeIframes;
    

    [Header("Incoming Ranged")]
    public bool isRangedable, isRangedParrying, rangedParryEnabled;
    public float incomingRangedDmg, rangedParryCd, deflectDuration;
    public int rangedParryLevel;
    public float rangedIframes;
    public GameObject deflectPrefab;

    private GameObject barrier;
    public Transform deflectPosition;

    [Header("Incoming Magic")]
    public bool isMagicable, isMagicParrying, magicParryEnabled;
    public int magicParryLevel;
    public float incomingMagicDmg, magicParryCd;
    public float magicIframes;

    [Header("Misc Stats")]
    public float health;
    public float maxHealth;
    public float defenceStat;

    public Slider healthSlider;

    public bool canCountdown;

    void Start()
    {
        health = maxHealth;
        healthSlider.maxValue = health;

        isMeleeable = true; 
        isMagicable = true; 
        isRangedable = true;
        meleeParryEnabled = true;
        rangedParryEnabled = true;
        magicParryEnabled = true;
        canCountdown = true;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse1) && meleeParryEnabled && (meleeParryLevel > 0) && !isMeleeParrying && (meleeParryCd <= 0) && (meleeIframes == 0))
        {
            isMeleeParrying = true;
            meleeIframes = (0.3f);
            meleeParryCd = 2f;
        }

        if (Input.GetKeyDown(KeyCode.C) && magicParryEnabled && (magicParryLevel > 0) && !isMagicParrying && (magicParryCd <= 0) && (magicIframes == 0))
        {
            isMagicParrying = true;
            magicIframes = (0.3f);
            magicParryCd = 2f;
        }

        if (Input.GetKeyDown(KeyCode.Q) && rangedParryEnabled && (rangedParryLevel > 0) && !isRangedParrying && (rangedParryCd <= 0))
        {
            isRangedParrying = true;
            rangedParryCd = 2f;

            barrier = Instantiate(deflectPrefab, deflectPosition.transform.position, Quaternion.identity);

            barrier.GetComponent<BlockCollider>().lifespan = deflectDuration;
            barrier.GetComponent<BlockCollider>().isDestroying = true;
            
            if(rangedParryLevel > 1)
            {
                barrier.GetComponent<BlockCollider>().canDeflect = true;
            }

        }

        //death
        healthSlider.value = health;
        if(health <= 0)
        {
            GameOver();
        }

        //Incoming damage taken
        if ((incomingMeleeDmg > 0) && isMeleeable)
        {
            TakeMeleeDmg();
        }
        if ((incomingRangedDmg > 0) && isRangedable)
        {
            TakeRangedDmg();
        }
        if ((incomingMagicDmg > 0) && isMagicable)
        {
            TakeMagicDmg();
        }

        //damage cleanse and enable incoming on end of iframe window
        if (meleeIframes == 0f)
        {
            incomingMeleeDmg = 0f;
            isMeleeable = true;

            if(isMeleeParrying)
            {
                isMeleeParrying = false;
            }
        }
        if (rangedIframes == 0f)
        {
            incomingRangedDmg = 0f;
            isRangedable = true;

            if(isRangedParrying)
            {
                isRangedParrying = false;
            }
        }
        if (magicIframes == 0f)
        {
            incomingMagicDmg = 0f;
            isMagicable = true;

            if(isMagicParrying)
            {
                isMagicParrying = false;
            }
        }

        if(meleeIframes < 0)
        {
            meleeIframes = 0;
        }
        if(rangedIframes < 0)
        {
            rangedIframes = 0;
        }
        if(magicIframes < 0)
        {
            magicIframes = 0;
        }

        //counts down when any value is above 0
        if ((meleeIframes>0)||(rangedIframes>0)||(magicIframes>0)||(meleeParryCd>0)||(rangedParryCd>0)||(magicParryCd>0)||(deflectDuration>0))
        {
            if (canCountdown)
            {
            StartCoroutine(Counter());
            }
        }
        
    }

    void GameOver()
    {
        //reloads scene, to be replaced with back to main menu and character reset
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void TakeMagicDmg()
    {
        if (!isMagicParrying)
        {
            health -= (incomingMagicDmg);
            incomingMagicDmg = 0;
            isMagicable = false;
        }
        else if (isMagicParrying)
        {
            incomingMagicDmg = 0;
            isMagicParrying = false;
            magicParryCd = 6f;

            if(magicParryLevel > 1)
            {
                //upgraded effect
            }
        }
    }

     void TakeMeleeDmg()
    {
        if (!isMeleeParrying)
        {

        if (incomingMeleeDmg <= 5f)
        {
            health -= (incomingMeleeDmg -= (defenceStat/2f));
            incomingMeleeDmg = 0;
            isMeleeable = false;
        }
        else if (incomingMeleeDmg > 5f)
        {
            health -= (incomingMeleeDmg -= (defenceStat));
            incomingMeleeDmg = 0;
            isMeleeable = false;
        }

        }

        if(isMeleeParrying)
        {
            incomingMeleeDmg = 0;
            isMeleeParrying = false;
            meleeParryCd = 6f;

            if (meleeParryLevel > 1)
            {
                //upgraded effect
            }
        }
    }

     void TakeRangedDmg()
    {
        if(!isRangedParrying)
        {

        if (incomingRangedDmg <= 5f)
        {
            health -= (incomingRangedDmg -= (defenceStat/4f));
            incomingRangedDmg = 0;
            isRangedable = false;
        }
        else if (incomingRangedDmg > 5f)
        {
            health -= (incomingRangedDmg -= (defenceStat/2f));
            incomingRangedDmg = 0;
            isRangedable = false;
        }

        }

        if(isRangedParrying)
        {
            incomingRangedDmg = 0;
            isRangedParrying = false;
            rangedParryCd = 6f;

            if(rangedParryLevel > 1)
            {
                //upgrade effect
            }
        }
    }

    private IEnumerator Counter()
    {
        canCountdown = false;

        yield return new WaitForSeconds(0.05f);

        if (meleeIframes > 0)
        {
            meleeIframes -= 0.05f;
        }

        if (rangedIframes > 0)
        {
            rangedIframes -= 0.05f;
        }

        if (magicIframes > 0)
        {
            magicIframes -= 0.05f;
        }

        if(magicParryCd > 0)
        {
            magicParryCd -= 0.05f;
        }

        if(meleeParryCd > 0)
        {
            meleeParryCd -= 0.05f;
        }

        if(rangedParryCd > 0)
        {
            rangedParryCd -= 0.05f;
        }

        if(deflectDuration > 0)
        {

        }

        canCountdown = true;
    }

}
