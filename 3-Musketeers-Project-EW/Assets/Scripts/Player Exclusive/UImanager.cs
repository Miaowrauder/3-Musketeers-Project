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

    public int o = 0; //allows to loop parry reroll a few times

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
            randomIDs[0] = Random.Range(0, 5);
            randomIDs[1] = Random.Range(6, 10);
            randomIDs[2] = Random.Range(11, 16);
            randomIDs[3] = Random.Range(24, 34);
            
            //not a refined reroll system but, well melee parry was meant to be weaker or something anyway so lower rates is a fine outcome for me...
           if((randomIDs[0] == 0) && (pl.GetComponent<plHealth>().meleeParryLevel == 2))
           {
             randomIDs[0] += 1;
           }
           if((randomIDs[0] == 1) && (pl.GetComponent<plHealth>().rangedParryLevel == 2))
           {
             randomIDs[0] += 1;
           }
           if((randomIDs[0] == 2) && (pl.GetComponent<plHealth>().magicParryLevel == 2))
           {
             randomIDs[0] += 1;
           }

            reroll = false;
            canText = true;
 

           if(canText && (reroll == false))
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

        //ranged parry unlock
        else if(randomIDs[0] == 1)
        {
            if(pl.GetComponent<plHealth>().rangedParryLevel == 0)
            {
            buttonString[0] = "Access Thy Ranged Parry";
            }
            else if(pl.GetComponent<plHealth>().rangedParryLevel == 1)
            {
            buttonString[0] = "Evolve Thy Ranged Parry To Reflect!";
            }
        }

        //magic parry unlock
        else if(randomIDs[0] == 2)
        {
            if(pl.GetComponent<plHealth>().magicParryLevel == 0)
            {
            buttonString[0] = "Access Thy Magic Parry";
            }
            else if(pl.GetComponent<plHealth>().magicParryLevel == 1)
            {
            buttonString[0] = "Evolve Thy Magic Parry With A Follow-up!";
            }
        }

        //choice level
        else if(randomIDs[0] == 3)
        {
            buttonString[0] = "Enhance Thy Conjured Items";
        }
        else if(randomIDs[0] == 4)
        {
            buttonString[0] = "Enhance Thy Conjured Items";
        }

        //musketeer level
        else if(randomIDs[0] == 5)
        {
            buttonString[0] = "Enhance Thy Musketeer Connection";
        }

        //health upgrade
        if(randomIDs[1] == 6)
        {
            buttonString[1] = "Improve & Restore Thy Good Health";
        }

        //attack speed
        else if(randomIDs[1] == 7)
        {
            buttonString[1] = "Quicken The Pace Of Thy Strikes";
        }

        //defence stat
        else if(randomIDs[1] == 8)
        {
            buttonString[1] = "Fortify Thy Bodily Defences";
        }

         //melee stat
        else if(randomIDs[1] == 9)
        {
            buttonString[1] = "Sharpen Thy Thrusting Blade";
        }

        //move speed
        else if(randomIDs[1] == 10)
        {
            buttonString[1] = "Hasten Thy Movement";
        }


        //burn pouch
        if(randomIDs[2] == 11)
        {
            buttonString[2] = "Choice Ability - Pouch Of Flame";
        }

        //poison pouch
        else if(randomIDs[2] == 12)
        {
            buttonString[2] = "Choice Ability - Pouch Of Pestilence";
        }

        //cleanse salve
        else if(randomIDs[2] == 13)
        {
            buttonString[2] = "Choice Ability - Salve Of Cleansing";
        }

        //regen salve
        else if(randomIDs[2] == 14)
        {
            buttonString[2] = "Choice Ability - Salve Of Rejuvenation";
        }

        else if(randomIDs[2] == 15)
        {
            buttonString[2] = "Choice Ability - Standard-Issue Musket";
        }
        else if(randomIDs[2] == 16)
        {
            buttonString[2] = "Choice Ability - Mages Musket";
        }

        //lucky buckle
        if(randomIDs[3] == 24)
        {
            buttonString[3] = "Trinket - Lucky Buckle";
        }

        //rogues map
        else if(randomIDs[3] == 25)
        {
            buttonString[3] = "Trinket - Scoundrel's Map";
        }

        //artillery gloves
        else if(randomIDs[3] == 26)
        {
            buttonString[3] = "Trinket - Artillery Gloves";
        }

        //diamond tags
        else if(randomIDs[3] == 27)
        {
            buttonString[3] = "Trinket - Diamond Compass";
        }

        //gifted ring
        else if(randomIDs[3] == 28)
        {
            buttonString[3] = "Trinket - Gifted Power Ring";
        }
        
        //tainted ring
        else if(randomIDs[3] == 29)
        {
            buttonString[3] = "Trinket - Tainted Power Ring";
        }

        //letter
        else if(randomIDs[3] == 30)
        {
            buttonString[3] = "Trinket - Letter of Introduction";
        }

        //anti burn
        else if(randomIDs[3] == 31)
        {
            buttonString[3] = "Trinket - Medicinal Burn Paste";
        }

        //anti poison
        else if(randomIDs[3] == 32)
        {
            buttonString[3] = "Trinket - Herbal Antivenom";
        }

        //anti weakness
        else if(randomIDs[3] == 33)
        {
            buttonString[3] = "Trinket - Strongman's Spinach";
        }

        //anti slowness
        else if(randomIDs[3] == 33)
        {
            buttonString[3] = "Trinket - ANTI-SLOWNESS-TRINKET";
        }
     
     

    


        buttonText[0].text = buttonString[0];
        buttonText[1].text = buttonString[1];
        buttonText[2].text = buttonString[2];
        buttonText[3].text = buttonString[3];

    }

    public void Upgrade()
    {
        if(upgradeID == 0) //melee parry upgrade
        {
            pl.GetComponent<plHealth>().meleeParryLevel +=1;
        }
        else if(upgradeID == 1) //ranged parry upgrade
        {
            pl.GetComponent<plHealth>().rangedParryLevel +=1;
        }
        else if(upgradeID == 2) //magic parry upgrade
        {
            pl.GetComponent<plHealth>().magicParryLevel +=1;
        }
        else if((upgradeID == 3) || (upgradeID == 4)) //choice level
        {
            pl.GetComponent<OffhandFunctions>().choiceLevel +=1;
        }
        else if(upgradeID == 5) //musketeer level
        {
           //Effect be added~ 
        }
        else if(upgradeID == 6) //health upgrade, full heal
        {
            pl.GetComponent<plHealth>().maxHealth += (30f * GameM.GetComponent<GameManager>().difficultyScaling);
            pl.GetComponent<plHealth>().health = pl.GetComponent<plHealth>().maxHealth;
            pl.GetComponent<plHealth>().healthSlider.maxValue = pl.GetComponent<plHealth>().health;

        }
        else if(upgradeID == 7) //attack speed
        {
            pl.GetComponent<MeleeAttack>().attackSpeedStat *= 0.8f;
        }
        else if(upgradeID == 8) //defense upgrade
        {
            pl.GetComponent<plHealth>().defenceStat += (3f * GameM.GetComponent<GameManager>().difficultyScaling);
        }
        else if(upgradeID == 9) //melee damage
        {
            pl.GetComponent<MeleeAttack>().damageStat += (10f * GameM.GetComponent<GameManager>().difficultyScaling);
        }
        else if(upgradeID == 10) //speed upgrade
        {
            pl.GetComponent<PlayerController>().baseSpeed += 2f;
            pl.GetComponent<PlayerController>().sprintSpeed += 3f;
            pl.GetComponent<PlayerController>().crouchSpeed += 1f;
        }
        else if(upgradeID == 11) //burn pouch
        {
            pl.GetComponent<OffhandFunctions>().abilityID = 0;  
        }
        else if(upgradeID == 12) //poison pouch
        {
            pl.GetComponent<OffhandFunctions>().abilityID = 1;  
        }
        else if(upgradeID == 13) //cleanse salve
        {
            pl.GetComponent<OffhandFunctions>().abilityID = 2;  
        }
        else if(upgradeID == 14) //regen salve
        {
            pl.GetComponent<OffhandFunctions>().abilityID = 3;  
        }
        else if(upgradeID == 15) //normal musket
        {
            pl.GetComponent<OffhandFunctions>().abilityID = 4;  
        }
        else if(upgradeID == 16) //magic musket
        {
            pl.GetComponent<OffhandFunctions>().abilityID = 5;  
        }
        else if(upgradeID == 24) //buckle
        {
            for(int i = 0; i < hasTrinket.Length; i++)
            {
                hasTrinket[i] = false;
            }
            hasTrinket[0] = true;
        }
        else if(upgradeID == 25) //map
        {
            for(int i = 0; i < hasTrinket.Length; i++)
            {
                hasTrinket[i] = false;
            }
            hasTrinket[1] = true;
        }
        else if(upgradeID == 26) //glove
        {
            for(int i = 0; i < hasTrinket.Length; i++)
            {
                hasTrinket[i] = false;
            }
            hasTrinket[2] = true;
        }
        else if(upgradeID == 27) //tags
        {
            for(int i = 0; i < hasTrinket.Length; i++)
            {
                hasTrinket[i] = false;
            }
            hasTrinket[3] = true;
        }
        else if(upgradeID == 28) //good ring
        {
            for(int i = 0; i < hasTrinket.Length; i++)
            {
                hasTrinket[i] = false;
            }
            hasTrinket[4] = true;
        }
        else if(upgradeID == 29) //bad ring
        {
            for(int i = 0; i < hasTrinket.Length; i++)
            {
                hasTrinket[i] = false;
            }
            hasTrinket[5] = true;
        }
        else if(upgradeID == 30) //letter
        {
            for(int i = 0; i < hasTrinket.Length; i++)
            {
                hasTrinket[i] = false;
            }
            hasTrinket[6] = true;
        }
        else if(upgradeID == 31) //anti burn
        {
            for(int i = 0; i < hasTrinket.Length; i++)
            {
                hasTrinket[i] = false;
            }
            hasTrinket[7] = true;
        }
        else if(upgradeID == 32) //anti poison
        {
            for(int i = 0; i < hasTrinket.Length; i++)
            {
                hasTrinket[i] = false;
            }
            hasTrinket[8] = true;
        }
        else if(upgradeID == 33) //anti weak
        {
            for(int i = 0; i < hasTrinket.Length; i++)
            {
                hasTrinket[i] = false;
            }
            hasTrinket[9] = true;
        }
        else if(upgradeID == 34) //anti slow
        {
            for(int i = 0; i < hasTrinket.Length; i++)
            {
                hasTrinket[i] = false;
            }
            hasTrinket[10] = true;
        }

        sceneNumber +=1;
        if(sceneNumber%3 == 1)
        {
            SceneManager.LoadScene("Scene 2");
        }
        else if(sceneNumber%3 != 1)
        {
            SceneManager.LoadScene("Scene 1");
        }
        
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

    public void onUpgrade3()
    {

        upgradeID = randomIDs[3];
        Upgrade();
        
    }

    
}
