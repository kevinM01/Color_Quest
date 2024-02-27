using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRotate : MonoBehaviour
{
    // Start is called before the first frame update
    private float rotateChange = 100;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, rotateChange * Time.deltaTime, 0f);
    }
}
