using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private  GameObject pausePanel;
    void Start()
    {
        pausePanel = transform.GetChild(0).gameObject;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) pausePanel.SetActive(!pausePanel.activeSelf);
    }
}
