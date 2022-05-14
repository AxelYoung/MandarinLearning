using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public GameObject[] buttons;
    public GameObject scaleOver;
    public Animator anim;

    public void SceneTransition(int button)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i != button)
            {
                buttons[i].transform.SetParent(scaleOver.GetComponent<RectTransform>());
                print(i);
            }
        }
        StartCoroutine(Transition(button));
    }

    public IEnumerator Transition(int scene)
    {
        anim.SetInteger("Trans", scene);
        yield return new WaitForSeconds(3);
        if(scene == 0)
        {
            SceneManager.LoadScene("Games");
        }
        if (scene == 1)
        {
            SceneManager.LoadScene("Vocab");
        }
        if (scene == 2)
        {
            SceneManager.LoadScene("Cards");
        }
        if (scene == 3)
        {
            SceneManager.LoadScene("Tests");
        }

    }
}
