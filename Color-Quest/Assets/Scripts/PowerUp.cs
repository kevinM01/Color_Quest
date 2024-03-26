using UnityEngine;
using TMPro;
using System.Collections;

public class DestroyCollectible : MonoBehaviour
{
    private bool flag;
    public TextMeshProUGUI firstPowerUpText;

    void Start()
    {
        firstPowerUpText.enabled = false;
        flag = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Collect(other.gameObject);
            if(!flag)
            {
                StartCoroutine(ShowPowerUpText());
                flag = false;
            }
        }
    }

    IEnumerator ShowPowerUpText()
        {
            firstPowerUpText.enabled = true;
            yield return new WaitForSeconds(4);
            firstPowerUpText.enabled = false;
        }

    private void Collect(GameObject player)
    {
        Destroy(gameObject);
    }

}
