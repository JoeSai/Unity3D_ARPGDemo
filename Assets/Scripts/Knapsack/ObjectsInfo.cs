using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class ObjectsInfo : MonoBehaviour {

    private  static ObjectsInfo _intance;
    public static ObjectsInfo Intance { get { return _intance; } } 

    private Dictionary<int, ObjectInfo> objectInfoDict = new Dictionary<int, ObjectInfo>();
    private static Dictionary<string, ObjectInfo> GridName = new Dictionary<string, ObjectInfo>();
   // private static Dictionary<string, ObjectInfo> GridIconName = new Dictionary<string, ObjectInfo>();

    private void Awake()
    {
        _intance = this;
        ParseObjectsInfoJson();
    }

    private void Start()
    {


    }
    public ObjectInfo GetObjectInfoById(int id)
    {
        ObjectInfo info =null;
        objectInfoDict.TryGetValue(id, out info);
        return info;
    }
    public string GetItem(string name)
    {
        ObjectInfo infoName =null;
        GridName.TryGetValue(name, out infoName);
        return infoName.icon_name;
    }
    public ObjectInfo GetId(string name)
    {
        ObjectInfo info=null ;
        GridName.TryGetValue(name, out info);
        return info;
    }



    private void ParseObjectsInfoJson()
    {
        
         TextAsset ta = Resources.Load<TextAsset>("ObjectsInfoList");

        JsonData InfoJD = JsonMapper.ToObject(ta.text);

        JsonData infoList = InfoJD["infoList"];

        foreach (JsonData jsonInfo in infoList)
        {
            ObjectInfo info = new ObjectInfo();
            int id = (int)jsonInfo["id"];
            string name = jsonInfo["name"].ToString();
            string iconName = jsonInfo["iconName"].ToString();
            info.id = id;
            info.name = name;
            info.icon_name = iconName;
            objectInfoDict.Add(id, info);
            GridName.Add(info.name, info);
           // GridIconName.Add(info.icon_name, info);
        }
    }

    private void test()
    {
        foreach (var item in objectInfoDict)
        {
            Debug.Log(item.Value.id+" "+ item.Value.name+" "+ item.Value.icon_name);
        }
    }

}

public class ObjectInfo
{
    //属性
    public int id;
    public string name;
    public string icon_name;

}
