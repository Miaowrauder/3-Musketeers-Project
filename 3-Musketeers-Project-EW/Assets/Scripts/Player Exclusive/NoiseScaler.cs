using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseScaler : MonoBehaviour
{
    public float scale;
    public GameObject pl;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(scale, scale, scale);
        transform.position = pl.transform.position;
    }
}
