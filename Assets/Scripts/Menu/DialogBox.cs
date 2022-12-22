using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogBox : MonoBehaviour
{
    public static DialogBox instance { get; private set; }

    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private Button okButton;

    private void Awake()
    {
        instance = this;
        HideDialogBox();
    }
    public void ShowDialogBox(string text)
    {
        gameObject.SetActive(true);
        textMeshPro.text = text;
        okButton.onClick.AddListener(() =>
        {
            HideDialogBox();
        });
    }
    private void HideDialogBox()
    {
        gameObject.SetActive(false);
    }
}
