using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StaticDataObject
{
    [System.Serializable]
    public class StaticData
    {
        public int id;
        public string DID;
        public string name;
        public List<Progressions> progressions;
    }

    [System.Serializable]
    public class StaticDataList
    {
        public List<StaticData> dataList;
    }
}
