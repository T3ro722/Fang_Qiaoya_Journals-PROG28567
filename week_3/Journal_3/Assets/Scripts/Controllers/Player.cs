using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public List<Transform> asteroidTransforms;
    public float bombTrailSpacing = 0.5f;
    public int numberOfTrailBombs = 5;
    public GameObject powerupPrefab;

    [Header("Movement Settings")]
    public float movespeed = 1f;
    public float maxSpeed = 5f;
    public float accelerationTime = 1f;
    public float decelerationTime = 0.5f;

    private Vector3 velocity;
    private float acceleration;
    private float deceleration;

    // Update is called once per frame
    void Update()
    {

        PlayerMovement();
        EnemyRadar(5.0f, 8);

        if (Input.GetKeyDown(KeyCode.B))
        {
            SpawnBombAtOffset(new Vector3(0, 1));
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            SpawnBombTrail(bombTrailSpacing, numberOfTrailBombs);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            SpawnBombOnRandomCorner(2.0f);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            WarpPlayer(enemyTransform, 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            WarpPlayer(enemyTransform, 1.0f);
        }

        if (Input.GetKeyDown(KeyCode.L))//radar
        {
            DetectAsteroids(5.0f, asteroidTransforms);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            SpawnPowerups(3.0f, 6);
        }
    }

    private void SpawnBombAtOffset(Vector3 inOffset)
    {
        Instantiate(bombPrefab, transform.position + inOffset, Quaternion.identity);
    }
    private void SpawnBombTrail(float BombSpacing, int NumberOfTrailBombs)
    {
        Vector3 Direc = transform.up.normalized;
        for (int i = 1; i <= NumberOfTrailBombs; i++)
        {
            Vector3 pos = transform.position - Direc * BombSpacing * (i + 1);
            Instantiate(bombPrefab, pos, Quaternion.identity);
        }
    }

    public void SpawnBombOnRandomCorner(float inDistance)
    {
        Vector3 Direc = transform.up.normalized;
        Vector3 Right = transform.right.normalized;
        List<Vector3> Corners = new List<Vector3>();
        {
            Corners.Add(transform.position + Direc * inDistance + Right * inDistance);
            Corners.Add(transform.position + Direc * inDistance - Right * inDistance);
            Corners.Add(transform.position - Direc * inDistance + Right * inDistance);
            Corners.Add(transform.position - Direc * inDistance - Right * inDistance);
            int RandIndex = Random.Range(0, Corners.Count);
            Instantiate(bombPrefab, Corners[RandIndex], Quaternion.identity);
        }
    }

    public void WarpPlayer(Transform target, float ratio)
    {
        Vector3.Lerp(transform.position, target.position, ratio);
        transform.position = Vector3.Lerp(transform.position, target.position, ratio);

        if (ratio == 1f)
        {
            Debug.Log("Player warped to enemy position!");
        }
        else if (ratio == 0.5f)
        {
            Debug.Log("Player warped halfway to the enemy.");
        }
        else if (ratio == 0f)
        {
            Debug.Log("Player remains at original position.");
        }
        else
        {
            Debug.Log("Player warped further towards enemy position!");
        }
    }

    public void DetectAsteroids(float inMaxRange, List<Transform> inAsteroids)
    {
        foreach (Transform asteroid in inAsteroids)
        {
            float distance = Vector3.Distance(transform.position, asteroid.position);
            if (distance <= inMaxRange)
            {
                Debug.DrawLine(transform.position, asteroid.position, Color.red, 2.5f);
            }
        }
    }

    private void PlayerMovement()
    {
        //Velocity = Vector3.zero;
        acceleration = maxSpeed / accelerationTime;
        deceleration = maxSpeed / decelerationTime;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            velocity += acceleration * Time.deltaTime * Vector3.up;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            velocity += acceleration * Time.deltaTime * Vector3.down;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            velocity += acceleration * Time.deltaTime * Vector3.left;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            velocity += acceleration * Time.deltaTime * Vector3.right;
        }

        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        transform.position += Time.deltaTime * velocity;

        //deceleration after key release
        if (Input.GetKeyUp(KeyCode.UpArrow) && (!Input.GetKey(KeyCode.UpArrow)))
        {
            float decelerateSpeed = velocity.magnitude - acceleration * Time.deltaTime;
            if (decelerateSpeed < 0) decelerateSpeed = 0;//avoid negative speed
            velocity += deceleration * Time.deltaTime * velocity.normalized;
            velocity = Vector3.ClampMagnitude(velocity, decelerateSpeed);
        }
    }

    public void EnemyRadar(float radius, int circlePoints)
    {
        float angleStep = 360f / circlePoints;

        //draw a circle with circlepoints bumber of points around the player and the specified radius
        for (int i = 0; i <= circlePoints; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector3 dire = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
            Vector3 point = transform.position + dire * radius;
            if (i > 0)
            {
                Debug.DrawLine(transform.position + new Vector3(Mathf.Cos((i - 1) * angleStep * Mathf.Deg2Rad), Mathf.Sin((i - 1) * angleStep * Mathf.Deg2Rad), 0) * radius, point, Color.green);
            }
        }
        //if the enemy is within the radius, change the green lines to red
        float distanceToEnemy = Vector3.Distance(transform.position, enemyTransform.position);
        if (distanceToEnemy <= radius)
        {
            for (int i = 0; i <= circlePoints; i++)
            {
                float angle = i * angleStep * Mathf.Deg2Rad;
                Vector3 dire = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
                Vector3 point = transform.position + dire * radius;
                if (i > 0)
                {
                    Debug.DrawLine(transform.position + new Vector3(Mathf.Cos((i - 1) * angleStep * Mathf.Deg2Rad), Mathf.Sin((i - 1) * angleStep * Mathf.Deg2Rad), 0) * radius, point, Color.red);
                }
            }
        }
    }

    public void SpawnPowerups(float radius, int numberOfPowerups)
    {
        for (int i = 0; i < numberOfPowerups; i++)
        {
            float angle = i * (360f / numberOfPowerups) * Mathf.Deg2Rad;
            Vector3 dire = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
            Vector3 pos = transform.position + dire * radius;
            Instantiate(powerupPrefab, pos, Quaternion.identity);
        }
    }
}
