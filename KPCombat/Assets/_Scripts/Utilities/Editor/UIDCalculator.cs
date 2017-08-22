using UnityEditor;
using UnityEngine;

public class UIDCalculator : EditorWindow
{
    int themeSelection, objTypeSelection, enemySelection, trapSelection, obstacleSelection, breakableSelection;

    string[] themeArr = new string[2] { "Forest", "Dungeon" };
    string[] objTypeArr = new string[4] { "Enemy", "Trap", "Obstacle", "Breakable" };
    string[] enemyArr = new string[4] { "Elemental Fire", "Imp", "Shielded Redbull", "Melee Redbull" };
    string[] trapArr = new string[3] { "Lava Ball", "Fire Wall", "Lava Thrower" };
    string[] obstacleArr = new string[2] { "Bridge", "Hill" };
    string[] breakableArr = new string[2] { "Urn 1", "Urn 2" };

    int id = -1;

    [MenuItem("Window/UIDCalculator")]
    static void OpenCalculator()
    {
        UIDCalculator window = (UIDCalculator)GetWindow(typeof(UIDCalculator));
        window.Show();
    }

    private void OnGUI()
    {
        EditorGUI.BeginChangeCheck();

        GUILayout.Label("Select Theme", EditorStyles.boldLabel);
        themeSelection = GUILayout.SelectionGrid(themeSelection, themeArr, 1, "toggle");

        EditorGUILayout.Space();

        GUILayout.Label("Select Obj Type", EditorStyles.boldLabel);
        objTypeSelection = GUILayout.SelectionGrid(objTypeSelection, objTypeArr, 1, "toggle");

        EditorGUILayout.Space();

        switch (objTypeSelection)
        {
            case 0:
                GUILayout.Label("Select Enemy", EditorStyles.boldLabel);
                enemySelection = GUILayout.SelectionGrid(enemySelection, enemyArr, 1, "toggle");

                id = Utilities.GetUID(themeSelection + 1, objTypeSelection, enemySelection);
                break;
            case 1:
                GUILayout.Label("Select Trap", EditorStyles.boldLabel);
                trapSelection = GUILayout.SelectionGrid(trapSelection, trapArr, 1, "toggle");

                id = Utilities.GetUID(themeSelection + 1, objTypeSelection, trapSelection);
                break;
            case 2:
                GUILayout.Label("Select Obstacle", EditorStyles.boldLabel);
                obstacleSelection = GUILayout.SelectionGrid(obstacleSelection, obstacleArr, 1, "toggle");

                id = Utilities.GetUID(themeSelection + 1, objTypeSelection, obstacleSelection);
                break;
            case 3:
                GUILayout.Label("Select Breakable", EditorStyles.boldLabel);
                breakableSelection = GUILayout.SelectionGrid(breakableSelection, breakableArr, 1, "toggle");

                id = Utilities.GetUID(themeSelection + 1, objTypeSelection, breakableSelection);
                break;
        }

        EditorGUILayout.Space();

        GUILayout.Label("Obj ID: " + id, EditorStyles.boldLabel);
    }
}
