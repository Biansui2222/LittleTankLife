using UnityEngine;
using UnityEngine.Internal;
using UnityEngine.SceneManagement;

class SceneManager2 : SceneManager
{
    public SceneManager2() : base()
    {
    }

    public static event System.Action onSceneGoingToLoad;

    [ExcludeFromDocs]
    public static new void LoadScene(string sceneName)
    {
        if (onSceneGoingToLoad != null) onSceneGoingToLoad();
        SceneManager.LoadScene(sceneName);
    }
    //
    // 摘要:
    //     ///
    //     Loads the scene by its name or index in Build Settings.
    //     ///
    //
    // 参数:
    //   sceneName:
    //     Name or path of the scene to load.
    //
    //   sceneBuildIndex:
    //     Index of the scene in the Build Settings to load.
    //
    //   mode:
    //     Allows you to specify whether or not to load the scene additively. /// See SceneManagement.LoadSceneMode
    //     for more information about the options.
    public static new void LoadScene(string sceneName, [DefaultValue("LoadSceneMode.Single")] LoadSceneMode mode)
    {
        if (onSceneGoingToLoad != null) onSceneGoingToLoad();
        SceneManager.LoadScene(sceneName, mode);
    }
    [ExcludeFromDocs]
    public static new void LoadScene(int sceneBuildIndex)
    {
        if (onSceneGoingToLoad != null) onSceneGoingToLoad();
        SceneManager.LoadScene(sceneBuildIndex);
    }
    //
    // 摘要:
    //     ///
    //     Loads the scene by its name or index in Build Settings.
    //     ///
    //
    // 参数:
    //   sceneName:
    //     Name or path of the scene to load.
    //
    //   sceneBuildIndex:
    //     Index of the scene in the Build Settings to load.
    //
    //   mode:
    //     Allows you to specify whether or not to load the scene additively. /// See SceneManagement.LoadSceneMode
    //     for more information about the options.
    public static new void LoadScene(int sceneBuildIndex, [DefaultValue("LoadSceneMode.Single")] LoadSceneMode mode)
    {
        if (onSceneGoingToLoad != null) onSceneGoingToLoad();
        SceneManager.LoadScene(sceneBuildIndex, mode);
    }
    [ExcludeFromDocs]
    public static new AsyncOperation LoadSceneAsync(int sceneBuildIndex)
    {
        if (onSceneGoingToLoad != null) onSceneGoingToLoad();
        return SceneManager.LoadSceneAsync(sceneBuildIndex);
    }
    //
    // 摘要:
    //     ///
    //     Loads the scene asynchronously in the background.
    //     ///
    //
    // 参数:
    //   sceneName:
    //     Name or path of the scene to load.
    //
    //   sceneBuildIndex:
    //     Index of the scene in the Build Settings to load.
    //
    //   mode:
    //     If LoadSceneMode.Single then all current scenes will be unloaded before loading.
    //
    // 返回结果:
    //     ///
    //     Use the AsyncOperation to determine if the operation has completed.
    //     ///
    public static new AsyncOperation LoadSceneAsync(string sceneName, [DefaultValue("LoadSceneMode.Single")] LoadSceneMode mode)
    {
        if (onSceneGoingToLoad != null) onSceneGoingToLoad();
        return SceneManager.LoadSceneAsync(sceneName, mode);
    }
    [ExcludeFromDocs]
    public static new AsyncOperation LoadSceneAsync(string sceneName)
    {
        if (onSceneGoingToLoad != null) onSceneGoingToLoad();
        return SceneManager.LoadSceneAsync(sceneName);
    }
    //
    // 摘要:
    //     ///
    //     Loads the scene asynchronously in the background.
    //     ///
    //
    // 参数:
    //   sceneName:
    //     Name or path of the scene to load.
    //
    //   sceneBuildIndex:
    //     Index of the scene in the Build Settings to load.
    //
    //   mode:
    //     If LoadSceneMode.Single then all current scenes will be unloaded before loading.
    //
    // 返回结果:
    //     ///
    //     Use the AsyncOperation to determine if the operation has completed.
    //     ///
    public static new AsyncOperation LoadSceneAsync(int sceneBuildIndex, [DefaultValue("LoadSceneMode.Single")] LoadSceneMode mode)
    {
        if (onSceneGoingToLoad != null) onSceneGoingToLoad();
        return SceneManager.LoadSceneAsync(sceneBuildIndex, mode);
    }
}