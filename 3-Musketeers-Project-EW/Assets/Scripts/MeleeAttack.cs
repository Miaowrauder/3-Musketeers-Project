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

    

    // Start is called before the first frame update
    void Start()
    {
        attackEnabled = true;
        mainhandEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && (currentlyAttacking == false) && (attackEnabled) && (mainhandEnabled))
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
        hitbox.GetComponent<DamageCollider>().meleeDmg = (damageStat + 15f);
        hitbox.GetComponent<DamageCollider>().lifespan = (attackSpeedStat * 0.2f);
        hitbox.GetComponent<DamageCollider>().scale = (hitboxScale);
        hitbox.GetComponent<DamageCollider>().canBreak = true;

        yield return new WaitForSeconds(attackSpeedStat * 0.2f);

        swordArm.transform.position = (pullPos[2].transform.position);

        yield return new WaitForSeconds(attackSpeedStat * 0.1f);
        currentlyAttacking = false;

    }
}