using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UImanager : MonoBehaviour
{
    public GameObject GameM;
    public bool canLoad;

    public int sceneNumber;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if((sceneNumber == 0) && canLoad)
        {
            onUpgrade();
            canLoad = false;
        }
    }

    public void onUpgrade()
    {
        sceneNumber +=1;
        //upgrade effects
        SceneManager.LoadScene("Scene 1");
        
    }
}
