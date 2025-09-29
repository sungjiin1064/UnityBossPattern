using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.InputSystem;

public class MushRoom : MonoBehaviour, IDamagable
{
    // Idle, Run, Stun, A1, A2, Hit, Die

    BehaviorGraphAgent behaviorAgent;

    [SerializeField] EnemyState stateState;
    [SerializeField] int MaxHealth = 100;
    [field:SerializeField] public int CurrentHealth {  get; private set; }

    public Action<bool> OnPatternStart;
    public Action<string,bool> OnSomeFuncStart;
    public Action<int, int> OnHealthbarUpdate;

    [SerializeField] ParticleSystem rageVFX;

    private void Awake()
    {
        behaviorAgent = GetComponent<BehaviorGraphAgent>();
    }

    private void Start()
    {
        behaviorAgent.SetVariableValue<EnemyState>("EnemyState", stateState);        

        CurrentHealth = MaxHealth;

        OnHealthbarUpdate?.Invoke(CurrentHealth, MaxHealth);
    }

    private void OnEnable()
    {
        OnPatternStart += HandlePatternStart;
        OnSomeFuncStart += HandleSomeFuncStart;
    }

    private void OnDisable()
    {
        OnPatternStart -= HandlePatternStart;
        OnSomeFuncStart -= HandleSomeFuncStart;
        
    }

    private void HandlePatternStart(bool enable)
    {
        Debug.Log("HandlePatternStart 함수 실행!");
        behaviorAgent.SetVariableValue<Boolean>("IsPatternTrigger", enable);

        if(rageVFX.isPlaying) return;
        rageVFX.Play();
    }

    private void HandleSomeFuncStart(string methodName, bool enable)
    {
        Debug.Log("HandleSomeFuncStart 함수 실행!");
        behaviorAgent.SetVariableValue<Boolean>(methodName, enable);
    }
      

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;

        OnHealthbarUpdate?.Invoke(CurrentHealth, MaxHealth);

        if (CurrentHealth < MaxHealth * 0.5f)
        {
            OnPatternStart?.Invoke(true);
            //OnSomeFuncStart?.Invoke("IsPatternTrigger", true);
        }

        if(IsStun())
        {
            StunRaise();
        }

        if (CurrentHealth <= 0)
        {
            Debug.Log("죽었다.");
            behaviorAgent.SetVariableValue<EnemyState>("EnemyState", EnemyState.Die);
        }
    }

    private bool IsStun()
    {
        int rand = UnityEngine.Random.Range(0, 101);
        //int rand = UnityEngine.Random.RandomRange(0, 101);

        if(rand <= 50)
        {
            return true;
        }

        return false;
    }

    private void StunRaise()
    {
        behaviorAgent.SetVariableValue<EnemyState>("EnemyState", EnemyState.Stun);
    }

    private void Update()
    {
        if(Keyboard.current.tKey.IsPressed())
        {
            TakeDamage(10);
        }
    }
}
