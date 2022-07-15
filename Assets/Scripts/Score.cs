using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    TextMeshProUGUI _textMeshPro;
    float amountOfSheeps, amountOfSheepsLeft;
    void Awake()
    {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
        amountOfSheeps = FindObjectsOfType<Sheep>().Length;
        amountOfSheepsLeft = amountOfSheeps;
        _textMeshPro.text = amountOfSheepsLeft + "/" + amountOfSheeps;

        
    }

    public void Decrease()
    {
        amountOfSheepsLeft--;
        _textMeshPro.text = amountOfSheepsLeft + "/" + amountOfSheeps;
    }
   
}
