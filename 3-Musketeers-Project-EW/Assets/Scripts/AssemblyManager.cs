using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssemblyManager : MonoBehaviour
{
    public Transform leftPos, rightPos, featurePos;
    public GameObject[] halfPrefab, featurePrefab, environment1, environment2;

    public Material[] materials1, materials2;




    GameObject lHalf, rHalf, feature, manager;
    // Awake is called even beforer the first frame update
    void Awake()
    {
        lHalf = Instantiate(halfPrefab[Random.Range(0, (halfPrefab.Length))], leftPos.transform.position, leftPos.transform.rotation);
        rHalf = Instantiate(halfPrefab[Random.Range(0, (halfPrefab.Length))], rightPos.transform.position, rightPos.transform.rotation);
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
        feature = Instantiate(featurePrefab[(manager.GetComponent<GameManager>().themeID)], featurePos.transform.position, featurePos.transform.rotation);


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
