using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using Anima2D;

[CustomEditor(typeof(SkinManager))]

public class SkinManagerEditor : Editor {

    private ReorderableList list;
    private ReorderableList currSkinList;

    private void OnEnable()
    {
        //Create the list
        list = new ReorderableList(serializedObject,
            serializedObject.FindProperty("skins"),
            true, true, false, false);

        
        list.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, "Skins");
        };


        //GUI Changes
        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
            var element = list.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;
            EditorGUI.PropertyField(
                new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
                element,
                GUIContent.none);
        };

    }


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        //Change Current Skin

        SkinManager myTarget = (SkinManager)target;

        if (myTarget.currentSkin != null)
        {
            EditorGUILayout.LabelField("Current Skin Name", myTarget.CurrentSkinName);
        }
        

        //Update changes
        serializedObject.Update();
        list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
    
}
