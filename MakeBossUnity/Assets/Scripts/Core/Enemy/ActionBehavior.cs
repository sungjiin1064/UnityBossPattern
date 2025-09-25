using UnityEngine;

public interface IStoppableActionBehavior
{
    void OnStop();
}

public abstract class ActionBehavior : MonoBehaviour
{
    public  bool IsPatternEnd;

    public abstract void OnStart();
    public abstract void OnUpdata();
    public abstract void OnEnd();
    public virtual void OnStop()
    {
        IsPatternEnd = false;
    }
   

}
