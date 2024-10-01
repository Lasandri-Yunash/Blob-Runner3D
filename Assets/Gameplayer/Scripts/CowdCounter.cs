using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CowdCounter : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private TextMeshPro crowdCounterText;
    [SerializeField] private Transform runnerParent;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        crowdCounterText.text = runnerParent.childCount.ToString();

        if(runnerParent.childCount <= 0)
        {
            Destroy(gameObject);
        }
    }
}
