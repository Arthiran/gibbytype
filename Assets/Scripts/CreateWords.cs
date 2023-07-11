using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CreateWords : MonoBehaviour
{
    public GameObject GibberishPrefab;

    public Transform Parent;

    List<TextMeshProUGUI> Words = new List<TextMeshProUGUI>();
    Dictionary<int, char> alphabet = new Dictionary<int, char>();

    private int CurrentMode = 0;

    private bool startedGame = false;

    private float StartDateTime;

    private int WordLimitCount = 15;

    private int letterCounter = 0;
    private int wordCounter = 0;

    public TextMeshProUGUI HighScoreText;
    public TextMeshProUGUI PreviousScoreText;
    public TextMeshProUGUI WordCountText;

    public Animator FlashAnimator;

    private void Start()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            HighScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
        }
        else
        {
            PlayerPrefs.SetInt("HighScore", 0);
            HighScoreText.text = "0";
        }

        if (PlayerPrefs.HasKey("PreviousScore"))
        {
            PreviousScoreText.text = PlayerPrefs.GetInt("PreviousScore").ToString();
        }
        else
        {
            PlayerPrefs.SetInt("PreviousScore", 0);
            PreviousScoreText.text = "0";
        }

        alphabet.Add(0, 'a');
        alphabet.Add(1, 'b');
        alphabet.Add(2, 'c');
        alphabet.Add(3, 'd');
        alphabet.Add(4, 'e');
        alphabet.Add(5, 'f');
        alphabet.Add(6, 'g');
        alphabet.Add(7, 'h');
        alphabet.Add(8, 'i');
        alphabet.Add(9, 'j');
        alphabet.Add(10, 'k');
        alphabet.Add(11, 'l');
        alphabet.Add(12, 'm');
        alphabet.Add(13, 'n');
        alphabet.Add(14, 'o');
        alphabet.Add(15, 'p');
        alphabet.Add(16, 'q');
        alphabet.Add(17, 'r');
        alphabet.Add(18, 's');
        alphabet.Add(19, 't');
        alphabet.Add(20, 'u');
        alphabet.Add(21, 'v');
        alphabet.Add(22, 'w');
        alphabet.Add(23, 'x');
        alphabet.Add(24, 'y');
        alphabet.Add(25, 'z');

        ChangeGameMode(1);
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    ChangeGameMode(0);
        //}
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ChangeGameMode(1);
        }
        //else if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    ChangeGameMode(2);
        //}
    }

    public void HandleLetter(char _Letter)
    {
        if (!startedGame)
        {
            startedGame = true;
            StartDateTime = Time.fixedTime;
        }

        if (letterCounter < 5)
        {
            if (_Letter == Words[0].text[0])
            {
                letterCounter++;
                Words[0].text = Words[0].text.Substring(1);
                if (Words[0].text == "")
                {
                    Destroy(Words[0].gameObject);
                    Words.RemoveAt(0);
                    letterCounter = 0;
                    wordCounter++;
                    WordCountText.text = wordCounter.ToString() + "/" + WordLimitCount;

                    if (CurrentMode == 0 || CurrentMode == 2)
                    {
                        GameObject NewGibby = Instantiate<GameObject>(GibberishPrefab, Parent);
                        for (int j = 0; j < 5; j++)
                        {
                            NewGibby.GetComponent<TextMeshProUGUI>().text += alphabet[UnityEngine.Random.Range(0, 25)];
                        }
                        NewGibby.transform.parent = Parent;
                        Words.Add(NewGibby.GetComponent<TextMeshProUGUI>());
                    }
                    else
                    {
                        if (Words.Count <= 0)
                        {
                            wordCounter = 0;
                            int wpm = Mathf.RoundToInt((float)(WordLimitCount / ((Time.fixedTime - StartDateTime) / 60)));
                            Debug.Log("WPM: " + wpm);
                            PlayerPrefs.SetInt("PreviousScore", wpm);
                            PreviousScoreText.text = wpm.ToString();
                            if (PlayerPrefs.GetInt("HighScore") < wpm)
                            {
                                PlayerPrefs.SetInt("HighScore", wpm);
                                HighScoreText.text = wpm.ToString();
                            }
                            ChangeGameMode(CurrentMode);
                        }
                    }
                }
            }
            else
            {
                Debug.Log("FAILED");
                FlashAnimator.SetTrigger("ShouldFlash");
                ChangeGameMode(CurrentMode);
            }
        }
    }

    public void ChangeGameMode(int _Mode)
    {
        CurrentMode = _Mode;
        startedGame = false;
        if (_Mode == 0)
        {
            Timed();
        }
        else if (_Mode == 1)
        {
            WordLimit();
        }
        else if (_Mode == 2)
        {
            Infinite();
        }
    }

    private void ClearWords()
    {
        letterCounter = 0;
        wordCounter = 0;
        for (int i = 0; i < Words.Count; i++)
        {
            Destroy(Words[i].gameObject);
        }
        Words = new List<TextMeshProUGUI>();
        WordCountText.text = wordCounter.ToString() + "/" + WordLimitCount;
    }

    private void Timed()
    {
        ClearWords();
        for (int i = 0; i < 50; i++)
        {
            GameObject NewGibby = Instantiate<GameObject>(GibberishPrefab, Parent);
            for (int j = 0; j < 5; j++)
            {
                NewGibby.GetComponent<TextMeshProUGUI>().text += alphabet[UnityEngine.Random.Range(0, 25)];
            }
            NewGibby.transform.SetParent(Parent);
            Words.Add(NewGibby.GetComponent<TextMeshProUGUI>());
        }
    }

    private void WordLimit()
    {
        ClearWords();
        for (int i = 0; i < WordLimitCount; i++)
        {
            GameObject NewGibby = Instantiate<GameObject>(GibberishPrefab, Parent);
            for (int j = 0; j < 5; j++)
            {
                NewGibby.GetComponent<TextMeshProUGUI>().text += alphabet[UnityEngine.Random.Range(0, 25)];
            }
            NewGibby.transform.parent = Parent;
            Words.Add(NewGibby.GetComponent<TextMeshProUGUI>());
        }
    }

    private void Infinite()
    {
        ClearWords();
        for (int i = 0; i < 50; i++)
        {
            GameObject NewGibby = Instantiate<GameObject>(GibberishPrefab, Parent);
            for (int j = 0; j < 5; j++)
            {
                NewGibby.GetComponent<TextMeshProUGUI>().text += alphabet[UnityEngine.Random.Range(0, 25)];
            }
            NewGibby.transform.parent = Parent;
            Words.Add(NewGibby.GetComponent<TextMeshProUGUI>());
        }
    }
}
