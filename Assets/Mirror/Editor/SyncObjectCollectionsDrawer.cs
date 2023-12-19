// helper class for NetworkBehaviourInspector to draw all enumerable SyncObjects
// (SyncList/Set/Dictionary)
// 'SyncObjectCollectionsDrawer' is a nicer name than 'IEnumerableSyncObjectsDrawer'

using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Mirror.Core;
using UnityEditor;

namespace Mirror.Editor
{
    class SyncObjectCollectionField
    {
        public readonly FieldInfo field;
        public readonly string label;
        public bool visible;

        public SyncObjectCollectionField(FieldInfo field)
        {
            this.field = field;
            visible = false;
            label = $"{field.Name}  [{field.FieldType.Name}]";
        }
    }

    public class SyncObjectCollectionsDrawer
    {
        readonly List<SyncObjectCollectionField> syncObjectCollectionFields;
        readonly UnityEngine.Object targetObject;

        public SyncObjectCollectionsDrawer(UnityEngine.Object targetObject)
        {
            this.targetObject = targetObject;
            syncObjectCollectionFields = new List<SyncObjectCollectionField>();
            foreach (FieldInfo field in InspectorHelper.GetAllFields(targetObject.GetType(), typeof(NetworkBehaviour)))
            {
                // only draw SyncObjects that are IEnumerable (SyncList/Set/Dictionary)
                if (field.IsVisibleSyncObject() &&
                    field.ImplementsInterface<SyncObject>() &&
                    field.ImplementsInterface<IEnumerable>())
                {
                    syncObjectCollectionFields.Add(new SyncObjectCollectionField(field));
                }
            }
        }

        public void Draw()
        {
            if (syncObjectCollectionFields.Count == 0)
            {
                return;
            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Sync Collections", EditorStyles.boldLabel);

            for (int i = 0; i < syncObjectCollectionFields.Count; i++)
            {
                DrawSyncObjectCollection(syncObjectCollectionFields[i]);
            }
        }

        void DrawSyncObjectCollection(SyncObjectCollectionField syncObjectCollectionField)
        {
            syncObjectCollectionField.visible =
                EditorGUILayout.Foldout(syncObjectCollectionField.visible, syncObjectCollectionField.label);
            if (syncObjectCollectionField.visible)
            {
                using (new EditorGUI.IndentLevelScope())
                {
                    object fieldValue = syncObjectCollectionField.field.GetValue(targetObject);
                    if (fieldValue is IEnumerable syncObject)
                    {
                        int index = 0;
                        foreach (object item in syncObject)
                        {
                            string itemValue = item != null ? item.ToString() : "NULL";
                            string itemLabel = $"Element {index}";
                            EditorGUILayout.LabelField(itemLabel, itemValue);

                            index++;
                        }
                    }
                }
            }
        }
    }
}