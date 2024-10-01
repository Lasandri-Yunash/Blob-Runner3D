using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using TMPro;
    
public enum BonusType { Addition, Difference, Product, Division }

public class Doors : MonoBehaviour
{

    [Header("Element")]
    [SerializeField] private SpriteRenderer rightDoorRenderer;
    [SerializeField] private SpriteRenderer leftDoorRenderer;
    [SerializeField] private TextMeshPro leftDoorText;
    [SerializeField] private TextMeshPro rightDoorText;
    [SerializeField] private Collider collider;


    [Header("Setting")]
    [SerializeField] private BonusType rightDoorBonusType;
    [SerializeField] private int RightDoorBonusAmount;

    [SerializeField] private BonusType leftDoorBonusType;
    [SerializeField] private int LeftDoorBonusAmount;

    [SerializeField] private Color bonusColor;
    [SerializeField] private Color penaltyColor;


    private void Start()
    {
        configureDoor();
    }

    private void configureDoor()
    {
         //CONFIGURE THE RIGHT DOOR


        switch (rightDoorBonusType)
        {
            case BonusType.Addition:

                rightDoorRenderer.color = bonusColor;
                rightDoorText.text = "+" + RightDoorBonusAmount;

                break;
            case BonusType.Difference:

                rightDoorRenderer.color = penaltyColor;
                rightDoorText.text = "-" + RightDoorBonusAmount;

                break;
            case BonusType.Product:

                rightDoorRenderer.color = bonusColor;
                rightDoorText.text = "x" + RightDoorBonusAmount;

                break;
            case BonusType.Division:

                rightDoorRenderer.color = penaltyColor;
                rightDoorText.text = "/" + RightDoorBonusAmount;

                break;
        }

        switch (leftDoorBonusType)
        {
            case BonusType.Addition:

                leftDoorRenderer.color = bonusColor;
                leftDoorText.text = "+" + LeftDoorBonusAmount;

                break;
            case BonusType.Difference:

                leftDoorRenderer.color = penaltyColor;
                leftDoorText.text = "-" + LeftDoorBonusAmount;

                break;
            case BonusType.Product:

                leftDoorRenderer.color = bonusColor;
                leftDoorText.text = "x" + LeftDoorBonusAmount;

                break;
            case BonusType.Division:

                leftDoorRenderer.color = penaltyColor;
                leftDoorText.text = "/" + LeftDoorBonusAmount;

                break;
        }



    }

    public int GetBonusAmount (float xPosition)
    {
        if (xPosition > 0)
        {
            return RightDoorBonusAmount;

        }
        else
            return LeftDoorBonusAmount;
    }

    public BonusType GetBonusType(float xPosition)
    {
        if (xPosition > 0)
        {
            return rightDoorBonusType;

        }
        else
            return leftDoorBonusType;
    }

    public void Disable()
    {
        collider.enabled = false;
    }
}


