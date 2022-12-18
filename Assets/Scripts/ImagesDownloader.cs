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
  [SerializeField]
  private Spawner _spawner;
  [SerializeField]
  private ImagesHandler _imagesHandler;

  private void Start() {
    _topBarMediator.SubscribeGetImagesButton(DownloadImage);
    _topBarMediator.SubscribeClearButton(_imagesHandler.Clear);
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
               string imageUrl = htmlCode.ConvertAsImageUrl();

               if (TextureHandler.ExtractObject(imageUrl, out Texture texture)) {
                 InstantiateRawImage(imageUrl, texture);
                 continue;
               }

               TryGetTexture(imageUrl);
             }
           };
  }

  private void CleanUp() {
    _imagesHandler.Clear();
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
    RawImagePrefab prefab = _spawner.Spawn(texture);
    _topBarMediator.IncreaseImagesCapacity();
    TextureHandler.AddObject(imageUrl, texture);
    _imagesHandler.AddImageObject(imageUrl, prefab);
  }

  private static void Get(string url, Action<string> onError, Action<string> onSuccess) {
    WebRequests.Get(url, onError, onSuccess);
  }

  private static void GetTexture(string url, Action<string> onError, Action<Texture2D> onSuccess) {
    WebRequests.GetTexture(url, onError, onSuccess);
  }
}