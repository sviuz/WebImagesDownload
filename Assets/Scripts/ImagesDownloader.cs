using System;
using Handlers;
using Helpers;
using UnityEngine;

public class ImagesDownloader : MonoBehaviour {
  /// <summary>
  ///   test links
  ///   https://www.coolgenerator.com/random-image-generator?new
  ///   https://www.thegentlemansjournal.com/article/dress-like-an-italian-alessandro-squarzis-guide-to-summer-style/
  ///   https://www.fortela.it/Shop/
  /// </summary>
  [SerializeField]
  private TopBarMediator _topBarMediator;

  private void Start() {
    _topBarMediator.SubscribeGetImagesButton(DownloadImage);
    _topBarMediator.SubscribeClearButton(CleanUp);
  }

  private void DownloadImage(string url) {
    CleanUp();

    Get(url, OnError(), OnSuccess());
  }

  private static Action<string> OnError() {
    return error => {
             Debug.Log("Error");
           };
  }

  private Action<string> OnSuccess() {
    return htmlCode => {
             var cycleProtection = 0;
             while (B(htmlCode, cycleProtection)) {
               cycleProtection++;
               string imageUrl = ExtractImageUtl(ref htmlCode);

               if (TextureHandler.ExtractObject(imageUrl, out Texture texture)) {
                 InstantiateRawImage(imageUrl, texture);
                 Debug.Log($"instantiating {imageUrl}");
                 continue;
               }

               TryGetTexture(imageUrl);
             }
           };
  }

  private static string ExtractImageUtl(ref string htmlCode) {
    var textToFind = "<img";
    htmlCode = htmlCode[(htmlCode.IndexOf(textToFind, StringComparison.Ordinal) + textToFind.Length)..];
    textToFind = "src=\"";
    htmlCode = htmlCode[(htmlCode.IndexOf(textToFind, StringComparison.Ordinal) + textToFind.Length)..];
    string imageUrl = htmlCode[..htmlCode.IndexOf("\"", StringComparison.Ordinal)];
    return imageUrl;
  }

  private void CleanUp() {
    ImagesHandler.Clear();
    _topBarMediator.ClearCapacity();
  }

  private static bool B(string htmlCode, int cycleProtection) {
    return htmlCode.ContainsImgTag() && cycleProtection < 100;
  }

  private void TryGetTexture(string imageUrl) {
    GetTexture(imageUrl, OnError(), texture => {
                                      if (texture.IsValid()) {
                                        InstantiateRawImage(imageUrl, texture);
                                      }
                                    });
  }

  private void InstantiateRawImage(string imageUrl, Texture texture) {
    RawImagePrefab prefab = Spawner.OnSpawn?.Invoke(texture);
    _topBarMediator.IncreaseImagesCapacity();
    TextureHandler.AddObject(imageUrl, texture);
    ImagesHandler.AddImageObject(imageUrl, prefab);
  }

  private static void Get(string url, Action<string> onError, Action<string> onSuccess) {
    WebRequests.Get(url, onError, onSuccess);
  }

  private static void GetTexture(string url, Action<string> onError, Action<Texture2D> onSuccess) {
    WebRequests.GetTexture(url, onError, onSuccess);
  }
}