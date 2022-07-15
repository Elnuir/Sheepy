using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dier : MonoBehaviour
{
    [SerializeField] UnityEvent Death;
    Score score;
    private void Awake()
    {
        score = FindObjectOfType<Score>();
    }
    public void Die()
    {
        Death?.Invoke();
        score.Decrease();
    }

}
