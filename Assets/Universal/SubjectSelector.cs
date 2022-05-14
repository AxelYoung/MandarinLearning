using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SubjectSelector : MonoBehaviour
{
    private GameObject subjectToggle;
    private CharLibrary lib;

    void Start()
    {
        subjectToggle = Resources.Load<GameObject>("Prefabs/SubjectToggle");
        lib = GameObject.FindGameObjectWithTag("Lib").GetComponent<CharLibrary>();
        lib.SplitText();
        int i = 0;
        foreach(Subjects sub in lib.subjects)
        {
            GameObject st = Instantiate(subjectToggle, transform.GetChild(0).GetChild(0).transform);
            st.name = i.ToString();
            st.GetComponent<Toggle>().onValueChanged.AddListener((bool v) => ToggleSubject(v));
            st.transform.GetChild(1).GetComponent<Text>().text = sub.name;
            i += 1;
        }
    }

    void ToggleSubject(bool value)
    {
        int sub = int.Parse(EventSystem.current.currentSelectedGameObject.name);
        print(sub + " + " + value);
        lib.subjects[sub].inPool = value;
        lib.CreatePool();
    }
}
