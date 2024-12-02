using UnityEngine;
using UnityEditor;
using SpaceTraveler.GameStructures.Stats;
using UnityEditor.TerrainTools;
using SpaceTraveler.GameStructures.Characters.HumanoidEnemyes;

[CustomEditor(typeof(HumanoidEnemy))]
public class HumanoidEnemyEditor : Editor
{
    public override void OnInspectorGUI()
    {
        HumanoidEnemy enemy = (HumanoidEnemy)target;

        base.OnInspectorGUI();

        
        
        if(GUILayout.Button("Update Stats") & Application.isPlaying)
        {
            Debug.Log("Stats updated");
            enemy.UpdateStats();
        }
    }
}
