using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject meleeEnemyPrefab;
    public GameObject rangedEnemyPrefab;
    public Transform[] spawnPoints;
    public int meleeEnemiesWave1 = 5;
    public int rangedEnemiesWave1 = 6;
    public int meleeEnemiesWave2 = 6;
    public int rangedEnemiesWave2 = 5;

    private int wave = 1;

    public Door[] doors;

    void Start()
    {
        SpawnEnemies();
        SetDoorState(true);
    }

    private void Update()
    {
        EnemyKilled();
        CheckDoorState();
    }

    void SpawnEnemies()
    {
        if (wave == 1)
        {
            for (int i = 0; i < meleeEnemiesWave1; i++)
            {
                Instantiate(meleeEnemyPrefab, spawnPoints[i % spawnPoints.Length].position, Quaternion.identity);
            }

            for (int i = 0; i < rangedEnemiesWave1; i++)
            {
                Instantiate(rangedEnemyPrefab, spawnPoints[i % spawnPoints.Length].position, Quaternion.identity);
            }
        }
        else if (wave == 2)
        {
            for (int i = 0; i < meleeEnemiesWave2; i++)
            {
                Instantiate(meleeEnemyPrefab, spawnPoints[i % spawnPoints.Length].position, Quaternion.identity);
            }

            for (int i = 0; i < rangedEnemiesWave2; i++)
            {
                Instantiate(rangedEnemyPrefab, spawnPoints[i % spawnPoints.Length].position, Quaternion.identity);
            }
        }
    }

    public void EnemyKilled()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy_Melee").Length == 0 && GameObject.FindGameObjectsWithTag("Enemy_Ranged").Length == 0)
        {
            wave++;
            SpawnEnemies();
        }
    }

    void CheckDoorState()
    {
        bool allDoorsClosed = true;
        bool anyDoorOpen = false;
        foreach (Door door in doors)
        {
            if (door.isOpen)
            {
                anyDoorOpen = true;
            }
            else
            {
                allDoorsClosed = false;
            }
        }

        if (!anyDoorOpen && allDoorsClosed && GameObject.FindGameObjectsWithTag("Enemy_Melee").Length == 0 && GameObject.FindGameObjectsWithTag("Enemy_Ranged").Length == 0)
        {
            SetDoorState(true);
        }
    }

    public void SetDoorState(bool open)
    {
        foreach (Door door in doors)
        {
            door.SetDoorState(open);
        }
    }
}

[System.Serializable]

public class Door : MonoBehaviour
{
    public GameObject openDoorPrefab;
    public GameObject closedDoorPrefab;
    public bool isOpen = true;

    private GameObject doorObject;
    private BoxCollider2D doorCollider;

    public void SetDoorState(bool open)
    {
        isOpen = open;
        if (open)
        {
            if (doorObject != null)
            {
                doorObject.SetActive(false);
            }
            doorObject = Instantiate(openDoorPrefab, transform.position, transform.rotation);
            doorCollider = doorObject.GetComponent<BoxCollider2D>();
        }
        else
        {
            if (doorObject != null)
            {
                doorObject.SetActive(false);
            }
            doorObject = Instantiate(closedDoorPrefab, transform.position, transform.rotation);
            doorCollider = doorObject.GetComponent<BoxCollider2D>();
        }
    }
}
