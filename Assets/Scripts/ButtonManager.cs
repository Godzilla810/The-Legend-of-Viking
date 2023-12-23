using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject instructionPage;
    private bool active;
    private void Start()
    {
        active = false;
        instructionPage.SetActive(active);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void ShowInstruction()
    {
        active = !active;
        instructionPage.SetActive(active);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
