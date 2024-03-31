using UnityEngine;
using TMPro;
using System.Collections;

public class DestroyCollectible : MonoBehaviour
{
    private bool flag;
    public TextMeshProUGUI powerUpText;

    // void Start()
    // {
    //     firstPowerUpText.enabled = false;
    //     flag = false;
    // }

    void Start()
    {
        powerUpText.enabled = false;
        flag = false;
    }

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

        if (other.CompareTag("Collectible"))
        {
            if (!flag)
            {
                StartCoroutine(ShowPowerUpText());
                flag = true;
            }
        }

    }

    // IEnumerator ShowPowerUpText()
    //     {

    //         firstPowerUpText.enabled = true;
    //         yield return new WaitForSeconds(4);

    //         firstPowerUpText.enabled = false;
    //         Debug.Log("firstPowerUpText diabled");
    //     }

    IEnumerator ShowPowerUpText()
    {
        powerUpText.enabled = true;
        yield return new WaitForSeconds(4);
        powerUpText.enabled = false;
    }

    private void Collect(GameObject player)
    {
        Destroy(gameObject);
    }

}
