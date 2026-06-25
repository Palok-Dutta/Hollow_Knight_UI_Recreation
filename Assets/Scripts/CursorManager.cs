using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D m_CursorTexture;
    [SerializeField] private Vector2 m_Hotspot = Vector2.zero;

    private void Start()
    {
        Cursor.SetCursor(m_CursorTexture, m_Hotspot, CursorMode.Auto);
    }
}
