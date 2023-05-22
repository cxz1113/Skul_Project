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
    //�����Ҷ� ����0��°���� ����
    void Start()
    {
        toggles[0].isOn = true;
        OnToggles(toggles[0]);
    }

    
    public void OnToggles(Toggle toggle)
    {
        if (toggle.isOn)
        {
            //������ ������ ����
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
