using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleGroup : MonoBehaviour
{
    [SerializeField] public Toggle[] toggles;
    [SerializeField] public GameObject[] frames;

    public SkulData skuldata;
    public ItemData itemdata;
    public EssenceData essencedata;

    public int count;
    //시작할때 스컬0번째부터 시작
    void Start()
    {
        toggles[0].isOn = true;
        OnToggles(toggles[0]);
    }

    
    public void OnToggles(Toggle toggle)
    {
        if (toggle.isOn)
        {
            //아이템 프레임 변경
            count = 0;
            foreach (var item in toggles)
            {
                frames[count].SetActive(true);
                if (item == toggle)
                {
                    frames[count].SetActive(true);
                }
                else
                {
                    frames[count].SetActive(false);
                }
                count++;
            }
        }
    }
}
