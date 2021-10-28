using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataID
{
    public int id;
    public string DID;
    public string name;

    public void SetName()
    {
        if (DID.Length != 0 )
        {
            string[] names = DID.Split('_');
            if(names.Length==2)
            {
                this.name = names[1];
            }
            else
            {
                this.name = names[0];
            }
        }
    }

   
}


[System.Serializable]
public class DataIDList
{
    static public DataIDList instance;
    public List<DataID> dataList;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            instance.dataList = this.dataList;
        }
        else
        {
            // something;
        }

    }

    public DataID FindDataID(string name)
    {
        foreach (DataID did in dataList)
        {
            if (did.name.Equals(name))
            {
                return did;
            }
        }
        //if not found
        return null;
    }
}



