using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;
    [SerializeField] GameObject pauseUI;
    [SerializeField] Image selectImage;
    [SerializeField] GameObject selecObj;
    [SerializeField] GameObject content;

    public List<GameObject> choice = new List<GameObject>();
    GameObject[][] objs = new GameObject[3][];
    int index = 0;

    void Awake() => Instance = this;

    void Update()
    {
        if (ProjectManager.Instance.isPasue)
            SelectMenu();
    }

    public void Pause()
    {
        ProjectManager.Instance.isPasue = true;
        pauseUI.SetActive(true);
        Time.timeScale = 0;
    }

    void EnterResume()
    {
        ProjectManager.Instance.isPasue = false;
        pauseUI.SetActive(false);
        Time.timeScale = 1;
    }

    void EnterReset()
    {
        SceneManager.LoadScene(0);
        ProjectManager.Instance.isPasue = false;
        Time.timeScale = 1;
    }
    void EnterExit()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }

    void SelectMenu()
    {
        Menu();
        if (Input.GetKeyDown(KeyCode.UpArrow) && index > 0)
            index--;
        else if (Input.GetKeyDown(KeyCode.DownArrow) && index < 2)
            index++;

        selecObj = objs[index][0];
        selectImage.transform.position = content.transform.GetChild(index).transform.position;

        if (Input.GetKeyDown(KeyCode.Return))
            SwitchMenu();
    }

    void SwitchMenu()
    {
        switch(index)
        {
            case 0:
                EnterResume();
                break;
            case 1:
                EnterReset();
                break;
            case 2:
                EnterExit();
                break;
        }
    }
    void Menu()
    {
        objs[0] = new GameObject[1];
        objs[1] = new GameObject[1];
        objs[2] = new GameObject[1];
        for(int i = 0; i < choice.Count;i++)
        {
            objs[i][0] = choice[i];
        }
    }
}
