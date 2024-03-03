using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_MM : MonoBehaviour
{
    public void LoadSceneByName()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
