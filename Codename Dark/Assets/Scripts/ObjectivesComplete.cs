using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectivesComplete : MonoBehaviour
{
    [Header("Objectives To Complete")]
    public TMP_Text objective1;
    public TMP_Text objective2;
    public TMP_Text objective3;
    public TMP_Text objective4;

    public static ObjectivesComplete occurrence;

    private void Awake()
    {
        occurrence = this;
    }

    public void GetObjectivesDone(bool obj1, bool obj2, bool obj3, bool obj4)
    {
        if(obj1 == true)
        {
            objective1.text = "";
            objective1.color = Color.green;
        }
        else
        {
            objective1.text = "";
            objective1.color = Color.white;
        }

        if (obj2 == true)
        {
            objective2.text = "";
            objective2.color = Color.green;
        }
        else
        {
            objective2.text = "";
            objective2.color = Color.white;
        }

        if (obj3 == true)
        {
            objective3.text = "";
            objective3.color = Color.green;
        }
        else
        {
            objective3.text = "";
            objective3.color = Color.white;
        }

        if (obj4 == true)
        {
            objective4.text = "";
            objective4.color = Color.green;
        }
        else
        {
            objective4.text = "";
            objective4.color = Color.white;
        }
    }
}
