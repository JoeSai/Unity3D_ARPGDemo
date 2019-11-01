
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using YM.Dungen;

public class DungenMapManager : BaseManager
{
    private GameObject Map;
    private Transform bossRoomPos;
    private DungenGenerator Dg;
    private bool getRoom = false;
    public DungenMapManager(GameFacade facade) : base(facade)
    {
        Map = facade.Map;
    }
    public override void OnInit()
    {
        base.OnInit();
        bossRoomPos = GameObject.Find("BossRoom").transform;
        Dg = Map.AddComponent(typeof(DungenGenerator)) as DungenGenerator;
        Dg.m_generatorData = Resources.Load("MapData/DungenGeneratorData") as DungenGeneratorData;
        Dg.player= (Resources.Load("Prefabs/Player/Player1") as GameObject).transform;
        Dg.Camer = GameObject.Find("Main Camera");
        NavMeshSurface Ns = Map.AddComponent(typeof(NavMeshSurface)) as NavMeshSurface;
        Ns.agentTypeID = NavMesh.GetSettingsByIndex(1).agentTypeID;
        Dg.surface = Ns;      
    }
    public override void Update()
    {
        base.Update();
        if (Dg.m_isFinishGen && getRoom==false)
        {
            getRoom = true;
            facade.RoomCount=GetEnemyRoom(Dg.m_rooms);
            GameObject BossRoomPre = Resources.Load("Prefabs/MapRoom/Room Boss_1") as GameObject;
            GameObject BossRoom= GameObject.Instantiate(BossRoomPre, bossRoomPos.position, Quaternion.identity);
            BossRoom.transform.parent = bossRoomPos;
            GameObject.Find("Canvas").transform.GetChild(0).GetComponent<GamePanel>().UpdateRoomCount(facade.RoomCount);
        }
    }

}
