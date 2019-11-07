using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using UnityEngine;
public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }
    public List<string> ChatList = new List<string>();

    string path = string.Empty;
    public SaveManager saveInstance { get; private set; }
    public ChatManager chatManager;

    private void Awake()
    {        
        path = Path.Combine(Application.persistentDataPath, "saves.json");
        saveInstance = new SaveManager(path);

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void Set_ChatDataList(string data)
    {
        ChatList.Add(data);
        saveInstance.set_ChatDataList(data);

        if (ChatList.Count > chatManager.ChatCount)
        {
            ChatList.RemoveAt(0);
            saveInstance.RemoveAt(0);
        }

        saveInstance.Save(path);
    }

    public void Get_ChatDataList()
    {
        saveInstance.Get_ChatDataList();
    }

    public void ClearDataBase()
    {
        saveInstance.ClearDataBase();
    }

    public void RemoveAt_DB(int index)
    {
        saveInstance.RemoveAt(index);
    }
}
