using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwitchT2 : MonoBehaviour
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
    void Update()
    {
        
    }
     IEnumerator Preview(){
        yield return new WaitForSeconds (4);
        // player.SetActive (true);
        maincam.SetActive (true);
        previecam.SetActive (false);
    }
}
