using UnityEngine;

using actions = Ghost_Watchers_Internal.src.Actions;
using menu = Ghost_Watchers_Internal.objects.preferences;

public class UI
{
    public static Rect MainMenu = new Rect(5f, 5f, 230f, 175f);
    public static Rect GhostMenu = new Rect(5f, 5f, 270f, 560f);
    public static Rect PlayerMenu = new Rect(5f, 5f, 270f, 300f);
    public static Rect HouseMenu = new Rect(5f, 5f, 270f, 350f);
    public static Rect PerksMenu = new Rect(5f, 5f, 270f, 280f);

    public static void displayUI()
    {
        if (menu.main_menu) { MainMenu = GUI.Window(0, MainMenu, MainWindow, menu.Title); }
        if (menu.ghost_menu) { GhostMenu = GUI.Window(1, GhostMenu, GhostWindow, "Ghost Menu"); }
        if (menu.player_menu) { PlayerMenu = GUI.Window(2, PlayerMenu, PlayerWindow, "Player Menu"); }
        if (menu.house_menu) { HouseMenu = GUI.Window(3, HouseMenu, HouseWindow, "House Menu"); }
        if (menu.perks_menu) { PerksMenu = GUI.Window(4, PerksMenu, PerksWindow, "Perks Menu"); }
    }

    public static void MainWindow(int windowID)
    {
        GUILayout.BeginVertical("box");

        GUILayout.BeginHorizontal("");
        menu.toggle_GhostMenu = GUILayout.Toggle(menu.ghost_menu, "Ghost Menu");
        if (menu.toggle_GhostMenu != menu.ghost_menu) { menu.ghost_menu = !menu.ghost_menu; }
        menu.toggle_PlayersMenu = GUILayout.Toggle(menu.player_menu, "Player Menu");
        if (menu.toggle_PlayersMenu != menu.player_menu) { menu.player_menu = !menu.player_menu; }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal("");
        menu.toggle_HouseMenu = GUILayout.Toggle(menu.house_menu, "House Menu");
        if (menu.toggle_HouseMenu != menu.house_menu) { menu.house_menu = !menu.house_menu; }
        menu.toggle_PerksMenu = GUILayout.Toggle(menu.perks_menu, "Perks Menu");
        if (menu.toggle_PerksMenu != menu.perks_menu) { menu.perks_menu = !menu.perks_menu; }
        GUILayout.EndHorizontal();

        GUILayout.EndVertical();

        GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));
    }

    public static void GhostWindow(int windowID)
    {
        GUILayout.BeginVertical("box");

        // Define a GUIStyle for the bold labels
        GUIStyle boldLabelStyle = new GUIStyle(GUI.skin.label);
        boldLabelStyle.fontStyle = FontStyle.Bold;

        foreach (Donteco.GhostAI ghost in UnityEngine.GameObject.FindObjectsOfType(typeof(Donteco.GhostAI)) as Donteco.GhostAI[])
        {
            GUILayout.Label($"<b>Ghost type:</b> {ghost.Data.name.Replace("(Clone)", "")}", boldLabelStyle);
            GUILayout.Label($"<b>Age:</b> {ghost.Data.Age.ToString()}", boldLabelStyle);
            GUILayout.Label($"<b>Mood:</b> {ghost.Data.Mood.ToString()}", boldLabelStyle);
            GUILayout.Label($"<b>Temperature:</b> {ghost.Data.GetTemperatureValue().ToString()}", boldLabelStyle);
            GUILayout.Label($"<b>EMP:</b> {ghost.Data.GetEmpValue().ToString()}", boldLabelStyle);
            GUILayout.Label($"<b>Ghost Rank:</b> {ghost.Data.Rank.ToString()}", boldLabelStyle);
            GUILayout.Label($"<b>Temperature:</b> {ghost.Data.GetTemperatureValue().ToString()}", boldLabelStyle);
            GUILayout.Label($"<b>Can Capture:</b> {ghost.CanStartCapture().ToString()}", boldLabelStyle);
            GUILayout.Label($"<b>Can Attack:</b> {ghost.CanAttack().ToString()}", boldLabelStyle);
            GUILayout.Label($"<b>Can Hunt:</b> {ghost.CanHunt().ToString()}", boldLabelStyle);
            GUILayout.Label($"<b>Can Critical Attack:</b> {ghost.CanCriticalAttack().ToString()}", boldLabelStyle);

        }

        GUILayout.BeginVertical("box");
        GUILayout.Label("Actions");

        GUILayout.BeginHorizontal("");
        menu.toggle_GhostEsp = GUILayout.Toggle(menu.show_ghost_esp, "Show ghost ESP");
        if (menu.toggle_GhostEsp != menu.show_ghost_esp) { menu.show_ghost_esp = !menu.show_ghost_esp; }
        if (GUILayout.Button("Show Snapline")) { menu.show_ghost_snapline = !menu.show_ghost_snapline; }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal("");
        if (GUILayout.Button("Appear")) { actions.Appear(); }
        if (GUILayout.Button("Do random action")) { actions.RandomAction(); }
        if (GUILayout.Button("Attack")) { actions.DoAttack(); }
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal("");
        if (GUILayout.Button("Fast Attack")) { actions.DoFastAttack(); }
        if (GUILayout.Button("Hunt")) { actions.DoHunt(); }
        if (GUILayout.Button("Capture player")) { actions.DoCapture(); }
        GUILayout.EndHorizontal();



        GUILayout.EndVertical();

        GUILayout.EndVertical();

        GUI.DragWindow(new Rect(0, 0, (float)Screen.width, (float)Screen.height));
    }
    public static void PlayerWindow(int windowID)
    {
        GUILayout.BeginVertical("box");

        GUILayout.BeginHorizontal("");
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal("");
        GUILayout.BeginVertical("box");
        GUILayout.Label("Lobby");

        GUILayout.BeginHorizontal("");
        menu.toggle_PlayersEsp = GUILayout.Toggle(menu.show_players_esp, "Show players ESP");
        if (menu.toggle_PlayersEsp != menu.show_players_esp) { menu.show_players_esp = !menu.show_players_esp; }
        if (GUILayout.Button("Show Snapline")) { menu.show_players_snapline = !menu.show_players_snapline; }
        GUILayout.EndHorizontal();

        GUILayout.EndVertical();
        GUILayout.EndHorizontal();

        GUILayout.EndVertical();

        GUI.DragWindow(new Rect(0, 0, (float)Screen.width, (float)Screen.height));

    }
    public static void HouseWindow(int windowID)
    {
        GUILayout.BeginVertical("box");

        GUILayout.BeginHorizontal("");
        menu.toggle_CursedItemEsp = GUILayout.Toggle(menu.show_cursedItem, "Show cursed Item");
        if (menu.toggle_CursedItemEsp != menu.show_cursedItem) { menu.show_cursedItem = !menu.show_cursedItem; }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal("");
        GUILayout.EndHorizontal();

        GUILayout.EndVertical();

        GUI.DragWindow(new Rect(0, 0, (float)Screen.width, (float)Screen.height));

    }
    public static void PerksWindow(int windowID)
    {
        GUILayout.BeginVertical("box");

        GUILayout.BeginHorizontal("");
        if (GUILayout.Button("Give money and XP (x1000)")) { actions.GiveMoneyAndExp(1000, 100); }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal("");
        if (GUILayout.Button("Unlock all achivements")) { actions.UnlockAllAchievements(); }
        GUILayout.EndHorizontal();

        GUILayout.EndVertical();

        GUI.DragWindow(new Rect(0, 0, (float)Screen.width, (float)Screen.height));

    }
}
