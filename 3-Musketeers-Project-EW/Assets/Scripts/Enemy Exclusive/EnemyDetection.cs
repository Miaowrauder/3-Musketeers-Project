using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public float visionRange, visionAngle, forgetTime;
    public GameObject player;
    public Transform[] playerCorners;
    public GameObject[] spawnSpots;
    bool canCast, canRunMemory;
    public GameObject enemySelf;
    public LayerMask layerMask;
    RaycastHit hit;

    int i;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        canRunMemory = true;
        spawnSpots = GameObject.FindGameObjectsWithTag("SpawnSpot");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetDirection = player.transform.position - this.transform.position;
        float angle = Vector3.Angle(targetDirection, this.transform.forward);

        if (angle < visionAngle)
        {
            canCast = true;
        }

        if(canCast && (enemySelf.GetComponent<EnemyMove>().canSeePlayer == false))
        {
            canCast = false;
            SeeCast();
        }

        if(enemySelf.GetComponent<EnemyMove>().canSeePlayer == true)
        {
            if(canRunMemory)
            {
                StartCoroutine(Memory());
            }

        }

        
    }

    void SeeCast()
    {
        Vector3 castDirection = (player.transform.position - this.transform.position);

        if(Physics.Raycast(this.transform.position, castDirection, out hit, visionRange, layerMask, QueryTriggerInteraction.Ignore))
        {
            if((hit.collider.gameObject.CompareTag("Player")))
            {
                
                enemySelf.GetComponent<EnemyMove>().canSeePlayer = true;
            }
                
        }
    }

    private IEnumerator Memory()
    {
        canRunMemory = false;
        yield return new WaitForSeconds(forgetTime);
        enemySelf.GetComponent<EnemyMove>().canSeePlayer = false;
        canRunMemory = true;
    }
}
