using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Result : MonoBehaviour
{
    private void Start()
    {
        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            child.SetActive(true);
            child.SetActive(false);
        }
    }
}
