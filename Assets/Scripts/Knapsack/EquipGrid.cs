using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipGrid : MonoBehaviour
{

    public List<EquipItemGrid> ItemList = new List<EquipItemGrid>();


    void Start()
    {
        if (Inventory._intance != null)
        {
            Inventory._intance.equipGrid = this;
        }
       
        else
        {
            Debug.Log("未实例化");
        }

        for (int i = 0; i < 6; i++)
        {
            ItemList.Add(transform.GetChild(i).GetComponent<EquipItemGrid>());
        }
    }


    public void EquipItemGrid_OnEnter(int ItemID)
    {
        switch (ItemID)
        {

            case 1001:
                ItemList[0].transform.GetChild(0).GetComponent<Image>().sprite= Resources.Load<Sprite>("inventory_icons/gloves");
                ItemList[0].transform.GetChild(0).name = "手套";
                break;
            case 1002:
                ItemList[0].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("inventory_icons/sword");
                ItemList[0].transform.GetChild(0).name = "大剑";
                break;
            case 1003:
                ItemList[0].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("inventory_icons/axe");
                ItemList[0].transform.GetChild(0).name = "重斧";
                break;
            case 1004:
                ItemList[1].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("inventory_icons/armor");
                ItemList[1].transform.GetChild(0).name = "盔甲";
                break;
            case 1005:
                ItemList[4].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("inventory_icons/pants");
                ItemList[4].transform.GetChild(0).name = "裤子";
                break;
            case 1006:
                ItemList[3].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("inventory_icons/helmets");
                ItemList[3].transform.GetChild(0).name = "头盔";
                break;
            case 1007:
                ItemList[2].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("inventory_icons/belts");
                ItemList[2].transform.GetChild(0).name = "腰带";
                break;
            case 1008:
                ItemList[5].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("inventory_icons/boots");
                ItemList[5].transform.GetChild(0).name = "靴子";
                break;
            default:
                break;
        }

    }
}
