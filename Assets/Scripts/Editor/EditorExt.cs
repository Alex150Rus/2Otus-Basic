//#if DEBUG
using UnityEditor;
//#endif
using UnityEngine;

namespace Assets.Scripts
{
    [CustomEditor(typeof(CameraControl))]
    public class CameraControlEdit : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }
    }

    public class EditData : EditorWindow
    {
        [MenuItem("Tools/EditData")]
        static void Show() => GetWindow(typeof(EditData));

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Редактор");
            if (GUILayout.Button("Загрузить"))
                Debug.Log("Что-то грузим");
        }
    }

    class EditorExt
    {
        static int x = 1, y = 2;

        //#if DEBUG
        [MenuItem("Tools/ToValidate", true, menuItem = "ToValidate")]
        //[IsValidateFunction]
        static bool ShowDebug()
        {
            Debug.Log($"Test: {x} {y}");
            return true;
        }

        [MenuItem("Tools/ToValidate")]
        static void ToValidate()
        {
            Debug.Log(x + y);
        }
        //#endif
    }
}
