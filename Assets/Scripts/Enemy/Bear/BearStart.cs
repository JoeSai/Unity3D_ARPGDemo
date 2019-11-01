using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class BearStart : Action
{

    public SharedBool isWake = false;
    public SharedMaterial material2;
    private float WaitTime = 3.5f;
    private bool start = false;
    
    private List<Material> materials = new List<Material>();
    private Material material;
    

    public override void OnAwake()
    {
        base.OnAwake();

        foreach (Transform child in this.transform.GetComponentsInChildren<Transform>(true))
        {
            if (child.tag == "Dissolve")
            {
                materials.Add(child.GetComponent<Renderer>().material);
            }
        }

        material= Resources.Load<Material>("Material/BearDissolve_1") as Material;
        material2.Value = GameObject.Instantiate(material) as Material;
    }
    
    public override TaskStatus OnUpdate()
    {

        if (isWake.Value == true)
            return TaskStatus.Success;

        foreach (var item in materials)
        {
            item.DOFloat(-2.0f ,"_AlphaValue", WaitTime).OnComplete(() => { start = true; });
        }
        if (start == true)
        {
            isWake.Value = true;
            foreach (Transform child in this.transform.GetComponentsInChildren<Transform>(true))
            {
                if (child.tag == "Dissolve")
                {
                    //materials.Add(child.GetComponent<Renderer>().material);
                    child.GetComponent<Renderer>().material = material2.Value;
                }
            }
        }
           

        return TaskStatus.Failure;
       
    }
}
