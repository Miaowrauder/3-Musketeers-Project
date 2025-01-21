using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UImanager : MonoBehaviour
{
    public GameObject GameM, pl;
    public bool canLoad, reroll;

    public int sceneNumber, upgradeID;

    public int[] randomIDs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if((sceneNumber == 0) && canLoad)
        {
            OnEnd();
            canLoad = false;
        }

        if(reroll)
        {
            //randomIDs[0] = (Random.Range())
        }


    }

    public void OnEnd()
    {

        sceneNumber +=1;
        //upgrade effects
        SceneManager.LoadScene("Scene 1");
        
    }

    public void Upgrade()
    {

        sceneNumber +=1;
        //upgrade effects
        SceneManager.LoadScene("Scene 1");
        
    }
    public void onUpgrade1()
    {

        
        
    }

    public void onUpgrade2()
    {

        sceneNumber +=1;
        //upgrade effects
        SceneManager.LoadScene("Scene 1");
        
    }

    public void onUpgrade3()
    {

        sceneNumber +=1;
        //upgrade effects
        SceneManager.LoadScene("Scene 1");
        
    }

    
}
