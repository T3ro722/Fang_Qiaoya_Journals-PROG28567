using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public List<Transform> asteroidTransforms;
    public float bombTrailSpacing = 0.5f;
    public int numberOfTrailBombs = 5;
    public GameObject powerupPrefab;
    public float moveSpeed = 3f;//bullet speed
    public GameObject astronautPrefab;
    public Transform astronautTransform;

    [Header("Movement Settings")]
    public float movespeed = 1f;
    public float maxSpeed = 5f;
    public float accelerationTime = 1f;
    public float decelerationTime = 0.5f;

    private Vector3 velocity;
    private float acceleration;
    private float deceleration;

    private void Start()
    {
        acceleration = maxSpeed / accelerationTime;
        deceleration = maxSpeed / decelerationTime;
    }

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

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            spawnDirBullet(new Vector3(0, 1, 0));
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            spawn4way();
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
        acceleration = maxSpeed / accelerationTime;
        deceleration = maxSpeed / decelerationTime;
        Vector2 PlayerInput = Vector2.zero;
        //Velocity = Vector3.zero;
        acceleration = maxSpeed / accelerationTime;
        deceleration = maxSpeed / decelerationTime;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            PlayerInput += Vector2.up;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            PlayerInput += Vector2.down;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            PlayerInput += Vector2.left;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            PlayerInput += Vector2.right;
        }

        if(PlayerInput.magnitude > 0)
        {
            velocity += (Vector3)PlayerInput.normalized * acceleration * Time.deltaTime;

            if(velocity.magnitude > maxSpeed)
            {
                velocity = velocity.normalized * maxSpeed;
            }
            else
            {
                Vector3 changeInVelocity = velocity.normalized * deceleration * Time.deltaTime;
                if (changeInVelocity.magnitude > velocity.magnitude)
                {
                    velocity = Vector3.zero;
                }
                else
                {
                    velocity -= changeInVelocity;
                }
            }
            transform.position += velocity * Time.deltaTime;
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

    public void spawnDirBullet(Vector3 inOffset)
    {
        //spawn a bullet right on top of player and shoot it in enemy's direction
        Instantiate(bombPrefab, transform.position + inOffset, Quaternion.identity);
        //shoot it in enemy's direction
        float angle = Mathf.Atan2(enemyTransform.position.y - transform.position.y, enemyTransform.position.x - transform.position.x) * Mathf.Rad2Deg;
        Vector3 direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;


    }

    public void spawn4way()
    {
        Instantiate(bombPrefab, transform.position + new Vector3(0, 1), Quaternion.identity);
        Instantiate(bombPrefab, transform.position + new Vector3(0, -1), Quaternion.identity);
        Instantiate(bombPrefab, transform.position + new Vector3(1, 0), Quaternion.identity);
        Instantiate(bombPrefab, transform.position + new Vector3(-1, 0), Quaternion.identity);

        //four bullets shoot out in 4 directions
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

    }

    
}
