using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public GameObject swordArm;
    public Transform[] pullPos;
    public bool currentlyAttacking;
    [Header("Is Enabled Checks")]
    public bool attackEnabled;
    public bool mainhandEnabled;
    [Header("Hitbox Bits")]
    public float hitboxScale;
    public GameObject hitboxPrefab;
    public Transform hitboxPos;
    [Header("PlayerStats")]
    public float damageStat;
    public float attackSpeedStat;
    [Header("Misc")]
    public GameObject ui;

    public bool porthosAb;

    

    // Start is called before the first frame update
    void Start()
    {
        ui = GameObject.FindWithTag("UImanager");
        attackEnabled = true;
        mainhandEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && (currentlyAttacking == false) && (attackEnabled) && (mainhandEnabled))
        {
            StartCoroutine(Attack());
        }
    }

    public IEnumerator Attack()

    {
        currentlyAttacking = true;

        swordArm.transform.position = (pullPos[0].transform.position);
        yield return new WaitForSeconds(attackSpeedStat * 0.1f);
        swordArm.transform.position = (pullPos[1].transform.position);
        
        GameObject hitbox = Instantiate(hitboxPrefab, hitboxPos.transform.position, hitboxPos.transform.rotation);
        hitbox.transform.parent = swordArm.transform;

        if(ui.GetComponent<UImanager>().hasTrinket[4])
        {
            if(ui.GetComponent<UImanager>().hasTrinket[9]) //inverts weakness
            {
                hitbox.GetComponent<DamageCollider>().meleeDmg = (((damageStat + 15f + (this.GetComponent<plHealth>().currentTags*3))*0.7f) / this.GetComponent<StatusManager>().DotDmg[5]);
                hitbox.GetComponent<DamageCollider>().magicDmg = ((damageStat + 15f + (this.GetComponent<plHealth>().currentTags*3))*0.3f);
            }
            else
            {
                hitbox.GetComponent<DamageCollider>().meleeDmg = (((damageStat + 15f + (this.GetComponent<plHealth>().currentTags*3))*0.7f) * this.GetComponent<StatusManager>().DotDmg[5]);
                hitbox.GetComponent<DamageCollider>().magicDmg = ((damageStat + 15f + (this.GetComponent<plHealth>().currentTags*3))*0.3f);
            }
            
        }
        else if(ui.GetComponent<UImanager>().hasTrinket[5])
        {
            hitbox.GetComponent<DamageCollider>().magicDmg = (damageStat + 15f);
        }
        else 
        {
            if(ui.GetComponent<UImanager>().hasTrinket[9]) //inverts weakness
            {
                hitbox.GetComponent<DamageCollider>().meleeDmg = ((damageStat + 15f + (this.GetComponent<plHealth>().currentTags*3)) / this.GetComponent<StatusManager>().DotDmg[5]);
            }
            else
            {
                hitbox.GetComponent<DamageCollider>().meleeDmg = ((damageStat + 15f + (this.GetComponent<plHealth>().currentTags*3)) * this.GetComponent<StatusManager>().DotDmg[5]);
            }
            
        }

        if(porthosAb)
        {
            hitbox.GetComponent<DamageCollider>().meleeDmg *= 1.5f;
            hitbox.GetComponent<DamageCollider>().magicDmg *= 1.5f;
        }
        
        hitbox.GetComponent<DamageCollider>().lifespan = (attackSpeedStat * 0.2f);
        hitbox.GetComponent<DamageCollider>().scale = (hitboxScale);
        hitbox.GetComponent<DamageCollider>().canBreak = true;

        if(porthosAb)
        {

        }

        this.GetComponent<PlayerInteractions>().castTime = true;

        if(porthosAb)
        {
            yield return new WaitForSeconds(attackSpeedStat * 0.15f);
        }
        else if (!porthosAb)
        {
            yield return new WaitForSeconds(attackSpeedStat * 0.2f);
        }
        

        swordArm.transform.position = (pullPos[2].transform.position);

        if(porthosAb)
        {
            yield return new WaitForSeconds(attackSpeedStat * 0.075f);
        }
        else if (!porthosAb)
        {
            yield return new WaitForSeconds(attackSpeedStat * 0.1f);
        }

        currentlyAttacking = false;

    }
}