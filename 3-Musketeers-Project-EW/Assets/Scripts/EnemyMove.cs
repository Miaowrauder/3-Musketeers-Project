using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    public float attackDelay, iFrames, attackDistance;
    public bool mainhandEnabled, offhandEnabled, isRanged, isMagic, isMelee, isGrenadier, isAttacking;
    
    public Transform shootingPos;

    [Header("Ranged")]
    public float bulletSpeed;
    public float bulletDamage;
    public GameObject musketBall;
    
    [Header("Grenadier")]
    public float throwForce;
    public GameObject[] grenade;

    [Header("Melee")]

    public Transform[] swordPos;
    public Transform slashPos;
    public GameObject sword, slashCollider;
    public float swordDmg, slashDuration;
    private int flip;

    [Header("Magic")]

    public GameObject magicThing;
    


    GameObject plObject;

    public GameObject head;

    NavMeshAgent navAgent;
 
    // Start is called before the first frame update
    void Start()
    {
        mainhandEnabled = true;
        offhandEnabled = true;
        plObject = GameObject.FindWithTag("Player");
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        head.transform.LookAt(plObject.transform.position);
        Move();

            if(!isAttacking)
            {

            if (Vector3.Distance(plObject.transform.position, transform.position) <= attackDistance)
            {
                StartCoroutine(Attack());
            }
            
            }
            
        
    }

    void Move()
    {
        navAgent.destination = plObject.transform.position;
    }

    private IEnumerator Attack()
    {
        isAttacking = true;

        yield return new WaitForSeconds(attackDelay);

        if(isRanged && mainhandEnabled)
        {
            GameObject proj = Instantiate(musketBall, shootingPos.transform.position, shootingPos.transform.rotation);
            proj.GetComponent<Projectile>().moveSpeed = bulletSpeed;
            proj.GetComponent<Projectile>().rangedDmg = bulletDamage;
            proj.GetComponent<Projectile>().lifespan = 5f;
            proj.GetComponent<Projectile>().appliedIframes = 0.05f;
            proj.GetComponent<Projectile>().isEnemy = true;
        }
        if(isGrenadier && (mainhandEnabled || offhandEnabled))
        {
            GameObject proj2 = Instantiate(grenade[Random.Range(0,2)], shootingPos.transform.position, shootingPos.transform.rotation);
            proj2.gameObject.transform.parent = shootingPos;
            proj2.GetComponent<Rigidbody>().isKinematic = true;
            yield return new WaitForSeconds(attackDelay/2f);
            proj2.GetComponent<Grenade>().isPrimed = true;
            proj2.GetComponent<Rigidbody>().isKinematic = false;
            proj2.gameObject.transform.parent = null;
            proj2.gameObject.GetComponent<Rigidbody>().AddForce(proj2.transform.forward * throwForce, ForceMode.Impulse);    
            proj2.gameObject.GetComponent<Rigidbody>().AddForce(proj2.transform.up * (throwForce), ForceMode.Impulse);
        }
        if(isMelee && mainhandEnabled)
        {
            flip += 1;

            GameObject slash = Instantiate(slashCollider, slashPos.transform.position, slashPos.transform.rotation);
            slash.GetComponent<DamageCollider>().isEnemy = true;
            slash.GetComponent<DamageCollider>().meleeDmg = swordDmg;
            slash.GetComponent<DamageCollider>().lifespan = slashDuration;


            if(flip%2 == 0)
            {
                sword.transform.position = swordPos[0].transform.position;
                sword.transform.rotation = swordPos[0].transform.rotation;
            }
            else 
            {
                sword.transform.position = swordPos[1].transform.position;
                sword.transform.rotation = swordPos[1].transform.rotation;
            }
                
            
        }
        if(isMagic && mainhandEnabled)
        {
            GameObject magic = Instantiate(magicThing, plObject.transform.position, Quaternion.identity);
            magic.GetComponent<Grenade>().isPrimed = true;
            magic.transform.parent = null;
        }

        

        isAttacking = false;

    }

}
