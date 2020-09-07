using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManagerGameOver : MonoBehaviour
{
    public TextMeshProUGUI EndScore;

    // Start is called before the first frame update
    void Start()
    {
        EndScore = GetComponent<TextMeshProUGUI>();
        EndScore.SetText(ScoreManager.Kills.ToString());
    }

}
