using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UImanager : MonoBehaviour
{
    public GameObject GameM, pl;
    public bool canLoad, reroll;
    public TMP_Text[] buttonText;
    public string[] buttonString;
    public int sceneNumber, upgradeID;
    bool canText;
    public int[] randomIDs;

    public bool[] hasTrinket;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if((sceneNumber == 0) && canLoad)
        {
            OnEnd();
            canLoad = false;
        }

        if(reroll)
        {
            randomIDs[0] = Random.Range(0, 13);
            randomIDs[1] = Random.Range(0, 13);
            randomIDs[2] = Random.Range(0, 13);
            
            //randomises if player parry max level but draws any parry upgrade id
           if( ( ((randomIDs[0] == 0)||(randomIDs[1] == 0)||(randomIDs[2] == 0)) && (pl.GetComponent<plHealth>().meleeParryLevel == 2) ) || ( ((randomIDs[0] == 1)||(randomIDs[1] == 1||(randomIDs[2] == 1)) && (pl.GetComponent<plHealth>().rangedParryLevel == 2)) ) || ( ((randomIDs[0] == 2)||(randomIDs[1] == 2||(randomIDs[2] == 2)) && (pl.GetComponent<plHealth>().magicParryLevel == 2)) ) );
           {
             randomIDs[0] = Random.Range(0, 13);
             randomIDs[1] = Random.Range(0, 13);
             randomIDs[2] = Random.Range(0, 13);
           }

           if((randomIDs[0] != randomIDs[1]) && (randomIDs[0] != randomIDs[2]) && (randomIDs[2] != randomIDs[1]));
           {
             canText = true;
             reroll = false;
           }

           if(canText)
           {
            UpgradeText();
           }
        }


    }

    public void OnEnd()
    {

        sceneNumber +=1;
        SceneManager.LoadScene("Scene 1");
        
    }

    public void UpgradeText()
    {
        canText = false;
        //melee parry
        if(randomIDs[0] == 0)
        {
            if(pl.GetComponent<plHealth>().meleeParryLevel == 1)
            {
            buttonString[0] = "Evolve Thy Melee Parry With A Follow-up!";
            }
        }
        if(randomIDs[1] == 0)
        {
            if(pl.GetComponent<plHealth>().meleeParryLevel == 1)
            {
            buttonString[0] = "Evolve Thy Melee Parry With A Follow-up!";
            }
        }
        if(randomIDs[2] == 0)
        {
            if(pl.GetComponent<plHealth>().meleeParryLevel == 1)
            {
            buttonString[0] = "Evolve Thy Melee Parry With A Follow-up!";
            }
        }

        //ranged parry unlock
        if(randomIDs[0] == 1)
        {
            if(pl.GetComponent<plHealth>().rangedParryLevel == 0)
            {
            buttonString[0] = "Access Thy Ranged Parry";
            }
            if(pl.GetComponent<plHealth>().rangedParryLevel == 1)
            {
            buttonString[0] = "Evolve Thy Ranged Parry To Reflect!";
            }
        }
        if(randomIDs[1] == 1)
        {
            if(pl.GetComponent<plHealth>().rangedParryLevel == 0)
            {
            buttonString[1] = "Access Thy Ranged Parry";
            }
            if(pl.GetComponent<plHealth>().rangedParryLevel == 1)
            {
            buttonString[1] = "Evolve Thy Ranged Parry To Reflect!";
            }
        }
        if(randomIDs[2] == 1)
        {
            if(pl.GetComponent<plHealth>().rangedParryLevel == 0)
            {
            buttonString[2] = "Access Thy Ranged Parry";
            }
            if(pl.GetComponent<plHealth>().rangedParryLevel == 1)
            {
            buttonString[2] = "Evolve Thy Ranged Parry To Reflect!";
            }
        }

        //magic parry unlock
        if(randomIDs[0] == 2)
        {
            if(pl.GetComponent<plHealth>().magicParryLevel == 0)
            {
            buttonString[0] = "Access Thy Magic Parry";
            }
            if(pl.GetComponent<plHealth>().magicParryLevel == 1)
            {
            buttonString[0] = "Evolve Thy Magic Parry With A Follow-up!";
            }
        }
        if(randomIDs[1] == 2)
        {
            if(pl.GetComponent<plHealth>().magicParryLevel == 0)
            {
            buttonString[1] = "Access Thy Magic Parry";
            }
            if(pl.GetComponent<plHealth>().magicParryLevel == 1)
            {
            buttonString[1] = "Evolve Thy Magic Parry With A Follow-up!";
            }
        }
        if(randomIDs[2] == 2)
        {
            if(pl.GetComponent<plHealth>().magicParryLevel == 0)
            {
            buttonString[2] = "Access Thy Magic Parry";
            }
            if(pl.GetComponent<plHealth>().magicParryLevel == 1)
            {
            buttonString[2] = "Evolve Thy Magic Parry With A Follow-up!";
            }
        }

        //health upgrade
        if((randomIDs[0] == 3) || (randomIDs[0] == 16))
        {
            buttonString[0] = "Improve & Restore Thy Good Health";
        }
        if((randomIDs[1] == 3) || (randomIDs[1] == 16))
        {
            buttonString[1] = "Improve & Restore Thy Good Health";
        }
        if((randomIDs[2] == 3) || (randomIDs[2] == 16))
        {
            buttonString[2] = "Improve & Restore Thy Good Health";
        }

        //attack speed
        if((randomIDs[0] == 4) || (randomIDs[0] == 17))
        {
            buttonString[0] = "Quicken The Pace Of Thy Strikes";
        }
        if((randomIDs[1] == 4) || (randomIDs[1] == 17))
        {
            buttonString[1] = "Quicken The Pace Of Thy Strikes";
        }
        if((randomIDs[2] == 4) || (randomIDs[2] == 17))
        {
            buttonString[2] = "Quicken The Pace Of Thy Strikes";
        }

        //defence stat
        if((randomIDs[0] == 5) || (randomIDs[0] == 18))
        {
            buttonString[0] = "Fortify Thy Bodily Defences";
        }
        if((randomIDs[1] == 5) || (randomIDs[1] == 18))
        {
            buttonString[1] = "Fortify Thy Bodily Defences";
        }
        if((randomIDs[2] == 5) || (randomIDs[2] == 18))
        {
            buttonString[2] = "Fortify Thy Bodily Defences";
        }

         //melee stat
        if((randomIDs[0] == 6) || (randomIDs[0] == 19))
        {
            buttonString[0] = "Sharpen Thy Thrusting Blade";
        }
        if((randomIDs[0] == 6) || (randomIDs[0] == 19))
        {
            buttonString[1] = "Sharpen Thy Thrusting Blade";
        }
        if((randomIDs[0] == 6) || (randomIDs[0] == 19))
        {
            buttonString[2] = "Sharpen Thy Thrusting Blade";
        }

        //burn pouch
        if(randomIDs[0] == 7)
        {
            buttonString[0] = "Choice Ability - Pouch Of Flame";
        }
        if(randomIDs[1] == 7)
        {
            buttonString[1] = "Choice Ability - Pouch Of Flame";
        }
        if(randomIDs[2] == 7)
        {
            buttonString[2] = "Choice Ability - Pouch Of Flame";
        }

        //poison pouch
        if(randomIDs[0] == 8)
        {
            buttonString[0] = "Choice Ability - Pouch Of Pestilence";
        }
        if(randomIDs[1] == 8)
        {
            buttonString[1] = "Choice Ability - Pouch Of Pestilence";
        }
        if(randomIDs[2] == 8)
        {
            buttonString[2] = "Choice Ability - Pouch Of Pestilence";
        }

        //cleanse salve
        if(randomIDs[0] == 9)
        {
            buttonString[0] = "Choice Ability - Salve Of Cleansing";
        }
        if(randomIDs[1] == 9)
        {
            buttonString[1] = "Choice Ability - Salve Of Cleansing";
        }
        if(randomIDs[2] == 9)
        {
            buttonString[2] = "Choice Ability - Salve Of Cleansing";
        }

        //regen salve
        if(randomIDs[0] == 10)
        {
            buttonString[0] = "Choice Ability - Salve Of Rejuvenation";
        }
        if(randomIDs[1] == 10)
        {
            buttonString[1] = "Choice Ability - Salve Of Rejuvenation";
        }
        if(randomIDs[2] == 10)
        {
            buttonString[2] = "Choice Ability - Salve Of Rejuvenation";
        }

        //choice level
        if((randomIDs[0] >= 12) && (randomIDs[0] <= 15))
        {
            buttonString[0] = "Enhance Thy Conjured Items";
        }
        if((randomIDs[0] >= 12) && (randomIDs[0] <= 15))
        {
            buttonString[1] = "Enhance Thy Conjured Items";
        }
        if((randomIDs[0] >= 12) && (randomIDs[0] <= 15))
        {
            buttonString[2] = "Enhance Thy Conjured Items";
        }

        //move speed
        if(randomIDs[0] == 11)
        {
            buttonString[0] = "Hasten Thy Movement";
        }
        if(randomIDs[1] == 11)
        {
            buttonString[1] = "Hasten Thy Movement";
        }
        if(randomIDs[2] == 11)
        {
            buttonString[2] = "Hasten Thy Movement";
        }


        buttonText[0].text = buttonString[0];
        buttonText[1].text = buttonString[1];
        buttonText[2].text = buttonString[2];

    }

    public void Upgrade()
    {
        if(upgradeID == 0) //melee parry upgrade
        {
            pl.GetComponent<plHealth>().meleeParryLevel +=1;
        }
        if(upgradeID == 1) //ranged parry upgrade
        {
            pl.GetComponent<plHealth>().rangedParryLevel +=1;
        }
        if(upgradeID == 2) //magic parry upgrade
        {
            pl.GetComponent<plHealth>().magicParryLevel +=1;
        }
        if((upgradeID == 3) || (upgradeID == 16)) //health upgrade, full heal
        {
            pl.GetComponent<plHealth>().maxHealth += (30f * GameM.GetComponent<GameManager>().difficultyScaling);
            pl.GetComponent<plHealth>().health = pl.GetComponent<plHealth>().maxHealth;
            pl.GetComponent<plHealth>().healthSlider.maxValue = pl.GetComponent<plHealth>().health;

        }
        if((upgradeID == 4) || (upgradeID == 17)) //attack speed
        {
            pl.GetComponent<MeleeAttack>().attackSpeedStat *= 0.8f;
        }
        if((upgradeID == 5) || (upgradeID == 18)) //defense upgrade
        {
            pl.GetComponent<plHealth>().defenceStat += (3f * GameM.GetComponent<GameManager>().difficultyScaling);
        }
        if((upgradeID == 6) || (upgradeID == 19)) //melee damage
        {
            pl.GetComponent<MeleeAttack>().damageStat += (10f * GameM.GetComponent<GameManager>().difficultyScaling);
        }
        if(upgradeID == 7) //burn pouch
        {
            pl.GetComponent<OffhandFunctions>().abilityID = 0;  
        }
        if(upgradeID == 8) //poison pouch
        {
            pl.GetComponent<OffhandFunctions>().abilityID = 1;  
        }
        if(upgradeID == 9) //cleanse salve
        {
            pl.GetComponent<OffhandFunctions>().abilityID = 2;  
        }
        if(upgradeID == 10) //regen salve
        {
            pl.GetComponent<OffhandFunctions>().abilityID = 3;  
        }
        if(upgradeID == 11) //speed upgrade
        {
            pl.GetComponent<PlayerController>().baseSpeed += 2f;
            pl.GetComponent<PlayerController>().sprintSpeed += 3f;
            pl.GetComponent<PlayerController>().crouchSpeed += 1f;
        }
        if((upgradeID >= 12) && (upgradeID <= 15)) //choice level
        {
            pl.GetComponent<OffhandFunctions>().choiceLevel +=1;
        }


        sceneNumber +=1;
        SceneManager.LoadScene("Scene 1");
        
    }
    
    public void onUpgrade0()
    {
        upgradeID = randomIDs[0];
        Upgrade();
        
    }

    public void onUpgrade1()
    {

        upgradeID = randomIDs[1];
        Upgrade();
        
    }

    public void onUpgrade2()
    {

        upgradeID = randomIDs[2];
        Upgrade();
        
    }

    
}
