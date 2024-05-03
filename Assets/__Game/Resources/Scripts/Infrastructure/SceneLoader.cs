using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.__Game.Scripts.Infrastructure
{
  public class SceneLoader
  {
    public void LoadScene(string sceneName)
    {
      SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void LoadSceneWithDelay(string sceneName, float delay, MonoBehaviour mono, Action callback)
    {
      mono.StartCoroutine(DoLoadSceneWithDelay(sceneName, delay, callback));
    }

    private IEnumerator DoLoadSceneWithDelay(string sceneName, float delay, Action callback)
    {
      yield return new WaitForSeconds(delay);

      SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
      callback?.Invoke();
    }


    public void LoadSceneAsync(string sceneName, Action callback)
    {
      SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single).completed += (AsyncOperation asyncOp) =>
      {
        callback?.Invoke();
      };
    }

    public void LoadSceneAsyncWithDelay(string sceneName, float delay, MonoBehaviour mono, Action callback)
    {
      mono.StartCoroutine(DoLoadSceneAsyncWithDelay(sceneName, delay, callback));
    }

    private IEnumerator DoLoadSceneAsyncWithDelay(string sceneName, float delay, Action callback)
    {
      yield return new WaitForSeconds(delay);

      AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

      asyncOperation.completed += (AsyncOperation asyncOp) =>
      {
        callback?.Invoke();
      };
    }

    public void RestartScene()
    {
      SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void RestartSceneAsync(Action callback)
    {
      SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single).completed += (AsyncOperation asyncOp) =>
      {
        callback?.Invoke();
      };
    }
  }
}