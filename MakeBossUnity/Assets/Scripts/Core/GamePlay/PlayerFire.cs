using System;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerFire : MonoBehaviour
{
    Player player;

    [Header("Fire ¼Ó¼º")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float fireRate = 2;
    [SerializeField] Transform firePos;
    bool shouldFire;
    float previousFireTime;
    int playerForwardDir = 1;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        player.OnFire += HandleFire;
    }

    private void OnDisable()
    {
        player.OnFire -= HandleFire;        
    }

    private void HandleFire(bool enable)
    {
        shouldFire = enable;
    }

    private void Update()
    {
        if(shouldFire == false) return;
        if(Time.time < previousFireTime + (1 / fireRate)) return;

        GameObject PJinstance = Instantiate(projectilePrefab, firePos.position, Quaternion.identity);

        float tempValue = player.controls.Player.Move.ReadValue<float>();

        if(tempValue < 0)
        {
            playerForwardDir = -1;
        }
        else if (tempValue > 0)
        {
            playerForwardDir = 1;
        }

        PJinstance.GetComponent<Rigidbody2D>().linearVelocity = Vector3.right * playerForwardDir * 10f;

        previousFireTime = Time.time;
    }
}
