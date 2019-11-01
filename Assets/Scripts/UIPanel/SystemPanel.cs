using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SystemPanel : BasePanel
{
    private Button closeButton;
    private Button quitGameButton;


    private void Start()
    {
        uIManager.havePanel = true;
        closeButton = transform.Find("CloseButton").GetComponent<Button>();
        quitGameButton = transform.Find("QuitGame").GetComponent<Button>();
        closeButton.onClick.AddListener(OnCloseClick);
        quitGameButton.onClick.AddListener(OnQuitGameClick);
    }
    public override void OnEnter()
    {
        base.OnEnter();
        uIManager.havePanel = true;
        this.gameObject.SetActive(true);
    }

    private void OnQuitGameClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        Debug.Log("退出游戏");
    }
    private void OnCloseClick()
    {
        uIManager.PopPanel();
    }
  
    public override void OnExit()
    {
        base.OnExit();
        this.gameObject.SetActive(false);
        uIManager.havePanel = false;
    }
   
}
