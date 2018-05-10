/*******************
* Rudolf Chrispens *
*******************/

#region using
using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditorInternal;
#endregion

namespace Dev6
{
    [CustomEditor(typeof(CAlertTooltips))]
    public class AlertToopltipsEditor : Editor
    {
        public bool bDrawDefaultInspector = false;
        private ReorderableList ButtonList;
        private const float fieldWidth = 70f;

        private void OnEnable()
        {
            ButtonList = new ReorderableList(serializedObject, serializedObject.FindProperty("ToolTips"), true, true, true, true);
            InitButtonList();
        }

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Toogle Inspector"))
            {
                bDrawDefaultInspector = !bDrawDefaultInspector;
            }

            if (bDrawDefaultInspector)
            {
                //draw default IDE inspector
                DrawDefaultInspector();
            }
            else
            {
                //draw custom inspector
                DrawCustomInspector();
            }
        }

        public void DrawCustomInspector()
        {
            serializedObject.Update();

            SerializedProperty boleanDeleteThis = serializedObject.FindProperty("deletethis");
            EditorGUILayout.Space();

            //draw ID
            //EditorGUILayout.BeginHorizontal();
            //EditorGUILayout.LabelField("Delete this after");
            EditorGUILayout.PropertyField(boleanDeleteThis, new GUIContent("Delete this after Init"));
            //EditorGUILayout.EndHorizontal();

            //draw the reordable lists:
            ButtonList.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }

        void InitButtonList()
        {
            ButtonList.drawHeaderCallback = (Rect rect) => {
                EditorGUI.LabelField(rect, "ToolTips");
            };

            ButtonList.elementHeight = EditorGUIUtility.singleLineHeight * 6f;

            ButtonList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var element = ButtonList.serializedProperty.GetArrayElementAtIndex(index);
                rect.y += 2f;

                int tQPos = 0;
                
                //draw the CreatOn and the Database field
                DrawLabelPropLeft(element, rect, tQPos, "CreateOn", "Create On");
                DrawLabelPropRight(element, rect, tQPos, "DataBase", "Database");
                tQPos++;

                //draw the ToolTip
                DrawToolTip(element,rect,tQPos, index);
            };

            ////higlight the prefab in hierachy
            //ButtonList.onSelectCallback = (ReorderableList _list) => {
            //    var prefab = _list.serializedProperty.GetArrayElementAtIndex(_list.index).FindPropertyRelative("Prefab").objectReferenceValue as GameObject;
            //    if (prefab)
            //        EditorGUIUtility.PingObject(prefab.gameObject);
            //};

