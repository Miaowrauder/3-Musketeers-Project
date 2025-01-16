using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    public float damageAmount, attackDelay, iFrames;
    public bool mainhandEnabled, offhandEnabled, isRanged, isMagic, isMelee, isGrenadier, isAttacking;
    public GameObject musketBall;
    public Transform shootingPos;

    [Header("RangedStats")]
    public float bulletSpeed;
    public float bulletDamage;


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

            if(mainhandEnabled && !isAttacking)
            {
            StartCoroutine(Attack());
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

        if(isRanged)
        {
            GameObject proj = Instantiate(musketBall, shootingPos.transform.position, shootingPos.transform.rotation);
            proj.GetComponent<Projectile>().moveSpeed = bulletSpeed;
            proj.GetComponent<Projectile>().rangedDmg = bulletDamage;
            proj.GetComponent<Projectile>().lifespan = 5f;
            proj.GetComponent<Projectile>().appliedIframes = 0.05f;
            proj.GetComponent<Projectile>().isEnemy = true;
        }

        

        isAttacking = false;
        //plObject.GetComponent<plHealth>().incomingMeleeDmg = damageAmount;

        //if ((plObject.GetComponent<plHealth>().meleeIframes <= 0f) && (plObject.GetComponent<plHealth>().isMeleeParrying == false))
        //{
        //plObject.GetComponent<plHealth>().meleeIframes = iFrames;
        //}
        //attackDelay = Time.time + attackRate;
    }

}
