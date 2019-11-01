using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipPanel : BasePanel
{
    private Button closeButton;

    private void Start()
    {
        closeButton = transform.Find("CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(OnCloseClick);
    }

    public override void OnEnter()
    {
        base.OnEnter();
        uIManager.havePanel = true;
        this.gameObject.SetActive(true);
    }

    public override void OnExit()
    {
        base.OnExit();
        this.gameObject.SetActive(false);
        uIManager.havePanel = false;
    }


    private void OnCloseClick()
    {
        uIManager.PopPanel();
    }
}
