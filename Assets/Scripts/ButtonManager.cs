using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject page;
    private void Start()
    {
        page.SetActive(false);
    }
    public void BackMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void ContinueGame()
    {
        page.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void ShowPage()
    {
        page.SetActive(true);
        Time.timeScale = 0;
    }
    public void Exit()
    {
        Application.Quit();
    }
}
