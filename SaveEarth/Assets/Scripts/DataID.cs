using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataID : MonoBehaviour
{
    private int id;
    private string name;

    public int ID
    {
        get
        {
            return id;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }
    }
    /// <summary>
    /// Makes a DataId for ingame object
    /// 0 - Town Center
    /// 1 - Farm
    /// 2 - Factory
    /// 3 - Filteration Plant
    /// 4 - House
    /// 11 - Food
    /// 12 - Wood
    /// 13 - Stone
    /// 14 - Metal
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="name"></param>
    public DataID(int ID, string name)
    {
        this.id = ID;
        this.name = name;
    }
   
}


