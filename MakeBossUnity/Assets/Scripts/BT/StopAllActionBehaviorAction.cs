using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using System.Collections.Generic;
using System.Linq;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "StopAllActionBehavior", story: "[Self] stop all [ActionBehavior] .", category: "Action/Pattern", id: "8e369584078e66868471dba13f521dcb")]
public partial class StopAllActionBehaviorAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> ActionBehavior;

    List<ActionBehavior> stopActions = new();

    protected override Status OnStart()
    {
        if (stopActions.Count <= 0)
        {
            stopActions = ActionBehavior.Value.GetComponents<ActionBehavior>().ToList();
        }


        foreach(var action in stopActions)
        {
            action.OnStop();
        }

        Self.Value.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
      
        return Status.Success;
    }

    

}

