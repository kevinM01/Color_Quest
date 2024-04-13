using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwitch4 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject maincam;
    public GameObject previecam;
    void Start()
    {
         maincam.SetActive (false);
        StartCoroutine (Preview());
    }

    // Update is called once per frame
    IEnumerator Preview(){
        yield return new WaitForSeconds (10);
        maincam.SetActive (true);
        previecam.SetActive (false);
    }
}
