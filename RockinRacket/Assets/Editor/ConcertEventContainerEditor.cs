using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(GameEventContainer))]
public class ConcertEventContainerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GameEventContainer container = (GameEventContainer)target;
        EditorGUILayout.HelpBox("Please ensure that all GameObjects in the list contain a ConcertEvent component.", MessageType.Info);

        for (int i = 0; i < container.EventPrefabs.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            GameObject obj = (GameObject)EditorGUILayout.ObjectField(container.EventPrefabs[i], typeof(GameObject), false);

            if (obj != container.EventPrefabs[i])
            {
                if (obj == null)
                {
                    container.EventPrefabs.RemoveAt(i);
                    i--;
                }
                else if (obj.GetComponent<GameEvent>() == null)
                {
                    EditorGUILayout.HelpBox("GameObject does not contain ConcertEvent script", MessageType.Info);
                }
                else
                {
                    container.EventPrefabs[i] = obj;
                }
            }

            if (GUILayout.Button("Remove"))
            {
                container.EventPrefabs.RemoveAt(i);
                i--;
            }
            
            EditorGUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Add"))
        {
            container.EventPrefabs.Add(null);
        }
    }
}
#endif
*/