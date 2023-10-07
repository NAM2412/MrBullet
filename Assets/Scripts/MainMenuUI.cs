using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] GameObject play, levelSelection;

    public void PlayGame()
    {
        play.SetActive(false);
        levelSelection.SetActive(true);
    }
}
