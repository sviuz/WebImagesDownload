using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Helpers {
  public static class WebRequests {
    private static WebRequestsMonoBehaviour webRequestsMonoBehaviour;

    public static void Get(string url, Action<string> onError, Action<string> onSuccess) {
      Init();
      webRequestsMonoBehaviour.StartCoroutine(GetCoroutine(url, onError, onSuccess));
    }

    public static void GetTexture(string url, Action<string> onError, Action<Texture2D> onSuccess) {
      Init();
      webRequestsMonoBehaviour.StartCoroutine(GetTextureCoroutine(url, onError, onSuccess));
    }

    private static void Init() {
      if (webRequestsMonoBehaviour == null) {
        var gameObject = new GameObject("WebRequests");
        webRequestsMonoBehaviour = gameObject.AddComponent<WebRequestsMonoBehaviour>();
      }
    }

    private static IEnumerator GetCoroutine(string url, Action<string> onError, Action<string> onSuccess) {
      using (UnityWebRequest unityWebRequest = UnityWebRequest.Get(url)) {
        yield return unityWebRequest.SendWebRequest();

        /*while (!unityWebRequest.isDone)
      {
          Debug.Log("is waiting");
          yield return null;
      }*/

        if (unityWebRequest.isNetworkError || unityWebRequest.isHttpError) {
          onError(unityWebRequest.error);
        } else {
          onSuccess(unityWebRequest.downloadHandler.text);
        }
      }
    }

    private static IEnumerator GetTextureCoroutine(string url, Action<string> onError, Action<Texture2D> onSuccess) {
      using (UnityWebRequest unityWebRequest = UnityWebRequestTexture.GetTexture(url)) {
        yield return unityWebRequest.SendWebRequest();

        /*while (!unityWebRequest.isDone)
      {
          Debug.Log("is waiting");
          yield return null;
      }*/

        if (unityWebRequest.isNetworkError || unityWebRequest.isHttpError) {
          onError(unityWebRequest.error);
        } else {
          var downloadHandlerTexture =
            unityWebRequest.downloadHandler as DownloadHandlerTexture;
          onSuccess(downloadHandlerTexture.texture);
        }
      }
    }

    private class WebRequestsMonoBehaviour : MonoBehaviour { }
  }
}