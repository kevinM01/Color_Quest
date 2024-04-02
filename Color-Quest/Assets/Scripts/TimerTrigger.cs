using UnityEngine;
using UnityEngine.UI;

public class TimerTrigger : MonoBehaviour
{
    public Text timerText;
    private float timer = 5f;
    private bool timerStarted = false;

    private void Update()
    {
        if (timerStarted)
        {
            timer -= Time.deltaTime;
            timerText.text = Mathf.Ceil(timer).ToString();
            if (timer <= 0)
            {
                timerStarted = false;
                timerText.enabled = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            timerStarted = true;
            timerText.enabled = true;
            if (timer <= 0)
            {
                timerStarted = false;
                timerText.enabled = false;
                Collect(other.gameObject);
            }
        }
    }

    private void Collect(GameObject player)
    {
        Destroy(gameObject);
    }
}
