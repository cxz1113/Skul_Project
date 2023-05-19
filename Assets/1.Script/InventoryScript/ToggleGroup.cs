using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleGroup : MonoBehaviour
{
    [SerializeField] Toggle[] toggles;
    [SerializeField] GameObject[] frames;

    [SerializeField] private Transform content;

    ItemData.Data itemdata;

    void Start()
    {
        toggles[0].isOn = true;
        OnToggles(toggles[0]);
    }

    public void OnToggles(Toggle toggle)
    {
        if (toggle.isOn)
        {
            int count = 0;
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
