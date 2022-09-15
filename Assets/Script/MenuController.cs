using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private string nameOfGameLevel;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject optionsMenuPanel;

    public void Play()
    {
        SceneManager.LoadScene(nameOfGameLevel);
    }

    public void Options()
    {
        mainMenuPanel.SetActive(false);
        optionsMenuPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsMenuPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void Quit()
    {
        Debug.Log("Quit game");
        Application.Quit();
    }
}