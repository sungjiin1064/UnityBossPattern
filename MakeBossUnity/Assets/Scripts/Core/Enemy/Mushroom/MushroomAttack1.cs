using System;
using System.Collections;
using UnityEngine;

public class MushroomAttack1 : ActionBehavior
{
    Transform target;
    Animator animator;
    SpriteRenderer spriteRenderer;

    [SerializeField] float waitTimeForCharging = 1f; // ��¡�ð�
    [SerializeField] GameObject projectilePrefab;    // ����ü
    [SerializeField] float projectileRange = 180f;   // ����ü �߻簢��
    [SerializeField] int loopCount = 2;              // ������ �ݺ� Ƚ��
    [SerializeField] float RightAngle = -60f;
    [SerializeField] float LeftAngle = 120f;

    //[SerializeField] AudioClip fireSFX;
    AudioSource audiosource;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audiosource = GetComponent<AudioSource>();
    }
    public override void OnEnd()
    {
        IsPatternEnd = false;
    }

    public override void OnStart()
    {
        Debug.Log("���� ����1 ����");
        IsPatternEnd = false;
        StartCoroutine(ChargingPattern());
    }

    public override void OnUpdata()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (transform.position.x < player.transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    public override void OnStop()
    {
        StopCoroutine(ChargingPattern());
        base.OnStop();
    }

    IEnumerator ChargingPattern()
    {
        animator.SetTrigger("A1");
        yield return new WaitForSeconds(waitTimeForCharging);

        for (int i = 0; i < loopCount; i++)
        {
            Fire();
            audiosource.clip = Resources.Load<AudioClip>("Sound/Ice");
            audiosource.Play();
            yield return new WaitForSeconds(1f);
        }

        animator.SetTrigger("Sturn");
        yield return new WaitForSeconds(2f);

        IsPatternEnd = true;
    }

    private void Fire()
    {
        float currentAngle = SelectAngleByPlayerPosition();

        float deltaAngle = projectileRange / 10;

        for (int i = 0; i < 10; i++)
        {
            GameObject projectileInstance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            float dirX = Mathf.Cos(currentAngle * Mathf.Deg2Rad);
            float dirY = Mathf.Sin(currentAngle * Mathf.Deg2Rad);

            Vector2 Spawn = new Vector2(transform.position.x + dirX, transform.position.y + dirY);
            Vector2 dir = (Spawn - (Vector2)transform.position).normalized;

            if (projectileInstance.TryGetComponent<Rigidbody2D>(out var rb))
            {
                rb.linearVelocity = dir * 5f;
            }

            currentAngle += deltaAngle;
        }
    }

    private float SelectAngleByPlayerPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (transform.position.x < player.transform.position.x)
        {
            return RightAngle;
        }
        else
        {
            return LeftAngle;
        }

    }
}
