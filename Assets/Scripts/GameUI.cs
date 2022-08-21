using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{

    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject nextLevelButton;
    [SerializeField] private GameObject swordPanel;
    [SerializeField] private GameObject applePanel;
    [SerializeField] private GameObject apple;
    [SerializeField] private GameObject iconSword;
    [SerializeField] Color usedIconSwordColor;


    public void ShowRestartButton()
    {
        restartButton.SetActive(true);
    }
    public void NextLevelButtonButton()
    {
        nextLevelButton.SetActive(true);
    }

    public void SetInitialDisplayedSwordCount(int count)
    {
        for (int i = 0; i < count; i++)
            Instantiate(iconSword, swordPanel.transform);
    }
    public void SetInitialDisplayedAppleCount(int appleCount)
    {
        for (int i = 0; i < appleCount; i++)
            Instantiate(apple, applePanel.transform);
    }

    private int swordIconIndexToChange = 0;

    public void DecrementDisplayedSwordCount()
    {
        if (swordIconIndexToChange <= 7)
        {
            swordPanel.transform.GetChild(swordIconIndexToChange++).GetComponent<Image>().color = usedIconSwordColor;
            if (swordIconIndexToChange == 7)
            {
                return;
            }
        }
    }

}
