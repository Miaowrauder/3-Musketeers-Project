using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class plHealth : MonoBehaviour
{
    [Header("Incoming Melee")]
    public bool isMeleeable;
    public float incomingMeleeDmg;
    public float meleeIframes;

    [Header("Incoming Ranged")]
    public bool isRangedable;
    public float incomingRangedDmg;
    public float rangedIframes;
    [Header("Incoming Magic")]
    public bool isMagicable;
    public float incomingMagicDmg;
    public float magicIframes;

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
        canCountdown = true;
    }

    void Update()
    {
        healthSlider.value = health;
        if(health <= 0)
        {
            GameOver();
        }

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

        if (meleeIframes == 0f)
        {
            incomingMeleeDmg = 0f;
            isMeleeable = true;
        }

        if (rangedIframes == 0f)
        {
            incomingRangedDmg = 0f;
            isRangedable = true;
        }

        if (magicIframes == 0f)
        {
            incomingMagicDmg = 0f;
            isMagicable = true;
        }

        if ((meleeIframes > 0) || (rangedIframes > 0) || (magicIframes > 0 ))
        {
            if (canCountdown)
            {
            StartCoroutine(IframeCounter());
            }
        }

        if (meleeIframes < 0f)
        {
            meleeIframes = 0f;
        }

        if (rangedIframes < 0f)
        {
            rangedIframes = 0f;
        }

        if (magicIframes < 0f)
        {
            magicIframes = 0f;
        }
    }

    void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void TakeMagicDmg()
    {
        health -= (incomingMagicDmg);
        incomingMagicDmg = 0;
        isMagicable = false;
    }

     void TakeMeleeDmg()
    {
        if (incomingMeleeDmg <= 5f)
        {
            health -= (incomingMeleeDmg -= (defenceStat/=2f));
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

     void TakeRangedDmg()
    {
        if (incomingRangedDmg <= 5f)
        {
            health -= (incomingRangedDmg -= (defenceStat/=4f));
            incomingRangedDmg = 0;
            isRangedable = false;
        }
        else if (incomingRangedDmg > 5f)
        {
            health -= (incomingRangedDmg -= (defenceStat/=2f));
            incomingRangedDmg = 0;
            isRangedable = false;
        }
    }

    private IEnumerator IframeCounter()
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

        canCountdown = true;
    }

}
