using UnityEditor;

namespace GameMain
{
    [CustomEditor(typeof(FocusElementEditor), true)]
    public class FocusElementEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }
    }
}