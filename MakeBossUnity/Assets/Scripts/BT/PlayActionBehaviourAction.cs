using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "PlayActionBehaviour", story: "play [ActionBehaviour] .", category: "Action/Pattern", id: "095d8a1bcc6080d04f42d0bd5f3b1a9a")]
public partial class PlayActionBehaviourAction : Action
{
    [SerializeReference] public BlackboardVariable<ActionBehaviour> ActionBehaviour;

    protected override Status OnStart()
    {
        ActionBehaviour.Value.OnStart();

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if(ActionBehaviour.Value.IsPatternEnd)
        {
            return Status.Success;
        }
        else
        {
            ActionBehaviour.Value.OnUpdata();
            return Status.Running;
        }

            
    }

    protected override void OnEnd()
    {
        ActionBehaviour.Value.OnEnd();
    }
}

