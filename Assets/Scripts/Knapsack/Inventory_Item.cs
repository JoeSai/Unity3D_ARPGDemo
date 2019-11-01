using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_Item : MonoBehaviour {

    private Image Image;       
    private void Awake()
    {
        Image = this.GetComponent<Image>();    
    }   
    public void SetIconName(string icon_name)
    {        
        Sprite sp = Resources.Load<Sprite>("inventory_icons/"+icon_name);
        Image.sprite = sp;
    }
}
