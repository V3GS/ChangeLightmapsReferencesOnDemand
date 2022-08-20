using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Lightmaps data", menuName = "Tools/Lightmaps helper", order = 1)]
public class SceneLightmaps : ScriptableObject
{
    [SerializeField]
    public LightingData[] lightmaps;
    public uint IDLightmapToLoad = 0;

    public LightingData GetLightingDataToLoad()
    {
        return lightmaps[IDLightmapToLoad];
    }

    public bool LoadLightmap()
    {
        if (IDLightmapToLoad >= lightmaps.Length)
        {
            Debug.LogWarning("The ID exceeds the number of lightmaps elements. Please ingress a valid ID or increase the number of lightmaps data.");
            return false;
        }

        LightingData lightingData = GetLightingDataToLoad();

        LightmapData[] lightmaparray = LightmapSettings.lightmaps;

        LightmapData mapdata = new LightmapData();
        mapdata.lightmapDir = lightingData.m_Dir;
        mapdata.lightmapColor = lightingData.m_Light;

        if (lightingData.m_ShadowMask)
        {
            mapdata.shadowMask = lightingData.m_ShadowMask;
        }

        lightmaparray[0] = mapdata;
        LightmapSettings.lightmaps = lightmaparray;

        return true;
    }
}
