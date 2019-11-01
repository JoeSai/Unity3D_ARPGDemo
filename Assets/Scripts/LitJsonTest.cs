using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class LitJsonTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MyCallLitJson();
    }

    private void MyCallLitJson()
    {

        string strJson = "{'heros':[{'name':'SuperMan','power':90}," +

        "{'name':'BatMan','power':87}]}";

        JsonData herosJD = JsonMapper.ToObject(strJson);

        JsonData heros = herosJD["heros"];

        foreach (JsonData hero in heros)
        {

            string name = hero["name"].ToString();

            int power = (int)hero["power"];

            print("name:" + name + "\npower:" + power);

        }
    }
}

