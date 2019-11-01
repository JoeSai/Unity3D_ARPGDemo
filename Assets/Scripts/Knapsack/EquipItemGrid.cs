using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipItemGrid : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{

    private Image itemImage;
    private bool isInEquip = false;
    private void Start()
    {
        itemImage = this.transform.GetChild(0).GetComponent<Image>();
    }
    private void Update()
    {
        if (isInEquip && Input.GetMouseButton(1))
        {
            if (itemImage.name != "f")
            {
                itemImage.sprite = Resources.Load<Sprite>("inventory_icons/f");
                itemImage.gameObject.name = "f";
            }
        }
    }


    public static Action<Transform> OnEnter;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerEnter.tag == "Grid")
        {
            isInEquip = true;
           
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        isInEquip = false;
    }

}
