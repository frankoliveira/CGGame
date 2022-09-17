using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuVictoryController : MonoBehaviour
{
    public TMPro.TextMeshProUGUI pointsText;
    public TMPro.TextMeshProUGUI lifeText;
    [SerializeField] private string nameOfGameLevel;
    [SerializeField] private GameObject menuVictory;

    public void Setup(int score)
    {
        //gameObject.SetActive(true);
        Debug.Log("Setup");
        menuVictory.SetActive(true);
        pointsText.text += score.ToString();
        //lifeText.text += vidas.ToString();
    }

    /*public void Restart()
    {
        SceneManager.LoadScene(nameOfGameLevel);
    }*/

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
