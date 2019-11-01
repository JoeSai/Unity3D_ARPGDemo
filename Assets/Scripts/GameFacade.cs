using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFacade : MonoBehaviour
{
    private static GameFacade _instance;
    public static GameFacade Instance { get { return _instance; } }

    public GameObject Map;
    public int RoomCount;
    private UIManager uiMng;
    private DungenMapManager DgMapMng;
    private EnemyManage EnMng;
    

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
        RoomCount = 1;
    }

    void Start()
    {
        Map = GameObject.Find("Map");
        InitManager();
        
    }
    private void Update()
    {
        UpdateManager();
       
    }
    private void InitManager()
    {
        uiMng = new UIManager(this);
        DgMapMng = new DungenMapManager(this);
        EnMng = new EnemyManage(this);

        uiMng.OnInit();
        DgMapMng.OnInit();
        EnMng.OnInit();
    }

    private void UpdateManager()
    {
        uiMng.Update();
        DgMapMng.Update();
        EnMng.Update();
    }
    private void DestroyManager()
    {
        uiMng.OnDestroy();
     
    }

    public void Coroutines(IEnumerator coroutines)
    {
        StartCoroutine(coroutines);
    }
}
