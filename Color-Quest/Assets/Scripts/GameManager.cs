using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Text redCounterText;
    public Text greenCounterText;
    public Text blueCounterText;

    private int redCount;
    private int greenCount;
    private int blueCount;

    void Awake()
    {
        Instance = this;
    }

    public void DestroyGameInstance()
    {
        Application.Quit();

    }
}
