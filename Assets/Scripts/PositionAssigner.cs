using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionAssigner : MonoBehaviour
{
    [SerializeField] GameObject objectToAssignPosition;

    void Update()
    {
        transform.position = objectToAssignPosition.transform.position;
    }
}
