using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    Camera cam;
    void Awake()
    {
        cam = FindObjectOfType<Camera>();
        gameObject.SetActive(false);
    }
    void Update()
    {
        transform.LookAt(cam.gameObject.transform.position);
    }
}
