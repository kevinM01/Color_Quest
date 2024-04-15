using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwitch5 : MonoBehaviour
{
    // Start is called before the first frame update
     public GameObject maincam;
    public GameObject previecam;
     public GameObject player; 
    void Start()
    {
         maincam.SetActive (false);
        StartCoroutine (Preview());
         player.SetActive (false);
    }

    // Update is called once per frame
    IEnumerator Preview(){
        yield return new WaitForSeconds (10);
        player.SetActive (true);
        maincam.SetActive (true);
        previecam.SetActive (false);
    }
}
