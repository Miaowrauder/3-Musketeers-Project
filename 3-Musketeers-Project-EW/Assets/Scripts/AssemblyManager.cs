using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssemblyManager : MonoBehaviour
{
    public Transform leftPos, rightPos, featurePos;
    public GameObject[] halfPrefab, featurePrefab;

    GameObject lHalf, rHalf, feature, manager;
    // Awake is called even beforer the first frame update
    void Awake()
    {
        lHalf = Instantiate(halfPrefab[Random.Range(0, (halfPrefab.Length))], leftPos.transform.position, leftPos.transform.rotation);
        rHalf = Instantiate(halfPrefab[Random.Range(0, (halfPrefab.Length))], rightPos.transform.position, rightPos.transform.rotation);
        feature = Instantiate(featurePrefab[Random.Range(0, (featurePrefab.Length))], featurePos.transform.position, featurePos.transform.rotation);
    }

    void Start()
    {
        manager = GameObject.Find("GameManager_DND");
        manager.GetComponent<GameManager>().enterScene = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
