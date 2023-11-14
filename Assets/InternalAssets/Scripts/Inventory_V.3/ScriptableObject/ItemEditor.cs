using UnityEditor;

[CustomEditor(typeof(Item))]
public class ItemEditor : Editor
{
    SerializedProperty _countOfBullets;

    private void OnEnable()
    {
        _countOfBullets = serializedObject.FindProperty("_countOfBullets");
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
            if (property.name != "_countOfBullets")
            {
                EditorGUILayout.PropertyField(property);
            }

            next = property.NextVisible(false);
        }

        // ����������, ����� ���� ���������� � ����������� �� ����
        if (item.Type == ItemType.Weapon)
        {
            EditorGUILayout.PropertyField(_countOfBullets);
        }
        serializedObject.ApplyModifiedProperties();
    }
}
