using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public class Inventory : MonoBehaviour {

    public static Inventory _intance;
    public List<InventoryItemGrid> itemGridList = new List<InventoryItemGrid>();
    public GameObject InventoryItem;
    public Image MoveGood;
    public EquipGrid equipGrid;
    private Transform emEmptyGrid;
    private int num=0;
    private Inventory_Item Inventory_Item;
    private Text DescriptionText;

    private void Awake()
    {
        _intance = this;
        //InventoryItemGrid.OnLeftBeginDrag += Inventory_Item_OnLeftBeginDrag;
        //InventoryItemGrid.OnLeftEndDrag += Inventory_Item_OnLeftEndDrag;
        InventoryItemGrid.OnEnter += InventoryItemGrid_OnEnter;
        InventoryItemGrid.OnExit += InventoryItemGrid_OnExit;
        DescriptionText = this.transform.parent.transform.Find("ContentDescription").GetComponentInChildren<Text>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            //    GetId(UnityEngine.Random.Range(1001, 1008),1);
            for (int i = 0; i < 8; i++)
            {
                GetId(1001 + i, 1);
            }
        }
        
    }

    public void GetId(int id,int number)
    {
        InventoryItemGrid grid = null;

        if (itemGridList.Count < 1&& number > 0)
        {
            GameObject itemGo = GameObject.Instantiate(InventoryItem);
            itemGo.transform.SetParent(this.transform);
            grid = itemGo.GetComponent<InventoryItemGrid>();
            grid.SetId(id, number);
            itemGridList.Add(grid);

        }
        else
        {
            foreach (InventoryItemGrid temp in itemGridList)
            {
                if (temp.id == id)
                {
                    grid = temp;
                    break;
                }
            }
            if (grid != null && number > 0)
            {
                grid.PlusNumber(number);
            }
            else
            {
                if (number > 0)
                {
                    GameObject itemGo = GameObject.Instantiate(InventoryItem);
                    itemGo.transform.SetParent(this.transform);
                    grid = itemGo.GetComponent<InventoryItemGrid>();
                    grid.SetId(id, number);
                    itemGridList.Add(grid);
                }
            }
        }
   
    }

    //private void Inventory_Item_OnLeftBeginDrag(Transform gridTransform)
    //{
    //    Transform gridTransformChild = gridTransform.transform.GetChild(0);
    //    if (gridTransform.childCount == 1)
    //    {
    //        return;
    //    }
    //    else
    //    {
    //        InventoryItemGrid Inventory_Item_Grid = gridTransform.GetComponent<InventoryItemGrid>();
    //        string infoName = ObjectsInfo.Intance.GetItem(gridTransformChild.name);
    //        MoveGood.enabled = true;
    //        Sprite sp = Resources.Load<Sprite>(infoName);
    //        MoveGood.sprite = sp;
    //        MoveGood.name = gridTransformChild.name;
    //        num = Inventory_Item_Grid.num;
    //        Destroy(gridTransformChild.gameObject);
    //    }
    //}
    //private void Inventory_Item_OnLeftEndDrag(Transform prevTransform, Transform enterTransform)
    //{
    //    InventoryItemGrid gridenter = null;
    //    InventoryItemGrid gridprev = null;
    //    gridprev = prevTransform.GetComponent<InventoryItemGrid>();
    //    ObjectInfo info = ObjectsInfo.Intance.GetId(MoveGood.name);
    //    if (enterTransform == null)//扔东西
    //    {           
    //        gridprev.CleanId();
    //        MoveGood.enabled = false;
    //        Debug.LogWarning("物品已扔");
    //    }
    //    else if (enterTransform.tag == "Grid")//拖到另一个或者当前格子里
    //    {
    //        gridprev.CleanId();
    //        if (enterTransform.childCount == 1)//直接扔进去
    //        {               
    //            gridenter = enterTransform.GetComponent<InventoryItemGrid>();
    //            Getitem(enterTransform,info);               
    //            gridenter.SetId(info.id, num);
    //        }
    //        else
    //        {
    //            Transform enterTransformChild = enterTransform.transform.GetChild(0);
    //            gridenter = enterTransform.GetComponent<InventoryItemGrid>();               
    //            int numEnter = gridenter.num;
    //            ObjectInfo infoEnter = ObjectsInfo.Intance.GetId(enterTransformChild.name);
    //            Getitem(prevTransform, infoEnter);               
    //            gridprev.exchangeId(infoEnter.id,numEnter, Inventory_Item);                
    //            Destroy(enterTransformChild.gameObject);
    //            Getitem(enterTransform, info);
    //            gridenter.exchangeId(info.id, num, Inventory_Item);
    //        }
    //    }
    //    else
    //    {           
    //        Getitem(prevTransform,info);
    //        gridprev.SetId(info.id, num);            
    //    }
    //}



    private void InventoryItemGrid_OnEnter(Transform gridTransform)
    {
        Transform gridTransformChild = gridTransform.transform.GetChild(0);
        ObjectInfo info = ObjectsInfo.Intance.GetId(gridTransformChild.name);
        if (info == null)
            return;
        //string text = GetTooltipText(info);
        string text = info.name + "\nTest";
        DescriptionText.text = text;
    }
    private void InventoryItemGrid_OnExit()
    {
        DescriptionText.text = "";
    }

    public void EquipGood(InventoryItemGrid grid)
    {
      
     equipGrid.EquipItemGrid_OnEnter(grid.id);
        itemGridList.Remove(grid);
        Destroy(grid.gameObject);
    }


    public void Getitem(Transform transform,ObjectInfo info)
    {       
        GameObject itemGo = GameObject.Instantiate(InventoryItem);
        Inventory_Item = itemGo.GetComponent<Inventory_Item>();
        itemGo.transform.SetParent(transform);
        itemGo.transform.localPosition = Vector3.zero;
        itemGo.name = info.name;
        MoveGood.enabled = false;       
    }


    //public string GetTooltipText(ObjectInfo info)
    //{
    //    if (info == null)
    //        return "";
    //    StringBuilder Text = new StringBuilder();
    //    Text.AppendFormat("{0}\n\n", info.name);
    //    switch (info.type)
    //    {
    //        case ObjectType.Drug:               
    //            Text.AppendFormat("HP+{0}\nMP+{1}\n\n", info.hp, info.mp); 
    //            break;
    //        case ObjectType.Equip:
    //            switch (info.equipType)
    //            {
    //                case EquipType.Armor:
    //                    {
    //                        if (info.user == "Swordman")
    //                            Text.AppendFormat("力量+{0}\n防御力+{1}\n\n", info.strength, info.defense);
    //                        else
    //                            Text.AppendFormat("智力+{0}\n防御力+{1}\n\n", info.intelligence, info.defense);
    //                    }                       
    //                    break;
    //                case EquipType.Shoe:
    //                    Text.AppendFormat("力量+{0}\n智力+{1}\n敏捷+{2}\n防御力+{3}\n\n", info.strength, info.intelligence,info.agility,info.defense);
    //                    break;
    //                case EquipType.Headgear:
    //                    Text.AppendFormat("力量+{0}\n智力+{1}\n防御力+{2}\n\n", info.strength, info.intelligence,info.defense);
    //                    break;
    //                case EquipType.Accessory:
    //                    Text.AppendFormat("力量+{0}\n智力+{1}\n敏捷+{2}\n\n", info.strength, info.intelligence, info.agility);
    //                    break;
    //                case EquipType.LeftHand:
    //                    {
    //                        if (info.user == "Magician")
    //                            Text.AppendFormat("智力+{0}\n防御力+{1}\n\n", info.intelligence, info.defense);                           
    //                        else
    //                            Text.AppendFormat("力量+{0}\n防御力+{1}\n\n", info.strength, info.defense);
    //                    }                        
    //                    break;
    //                case EquipType.RightHand:
    //                    {
    //                        if (info.user == "Swordman")
    //                            Text.AppendFormat("力量+{0}\n\n", info.strength);
    //                        else
    //                            Text.AppendFormat("智力+{0}\n\n", info.intelligence);
    //                    }                       
    //                    break;
    //            }               
    //            break;
    //        case ObjectType.Mat:
    //            //Text.AppendFormat("攻击:{0}\n\n");
    //            break;
    //        default:
    //            break;
    //    }
    //    Text.AppendFormat("购买价格：{0}\n出售价格：{1}\n", info.price_buy, info.price_sell);
    //    return Text.ToString();
    //}

}
