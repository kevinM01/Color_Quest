using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwitchT2 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject maincam;
    public GameObject previecam;

    public GameObject playerObject; // Reference to the player GameObject
    private PlayerJump playerJumpScript; // Reference to the PlayerJump script attached to the player

    // public GameObject player; 
    void Start()
    {
        // Get the PlayerJump script component from the player GameObject
        playerJumpScript = playerObject.GetComponent<PlayerJump>();

        maincam.SetActive(false);
        DisablePlayerJumpScript();
        StartCoroutine(Preview());
        //  player.SetActive (false);
    }

    public void EnablePlayerJumpScript()
    {
        if (playerJumpScript != null)
        {
            playerJumpScript.enabled = true;
            Debug.Log("PlayerJump script enabled.");
        }
        else
        {
            Debug.LogWarning("PlayerJump script is not assigned.");
        }
    }

    // Method to disable the PlayerJump script
    public void DisablePlayerJumpScript()
    {
        if (playerJumpScript != null)
        {
            playerJumpScript.enabled = false;
            Debug.Log("PlayerJump script disabled.");
        }
        else
        {
            Debug.LogWarning("PlayerJump script is not assigned.");
        }
    }


    // Update is called once per frame
    IEnumerator Preview()
    {
        yield return new WaitForSeconds(6);
        //  player.SetActive (true);
        EnablePlayerJumpScript();
        maincam.SetActive(true);
        previecam.SetActive(false);

    }
}