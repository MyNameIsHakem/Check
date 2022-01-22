using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject Pause , canvas , Setting , PauseButton;
    public GameObject MainCamera, PauseCamera;

    
    public void Pause_EventHandler()
    {
        Time.timeScale = 0f;
        Pause.SetActive(true);
        canvas.SetActive(false);
        MainCamera.SetActive(false);
        PauseCamera.SetActive(true);
    }

    public void Resume_EventHandler()
    {
        Time.timeScale = 1f;
        Pause.SetActive(false);
        canvas.SetActive(true);
        MainCamera.SetActive(true);
        PauseCamera.SetActive(false);
    } 

    public void Setting_EventHandler()
    {
        Setting.SetActive(true);
        PauseButton.SetActive(false);
    }

    public void Slider_EventHandler(float Value)
    {
        PlayerBehavior.Speed = Value;
    }

    public void Back_EventHandler()
    {
        Setting.SetActive(false);
        PauseButton.SetActive(true);
    }    

    public void Quit_EventHandler()
    {
        Application.Quit();
    }

    public void Restrat_EventHandler()
    {
        SceneManager.LoadScene(1);
    }
    
}
