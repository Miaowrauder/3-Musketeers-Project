using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.AI.Navigation;
using TMPro;


public class GameManager : MonoBehaviour
{
    public TMP_Text infoText;

    public int lightID, mediumID, heavyID, themeID, bossID;

    public float difficultyScaling, waveCount, enemyCount;

    public bool initiateSpawn, enterScene, isTesting, canSpawnBoss, canBake;

    public GameObject[] enemySet, spawnSpots, plSpawn, bossPrefabs;

    public GameObject navmesh, pl, UI;

    public NavMeshSurface navmeshSurface;

    [Header("Game End Stuff")]

    
    GameObject assemMan;
    public bool canEnd, themeReroll;

    public int bossLimit;
    int tell;    

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        themeID = (Random.Range(0,3));
        
    }

    void NewScene()
    {
            tell = 1;
     
        

        Cursor.lockState = CursorLockMode.Locked;
        for (int j = 0; j < spawnSpots.Length; j++)
        {
            spawnSpots[j] = null; 
        }
        for (int k = 0; k < plSpawn.Length; k++)
        {
            plSpawn[k] = null; 
        }

        Bake();

        plSpawn = (GameObject.FindGameObjectsWithTag("PlayerSpawn"));
        pl = (GameObject.FindGameObjectWithTag("Player"));


        pl.GetComponent<PlayerController>().canMove = false;
        pl.transform.position = plSpawn[(Random.Range(0, plSpawn.Length))].transform.position;

        if(UI.GetComponent<UImanager>().hasTrinket[6])
        {
            //letter of introduction
            pl.GetComponent<plHealth>().hasLetterBuff = true;
            pl.GetComponent<plHealth>().defenceStat *= 2;
            pl.GetComponent<MeleeAttack>().damageStat *= 1.5f;
            pl.GetComponent<PlayerController>().baseSpeed *= 1.5f;
            pl.GetComponent<PlayerController>().sprintSpeed *= 1.5f;
            pl.GetComponent<PlayerController>().crouchSpeed *= 1.5f;
        }

        if(canSpawnBoss == false)
        {
            spawnSpots = (GameObject.FindGameObjectsWithTag("SpawnSpot"));
            waveCount = (1f + (difficultyScaling/2));
            lightID = (Random.Range(0,4));
            mediumID = (Random.Range(0,4));
            heavyID = (Random.Range(0,4));

            enemyCount = (4f + (difficultyScaling*3));
        }
        

        

        

        UI.GetComponent<UImanager>().endCanvas.enabled = false;
        UI.GetComponent<UImanager>().controlsCanvas.enabled = false;
        UI.GetComponent<UImanager>().settingsCanvas.enabled = false;
        UI.GetComponent<UImanager>().mainMenuCanvas.enabled = false;
        UI.GetComponent<UImanager>().loadingCanvas.enabled = false;
        UI.GetComponent<UImanager>().inGameCanvas.enabled = true;
        initiateSpawn = true;
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    void Bake()
    {
        navmesh = null;
        navmesh = GameObject.Find("NavMesh");
        navmeshSurface = navmesh.GetComponent<NavMeshSurface>();
        navmeshSurface.BuildNavMesh();
    }

    // Update is called once per frame
    void Update()
    {
        if(enterScene)
        {
            enterScene = false;
            NewScene();
        }
        if((lightID == mediumID) || (lightID == heavyID) || (heavyID == mediumID))
        {
            lightID = (Random.Range(0,4));
            mediumID = (Random.Range(0,4));
            heavyID = (Random.Range(0,4));
        }
        if((initiateSpawn == true) && (waveCount >= 1) && (GameObject.FindGameObjectsWithTag("Enemy").Length == 0) && !canSpawnBoss )
        {          
            StartCoroutine(WaveSpawn());
        }
        else if((initiateSpawn == true) && canSpawnBoss && (bossLimit > 0))
        {
            bossLimit -= 1;
            spawnSpots[0] = GameObject.FindWithTag("SpawnSpot");
            GameObject boss = Instantiate(bossPrefabs[bossID], spawnSpots[0].transform.position, Quaternion.identity);
        }
        if((waveCount < 1) && (GameObject.FindGameObjectsWithTag("Enemy").Length == 0) && canEnd)
        {
            End();
        }

        if(canBake)
        {
            canBake = false;
            Bake();
        }

        if(themeReroll)
        {
            themeReroll = false;
            themeID = (Random.Range(0,3));
        }
    }

    private IEnumerator WaveSpawn()
    {
        initiateSpawn = false;

        for(int i = 0; i < enemyCount; i++)
        {
            int weight = (Random.Range(0,12));

            if ((weight >= 0) && (weight <= 1))
            {
              GameObject enemy = Instantiate(enemySet[lightID], spawnSpots[Random.Range(0, spawnSpots.Length)].transform.position, Quaternion.identity);
            }
            if ((weight >= 2) && (weight <= 5))
            {
              GameObject enemy = Instantiate(enemySet[mediumID], spawnSpots[Random.Range(0, spawnSpots.Length)].transform.position, Quaternion.identity);
            }
            if ((weight >= 6) && (weight <= 11))
            {
              GameObject enemy = Instantiate(enemySet[heavyID], spawnSpots[Random.Range(0, spawnSpots.Length)].transform.position, Quaternion.identity);
            }
            
            yield return new WaitForSeconds((6f/difficultyScaling));

            if(tell > 0)
            {
                tell--;
                assemMan = GameObject.Find("Environment");
                assemMan.GetComponent<AssemblyManager>().canWalkabout = true;
            }
            
        }

        waveCount -= 1;

        if(waveCount >= 1)
        {
            initiateSpawn = true;
        }

        if(waveCount < 1f)
        {
        canEnd = true;
        }
    }

    void End()
    {
        canEnd = false;

        if(canSpawnBoss)
        {
        difficultyScaling += 0.1f;
        difficultyScaling *= 1.2f;
        UI.GetComponent<UImanager>().endCanvas.enabled = true;
        UI.GetComponent<UImanager>().inGameCanvas.enabled = false;
        UI.GetComponent<UImanager>().reroll = true;
        UI.GetComponent<UImanager>().o = 30;
        Cursor.lockState = CursorLockMode.None;
        themeID = (Random.Range(0,3));
        }
        else if(!canSpawnBoss)
        {
            UI.GetComponent<UImanager>().instantEnd = true;
        }
        
        
        
        
        

        //letter of introduction
        if(UI.GetComponent<UImanager>().hasTrinket[6] && pl.GetComponent<plHealth>().hasLetterBuff)
        {
        pl.GetComponent<plHealth>().hasLetterBuff = false;
        pl.GetComponent<plHealth>().defenceStat /= 2;
        pl.GetComponent<MeleeAttack>().damageStat = ((pl.GetComponent<MeleeAttack>().damageStat / 3f) *2f );
        pl.GetComponent<PlayerController>().baseSpeed = ((pl.GetComponent<PlayerController>().baseSpeed / 3f) *2f );
        pl.GetComponent<PlayerController>().sprintSpeed = ((pl.GetComponent<PlayerController>().sprintSpeed / 3f) *2f );
        pl.GetComponent<PlayerController>().crouchSpeed = ((pl.GetComponent<PlayerController>().crouchSpeed / 3f) *2f );
        }
    }

}
