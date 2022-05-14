using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Flashcards : MonoBehaviour
{
    private GameObject flashcard;
    private CharLibrary lib;
    public Animator anim;

    void Start()
    {
        flashcard = Resources.Load<GameObject>("Prefabs/Flashcard");
        lib = GameObject.FindGameObjectWithTag("Lib").GetComponent<CharLibrary>();
        int i = 0;
        lib.SplitText();
        foreach (string ch in lib.characters)
        {
            GameObject fc = Instantiate(flashcard, transform.GetChild(0).GetChild(0).transform);
            fc.name = i.ToString();
            fc.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => ChangeText());
            fc.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => PlayAudio());
            fc.transform.GetChild(1).GetComponent<Text>().text = ch;
            transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().position += new Vector3(0, -280,0);
            transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta += new Vector2(0, 532.75f);
            i += 1;
        }
    }

    public void ChangeText()
    {
        int pool = int.Parse(EventSystem.current.currentSelectedGameObject.transform.parent.name);
        if(EventSystem.current.currentSelectedGameObject.GetComponent<Text>().text == lib.characters[pool])
        {
            EventSystem.current.currentSelectedGameObject.GetComponent<Text>().text = lib.translation[pool];
        }
        else
        {
            EventSystem.current.currentSelectedGameObject.GetComponent<Text>().text = lib.characters[pool];
        }
    }

    public void PlayAudio()
    {
        int pool = int.Parse(EventSystem.current.currentSelectedGameObject.transform.parent.name);
        GetComponent<AudioSource>().clip = lib.audioClips[pool];
        GetComponent<AudioSource>().Play();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(Exit());
        }
    }

    public IEnumerator Exit()
    {
        anim.SetBool("Revert", true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("MainMenu");
    }
}
