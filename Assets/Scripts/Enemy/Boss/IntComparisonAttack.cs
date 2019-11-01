using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IntComparisonAttack : Conditional
{

    public SharedBool inPhaseTwo;
   


    public override TaskStatus OnUpdate()
    {

        if (inPhaseTwo.Value == true)
         return TaskStatus.Success;         
        else
        return TaskStatus.Failure;
    }
}