            //ButtonList.onRemoveCallback = (ReorderableList _list) => {
            //    if (EditorUtility.DisplayDialog("Warning!",
            //        "Are you sure you want to delete the selected object?", "Yes", "No"))
            //    {
            //        ReorderableList.defaultBehaviours.DoRemoveButton(_list);
            //    }
            //};
        }

        void DrawToolTip(SerializedProperty _Element, Rect _Rect, int _QueuePosition, int _ArrayIndex)
        {
            int tQP = _QueuePosition;
            SerializedProperty TTProperty = _Element.FindPropertyRelative("ToolTip");

            //ID
            DrawLabelPropLeft(TTProperty, _Rect, tQP , "UniqueID", "ID");
            //Key
            DrawLabelPropRight(TTProperty, _Rect, tQP, "DictKey", "Key");
            tQP++;

            //Font Size
            DrawLabelPropLeft(TTProperty, _Rect, tQP, "FontSize", "Font Size");
            //Font
            DrawLabelPropRight(TTProperty, _Rect, tQP, "ttFont", "Font");
            tQP++;

            //Tooltip width
            DrawLabelPropLeft(TTProperty, _Rect, tQP, "Width", "Width");
            //Sprite
            DrawLabelPropRight(TTProperty, _Rect, tQP, "bgSprite", "Background");
            tQP++;

            //TextColor
            DrawLabelPropLeft(TTProperty, _Rect, tQP, "TextColor", "Textcolor");
            //Padding
            DrawPadding(TTProperty, _Rect, tQP, _ArrayIndex);

        }

        void DrawPadding(SerializedProperty _Element, Rect _Rect, int _QueuePosition, int _ArrayIndex)
        {
            EditorGUI.LabelField(
                    new Rect(_Rect.x + _Rect.width * 0.5f, _Rect.y + EditorGUIUtility.singleLineHeight * _QueuePosition, fieldWidth, EditorGUIUtility.singleLineHeight),
                    "Padding"
                    );

            CAlertTooltips Target = (CAlertTooltips)target;

            float twidth = (_Rect.width * 0.5f - fieldWidth) / 4f;

            Target.ToolTips[_ArrayIndex].ToolTip.Padding.left = EditorGUI.IntField(
                new Rect(_Rect.x + _Rect.width * 0.5f + fieldWidth, _Rect.y + EditorGUIUtility.singleLineHeight * _QueuePosition, _Rect.width * 0.5f - fieldWidth - (twidth * 3), EditorGUIUtility.singleLineHeight),
                Target.ToolTips[_ArrayIndex].ToolTip.Padding.left
                );

            Target.ToolTips[_ArrayIndex].ToolTip.Padding.right = EditorGUI.IntField(
                new Rect(_Rect.x + _Rect.width * 0.5f + fieldWidth + twidth, _Rect.y + EditorGUIUtility.singleLineHeight * _QueuePosition, _Rect.width * 0.5f - fieldWidth - (twidth * 3), EditorGUIUtility.singleLineHeight),
                Target.ToolTips[_ArrayIndex].ToolTip.Padding.right
                );

            Target.ToolTips[_ArrayIndex].ToolTip.Padding.top = EditorGUI.IntField(
                new Rect(_Rect.x + _Rect.width * 0.5f + fieldWidth + twidth +twidth, _Rect.y + EditorGUIUtility.singleLineHeight * _QueuePosition, _Rect.width * 0.5f - fieldWidth - (twidth * 3), EditorGUIUtility.singleLineHeight),
                Target.ToolTips[_ArrayIndex].ToolTip.Padding.top
                );

            Target.ToolTips[_ArrayIndex].ToolTip.Padding.bottom = EditorGUI.IntField(
                new Rect(_Rect.x + _Rect.width * 0.5f + fieldWidth + twidth + twidth + twidth, _Rect.y + EditorGUIUtility.singleLineHeight * _QueuePosition, _Rect.width * 0.5f - fieldWidth - (twidth * 3), EditorGUIUtility.singleLineHeight),
                Target.ToolTips[_ArrayIndex].ToolTip.Padding.bottom
                );

            //EditorGUI.PropertyField(
            //        new Rect(_Rect.x, _Rect.y + EditorGUIUtility.singleLineHeight * _QueuePosition +1, _Rect.width * 0.5f - fieldWidth, EditorGUIUtility.singleLineHeight),
            //        , GUIContent.none, true
            //        );
            //EditorGUI.PropertyField(
            //        new Rect(_Rect.x + _Rect.width * 0.5f + fieldWidth + 20f, _Rect.y + EditorGUIUtility.singleLineHeight * _QueuePosition, _Rect.width * 0.5f - 60, EditorGUIUtility.singleLineHeight),
            //        _Element.FindPropertyRelative("Padding").FindPropertyRelative("top"), GUIContent.none
            //        );
            //EditorGUI.PropertyField(
            //        new Rect(_Rect.x + _Rect.width * 0.5f + fieldWidth + 40f, _Rect.y + EditorGUIUtility.singleLineHeight * _QueuePosition, _Rect.width * 0.5f - 40, EditorGUIUtility.singleLineHeight),
            //        _Element.FindPropertyRelative("Padding").FindPropertyRelative("right"), GUIContent.none
            //        );
            //EditorGUI.PropertyField(
            //        new Rect(_Rect.x + _Rect.width * 0.5f + fieldWidth + 60f, _Rect.y + EditorGUIUtility.singleLineHeight * _QueuePosition, _Rect.width * 0.5f - 20, EditorGUIUtility.singleLineHeight),
            //        _Element.FindPropertyRelative("Padding").FindPropertyRelative("bottom"), GUIContent.none
            //        );
        }

        void DrawLabelPropLeft(SerializedProperty _Element, Rect _Rect, int _QueuePosition, string _PropertyName, string _LabelName)
        {
            //draw label
            EditorGUI.LabelField(
                    new Rect(_Rect.x, _Rect.y + EditorGUIUtility.singleLineHeight * _QueuePosition, fieldWidth, EditorGUIUtility.singleLineHeight),
                    _LabelName
                    );
            //draw property
            EditorGUI.PropertyField(
                    new Rect(_Rect.x + fieldWidth, _Rect.y + EditorGUIUtility.singleLineHeight * _QueuePosition, _Rect.width * 0.5f - fieldWidth, EditorGUIUtility.singleLineHeight),
                    _Element.FindPropertyRelative(_PropertyName), GUIContent.none
                    );
        }

        void DrawLabelPropRight(SerializedProperty _Element, Rect _Rect, int _QueuePosition, string _PropertyName, string _LabelName)
        {
            //draw label
            EditorGUI.LabelField(
                    new Rect(_Rect.x + _Rect.width * 0.5f, _Rect.y + EditorGUIUtility.singleLineHeight * _QueuePosition, fieldWidth, EditorGUIUtility.singleLineHeight),
                    _LabelName
                    );
            //draw property
            EditorGUI.PropertyField(
                    new Rect(_Rect.x + _Rect.width * 0.5f + fieldWidth, _Rect.y + EditorGUIUtility.singleLineHeight * _QueuePosition, _Rect.width * 0.5f - fieldWidth, EditorGUIUtility.singleLineHeight),
                    _Element.FindPropertyRelative(_PropertyName), GUIContent.none
                    );
        }
    }
}