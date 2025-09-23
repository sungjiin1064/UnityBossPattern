using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetPlayerToTarget", story: "Find player to [TargetLocation] .", category: "Action/Find", id: "48159ea3442751c2c11cb025ffc124e7")]
public partial class SetPlayerToTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<Vector3> TargetLocation;

    protected override Status OnStart()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.Log("태그가 player인 오브젝트가 없습니다.");
            return Status.Failure;
        }
        else
        {
            TargetLocation.Value = player.transform.position;
            return Status.Success;
        }
    }


}

