using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGameOverController : MonoBehaviour
{
    public TMPro.TextMeshProUGUI pointsText;
    [SerializeField] private string nameOfGameLevel;
    [SerializeField] private GameObject menuGameOver;

    public void Setup(int score)
    {
        //gameObject.SetActive(true);
        Debug.Log("Setup");
        menuGameOver.SetActive(true);
        pointsText.text += score.ToString();
    }

    public void Restart()
    {
        SceneManager.LoadScene(nameOfGameLevel);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
