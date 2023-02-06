using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class CustomDictionaryEditor
{
    public static void ShowDictionary(List<Item> items, List<int> counts)
    {
        if (items.Count > 0)
        {
            EditorGUILayout.BeginHorizontal("box");
            EditorGUILayout.LabelField("Item");
            EditorGUILayout.LabelField("Count");
            EditorGUILayout.EndHorizontal();
        }

        for (int i = 0; i < items.Count; i++)
        {
            EditorGUILayout.BeginHorizontal("Button");
            items[i] = (Item)EditorGUILayout.ObjectField(items[i], typeof(Item));
            counts[i] = EditorGUILayout.IntField(counts[i]);
            EditorGUILayout.EndHorizontal();
        }
        var button = EditorGUILayout.BeginHorizontal("Button");

        if(GUILayout.Button("Add slot"))
        {
            items.Add(null);
            counts.Add(1);
        }

        if (GUILayout.Button("Remove slot") && items.Count != 0)
        {
            items.Remove(items.Last());
            counts.Remove(counts.Last());
        }

        EditorGUILayout.EndHorizontal();

    }

    private static void RenameKey<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey fromKey, TKey toKey)
    {
        TValue value = dic[fromKey];
        dic.Remove(fromKey);
        dic[toKey] = value;
    }
}
