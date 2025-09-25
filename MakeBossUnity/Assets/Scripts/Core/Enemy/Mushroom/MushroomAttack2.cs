using UnityEngine;

public class MushroomAttack2 : ActionBehavior
{
    public override void OnEnd()
    {
  
    }

    public override void OnStart()
    {
        Debug.Log("버섯 공격2 실행");
        IsPatternEnd = true;
    }

    public override void OnUpdata()
    {

    }
    public override void OnStop()
    {
       base.OnStop();
    }
}
