using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CrowdSystem : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField] private float radius;
    [SerializeField] private float angle;

    [Header("Elements")]
    [SerializeField] PlayerAminator playerAminator;
    [SerializeField] private Transform runnerParent;
    [SerializeField] private GameObject runnerPrefab;
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!GameManager.Instance.IsGameState())
        
            return;
        

        PlaceRunners();

        if(runnerParent.childCount <= 0)  // dont have more runner
        
            GameManager.Instance.SetGameState(GameManager.GameState.GameOver);
        
    }

    private void PlaceRunners()
    {
         for (int i = 0; i < runnerParent.childCount; i++)
         {
            //float x = i * 1;

            Vector3 childLocalPosition = GetRunnerLocalPosition(i);
            runnerParent.GetChild(i).localPosition = childLocalPosition;
         }

    }

    private Vector3 GetRunnerLocalPosition(int index)                                            // r = c√n
    {                                                                                            // θ = n * 137.508°

        float x = radius *Mathf.Sqrt(index) * Mathf.Cos(Mathf.Deg2Rad *index * angle);

        float z = radius * Mathf.Sqrt(index) * Mathf.Sin(Mathf.Deg2Rad * index * angle);

        return new Vector3 (x,0,z);

    }

    public float GetCrowdRadius()
    {
        return radius * Mathf.Sqrt(runnerParent.childCount);
    }

    public void ApplyBonus(BonusType bonusType, int bonusAmount)
    {
        switch (bonusType)
        {
            case BonusType.Addition:

                AddRunners(bonusAmount);

                break;

            case BonusType.Product:

                int runnerToAdd = (runnerParent.childCount*bonusAmount) - runnerParent.childCount;
                AddRunners(runnerToAdd);

                break;

            case BonusType.Difference:
                RemoveRunner(bonusAmount);
                break;

            case BonusType.Division:
                int runnerToRemove = runnerParent.childCount - (runnerParent.childCount / bonusAmount);
                RemoveRunner(runnerToRemove);
                break;
        }
    }

    private void AddRunners(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(runnerPrefab, runnerParent);
        }
        playerAminator.Run();
    }

    private void RemoveRunner (int amount)
    {
        if (amount>runnerParent.childCount)

                amount = runnerParent.childCount;

        int runnersAmount = runnerParent.childCount;

        for (int i = runnersAmount - 1; i >= runnersAmount - amount; i--)
        {
            Transform runnerToDestroy = runnerParent.GetChild(i);
            runnerToDestroy.SetParent(null);
            Destroy(runnerToDestroy.gameObject);
        }
    }
}
