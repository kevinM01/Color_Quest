using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShatterPlatform : MonoBehaviour
{
    // Start is called before the first frame update
//    public float platformWeight = 2f;  
//     public float playerWeight = 15f;    

    void OnCollisionEnter2D(Collision2D collision)
    {

    GameObject player = GameObject.FindGameObjectWithTag("Player");
    float xScale = player.transform.localScale.y;

        if (player != null)
        {
            
            // Debug.Log("Player's X Scale: " + xScale);
        }
        else
        {
            Debug.LogWarning("Player not found in the scene.");

        }
        if (collision.gameObject.CompareTag("Player"))
        {
            if (4.5f < xScale)
            {
                Debug.Log("Player's X Scale: " + xScale);
                Shatter();
            }
        }
    }

    void Shatter()
    {
        Destroy(gameObject);
        

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
