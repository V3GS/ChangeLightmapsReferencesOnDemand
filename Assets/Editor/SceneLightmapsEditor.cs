using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SceneLightmaps))]

public class SceneLightmapsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        SceneLightmaps sceneLightmapsObj = (SceneLightmaps)target;

        if (GUILayout.Button("Load Lightmap"))
        {
            if (sceneLightmapsObj.LoadLightmap())
            {
                ModifyLightingDataAsset(sceneLightmapsObj.GetLightingDataToLoad());
            }
        }
    }

    private void ModifyLightingDataAsset(LightingData lightingData)
    {
        SerializedObject lightingDataSerializedObject = new SerializedObject(Lightmapping.lightingDataAsset);

        PropertyInfo inspectorMode = typeof(SerializedObject).GetProperty("inspectorMode", BindingFlags.NonPublic | BindingFlags.Instance);
        inspectorMode.SetValue(lightingDataSerializedObject, (int)InspectorMode.DebugInternal);

        SerializedProperty serializedLightmaps = lightingDataSerializedObject.FindProperty("m_Lightmaps");
        SerializedProperty serializedLightmap = serializedLightmaps.GetArrayElementAtIndex(0);

        serializedLightmap.FindPropertyRelative("m_Lightmap").objectReferenceValue = AssetDatabase.LoadAssetAtPath<Texture2D>( AssetDatabase.GetAssetPath(lightingData.m_Light) );
        serializedLightmap.FindPropertyRelative("m_DirLightmap").objectReferenceValue = AssetDatabase.LoadAssetAtPath<Texture2D>(AssetDatabase.GetAssetPath(lightingData.m_Dir));
        if (lightingData.m_ShadowMask)
        {
            serializedLightmap.FindPropertyRelative("m_ShadowMask").objectReferenceValue = AssetDatabase.LoadAssetAtPath<Texture2D>(AssetDatabase.GetAssetPath(lightingData.m_ShadowMask));
        }

        lightingDataSerializedObject.ApplyModifiedProperties();
    }
}
