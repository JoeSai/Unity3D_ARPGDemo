using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemGrid : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler

{

    public int id;
    private ObjectInfo info = null;
    public bool isDrag = false;
    public int num = 0;
    private Text numLabel;
    private bool isHaveGood=false;
    private bool isInGood = false;
    public RectTransform Description;
    private void Awake()
    {
        numLabel = this.GetComponentInChildren<Text>();
        Description = GameObject.Find("KnapsackPanel(Clone)").transform.Find("ContentDescription")as RectTransform ;
    }
    void Update()
    {
        Vector3 mouseWorldPosition;

        //判断是否点到UI图片上的时候
        if (isInGood)
        {          
            RectTransformUtility.ScreenPointToWorldPointInRectangle(Description, Input.mousePosition, null, out mouseWorldPosition);
            Description.position = mouseWorldPosition;



            if (Input.GetMouseButtonDown(1))
            {
                Inventory._intance.EquipGood(this);
                isInGood = false;
                Description.gameObject.SetActive(false);
            }
        }
    }
    
    public void SetId(int id, int num)
    {
        this.id = id;
        info = ObjectsInfo.Intance.GetObjectInfoById(id);
        Inventory_Item item = this.GetComponentInChildren<Inventory_Item>();
        item.SetIconName(info.icon_name);
        item.name = info.name;
        this.num = num;
        numLabel.text = num.ToString();
        numLabel.transform.SetSiblingIndex(1);
    }
    //交换物品栏
    //public void exchangeId(int id,int num,Inventory_Item item)
    //{
    //    this.id = id;
    //    info = ObjectsInfo.Intance.GetObjectInfoById(id);
    //    item.SetIconName(info.icon_name);
    //    this.num = num;
    //    numLabel.text = num.ToString();
    //    numLabel.transform.SetSiblingIndex(2);
    //    numLabel.enabled = true;
    //}


    
    //public void CleanId()
    //{
    //    this.id = 0;
    //    info = null;
    //    this.num = 0;
    //    numLabel.text = num.ToString();
    //    numLabel.enabled = false;
    //}


    public void PlusNumber(int num)
    {
        this.num += num;
        numLabel.text = this.num.ToString();
    }
    //public void MinusNumber(int num)
    //{
    //    this.num -= num;
    //    numLabel.text = this.num.ToString();
    //}

    //拖拽物品功能
    //#region Enter&&Exit 
    //public static Action<Transform> OnLeftBeginDrag;
    //public static Action<Transform, Transform> OnLeftEndDrag;
    //public void OnBeginDrag(PointerEventData eventData)
    //{
    //    if (eventData.button == PointerEventData.InputButton.Left)
    //    {
    //        if (this.transform.childCount != 1)
    //            isHaveGood = true;
    //        if (OnLeftBeginDrag != null && isHaveGood)
    //        {
    //            OnLeftBeginDrag(transform);
    //            numLabel.enabled = false;
    //        }
    //    }
    //}

    //public void OnDrag(PointerEventData eventData)
    //{
    //    if (isHaveGood)
    //    {
    //        isDrag = true;
    //        isMoveInGood = false;
    //        Description.gameObject.SetActive(false);
    //        SetDragObjPostion(eventData);
    //    }
    //}

    //public void OnEndDrag(PointerEventData eventData)
    //{

    //    if (eventData.button == PointerEventData.InputButton.Left)
    //    {
    //        isDrag = false;
    //        if (OnLeftEndDrag != null && isHaveGood)
    //        {
    //            if (eventData.pointerEnter == null)
    //            {
    //                OnLeftEndDrag(transform, null);
    //                isHaveGood = false;
    //            }
    //            else
    //            {
    //                OnLeftEndDrag(transform, eventData.pointerEnter.transform);
    //                isHaveGood = false;
    //            }
    //        }
    //    }
    //}
    //#endregion


    //物品描述显示



    #region Enter&&Exit
    public static Action<Transform> OnEnter;
    public static Action OnExit;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerEnter.tag == "Grid")
        {
            if (this.transform.childCount != 1)
                isInGood = true;
            if (OnEnter != null && isInGood)
            {
                Description.gameObject.SetActive(true);
                OnEnter(transform);               
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
            if (OnExit != null && isInGood)
            {
                OnExit();
                isInGood = false;
                Description.gameObject.SetActive(false);
            }
    }

   
    #endregion

    //void SetDragObjPostion(PointerEventData eventData)
    //{       
    //     Vector3 mouseWorldPosition;

    //    //判断是否点到UI图片上的时候
    //    if (RectTransformUtility.ScreenPointToWorldPointInRectangle(MoveGood, eventData.position, eventData.pressEventCamera, out mouseWorldPosition))
    //    {           
    //        if (isDrag)
    //        {
    //            MoveGood.position = mouseWorldPosition;
    //        }
    //    }
    //}
}
