using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReactionTest : MonoBehaviour
{
    Image myImage;

    public float minTime;
    public float maxTime;

    public Color firstColor;
    public Color secondColor;

    public Text timer;
    public Text bestScore;

    public GameObject startText;
    public GameObject instructionText;
    public GameObject clickText;
    public GameObject missedText;

    float currentTime;

    bool running;
    bool canClick;
    float startTime;
    float randomTime;

    private void Start()
    {
        myImage = GetComponent<Image>();

        StopTest();

        CheckHighScore();
    }

    private void Update()
    {
        if (running)
        {
            if (Time.realtimeSinceStartup >= startTime + randomTime & !canClick)
            {
                TimeToClick();
            }

            if (canClick)
            {
                currentTime += Time.deltaTime;

                timer.text = Mathf.Round(currentTime * 1000f).ToString();
            }

            if (Input.anyKeyDown)
            {
                StopTest();
            }
        }
        else if (Input.anyKeyDown)
        {
            StartTest();
        }
    }

    void StartTest()
    {
        running = true;

        CheckHighScore();

        currentTime = 0f;
        
        startTime = Time.realtimeSinceStartup;
        
        randomTime = Random.Range(minTime, maxTime);

        startText.SetActive(false);
        missedText.SetActive(false);
        instructionText.SetActive(true);

        timer.text = "000";
    }

    void TimeToClick()
    {
        canClick = true;

        myImage.color = secondColor;

        instructionText.SetActive(false);
        clickText.SetActive(true);
    }
    
    void StopTest()
    {
        myImage.color = firstColor;

        instructionText.SetActive(false);
        clickText.SetActive(false);
        
        if (canClick)
        {
            canClick = false;

            startText.SetActive(true);
        }
        else if (running)
        {
            missedText.SetActive(true);
        }

        running = false;
    }

    void CheckHighScore()
    {
        if ((currentTime < PlayerPrefs.GetFloat("Best Score") || PlayerPrefs.GetFloat("Best Score") == 0f) && currentTime > 0f)
        {
            PlayerPrefs.SetFloat("Best Score", currentTime);
        }

        if (PlayerPrefs.GetFloat("Best Score") > 0)
        {
            bestScore.text = Mathf.Round(PlayerPrefs.GetFloat("Best Score") * 1000f) + " ms";
        }
    }
}
