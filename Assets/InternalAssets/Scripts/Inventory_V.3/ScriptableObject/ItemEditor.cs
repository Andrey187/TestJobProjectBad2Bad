using UnityEditor;

[CustomEditor(typeof(Item))]
public class ItemEditor : Editor
{
    SerializedProperty _magazineSize;

    private void OnEnable()
    {
        _magazineSize = serializedObject.FindProperty("_magazineSize");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        Item item = target as Item;

        // Loop through all serialized fields
        SerializedProperty property = serializedObject.GetIterator();
        bool next = property.NextVisible(true);

        while (next)
        {
            if (property.name != "_magazineSize")
            {
                EditorGUILayout.PropertyField(property);
            }

            next = property.NextVisible(false);
        }

        // Determine which fields to display based on type
        if (item.Type == ItemType.Weapon)
        {
            EditorGUILayout.PropertyField(_magazineSize);
        }
        serializedObject.ApplyModifiedProperties();
    }
}
