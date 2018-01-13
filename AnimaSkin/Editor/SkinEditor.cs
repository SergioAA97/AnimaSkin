using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using Anima2D;
using System;

[CustomEditor(typeof(Skin))]
public class SkinEditor : Editor {

    private ReorderableList list;

    private void OnEnable()
    {
        //Create the list
        list = new ReorderableList(serializedObject,
            serializedObject.FindProperty("skinParts"),
            true, true, true, true);

        list.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, "Skin Parts");
        };


        //GUI Changes
        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
            var element = list.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;
            EditorGUI.PropertyField(
                new Rect(rect.x, rect.y, rect.width / 2, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("bodyPart"),
                GUIContent.none);
            EditorGUI.PropertyField(
                new Rect(rect.x + (rect.width / 2), rect.y, rect.width / 2, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("spriteMesh"),
                GUIContent.none);
        };


        list.onAddDropdownCallback = (Rect buttonRect, ReorderableList l) => {
            var menu = new GenericMenu();

            menu.AddItem(new GUIContent("New Empty Field"), false, onAddHandler, new SkinPart());
            menu.AddItem(new GUIContent("From Selection"), false, onAddSelectionHandler, new SkinPart());

            menu.ShowAsContext();
        };

    }

    private void onAddSelectionHandler(object target)
    {

        var selection = Selection.activeGameObject;

        if (selection == null) return;

        SpriteMeshInstance[] instances = selection.GetComponentsInChildren<SpriteMeshInstance>();
        
        //Warning
        if (EditorUtility.DisplayDialog("Warning!",
        instances.Length + " References will be added, do you want to continue?", "Yes", "No"))
        {
            foreach(SpriteMeshInstance instance in instances){
                //Add a new field
                var index = list.serializedProperty.arraySize;
                list.serializedProperty.arraySize++;
                list.index = index;
                //Modify the field
                var element = list.serializedProperty.GetArrayElementAtIndex(index);
                //Change reference
                element.FindPropertyRelative("bodyPart").objectReferenceValue = instance;
            }

            serializedObject.ApplyModifiedProperties();
        }
    }

    private void onAddHandler(object target)
    {
        //Add a normal empty field
        var index = list.serializedProperty.arraySize;
        list.serializedProperty.arraySize++;
        //Apply changes
        serializedObject.ApplyModifiedProperties();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        //Update changes
        serializedObject.Update();
        list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}
