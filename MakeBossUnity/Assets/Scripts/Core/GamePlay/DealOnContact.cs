using UnityEngine;

public class DealOnContact : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] private int applyDamage = 5;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<IDamagable>(out var damagable))
        {
            SetApplyDamage();
            damagable.TakeDamage(applyDamage);

            Destroy(gameObject);
        }
    }

    private void SetApplyDamage()
    {

    }

}
