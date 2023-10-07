using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Level : MonoBehaviour
{
    private Button levelButton;
    [SerializeField] int levelReq;
    void Start()
    {
        levelButton = GetComponent<Button>();

        if (PlayerPrefs.GetInt("Level",1) >= levelReq)
        {
            levelButton.onClick.AddListener(() => LoadLevel());
        }
        else
        {
            this.GetComponent<CanvasGroup>().alpha = 0.5f;
        }
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(gameObject.name);
    }
}
