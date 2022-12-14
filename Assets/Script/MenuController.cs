using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private string nameOfGameLevel;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject instructionsMenuPanel;
    [SerializeField] private GameObject aboutMenuPanel;

    public void Play()
    {
        SceneManager.LoadScene(nameOfGameLevel);
    }

    public void About()
    {
        mainMenuPanel.SetActive(false);
        aboutMenuPanel.SetActive(true);
    }

    public void Instructions()
    {
        mainMenuPanel.SetActive(false);
        instructionsMenuPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        instructionsMenuPanel.SetActive(false);
        aboutMenuPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void Quit()
    {
        #if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
        #else
         Application.Quit();
        #endif
    }
}
