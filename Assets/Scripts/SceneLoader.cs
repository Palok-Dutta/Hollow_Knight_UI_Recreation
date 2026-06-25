using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;
    private readonly HashSet<string> _loadedSceneNames = new HashSet<string>();
    private string _currentLevel;

    private void Start()
    {
        LoadSceneAdditive("MainUi");
        LoadLevel("GameScene");
    }

    public void LoadSceneAdditive(string sceneName, bool setActive = false)
    {
        if (_loadedSceneNames.Contains(sceneName))
        {
            Debug.Log("Scene " + sceneName + " is already loaded");
            return;
        }
        _loadedSceneNames.Add(sceneName);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);

        if (!setActive) return;
        var scene = SceneManager.GetSceneByName(sceneName);
        SceneManager.SetActiveScene(scene);
    }

    public void UnloadSceneAdditive(string sceneName)
    {
        if (!_loadedSceneNames.Contains(sceneName))
        {
            Debug.Log("Scene " + sceneName + " is not loaded");
            return;
        }
        _loadedSceneNames.Remove(sceneName);
        SceneManager.UnloadSceneAsync(sceneName);
    }

    public void LoadLevel(string levelName)
    {
        if (_currentLevel != null)
        {
            UnloadSceneAdditive(_currentLevel);
        }
        SceneManager.LoadScene(levelName, LoadSceneMode.Additive);
        _currentLevel = levelName;
    }
}
