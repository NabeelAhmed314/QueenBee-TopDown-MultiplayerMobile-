using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighSCoreManager : MonoBehaviour
{
    public TextMeshProUGUI EndScore;

    // Start is called before the first frame update
    void Start()
    {
        EndScore = GetComponent<TextMeshProUGUI>();
        EndScore.SetText("Highscore: "+PlayerPrefs.GetInt("Highscore"));
    }
}
