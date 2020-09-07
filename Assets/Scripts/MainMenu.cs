using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioMixer audiomixer;

    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("Main");
    }
    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }

    public void OnMainMenuButtonClicked()
    {
        SceneManager.LoadScene("Menu");
    }

    public void SetVolume(float volume)
    {
        audiomixer.SetFloat("volume",volume);
    }
}
