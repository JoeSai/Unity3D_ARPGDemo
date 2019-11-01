using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHp : MonoBehaviour
{
    public Image background;
    public Image foreground;

    

    //血条颜色信息数组 
    private Color[] hpColors = { Color.red, Color.yellow, Color.green };

    //一层血条的血量
    public int singleLayerHP = 100;

    //总血量 
    public int totalHP = 300;

    //当前剩余血量
    private int currentHP;
    public int CurrentHP
    {
        get
        {
            return currentHP;
        }

        set
        {
            currentHP = value;
            
            ShowHPLayer();
        }
    }

    private void Start()
    {
        CurrentHP = totalHP;
    }

   
    public void ChangeHP(int num)
    {
        CurrentHP -= num;
        if (CurrentHP <= 0)
        {
            CurrentHP = 0;
        }
    }

   
    private void ShowHPLayer()
    {      
        if (CurrentHP == 0)
        {
            foreground.fillAmount = 0;
            
            return;
        }

        //首先计算出当前血量是第几层血
        int layerNum = CurrentHP / singleLayerHP;
        //无法整除的情况下多加1层
        if (CurrentHP % singleLayerHP != 0)
        {
            layerNum++;
        }
       

        //根据层数获取对应前景色
        int foregroundColorIndex = (layerNum % hpColors.Length) - 1;
        //层数是颜色数组长度的整数倍时，颜色为最后1种颜色
        if (foregroundColorIndex == -1)
        {
            foregroundColorIndex = hpColors.Length - 1;
        }
        foreground.color = hpColors[foregroundColorIndex];

        
        if (layerNum == 1)
        {
            background.color = Color.black;
        }
        else
        {
            int backgroundColorIndex;
            if (foregroundColorIndex != 0)
            {
                //前景色索引不为0就倒退1位（x不为0时，x层的背景色=x-1层的前景色）
                backgroundColorIndex = foregroundColorIndex - 1;
            }
            else
            {
               
                backgroundColorIndex = hpColors.Length - 1;
            }

            background.color = hpColors[backgroundColorIndex];
        }

        
        float length = 1.0f * (CurrentHP % singleLayerHP) / singleLayerHP;       
        if (length == 0)
        {
            length = 1;
        }       
        foreground.fillAmount = length;
    }


   
}
