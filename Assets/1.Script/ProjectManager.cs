using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ProjectManager : MonoBehaviour
{
    public static ProjectManager Instacne;
    public PlayerData json;
    [SerializeField] GameObject textObj;
    //[SerializeField] private TMP_Text text;
    void Awake() => Instacne = this;

    void Start()
    {
        textObj.SetActive(false);
        StartCoroutine(ShowReady());
    }

    void Update()
    {
        if(Input.anyKey)
        {
            SceneManager.LoadScene(1);
            DontDestroyOnLoad(json);
            //PlayerDemo playerDemo = GetComponent<PlayerDemo>();
        }
    }

    IEnumerator ShowReady()
    {
        int count = 0;
        while(count < 10)
        {
            textObj.SetActive(true);
            yield return new WaitForSeconds(1f);
            textObj.SetActive(false);
            yield return new WaitForSeconds(1f);
            count++;
        }
    }
}
