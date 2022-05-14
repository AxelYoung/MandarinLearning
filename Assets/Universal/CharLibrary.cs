using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharLibrary : MonoBehaviour
{
    private TextAsset characterTextFile;
    public string[] characters;
    private List<string> tempChar;

    private TextAsset englishTextFile;
    public string[] translation;
    private List<string> tempTrans;

    private TextAsset pinyinTextFile;
    public string[] pinyin;
    private List<string> tempPinyin;

    public AudioClip[] audioClips;

    private List<string> subjectName;

    private List<int> subjectInt;

    public List<Subjects> subjects;

    public List<Pool> pool;

    private List<int> intNotInPool;

    private static CharLibrary lib;

    // Lib exists in all scenes
    private void Awake()
    {
        DontDestroyOnLoad(this);
        if(lib == null)
        {
            lib = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Adds the context menu (right click function) to script so that this can be checked before running
    [ContextMenu("Update Library")]
    public void SplitText()
    {
        // Resets all values
        characters = new string[0];
        translation = new string[0];
        pinyin = new string[0];
        tempChar = new List<string>();
        tempTrans = new List<string>();
        tempPinyin = new List<string>();
        subjects = new List<Subjects>();
        subjectName = new List<string>();
        subjectInt = new List<int>();
        // Retrieves the text files
        characterTextFile = Resources.Load<TextAsset>("Text/ChineseCharacters");
        englishTextFile = Resources.Load<TextAsset>("Text/EnglishTranslations");
        pinyinTextFile = Resources.Load<TextAsset>("Text/Pinyin");
        // Splits the ChineseCharacters.txt file and EnglishTrans.txt file based on line breaks (seperate lines)
        // and converts them into string arrays so each word can be selected via number in array
        characters = characterTextFile.text.Split('\n');
        translation = englishTextFile.text.Split('\n');
        pinyin = pinyinTextFile.text.Split('\n');
        foreach (Match match in Regex.Matches(englishTextFile.text, "\"([^\"]*)\""))
        {
            subjectName.Add(match.ToString().Replace("\"", ""));
            Subjects sub = new Subjects();
            sub.name = match.ToString().Replace("\"", "");
            subjects.Add(sub);
        }
        int b = 0;
        // Remove line breaks from all strings and empty strings
        for (int i = 0; i < characters.Length; i++)
        {
            if (characters[i].Contains("_"))
            {
                characters[i] = characters[i].Replace("_", "").Replace("\r", "").Replace("\n", "");
                tempChar.Add(characters[i]);
            }
            else
            {
                characters[i] = null;
            }
        }
        for (int i = 0; i < translation.Length; i++)
        {
            translation[i] = translation[i].Replace("\r", "").Replace("\n", "");
            subjectName[b] = subjectName[b].Replace("\r", "").Replace("\n", "");
            if (translation[i].Replace("\"", "") == subjectName[b])
            {
                subjectInt.Add(i);
                if (b < subjects.Count - 1)
                {
                    b += 1;
                }
            }
            if (translation[i].Contains("_"))
            {
                translation[i] = translation[i].Replace("_", "");
                tempTrans.Add(translation[i]);
            }
            else
            {
                translation[i] = null;
            }
        }
        for(int i = 0; i < subjectInt.Count; i++)
        {
            if(i < subjectInt.Count - 1)
            {
                subjects[i].amount = subjectInt[i + 1] - subjectInt[i] - 2;
            }
            else
            {
                subjects[i].amount = englishTextFile.text.Split('\n').Length - subjectInt[i] - 1;
            }
        }
        for (int i = 0; i < pinyin.Length; i++)
        {
            if (pinyin[i].Contains("_"))
            {
                pinyin[i] = pinyin[i].Replace("_", "").Replace("\r", "").Replace("\n", "");
                tempPinyin.Add(pinyin[i]);
            }
            else
            {
                pinyin[i] = null;
            }
        }
        characters = tempChar.ToArray();
        translation = tempTrans.ToArray();
        pinyin = tempPinyin.ToArray();
        // Reset unsorted clips
        audioClips = new AudioClip[characters.Length];
        // Retrieves all MP3's from Assets/MP3s
        for (int i = 0; i < characters.Length; i++)
        {
            audioClips[i] = Resources.Load<AudioClip>("MP3/" + characters[i]);
        }
    }

    [ContextMenu("CreatePool")]
    public void CreatePool()
    {
        int allAmount = 0;
        intNotInPool = new List<int>();
        for(int i = 0; i < subjects.Count; i++)
        {
            if(i > 0)
            {
                allAmount += subjects[i - 1].amount;
            }
            if (!subjects[i].inPool)
            {
                if(i > 0)
                {
                    for (int b = 0; b < subjects[i].amount; b++)
                    {
                        intNotInPool.Add(allAmount + b);
                    }
                }
                else
                {
                    for (int b = 0; b < subjects[i].amount; b++)
                    {
                        intNotInPool.Add(b);
                    }
                }

            }
        }
        pool = new List<Pool>();
        for (int i = 0; i < translation.Length; i++)
        {
            if (!intNotInPool.Contains(i))
            {
                Pool pl = new Pool();
                pl.translation = translation[i];
                pl.character = characters[i];
                pl.audio = audioClips[i];
                pool.Add(pl);
            }
        }
    }
}

[System.Serializable]
public class Subjects
{
    public string name;
    public bool inPool;
    public int amount;
}

[System.Serializable]
public class Pool
{
    public string character;
    public string translation;
    public AudioClip audio;
}