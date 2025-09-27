using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Movement Settings")]
    public float EmoveSpeed = 0.5f;
    public float EmaxSpeed = 3f;
    public float EaccelerationTime = 1f;
    

    private Vector3 Evelocity;
    private float Eacceleration;
    private Vector3 EtargetPosition;
    private Vector3 EtargetDirection;


    private void Update()
    {
        EnemyMovement();
    }

    private void EnemyMovement()
    {
        Eacceleration = EmaxSpeed / EaccelerationTime;

        EtargetPosition = new Vector3(Random.Range(-60f, 60f), Random.Range(-60f, 60f), 0);
        EtargetDirection = (EtargetPosition - transform.position).normalized;
        Evelocity += EtargetDirection * Eacceleration * Time.deltaTime;

        Evelocity = Vector3.ClampMagnitude(Evelocity, EmaxSpeed);
        transform.position += Time.deltaTime * Evelocity;

    }
}
