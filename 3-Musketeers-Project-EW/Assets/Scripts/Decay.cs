using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decay : MonoBehaviour
{
    public float scale;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
            transform.localScale = new Vector3(scale, scale, scale);

            scale *= 0.999f;

        if (scale <= 0.1f)
        {
            Destroy(this.gameObject);
        }
    }
}
