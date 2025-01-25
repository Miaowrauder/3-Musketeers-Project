using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class cooldownManager : MonoBehaviour
{

    public float choiceCD;
    bool isTicking;

    public TMP_Text[] cdText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if((choiceCD > 0) && !isTicking)
        {
            StartCoroutine(TickDown());
        }
        if(choiceCD < 0)
        {
            choiceCD = 0f;
        }

    }

    private IEnumerator TickDown()
    {
        isTicking = true;
        yield return new WaitForSeconds(0.1f);
        if(choiceCD > 0)
        {
            choiceCD -= 0.1f;
            cdText[0].text = " " + choiceCD + " ";
        }
        isTicking = false;

    }
}
