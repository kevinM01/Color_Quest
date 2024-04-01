using UnityEngine;
using UnityEngine.Networking; // Added to use UnityWebRequest
using System.Collections;
using System.Collections.Generic;

public class SendCoinXHealthToGoogle : MonoBehaviour
{
    [SerializeField] private string URL;
    private long _sessionID;
    /*private float x_coord;
    private int level;*/

    private void Awake()
    {
        _sessionID = System.DateTime.Now.Ticks;
    }

    public void Send(float coinCollected, float healthIncreased, string level)
    {
        /*x_coord = Random.Range(0, 101);*/     // x coord of the player at the time of death
        /*level = 0;*/                          // level at the time of death
        coinCollected = (float)System.Math.Floor(coinCollected);
        healthIncreased = (float)System.Math.Floor(healthIncreased);
        // char levelNum = level[level.Length - 1];
        // StartCoroutine(Post(_sessionID.ToString(), x_coord.ToString(), y_coord.ToString(), levelNum.ToString()));
        StartCoroutine(Post(_sessionID.ToString(), coinCollected.ToString(), healthIncreased.ToString(), level.ToString()));
        /*Debug.Log("Sendinggg Daa-taa" + levelNum);*/
    }

    private IEnumerator Post(string _sessionID, string coinCollected, string healthIncreased, string level)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.32739510", _sessionID);
        form.AddField("entry.1672048126", coinCollected);
        form.AddField("entry.1328947805", healthIncreased);
        form.AddField("entry.1550796188", level);

        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete");
            }
        }
    }
}
