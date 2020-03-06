using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NPBehave;

public class SkeletonBehaviorTree : MonoBehaviour
{
    private Root behaviorTree;
    private Blackboard blackboard;
    private Animator anim;
    private Transform target;
    private NavMeshAgent meshAgent;
    private RoomEnemy_2 roomEnemy_2;
    public Animator Anim { get { return anim; } }
    public Transform Target { get { return target; } }
    public NavMeshAgent MeshAgent { get { return meshAgent; } }
    public Blackboard Blackboard { get { return blackboard; } }
    public RoomEnemy_2 RoomEnemy_2 { get { return roomEnemy_2; } set { roomEnemy_2 = value; } }
    void Start()
    {
        anim = GetComponent<Animator>();
        meshAgent= GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        behaviorTree = CreateBehaviourTree();
        blackboard = behaviorTree.Blackboard;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Root CreateBehaviourTree()
    {

        return new Root(new Parallel(Parallel.Policy.ONE, Parallel.Policy.ONE,
            //new Sequence(new Skeleton_Start(this), new SkeletonRun(this))
            new Selector()
            ));

    }

    public void StartCoroutine_BehaviorTree(IEnumerator routine)
    {
        StartCoroutine(routine);
    }

    public void StartBehaviorTree()
    {
        behaviorTree.Start();
    }
}
