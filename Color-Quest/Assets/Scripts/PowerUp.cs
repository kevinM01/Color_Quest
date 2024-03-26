using UnityEngine;
using TMPro;
using System.Collections;

public class DestroyCollectible : MonoBehaviour
{
    // private bool flag;
    // public TextMeshProUGUI firstPowerUpText;

    // void Start()
    // {
    //     firstPowerUpText.enabled = false;
    //     flag = false;
    // }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Collect(other.gameObject);
            // if(!flag)
            // {
            //     StartCoroutine(ShowPowerUpText());
            //     flag = true;
            // }
        }
    }

    // IEnumerator ShowPowerUpText()
    //     {
            
    //         firstPowerUpText.enabled = true;
    //         yield return new WaitForSeconds(4);
            
    //         firstPowerUpText.enabled = false;
    //         Debug.Log("firstPowerUpText diabled");
    //     }

    private void Collect(GameObject player)
    {
        Destroy(gameObject);
    }

}
