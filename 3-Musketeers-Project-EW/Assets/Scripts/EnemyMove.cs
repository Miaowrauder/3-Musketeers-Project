using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    public float damageAmount, attackDelay, attackRate, attackDistance, health, iFrames;
    public bool mainhandEnabled, offhandEnabled;

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

        // to be redone
        if (Time.time > attackDelay)
        {
            if (Vector3.Distance(plObject.transform.position, transform.position) <= attackDistance)
            {
                if(mainhandEnabled)
                {
                Attack();
                }
            }
        }
    }

    void Move()
    {
        navAgent.destination = plObject.transform.position;
    }

    void Attack()
    {
        plObject.GetComponent<plHealth>().incomingMeleeDmg = damageAmount;

        if ((plObject.GetComponent<plHealth>().meleeIframes <= 0f) && (plObject.GetComponent<plHealth>().isMeleeParrying == false))
        {
        plObject.GetComponent<plHealth>().meleeIframes = iFrames;
        }
        attackDelay = Time.time + attackRate;
    }
}
