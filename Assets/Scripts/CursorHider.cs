using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorHider : MonoBehaviour
{
    void Awake()
    {
        Cursor.visible = false;
    }
}
