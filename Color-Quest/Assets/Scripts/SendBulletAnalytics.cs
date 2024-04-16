using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking; // Added to use UnityWebRequest

public class SendBulletAnalytics : MonoBehaviour
{
    [SerializeField] private string URL;
    private long _sessionID;
    /*private float x_coord;
    private int level;*/

    private void Awake()
    {
        _sessionID = System.DateTime.Now.Ticks;
    }

    public void Send(string level, int bulletsWasted)
    {
        /*x_coord = Random.Range(0, 101);*/     // x coord of the player at the time of death
        /*level = 0;*/                          // level at the time of death
        // x_coord = (float)System.Math.Floor(x_coord);
        // y_coord = (float)System.Math.Floor(y_coord);
        // char levelNum = level[level.Length - 1];
        // StartCoroutine(Post(_sessionID.ToString(), x_coord.ToString(), y_coord.ToString(), levelNum.ToString()));
        // Debug.Log(_sessionID.ToString() + level.ToString() + bulletsWasted.ToString());
        StartCoroutine(Post(_sessionID.ToString(), level.ToString(), bulletsWasted.ToString()));
        /*Debug.Log("Sendinggg Daa-taa" + levelNum);*/
    }

    private IEnumerator Post(string _sessionID, string level, string bulletsWasted)
    {
        Debug.Log("Mian idhr hunnn :)");
        WWWForm form = new WWWForm();
        form.AddField("entry.1507197359", _sessionID);
        form.AddField("entry.1074737371", level);
        form.AddField("entry.1724719828", bulletsWasted);
        Debug.Log("thoda niche dekho");

        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {
            yield return www.SendWebRequest();
            Debug.Log("areyy mekoo to andarr looo - ");
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Bullet -Form upload complete");
            }
        }
    }
}
