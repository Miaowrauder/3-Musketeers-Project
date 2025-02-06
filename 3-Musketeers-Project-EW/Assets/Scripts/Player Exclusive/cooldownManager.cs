using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class cooldownManager : MonoBehaviour
{

    public float choiceCD, magicParryCd, meleeParryCd, rangedParryCd, dodgeCd;
    bool isTicking;
    public TMP_Text[] cdText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(((choiceCD > 0) || (rangedParryCd > 0) || (meleeParryCd > 0) || (dodgeCd > 0 ) || (magicParryCd > 0)) && !isTicking)
        {
            StartCoroutine(TickDown());
        }
        if(choiceCD < 0)
        {
            choiceCD = 0f;
            cdText[0].text = " F ";
        }
        if(rangedParryCd < 0)
        {
            rangedParryCd = 0f;
            cdText[1].text = " Q ";
        }
        if(meleeParryCd < 0)
        {
            meleeParryCd = 0f;
            cdText[2].text = " |) ";
        }
        if(magicParryCd < 0)
        {
            magicParryCd = 0f;
            cdText[3].text = " E ";
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
        if(rangedParryCd > 0)
        {
            rangedParryCd -= 0.1f;
            cdText[1].text = " " + rangedParryCd + " ";
        }
        if(meleeParryCd > 0)
        {
            meleeParryCd -= 0.1f;
            cdText[2].text = " " + meleeParryCd + " ";
        }
        if(magicParryCd > 0)
        {
            magicParryCd -= 0.1f;
            cdText[3].text = " " + magicParryCd + " ";
        }
        if(dodgeCd > 0)
        {
            dodgeCd -= 0.1f;
        }
        isTicking = false;

    }
}
