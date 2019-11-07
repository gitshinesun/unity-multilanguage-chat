using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct DataToSave 
{
    public List<string> ChatList;

    public DataToSave(int data)
    {
        ChatList = new List<string>();
    }
}
