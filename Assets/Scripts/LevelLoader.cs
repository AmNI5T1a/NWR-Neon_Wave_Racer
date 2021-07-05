using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NWR.Modules
{
    public class LevelLoader : Singleton<LevelLoader>
    {
        private static bool sceneIsLoading;
        private AsyncOperation operation;

        void Awake()
        {
            InstanciateLoadingScreenCanvasFromRes();
        }

        private void InstanciateLoadingScreenCanvasFromRes()
        {
            var loadingScreen = Resources.Load("loadingScreen");
            var obj = Instantiate(loadingScreen) as GameObject;
            obj.transform.SetParent(this.gameObject.transform);
        }

        public void LoadScene(int sceneIdToLoad)
        {
            if (!sceneIsLoading)
                StartCoroutine(Load(sceneIdToLoad));
            else
                Debug.LogWarning("Scene is already loading!");
        }

        private IEnumerator Load(int sceneIdToLoad)
        {
            sceneIsLoading = true;
            yield return null;
            Debug.Log("Yeah everything works fine");
            yield return new WaitForSeconds(1f);
            Destroy(this.gameObject);
        }
    }
}
