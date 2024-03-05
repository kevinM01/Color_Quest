using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeIncrease : MonoBehaviour
{
    // Start is called before the first frame update
    private float sizeIncreaseAmount = 0.45f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Increase the size of the player on collision, only on the Y-axis
            other.transform.localScale += new Vector3(0f, sizeIncreaseAmount, 0f);

            // Optionally, you can destroy the green diamond after the collision
            Destroy(gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Increase the size of the player on exit, only on the Y-axis
            other.transform.localScale += new Vector3(0f, sizeIncreaseAmount, 0f);
        }
    }
}
