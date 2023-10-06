using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;


    private GameManager gameManager;
    private int startBlackBullet;

    [Header("WinScreen")]
    [SerializeField] TextMeshProUGUI statusText;
    [SerializeField] GameObject winPanel;
    [SerializeField] Image star1, star2, star3;
    [SerializeField] Sprite shineStar, darkStar;
    [SerializeField] float timeToShowStars = 0.5f;
    [SerializeField] float timeToShowShineStars = 0.15f;

    [Header("GameOver")]
    [SerializeField] GameObject gameOverPanel;

    void Awake()
    {
        Instance = this;
        gameManager = FindObjectOfType<GameManager>();
    }

    void Start()
    {
        startBlackBullet = gameManager.GetBlackBullets();
    }

    public void ShowGameOverScreen()
    {
        gameOverPanel.SetActive(true);
    }

    public void ShowWinScreen()
    {        
        ModifyWinScreen();
        winPanel.SetActive(true);
    }

    private void ModifyWinScreen()
    {
        int numberOfBlackBullets = gameManager.GetBlackBullets();
        if (numberOfBlackBullets >= startBlackBullet)
        {
            statusText.text = "FANTASTIC!";
            StartCoroutine(ShowStars(3));
        }
        else if (numberOfBlackBullets >= startBlackBullet - (numberOfBlackBullets) / 2)
        {
            statusText.text = "AWESOME!";
            StartCoroutine(ShowStars(2));
        }
        else if (numberOfBlackBullets > 0)
        {
            statusText.text = "WELL DONE";
            StartCoroutine(ShowStars(1));
        }
        else
        {
            statusText.text = "GOOD";
            StartCoroutine(ShowStars(0));
        }
    }

    private IEnumerator ShowStars(int shineStarNumber)
    {
        yield return new WaitForSeconds(timeToShowStars);
        switch(shineStarNumber)
        {
            case 3:
                yield return new WaitForSeconds(timeToShowShineStars);
                star1.sprite = shineStar;
                yield return new WaitForSeconds(timeToShowShineStars);
                star2.sprite = shineStar;
                yield return new WaitForSeconds(timeToShowShineStars);
                star3.sprite = shineStar;
                break;
            case 2:
                yield return new WaitForSeconds(timeToShowShineStars);
                star1.sprite = shineStar;
                yield return new WaitForSeconds(timeToShowShineStars);
                star2.sprite = shineStar;        
                star3.sprite = darkStar;
                break;
            case 1:
                yield return new WaitForSeconds(timeToShowShineStars);
                star1.sprite = shineStar;
                star2.sprite = darkStar;
                star3.sprite = darkStar;
                break;
            case 0:
                star1.sprite = darkStar;
                star2.sprite = darkStar;
                star3.sprite = darkStar;
                break;
        }

    }
}
