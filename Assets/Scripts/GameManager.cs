using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int enemyCount = 1;

    [HideInInspector] public bool gameOver;

    void Start()
    {
        
    }


    void Update()
    {
        if (!gameOver && FindObjectOfType<PlayerController>().GetNumberOfBullets() <= 0 && enemyCount > 0)
        {
            gameOver = true;
            Debug.Log("GameOver");
        }
    }

    public void CheckEnemyCount()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemyCount <= 0)
        {
            Debug.Log("Win");
        }
    }
}
