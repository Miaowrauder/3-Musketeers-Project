using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingTools : MonoBehaviour
{
    public GameObject killbox, pl;
    public bool killAll;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(killAll)
        {
            killAll = false;
            GameObject box = Instantiate(killbox, pl.transform.position, pl.transform.rotation);
        }
    }
}
