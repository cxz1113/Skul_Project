using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;
    [SerializeField] GameObject pauseUI;

    void Awake() => Instance = this;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pause()
    {
        ProjectManager.Instance.isPasue = true;
        pauseUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void EnterResume()
    {
        ProjectManager.Instance.isPasue = false;
        pauseUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void EnterExit()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }
}
