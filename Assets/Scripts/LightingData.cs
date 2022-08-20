using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LightingData
{
    [SerializeField] public Texture2D m_Dir;
    [SerializeField] public Texture2D m_Light;
    [SerializeField] public Texture2D m_ShadowMask;
}
