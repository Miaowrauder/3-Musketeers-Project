using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class plHealth : MonoBehaviour
{
    [Header("Incoming Melee")]
    public bool isMeleeable, isMeleeParrying, meleeParryEnabled;
    public float incomingMeleeDmg;
    public int meleeParryLevel; // used as both unlock condition >1 and upgrade condition >2
    public float meleeIframes;
    public GameObject slashPrefab;
    

    [Header("Incoming Ranged")]
    public bool isRangedable, isRangedParrying, rangedParryEnabled;
    public float incomingRangedDmg, deflectDuration;
    public int rangedParryLevel;
    public float rangedIframes;
    public GameObject deflectPrefab;

    private GameObject barrier;
    public Transform deflectPosition;

    [Header("Incoming Magic")]
    public bool isMagicable, isMagicParrying, magicParryEnabled;
    public int magicParryLevel;
    public float incomingMagicDmg;
    public float magicIframes;
    public GameObject magicProjectile;

    [Header("Misc Stats")]
    public float health;
    public float maxHealth;
    public float defenceStat;
    
    [Header("Misc")]
    public Slider healthSlider;

    public bool canCountdown, hasLetterBuff;
    public GameObject head, manager, ui;
    public GameObject[] parryIcon;
    public Transform iconPos;

    public GameObject melIcon, ranIcon, magIcon; //dont assign

    [Header("Diamond Tags")]

    public int minTags, currentTags;

    void Start()
    {
        
        ui = GameObject.FindWithTag("UImanager");
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
        if(currentTags == 10)
        {
            currentTags += 2;
            minTags += 2;
        }

        if(health > maxHealth)
        {
            health = maxHealth;
        }
        
        if (Input.GetKeyDown(KeyCode.Mouse1) && meleeParryEnabled && (meleeParryLevel > 0) && !isMeleeParrying && (this.GetComponent<cooldownManager>().meleeParryCd <= 0) && (meleeIframes == 0))
        {
            isMeleeParrying = true;
            meleeIframes = (0.3f);
            this.GetComponent<cooldownManager>().meleeParryCd = 2f;
            melIcon = Instantiate(parryIcon[0], iconPos.transform.position, Quaternion.identity);
            melIcon.transform.parent = iconPos;
        }

        if (Input.GetKeyDown(KeyCode.E) && magicParryEnabled && (magicParryLevel > 0) && !isMagicParrying && (this.GetComponent<cooldownManager>().magicParryCd <= 0) && (magicIframes == 0))
        {
            isMagicParrying = true;
            magicIframes = (0.3f);
            this.GetComponent<cooldownManager>().magicParryCd = 2f;
            magIcon = Instantiate(parryIcon[2], iconPos.transform.position, Quaternion.identity);
            magIcon.transform.parent = iconPos;
        }

        if (Input.GetKeyDown(KeyCode.Q) && rangedParryEnabled && (rangedParryLevel > 0) && !isRangedParrying && (this.GetComponent<cooldownManager>().rangedParryCd <= 0))
        {
            isRangedParrying = true;
            this.GetComponent<cooldownManager>().rangedParryCd = 6f;
            ranIcon = Instantiate(parryIcon[1], iconPos.transform.position, Quaternion.identity);
            ranIcon.transform.parent = iconPos;

            barrier = Instantiate(deflectPrefab, deflectPosition.transform.position, deflectPosition.transform.rotation);
            barrier.transform.parent = head.transform;

            barrier.GetComponent<BlockCollider>().lifespan = deflectDuration;
            
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
            Destroy(melIcon);

            if(isMeleeParrying)
            {
                isMeleeParrying = false;
            }
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
            Destroy(magIcon);

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
        if ((meleeIframes>0)||(rangedIframes>0)||(magicIframes>0)||(this.GetComponent<cooldownManager>().meleeParryCd > 0)||(this.GetComponent<cooldownManager>().magicParryCd > 0)||(this.GetComponent<cooldownManager>().rangedParryCd > 0)||(deflectDuration>0))
        {
            if (canCountdown)
            {
            StartCoroutine(Counter());
            }
        }
        
    }

    void GameOver()
    {
        Destroy(manager);
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Main Menu");
    }

    void TakeMagicDmg()
    {
        if (!isMagicParrying)
        {
            health -= (incomingMagicDmg);
            incomingMagicDmg = 0;
            isMagicable = false;

            if(currentTags > minTags)
            {
                currentTags -= 1;
            }

            if(hasLetterBuff)
            {
                hasLetterBuff = false;
                defenceStat /= 2;
                this.GetComponent<MeleeAttack>().damageStat = ((this.GetComponent<MeleeAttack>().damageStat / 3f) *2);
                this.GetComponent<PlayerController>().baseSpeed = ((this.GetComponent<PlayerController>().baseSpeed / 3f) *2);
                this.GetComponent<PlayerController>().sprintSpeed = ((this.GetComponent<PlayerController>().sprintSpeed / 3f) *2);
                this.GetComponent<PlayerController>().crouchSpeed = ((this.GetComponent<PlayerController>().crouchSpeed / 3f) *2);
            }
        
            
        }
        else if (isMagicParrying)
        {
            if(magicParryLevel > 1)
            {

                GameObject magProj = Instantiate(magicProjectile, (this.GetComponent<MeleeAttack>().hitboxPos).transform.position, (this.GetComponent<MeleeAttack>().hitboxPos).transform.rotation);
                magProj.GetComponent<Projectile>().magicDmg = (incomingMagicDmg*4);
                magProj.GetComponent<Projectile>().isHoming = true;
                magProj.GetComponent<Projectile>().moveSpeed = 2;
                magProj.GetComponent<Projectile>().lifespan = 5f;
                magProj.transform.parent = (this.GetComponent<MeleeAttack>().swordArm).transform;
                //upgraded effect
            }

            ui.GetComponent<UImanager>().musketeerCharge += (4 + this.GetComponent<MusketeerAbilities>().musketeerLevel);
            incomingMagicDmg = 0;
            isMagicParrying = false;
            this.GetComponent<cooldownManager>().magicParryCd = 6f;
      
        }
    }

     void TakeMeleeDmg()
    {
        if (!isMeleeParrying)
        {
            if(hasLetterBuff)
            {
                hasLetterBuff = false;
                defenceStat /= 2;
                this.GetComponent<MeleeAttack>().damageStat = ((this.GetComponent<MeleeAttack>().damageStat / 3f) *2);
                this.GetComponent<PlayerController>().baseSpeed = ((this.GetComponent<PlayerController>().baseSpeed / 3f) *2);
                this.GetComponent<PlayerController>().sprintSpeed = ((this.GetComponent<PlayerController>().sprintSpeed / 3f) *2);
                this.GetComponent<PlayerController>().crouchSpeed = ((this.GetComponent<PlayerController>().crouchSpeed / 3f) *2);
            }

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

        if(currentTags > minTags)
            {
                currentTags -= 1;
            }

        }

        if(isMeleeParrying)
        {

            if (meleeParryLevel > 1)
            {
                GameObject slash = Instantiate(slashPrefab, this.transform.position, this.transform.rotation);
                slash.GetComponent<DamageCollider>().meleeDmg = (incomingMeleeDmg * 3);
                slash.GetComponent<DamageCollider>().lifespan = (0.2f);
                slash.GetComponent<DamageCollider>().scale = (1.2f);
                slash.GetComponent<DamageCollider>().canBreak = true;
            }

            ui.GetComponent<UImanager>().musketeerCharge += (4 + this.GetComponent<MusketeerAbilities>().musketeerLevel);
            incomingMeleeDmg = 0;
            isMeleeParrying = false;
            this.GetComponent<cooldownManager>().meleeParryCd = 6f;
        }
    }

     void TakeRangedDmg()
    {
        if(hasLetterBuff)
            {
                hasLetterBuff = false;
                defenceStat /= 2;
                this.GetComponent<MeleeAttack>().damageStat = ((this.GetComponent<MeleeAttack>().damageStat / 3f) *2);
                this.GetComponent<PlayerController>().baseSpeed = ((this.GetComponent<PlayerController>().baseSpeed / 3f) *2);
                this.GetComponent<PlayerController>().sprintSpeed = ((this.GetComponent<PlayerController>().sprintSpeed / 3f) *2);
                this.GetComponent<PlayerController>().crouchSpeed = ((this.GetComponent<PlayerController>().crouchSpeed / 3f) *2);
            }

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

        if(currentTags > minTags)
        {
            currentTags -= 1;
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

        canCountdown = true;
    }

}
