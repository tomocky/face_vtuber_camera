using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSwitch : MonoBehaviour
{
    [SerializeField] private Button openButton;
    [SerializeField] private GameObject panel;

    private void Start()
    {
        openButton.gameObject.SetActive(true);
        panel.SetActive(false);
    }

    public void open()
    {
        openButton.gameObject.SetActive(false);
        panel.SetActive(true);
    }

    public void close()
    {
        openButton.gameObject.SetActive(true);
        panel.SetActive(false);
    }
}
