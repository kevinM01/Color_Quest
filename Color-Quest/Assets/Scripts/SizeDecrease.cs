using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeDecrease : MonoBehaviour
{
    // Start is called before the first frame update
    private float sizeDecreaseAmount = -0.68f;

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
