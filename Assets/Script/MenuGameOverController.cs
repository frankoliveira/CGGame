using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGameOverController : MonoBehaviour
{
    public TMPro.TextMeshProUGUI pointsText;
    public TMPro.TextMeshProUGUI lifeText;
    [SerializeField] private string nameOfGameLevel;
    [SerializeField] private GameObject menuGameOver;

    public void Setup(int score, int vidas)
    {
        //gameObject.SetActive(true);
        Debug.Log("Setup");
        menuGameOver.SetActive(true);
        pointsText.text += score.ToString();
        lifeText.text += vidas.ToString();
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
