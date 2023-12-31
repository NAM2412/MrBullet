using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public bool gameOver = false;

    [SerializeField] int enemyCount = 1;
    [SerializeField] int goldenBullets = 1;
    [SerializeField] int blackBullets = 3;

    [SerializeField] GameObject blackBullet, goldenBullet;

    private int levelNumber;

    public int GetBlackBullets() => blackBullets;

    void Awake()
    {
        levelNumber = PlayerPrefs.GetInt("Level", 1);

     

        FindObjectOfType<PlayerController>().numberOfBullets = blackBullets + goldenBullets;
        for (int i = 0; i < blackBullets; i++)
        {
            GameObject blackBulletInstance = Instantiate(blackBullet);
            blackBulletInstance.transform.SetParent(GameObject.Find("BulletUI").transform);
        }

        for (int i = 0; i < goldenBullets; i++)
        {
            GameObject goldenBulletInstance = Instantiate(goldenBullet);
            goldenBulletInstance.transform.SetParent(GameObject.Find("BulletUI").transform);
        }

    }

    void Update()
    {
        if (!gameOver && FindObjectOfType<PlayerController>().numberOfBullets <= 0 && enemyCount > 0
            && GameObject.FindGameObjectsWithTag("Bullet").Length <= 0)
        {
            gameOver = true;
            UIManager.Instance.ShowGameOverScreen();
        }
    }

    public void CheckEnemyCount()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemyCount <= 0)
        {
            UIManager.Instance.ShowWinScreen();
            if (levelNumber >= SceneManager.GetActiveScene().buildIndex)
            {
                PlayerPrefs.SetInt("Level", levelNumber + 1);
            }
        }
    }

    public void CheckBullets()
    {
        if (goldenBullets > 0)
        {
            goldenBullets--;
            GameObject.FindGameObjectWithTag("GoldenBullet").SetActive(false);
        }
        else if (blackBullets > 0)
        {
            blackBullets--;
            GameObject.FindGameObjectWithTag("BlackBullet").SetActive(false);
        }
    }

  
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
}