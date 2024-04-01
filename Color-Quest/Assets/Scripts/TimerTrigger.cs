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
                timerText.text = "";
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Change "Player" to the tag of the object you want to trigger the timer
        {
            timerStarted = true;
        }
    }
}
