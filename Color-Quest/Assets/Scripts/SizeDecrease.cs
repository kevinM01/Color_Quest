using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SizeDecrease : MonoBehaviour
{
    // Start is called before the first frame update
    private float sizeDecreaseAmount = -0.27f;
    private PlayerJump playerJump;

    void Start()
    {
        playerJump = FindObjectOfType<PlayerJump>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        float xScale = player.transform.localScale.y;
        if (xScale <= 0.3 )
        {
            playerJump.CollectAnalytics();
            SceneManager.LoadScene("SmallSize");
            Debug.Log("Die " + xScale);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            // Decrease the size of the player on collision, only on the Y-axis
            other.transform.localScale += new Vector3(0f, sizeDecreaseAmount, 0f);

            // Optionally, you can destroy the green diamond after the collision
            Destroy(gameObject);
        }

       
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Decrease the size of the player on exit, only on the Y-axis
            other.transform.localScale += new Vector3(0f, sizeDecreaseAmount, 0f);
        }
    }
}
