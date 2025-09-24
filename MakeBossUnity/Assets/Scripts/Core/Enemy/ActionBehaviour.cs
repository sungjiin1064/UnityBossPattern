using UnityEngine;

public abstract class ActionBehaviour : MonoBehaviour
{
    public  bool IsPatternEnd;

    public abstract void OnStart();
    public abstract void OnUpdata();
    public abstract void OnEnd();
}
