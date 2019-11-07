using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;



public class SaveManager : MonoBehaviour
{
    DataToSave saveData;

    public SaveManager(string path)
    {
        saveData = LoadDataFromJson(path);
    }

    public bool Check(string path)
    {
        return File.Exists(path);
    }

    public void Save(string path)
    {
        SaveDataToJson(path);
    }

    private DataToSave LoadDataFromJson(string path)
    {
        if (Check(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<DataToSave>(json);
        }
        else
        {
            return new DataToSave(0);
        }
    }

    private void SaveDataToJson(string path)
    {
        string json = JsonUtility.ToJson(saveData, true);
        StreamWriter sw = File.CreateText(path);
        sw.Close();
        File.WriteAllText(path, json);
    }

    public void ClearDataBase()
    {
        saveData.ChatList.Clear();
    }

    public void RemoveAt(int index)
    {
        saveData.ChatList.RemoveAt(index);
    }

    public void set_ChatDataList(string data)
    {
        saveData.ChatList.Add(data);
    }

    public void Get_ChatDataList()
    {
        if (saveData.ChatList.Count != 0)
        {
            for (int i = 0; i < saveData.ChatList.Count; i++)
            {
               DataManager.Instance.ChatList.Add(saveData.ChatList[i]);
            }
        }
    }
}
