using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayModeManager : MonoBehaviour {

    [SerializeField]
    private Player PlayerPrefab;

    public static bool HasLoaded { get { return Loader.HasLoaded; } }
    public static Player Player { get; private set; }

    private static PlayModeManager instance;

    private const string DontDestroySceneName = "DontDestroy";
    
    private void Awake()
    {
        instance = this;

        Game.Initialize();
    }
    public static void LoadPlayer()
    {
        if (Player != null)
            return;

        Player = Instantiate(instance.PlayerPrefab);
        Player.gameObject.AddToWorld();
    }
    public static void LoadGlobalManagers()
    {
        List<Scene> allLoadedScenes = new List<Scene>(SceneManager.GetAllScenes());
        bool managerSceneLoaded = false;

        for (int i = 0; i < allLoadedScenes.Count; i++)
        {
            if(allLoadedScenes[i].name == DontDestroySceneName)
            {
                managerSceneLoaded = true;
                break;
            }
        }

        if (!managerSceneLoaded)
        {
            SceneManager.LoadScene(DontDestroySceneName, LoadSceneMode.Additive);
        }
    }
}
