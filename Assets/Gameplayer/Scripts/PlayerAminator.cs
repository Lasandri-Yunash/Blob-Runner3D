using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAminator : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Transform runnerParent;  
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Run()
    {
        for (int i = 0; i < runnerParent.childCount; i++)
        {
            Transform runner = runnerParent.GetChild(i);
            Animator runnrAnimator = runner.GetComponent<Animator>();

            runnrAnimator.Play("Run");
        }

    }

    public void Idel()
    {
        for (int i = 0; i < runnerParent.childCount; i++)
        {
            Transform runner = runnerParent.GetChild(i);
            Animator runnrAnimator = runner.GetComponent<Animator>();

            runnrAnimator.Play("Idel");
        }
    }
}
