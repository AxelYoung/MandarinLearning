using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class MultipleChoiceTestGeneration : MonoBehaviour
{
    public string testType;

    public List<Tests> tests;

    public int testsAmount;
    private int testsRun;

    private CharLibrary lib;

    private List<int> prevChar;

    // Adds the context menu (right click function) to script so that this can be checked before running
    [ContextMenu("Generate Test")]
    public void GenerateTest()
    {
        // Grabs the lib from the same gamecomponent
        lib = GameObject.FindGameObjectWithTag("Lib").GetComponent<CharLibrary>();
        // Blanks out previous characters
        prevChar = new List<int>();
        // Blanks out generated tests
        tests = new List<Tests>();
        if(testsAmount > lib.pool.Count)
        {
            testsAmount = lib.pool.Count;
        }
        // Runs for every test wanted
        for (int i = 0; i < testsAmount; i++)
        {
            if(testType == "Translations")
            {
                // Sets x to any random number within the length of the list of characters
                int x = Random.Range(0, lib.pool.Count);
                // While x is equal to b
                while (prevChar.Contains(x))
                {
                    // Change x once again
                    x = Random.Range(0, lib.pool.Count);
                }
                prevChar.Add(x);
                // Create a temp test
                Tests temp = new Tests();
                // Set the choices to avoid error
                temp.choices = new List<string>();
                // Uses x to determine the character as well as answer for the current test
                temp.character = lib.pool[x].character;
                temp.answer = lib.pool[x].translation;
                // Add the answer to pool of choices
                temp.choices.Add(lib.pool[x].translation);
                // Add audio clip to test
                temp.characterClip = lib.pool[x].audio;
                // Run 3 times
                for (int z = 0; z < 3; z++)
                {
                    // tempZ is any random number withing translation length range
                    int tempZ = Random.Range(0, lib.pool.Count);
                    // If tempZ has already been added to array of choices
                    while (temp.choices.Contains(lib.pool[tempZ].translation))
                    {
                        // Rechoose tempZ
                        tempZ = Random.Range(0, lib.pool.Count);
                    }
                    // Add tempZ translation to pool of choices
                    temp.choices.Add(lib.pool[tempZ].translation);
                }
                // Add the temp test to Tests array
                tests.Add(temp);
            }
            if (testType == "Characters")
            {
                // Sets x to any random number within the length of the list of characters
                int x = Random.Range(0, lib.pool.Count);
                // While x is equal to b
                while (prevChar.Contains(x))
                {
                    // Change x once again
                    x = Random.Range(0, lib.pool.Count);
                }
                prevChar.Add(x);
                // Create a temp test
                Tests temp = new Tests();
                // Set the choices to avoid error
                temp.choices = new List<string>();
                // Uses x to determine the character as well as answer for the current test
                temp.character = lib.pool[x].translation;
                temp.answer = lib.pool[x].character;
                // Add the answer to pool of choices
                temp.choices.Add(lib.pool[x].character);
                // Add audio clip to test
                temp.characterClip = lib.pool[x].audio;
                // Run 3 times
                for (int z = 0; z < 3; z++)
                {
                    // tempZ is any random number withing translation length range
                    int tempZ = Random.Range(0, lib.pool.Count);
                    // If tempZ has already been added to array of choices
                    while (temp.choices.Contains(lib.pool[tempZ].character))
                    {
                        // Rechoose tempZ
                        tempZ = Random.Range(0, lib.pool.Count);
                    }
                    // Add tempZ translation to pool of choices
                    temp.choices.Add(lib.pool[tempZ].character);
                }
                // Add the temp test to Tests array
                tests.Add(temp);
            }
            if (testType == "Characters (audio only)")
            {
                // Sets x to any random number within the length of the list of characters
                int x = Random.Range(0, lib.pool.Count);
                // While x is equal to b
                while (prevChar.Contains(x))
                {
                    // Change x once again
                    x = Random.Range(0, lib.pool.Count);
                }
                prevChar.Add(x);
                // Create a temp test
                Tests temp = new Tests();
                // Set the choices to avoid error
                temp.choices = new List<string>();
                // Uses x to determine the character as well as answer for the current test
                temp.character = null;
                temp.answer = lib.pool[x].character;
                // Add the answer to pool of choices
                temp.choices.Add(lib.pool[x].character);
                // Add audio clip to test
                temp.characterClip = lib.pool[x].audio;
                // Run 3 times
                for (int z = 0; z < 3; z++)
                {
                    // tempZ is any random number withing translation length range
                    int tempZ = Random.Range(0, lib.pool.Count);
                    // If tempZ has already been added to array of choices
                    while (temp.choices.Contains(lib.pool[tempZ].character))
                    {
                        // Rechoose tempZ
                        tempZ = Random.Range(0, lib.pool.Count);
                    }
                    // Add tempZ translation to pool of choices
                    temp.choices.Add(lib.pool[tempZ].character);
                }
                // Add the temp test to Tests array
                tests.Add(temp);
            }
            if (testType == "Translations (audio only)")
            {
                // Sets x to any random number within the length of the list of characters
                int x = Random.Range(0, lib.pool.Count);
                // While x is equal to b
                while (prevChar.Contains(x))
                {
                    // Change x once again
                    x = Random.Range(0, lib.pool.Count);
                }
                prevChar.Add(x);
                // Create a temp test
                Tests temp = new Tests();
                // Set the choices to avoid error
                temp.choices = new List<string>();
                // Uses x to determine the character as well as answer for the current test
                temp.character = null;
                temp.answer = lib.pool[x].translation;
                // Add the answer to pool of choices
                temp.choices.Add(lib.pool[x].translation);
                // Add audio clip to test
                temp.characterClip = lib.pool[x].audio;
                // Run 3 times
                for (int z = 0; z < 3; z++)
                {
                    // tempZ is any random number withing translation length range
                    int tempZ = Random.Range(0, lib.pool.Count);
                    // If tempZ has already been added to array of choices
                    while (temp.choices.Contains(lib.pool[tempZ].translation))
                    {
                        // Rechoose tempZ
                        tempZ = Random.Range(0, lib.pool.Count);
                    }
                    // Add tempZ translation to pool of choices
                    temp.choices.Add(lib.pool[tempZ].translation);
                }
                // Add the temp test to Tests array
                tests.Add(temp);
            }
        }
    }
}

[System.Serializable]
public class Tests
{
    public string character;
    public AudioClip characterClip;
    public string answer;
    public List<string> choices = new List<string>();
}
