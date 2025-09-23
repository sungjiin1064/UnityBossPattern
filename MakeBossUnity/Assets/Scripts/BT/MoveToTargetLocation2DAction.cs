using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MoveToTargetLocation2D", story: "[Self] move to [TargetLocation] .", category: "Action/Navigation", id: "3b06124baa9f37d0597c961c27310e8c")]
public partial class MoveToTargetLocation2DAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<Vector3> TargetLocation;
    [SerializeReference] public BlackboardVariable<float> Speed;
    [SerializeReference] public BlackboardVariable<float> StopingDistance;
    Rigidbody2D rigidbody2D;
    Animator animator;
    SpriteRenderer spriteRenderer;

    protected override Status OnStart()
    {
        if (Self.Value.TryGetComponent<SpriteRenderer>(out var _spriteRenderer))
        {
            spriteRenderer = _spriteRenderer;
        }

        if (Self.Value.TryGetComponent<Animator>(out var anim))
        {
            animator = anim;
        }

        if (Vector3.Distance(Self.Value.transform.position, TargetLocation.Value) < 0.1f)
        {
            return Status.Success;
        }

        if (Self.Value.TryGetComponent<Rigidbody2D>(out Rigidbody2D rigid))
        {
            rigidbody2D = rigid;
            return Status.Running;
        }
        else
        {
            return Status.Failure;
        }

    }

    protected override Status OnUpdate()
    {
        if (Self.Value.transform.position.x < TargetLocation.Value.x)
        {
            spriteRenderer.flipX = true; // ¿ÞÂÊ
        }
        else
        {
            spriteRenderer.flipX = false; // ¿À¸¥ÂÊ
        }


        if (Vector3.Distance(Self.Value.transform.position, TargetLocation.Value) < StopingDistance.Value)
        {
            animator.SetBool("IsRun", false);
            rigidbody2D.linearVelocity = Vector2.zero;
            return Status.Success;
        }
        else
        {
            animator.SetBool("IsRun", true);
            rigidbody2D.linearVelocity = (TargetLocation.Value - Self.Value.transform.position).normalized * Speed.Value;

            return Status.Running;
        }
    }

}

