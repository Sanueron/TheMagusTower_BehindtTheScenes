using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Enemy))]
public class ControlEditorEnemies : Editor
{
    private Enemy myTarget;
    GUILayoutOption[] options;

    private string[] types = { "Undead", "Monster", "Esoteric", "Golem" };
    private int typeSelected = -1;

    SerializedProperty enemyClass;

    public override void OnInspectorGUI()
    {
        GUILayout.Label("Type of Enemy");
        EditorGUILayout.BeginVertical();
        typeSelected = GUILayout.Toolbar(typeSelected, types);
        EditorGUILayout.EndVertical();
        base.OnInspectorGUI();
        EnemyClass myTarget = (Enemy)target;

        GUILayout.Label("Class");
        if (typeSelected >= 0)
        {
            switch (types[typeSelected])
            {
                case "Undead":
                    myTarget.type = EnemyType.Type.Undead;
                    //EditorGUILayout.EnumFlagsField(myTarget._UndeadClass, options);
                    //EditorGUILayout.EnumPopup(myTarget._UndeadClass, options);
                    enemyClass = serializedObject.FindProperty("_UndeadClass");
                    serializedObject.Update();
                    EditorGUILayout.PropertyField(enemyClass);
                    serializedObject.ApplyModifiedProperties();
                    break;

                case "Monster":
                    myTarget.type = EnemyType.Type.Monster;
                    enemyClass = serializedObject.FindProperty("_class");
                    serializedObject.Update();
                    EditorGUILayout.PropertyField(enemyClass);
                    serializedObject.ApplyModifiedProperties();
                    break;

                case "Esoteric":
                    myTarget.type = EnemyType.Type.EsotericCreature;
                    enemyClass = serializedObject.FindProperty("_EsotericCreature");
                    serializedObject.Update();
                    EditorGUILayout.PropertyField(enemyClass);
                    serializedObject.ApplyModifiedProperties();
                    break;

                case "Golem":
                    myTarget.type = EnemyType.Type.Golem;
                    enemyClass = serializedObject.FindProperty("_Golem");
                    serializedObject.Update();
                    EditorGUILayout.PropertyField(enemyClass);
                    serializedObject.ApplyModifiedProperties();
                    break;
            }
        }
    }


}
