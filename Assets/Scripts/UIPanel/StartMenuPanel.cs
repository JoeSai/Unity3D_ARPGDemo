using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StartMenuPanel : BasePanel
{
    private Button CloseButton;

    public override void OnEnter()
    {
        base.OnEnter();
        CloseButton = transform.Find("CloseButton").GetComponent<Button>();
        CloseButton.onClick.AddListener(OnClick);
        Debug.Log("加载成功！");
    }

    private void OnClick()
    {
        Debug.Log("点击关闭");
        uIManager.PopPanel();
        //uIManager.PushPanel(UIPanelType.System);
    }

    public override void OnExit()
    {
        base.OnExit();
        this.gameObject.SetActive(false);
        uIManager.havePanel = false;
        transform.DOMove(new Vector2(1000, 0), 0.1f);
    }
}
