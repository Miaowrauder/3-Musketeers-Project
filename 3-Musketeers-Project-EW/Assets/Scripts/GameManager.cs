using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.AI.Navigation;
using TMPro;


public class GameManager : MonoBehaviour
{
    public TMP_Text infoText;

    public int lightID, mediumID, heavyID, themeID;

    public float difficultyScaling, waveCount, enemyCount;

    public bool intiateSpawn, enterScene, isTesting;

    public GameObject[] enemySet, spawnSpots, plSpawn;

    public GameObject navmesh, pl, UI;

    public NavMeshSurface navmeshSurface;

    [Header("Game End Stuff")]

    public Canvas endCanvas, inGameCanvas;

    bool canEnd;
    

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        endCanvas.enabled = false;
        inGameCanvas.enabled = true;
        themeID = (Random.Range(0,3));

        if(!isTesting)
        {
        UI.GetComponent<UImanager>().canLoad = true;
        }
        
    }

    void NewScene()
    {
        

        for (int j = 0; j < spawnSpots.Length; j++)
        {
            spawnSpots[j] = null; 
        }
        for (int k = 0; k < plSpawn.Length; k++)
        {
            plSpawn[k] = null; 
        }

        navmesh = GameObject.Find("NavMesh");
        navmeshSurface = navmesh.GetComponent<NavMeshSurface>();
        navmeshSurface.BuildNavMesh();

        plSpawn = (GameObject.FindGameObjectsWithTag("PlayerSpawn"));
        pl = (GameObject.FindGameObjectWithTag("Player"));


        pl.GetComponent<PlayerController>().canMove = false;
        pl.transform.position = plSpawn[(Random.Range(0, plSpawn.Length))].transform.position;


        spawnSpots = (GameObject.FindGameObjectsWithTag("SpawnSpot"));

        

        waveCount = (1f + (difficultyScaling/2));
        lightID = (Random.Range(0,4));
        mediumID = (Random.Range(0,4));
        heavyID = (Random.Range(0,4));

        enemyCount = (5f + difficultyScaling);

        endCanvas.enabled = false;
        inGameCanvas.enabled = true;
        intiateSpawn = true;
        Cursor.lockState = CursorLockMode.Locked;
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
        if((intiateSpawn == true) && (waveCount >= 1) && (GameObject.FindGameObjectsWithTag("Enemy").Length == 0))
        {          
            
            StartCoroutine(WaveSpawn());
        }
        if((waveCount < 1) && (GameObject.FindGameObjectsWithTag("Enemy").Length == 0) && canEnd)
        {
            End();
        }
    }

    private IEnumerator WaveSpawn()
    {
        intiateSpawn = false;

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
            
            yield return new WaitForSeconds((8f/difficultyScaling));
            
        }

        waveCount -= 1;

        if(waveCount < 1f)
        {
        canEnd = true;
        }
        intiateSpawn = false;
    }

    void End()
    {
        canEnd = false;
        difficultyScaling += 0.1f;
        difficultyScaling *= 1.2f;
        endCanvas.enabled = true;
        inGameCanvas.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        UI.GetComponent<UImanager>().canLoad = true;
        UI.GetComponent<UImanager>().reroll = true;
    }

}
