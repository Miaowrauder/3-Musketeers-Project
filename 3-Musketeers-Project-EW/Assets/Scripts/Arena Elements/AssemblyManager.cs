using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssemblyManager : MonoBehaviour
{
    public Transform leftPos, rightPos, featurePos, bossPos;
    public GameObject[] halfPrefab, featurePrefab0, featurePrefab1, featurePrefab2, environment1, environment2, bossArena;
    public bool isBoss;
    public Material[] materials1, materials2;

    public int bossID;



    GameObject lHalf, rHalf, feature, manager, bArena;
    // Awake is called even beforer the first frame update
    void Awake()
    {
        if(!isBoss)
        {
        lHalf = Instantiate(halfPrefab[Random.Range(0, (halfPrefab.Length))], leftPos.transform.position, leftPos.transform.rotation);
        rHalf = Instantiate(halfPrefab[Random.Range(0, (halfPrefab.Length))], rightPos.transform.position, rightPos.transform.rotation);
        }
        else if(isBoss)
        {
            bossID = Random.Range(0,0);//change to match number of bosses if time permits adding multiple
            bArena = Instantiate((bossArena[bossID]), bossPos.transform.position, bossPos.transform.rotation);
            manager.GetComponent<GameManager>().bossID = bossID;
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

        manager = GameObject.Find("GameManager_DND");
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
        }
        


        for (int i = 0; i < environment1.Length; i++)
        {    
            environment1[i].GetComponent<Renderer>().material = materials1[(manager.GetComponent<GameManager>().themeID)];
        }
        for (int i = 0; i < environment2.Length; i++)
        {
            environment2[i].GetComponent<Renderer>().material = materials2[(manager.GetComponent<GameManager>().themeID)];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
