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

        // ������ �� ���� ��������������� �����
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

        // ����������, ����� ���� ���������� � ����������� �� ����
        if (item.Type == ItemType.Weapon)
        {
            EditorGUILayout.PropertyField(_magazineSize);
        }
        serializedObject.ApplyModifiedProperties();
    }
}
