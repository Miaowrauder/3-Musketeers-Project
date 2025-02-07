using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssemblyManager : MonoBehaviour
{
    public Transform leftPos, rightPos, featurePos, bossPos;
    public GameObject[] halfPrefab, featurePrefab0, featurePrefab1, featurePrefab2, environment1, environment2, destroyables, breakables, bossPrefab, tagSpots;
    public GameObject tagPrefab;
    public bool isBoss;
    public Material[] materials1, materials2;

    public int bossID;

    int h;



    GameObject lHalf, rHalf, feature, manager, bArena, ui, pl;
    public GameObject[] spawnSpots, enemies;
    public bool canWalkabout;
    // Awake is called even beforer the first frame update
    void Awake()
    {
        
        manager = GameObject.Find("GameManager_DND");
        ui = GameObject.FindWithTag("UImanager");
        pl = GameObject.FindWithTag("Player");
        if(!isBoss)
        {
        lHalf = Instantiate(halfPrefab[Random.Range(0, (halfPrefab.Length))], leftPos.transform.position, leftPos.transform.rotation);
        rHalf = Instantiate(halfPrefab[Random.Range(0, (halfPrefab.Length))], rightPos.transform.position, rightPos.transform.rotation);
        manager.GetComponent<GameManager>().canSpawnBoss = false;
        }
        else if(isBoss)
        {
            bossID = Random.Range(0,0);//change to match number of bosses if time permits adding multiple
            bArena = Instantiate((bossPrefab[bossID]), bossPos.transform.position, bossPos.transform.rotation);
            manager.GetComponent<GameManager>().bossLimit = 1;
            manager.GetComponent<GameManager>().bossID = bossID;
            manager.GetComponent<GameManager>().canSpawnBoss = true;
        }
    }

    void Start()
    {
    
        for (int i = 0; i < environment1.Length; i++)
        {
            environment1[i] = null; 
        }
        for (int i = 0; i < environment2.Length; i++)
        {
            environment2[i] = null; 
        }

        environment1 = (GameObject.FindGameObjectsWithTag("Environment"));
        environment2 = (GameObject.FindGameObjectsWithTag("Environment2"));
        destroyables = (GameObject.FindGameObjectsWithTag("Destroyable"));
        breakables = (GameObject.FindGameObjectsWithTag("Breakable"));

        
        manager.GetComponent<GameManager>().enterScene = true;


        if(!isBoss)
        {
            if(manager.GetComponent<GameManager>().themeID == 0)
            {
                feature = Instantiate(featurePrefab0[(Random.Range(0,featurePrefab0.Length))], featurePos.transform.position, featurePos.transform.rotation);
            }
            else if(manager.GetComponent<GameManager>().themeID == 1)
            {   
                feature = Instantiate(featurePrefab1[(Random.Range(0,featurePrefab1.Length))], featurePos.transform.position, featurePos.transform.rotation);
            }
            else if(manager.GetComponent<GameManager>().themeID == 2)
            {
                feature = Instantiate(featurePrefab2[(Random.Range(0,featurePrefab2.Length))], featurePos.transform.position, featurePos.transform.rotation);
            }

            spawnSpots = GameObject.FindGameObjectsWithTag("SpawnSpot");
        }
        


        for (int i = 0; i < environment1.Length; i++)
        {    
            environment1[i].GetComponent<Renderer>().material = materials1[(manager.GetComponent<GameManager>().themeID)];
        }
        for (int i = 0; i < environment2.Length; i++)
        {
            environment2[i].GetComponent<Renderer>().material = materials2[(manager.GetComponent<GameManager>().themeID)];
        }
        for (int i = 0; i < breakables.Length; i++)
        {    
            int p = Random.Range(0, 2);

            if(p == 0)
            {
                breakables[i].GetComponent<Breakable>().maxHp = 0;
                breakables[i].GetComponent<Breakable>().hp = 0; //chance to destroy posts and triggers elements to fall, neutralises any charge gained from destroy
                ui.GetComponent<UImanager>().musketeerCharge -= (6 + (pl.GetComponent<MusketeerAbilities>().musketeerLevel * 2));
            }
            else if(p == 1)
            {
                breakables[i].GetComponent<Renderer>().material = materials2[(manager.GetComponent<GameManager>().themeID)];
            }
            

        }
        for (int i = 0; i < destroyables.Length; i++)
        {
            int p = Random.Range(0, 10);

            if(ui.GetComponent<UImanager>().hasTrinket[1])
            {
                p = Random.Range(3, 10);
            }
            if((p >= 0) && (p <= 5))
            {
                Destroy(destroyables[i]);  //essentially 'stops' 60% of pots spawning, or ~40% if rogues map trinkety
                
            }
            else if(p > 5)
            {
                destroyables[i].GetComponent<Renderer>().material = materials2[(manager.GetComponent<GameManager>().themeID)];
            }

        }

        if((ui.GetComponent<UImanager>().hasTrinket[3] == true) && (!isBoss))
        {
            tagSpots = GameObject.FindGameObjectsWithTag("TagSpawn");
            int totalTags = 3;

            for(int i = 0; i < totalTags; i++)
            {
                int j = Random.Range(0, tagSpots.Length);
                Instantiate(tagPrefab, tagSpots[j].transform.position, Quaternion.identity);
            }
        }
        
        

    }

    // Update is called once per frame
    void Update()
    {
        if((!isBoss) && (canWalkabout == true))
        {
            canWalkabout = false;
            StartCoroutine(Walkabout());
        }
    }

    private IEnumerator Walkabout()
    {
        
        //if(enemies[0] != null)
        //{
            for(int i = 0; i < enemies.Length; i++)
            {
                enemies[i] = null;
            }
        //}
        

        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for(int i = 0; i < enemies.Length; i++)
        {
            if(enemies[i].GetComponent<EnemyMove>().canSeePlayer == false)
            {
                enemies[i].GetComponent<EnemyMove>().target = spawnSpots[Random.Range(0, spawnSpots.Length)];
            }
        }

        yield return new WaitForSeconds(8f);

        canWalkabout = true;
    }
}
