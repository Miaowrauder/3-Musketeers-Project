using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChessMove : MonoBehaviour
{
    public float damageAmount, attackDelay, attackRate, attackDistance, health;

    GameObject plObject;

    public float moveDuration, moveDelay;

    public Transform[] moveLocations;
    public int currentLocationCheck;
    public int chosenLocation;
    public float[] savedDistances;


    public bool isChecking;
    public bool canMove;

    NavMeshAgent navAgent;

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        isChecking = true;
        plObject = GameObject.FindWithTag("Player");
        navAgent = GetComponent<NavMeshAgent>();
        currentLocationCheck = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            
            if ((currentLocationCheck <= moveLocations.Length) && isChecking)
            {
                LocationCheck();
            }
           
        }

        if(currentLocationCheck == moveLocations.Length)
        {
            FindMinimumDistance();
            StartCoroutine(Move());
        }

        if (Time.time > attackDelay)
        {
            if (Vector3.Distance(plObject.transform.position, transform.position) <= attackDistance)
            {
                Attack();
            }
        }
    }

    void LocationCheck()
    {
       

        if (currentLocationCheck == moveLocations.Length)
        {
            isChecking = false;
        }

         //saves the distance between a location and the player in an array
        savedDistances[currentLocationCheck] = Vector3.Distance(moveLocations[currentLocationCheck].position, plObject.transform.position);

        if (currentLocationCheck < moveLocations.Length)
        {
            currentLocationCheck += 1;
        }
        

    }

    void FindMinimumDistance()
    {
        int pos = 0;
        for (int i = 0; i < savedDistances.Length; i++)
        {
            if (savedDistances[i] < savedDistances[pos]) { pos = i; }
            chosenLocation = i;
        }
      
    }

    IEnumerator Move()
    {
        canMove = false;
        isChecking = false;
        navAgent.destination = moveLocations[chosenLocation].position;
        yield return new WaitForSeconds(moveDuration);
        navAgent.destination = moveLocations[0].position;
        yield return new WaitForSeconds(moveDelay);
        canMove = true;
        isChecking = true;
        currentLocationCheck = 0;
    }

    void Attack()
    {
        plObject.GetComponent<plHealth>().health -= damageAmount;
        attackDelay = Time.time + attackRate;
    }

    


}