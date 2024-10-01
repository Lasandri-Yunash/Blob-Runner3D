using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private Transform enemiesParent;

    [Header("Setting")]
    [SerializeField] private int amount;
    [SerializeField] private float radius;
    [SerializeField] private float angle;


    void Start()
    {
        GenerateEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateEnemy()
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 enemyLocalPosition = GetRunnerLocalPosition(i);

            Vector3 enemyWorlPosition = enemiesParent.TransformPoint(enemyLocalPosition);

            Instantiate(enemyPrefab, enemyWorlPosition, Quaternion.identity, enemiesParent);
        }

    }

    private Vector3 GetRunnerLocalPosition(int index)                                            // r = c√n
    {                                                                                            // θ = n * 137.508°

        float x = radius * Mathf.Sqrt(index) * Mathf.Cos(Mathf.Deg2Rad * index * angle);

        float z = radius * Mathf.Sqrt(index) * Mathf.Sin(Mathf.Deg2Rad * index * angle);

        return new Vector3(x, 0, z);

    }
}
