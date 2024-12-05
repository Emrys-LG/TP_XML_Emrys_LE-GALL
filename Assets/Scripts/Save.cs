using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Save : MonoBehaviour
{
    int saveAmount;
    float timeSpent = 0.0f;
    string name;
    UI uI;

    private void Start()
    {
        uI = GetComponent<UI>(); 
        LoadGame();
        
    }

    private void Update()
    {
        timeSpent += Time.deltaTime;
    }
    void LoadGame()
    {
        XmlDocument saveFile = new XmlDocument();
        if (!System.IO.File.Exists(Application.persistentDataPath +  "/" + "Save" + ".xml"))
        {
            NoSaveFound();
            return;
        }

        saveFile.LoadXml(System.IO.File.ReadAllText(Application.persistentDataPath + "/" + "Save" + ".xml"));

        string key;
        string value;
        foreach (XmlNode node in saveFile.ChildNodes[1])
        {
            key = node.Name;
            value = node.InnerText;
            switch (key) 
            {
                case "name":
                    name = value; 
                    break;
                case "saveAmount":
                    saveAmount = Convert.ToInt32(value); 
                    break;
                case "timer":
                    timeSpent = float.Parse(value);
                    break;
            }
        }
        int secondsSpent = Mathf.FloorToInt(timeSpent);
        uI.LoadUI(name, secondsSpent.ToString(), saveAmount.ToString());
    }
    public void DoSave()
    {
        Debug.Log("DoSave");
        XmlWriterSettings xmlWriterSettings = new XmlWriterSettings
        {
            NewLineOnAttributes = true,
            Indent = true
        };
        saveAmount++;
        XmlWriter writer = XmlWriter.Create(Application.persistentDataPath + "/" + "Save" + ".xml", xmlWriterSettings);
        writer.WriteStartDocument();
        writer.WriteStartElement("Data");
        WriteXml(writer, "name", "Emrys");
        WriteXml(writer, "timer", timeSpent.ToString());
        WriteXml(writer, "saveAmount", saveAmount.ToString());

        writer.WriteEndElement();
        writer.WriteEndDocument();

        writer.Close();
        LoadGame();
    }

    void NoSaveFound()
    {
        Debug.Log("No save found");
        XmlWriterSettings xmlWriterSettings = new XmlWriterSettings
        {
            NewLineOnAttributes = true,
            Indent = true
        };
        XmlWriter writer = XmlWriter.Create(Application.persistentDataPath + "/" + "Save" + ".xml", xmlWriterSettings);
        writer.WriteStartDocument();
        writer.WriteStartElement("Data");
        WriteXml(writer, "name", "none");
        WriteXml(writer, "timer", "0");
        WriteXml(writer, "saveAmount", "0");

        writer.WriteEndElement();
        writer.WriteEndDocument();

        writer.Close();
        LoadGame();
    }

    static void WriteXml(XmlWriter _writer, string _key, string _value)
    {
        _writer.WriteStartElement(_key);
        _writer.WriteString(_value);
        _writer.WriteEndElement();
    }
}
