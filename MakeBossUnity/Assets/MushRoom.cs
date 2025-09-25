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



    private void Awake()
    {
        behaviorAgent = GetComponent<BehaviorGraphAgent>();
    }

    private void Start()
    {
        behaviorAgent.SetVariableValue<EnemyState>("EnemyState", stateState);
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;

        if(IsStun())
        {
            StunRaise();
        }

        if (CurrentHealth <= 0)
        {
            Debug.Log("ав╬З╢ы.");
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
