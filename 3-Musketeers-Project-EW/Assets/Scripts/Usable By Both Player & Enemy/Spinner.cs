using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    // Start is called before the first frame update
    public float spinSpeed;
    public bool forwardRoll, cartWheel, lazySusan;

    bool canSpin;
    void Start()
    {
        canSpin = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(canSpin)
        {
            canSpin = false;
            StartCoroutine(Spin());
        }
        
    }

    private IEnumerator Spin()
    {
        if(forwardRoll)
        {
            transform.Rotate(Vector3.right * spinSpeed);
        }

        if(cartWheel)
        {
            transform.Rotate(Vector3.forward * spinSpeed);
        }

        if(lazySusan)
        {
            transform.Rotate(Vector3.up * spinSpeed);
        }

        yield return new WaitForSeconds(0.1f);

        canSpin = true;
    }
}
