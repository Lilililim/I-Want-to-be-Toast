using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    public static timer instance;

    public float timeStart = 0f;
    public Text textbox;

    private void Start()
    {
        textbox.text = timeStart.ToString();
    }
    private void Update()
    {
        timeStart += Time.deltaTime;
        textbox.text = Mathf.Round(timeStart).ToString();
    }
    private void Awake()
    {
        instance = this;

        DontDestroyOnLoad(this.gameObject);
    }
}
