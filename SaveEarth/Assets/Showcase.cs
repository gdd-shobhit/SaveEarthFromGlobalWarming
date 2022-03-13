using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Showcase : MonoBehaviour
{
    public float angle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(gameObject.activeSelf)
        {
            gameObject.transform.Rotate(Vector3.up, 10f*Time.deltaTime);
        }
       
    }
}
