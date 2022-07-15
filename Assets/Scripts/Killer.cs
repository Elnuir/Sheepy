using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killer : MonoBehaviour
{
    [SerializeField] GameObject catchText;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Sheep") && other.gameObject.GetComponent<Dier>())
        {
            other.GetComponent<Dier>().Die();
            catchText.SetActive(true);
        }
    }
}
