using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MultipleChoiceTestDisplay : MonoBehaviour
{
    public int score;

    private float time = 2;
    public float repeatTime;
    public float ratePerSecond;
    private float scale;
    public Image timer;

    private int currentTest = 0;
    public Text character;
    public Text[] choices;
    public Text scoreTxt;

    private MultipleChoiceTestGeneration gen;

    public Animator anim;

    public int correctNum;

    private AudioSource aud;

    public AudioSource pfAud;
    public AudioClip passAudio;
    public AudioClip failAudio;

    public Animator rAnim;

    public void StartTest()
    {
        // Retrieves generation
        gen = GetComponent<MultipleChoiceTestGeneration>();
        // Retrieves audio source
        aud = GetComponent<AudioSource>();
        // Gets the rate per second needed to add to scale
        ratePerSecond = repeatTime / time;
        // Generates test
        gen.GenerateTest();
        // Creates test
        CreateTest();
        // Starts timer
        StartCoroutine(Timer());
    }

    public void CreateTest()
    {
        // If past the last test
        if (currentTest != gen.testsAmount)
        {
            // Sets current character to that of current test character
            character.text = gen.tests[currentTest].character;
            // Creates a new temporary choicesChosen list
            List<int> choicesChosen = new List<int>();
            // Run 4 times
            for (int i = 0; i < 4; i++)
            {
                // tempI is any random number between 0 and 3
                int tempI = Random.Range(0, 4);
                // If tempI has already been added to array of choices
                while (choicesChosen.Contains(tempI))
                {
                    // Rechoose tempI
                    tempI = Random.Range(0, 4);
                }
                // Add tempI translation to pool of choices
                choicesChosen.Add(tempI);
                // One Text asset is chosen to have text of current random choice of current test
                choices[i].text = gen.tests[currentTest].choices[tempI];
                // If current choice chosen from current test is equal to the currect number of correct answer
                if (tempI == 0)
                {
                    // Correct number to choose is the current i
                    correctNum = i;
                }
            }
            // Play audio file of character
            aud.clip = gen.tests[currentTest].characterClip;
            aud.Play();
        }
    }

    public IEnumerator Timer()
    {
        // If the scale is greater than 1 
        if (scale < 1)
        {
            scale += ratePerSecond;
        }
        // Scale past or reaches 1
        else
        {
            FailTest();
        }
        // Scale is set to scale int
        timer.transform.localScale = new Vector3(scale, scale, scale);
        // Wait for repeat time
        yield return new WaitForSeconds(repeatTime);
        // Restart coroutine
        StartCoroutine(Timer());
    }

    public void FailTest()
    {
        // Make sound
        pfAud.clip = failAudio;
        pfAud.Play();
        // Animate red flag
        anim.SetInteger("Score", -1);
        anim.transform.GetChild(0).GetComponent<Text>().text = "+0";
        // Reset scale
        scale = 0;
        // Advance Scale
        currentTest += 1;
        // Create new test
        CreateTest();
        // Reset anim
        StartCoroutine(ResetAnim());
    }

    public void PassTest()
    {
        // Make sound
        pfAud.clip = passAudio;
        pfAud.Play();
        // Animate green flag
        anim.SetInteger("Score", 1);
        anim.transform.GetChild(0).GetComponent<Text>().text = "+1";
        // Add to score
        score += 1;
        scoreTxt.text = score.ToString();
        // Reset scale
        scale = 0;
        // Advance Scale
        currentTest += 1;
        // Create new test
        CreateTest();
        // Reset anim
        StartCoroutine(ResetAnim());
    }

    public IEnumerator ResetAnim()
    {
        yield return new WaitForSeconds(5/6f);
        // Reset anim
        anim.SetInteger("Score", 0);
        if(currentTest == gen.testsAmount)
        {
            rAnim.SetBool("Revert", true);
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene("MainMenu");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SubmitAnswer(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SubmitAnswer(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SubmitAnswer(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SubmitAnswer(3);
        }
    }

    public void SubmitAnswer(int answer)
    {
        if(answer == correctNum)
        {
            PassTest();
        }
        else
        {
            FailTest();
        }
    }
}
