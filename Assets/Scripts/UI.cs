using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] GameObject nameUI;
    [SerializeField] GameObject timeUI;
    [SerializeField] GameObject saveAmountUI;

    TextMeshProUGUI nameUIText;
    TextMeshProUGUI timeUIText;
    TextMeshProUGUI saveAmountUIText;
    // Start is called before the first frame update
    void Awake()
    {
        nameUIText = nameUI.GetComponent<TextMeshProUGUI>();
        timeUIText = timeUI.GetComponent<TextMeshProUGUI>();
        saveAmountUIText = saveAmountUI.GetComponent <TextMeshProUGUI>();
    }

    // Update is called once per frame
    public void LoadUI(string newName, string newTime, string newSaveAmount)
    {
        nameUIText.text = newName;
        timeUIText.text = newTime;
        saveAmountUIText.text = newSaveAmount;
    }
}
