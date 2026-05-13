using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace SceneLoading
{
    public class SceneLoader : Utils.Singleton<SceneLoader>
    {
        private const string LoadingSceneName = "LoadingScene";
        private const float FadeInTime = 1f;
        private const float FadeOutTime = 2f;

        public static Action SceneLoadFinished;

        public static void LoadScene(string sceneName)
        {
            if (Instance == null)
            {
                Debug.LogError("[SceneLoader]: SceneLoader Instance Not Found");
                return;
            }

            if (SceneUtility.GetBuildIndexByScenePath(sceneName) == -1)
            {
                Debug.LogError("[SceneLoader]: Scene not found");
                return;
            }

            Instance.StartSceneLoad(sceneName);
        }

        #region Instance
        public float LoadProgress { get; private set; }
        public string SceneToLoad { get; private set; }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void StartSceneLoad(string sceneName)
        {
            SceneToLoad = sceneName;
            StartCoroutine(LoadSceneAsync(sceneName));
        }

        private IEnumerator LoadSceneAsync(string sceneName)
        {
            // First load the loading scene
            var sceneLoadOperation = SceneManager.LoadSceneAsync(LoadingSceneName, LoadSceneMode.Additive);
            yield return new WaitUntil(() => sceneLoadOperation.isDone);


            // Small delay for animations
            yield return new WaitForSeconds(FadeInTime);


            // Get rid of the current scenes
            for (int i = 0; i < SceneManager.loadedSceneCount; i++)
            {
                string scene = SceneManager.GetSceneAt(i).name;

                if (scene == LoadingSceneName)
                    continue;

                SceneManager.UnloadSceneAsync(scene);
            }


            // Next load the actual scene in the background
            var targetSceneLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            while (!targetSceneLoad.isDone)
            {
                LoadProgress = targetSceneLoad.progress;
                yield return null;
            }

            SceneLoadFinished?.Invoke();


            // Small delay for animations
            yield return new WaitForSeconds(FadeOutTime);


            // Now unload the loading scene
            SceneManager.UnloadSceneAsync(LoadingSceneName);
        }
        #endregion
    }
}