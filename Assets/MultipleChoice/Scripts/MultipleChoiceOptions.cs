using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MultipleChoiceOptions : MonoBehaviour
{
    public TextMeshProUGUI typeText; 
    public string type;
    public TMP_Text amountText;
    public int questionAmount;
    public Animator anim;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MultipleChoice")
        {
            // Set all important values to correct instances and then remove gameobject
            GameObject gm = GameObject.Find("GameMaster");
            gm.GetComponent<MultipleChoiceTestGeneration>().testsAmount = questionAmount;
            gm.GetComponent<MultipleChoiceTestGeneration>().testType = type;
            gm.GetComponent<MultipleChoiceTestDisplay>().StartTest();
            Destroy(gameObject);
        }
    }

    public void ChangeScene()
    {
        anim.SetBool("Revert", true);
        StartCoroutine(ChangeSceneIEnum());
    }

    public IEnumerator ChangeSceneIEnum()
    {
        yield return new WaitForSeconds(1);
        // Set values accordingly and change scene
        type = typeText.text;
        questionAmount = int.Parse(amountText.text);
        SceneManager.LoadScene("MultipleChoice");
    }
}
