using System;
using DefaultNamespace;
using Handlers;
using UnityEngine;

public class ImagesDownloader : MonoBehaviour {
  /// <summary>
  ///   test links
  ///   https://www.coolgenerator.com/random-image-generator?new
  ///   https://www.thegentlemansjournal.com/article/dress-like-an-italian-alessandro-squarzis-guide-to-summer-style/
  ///   https://www.fortela.it/Shop/
  /// </summary>
  /// 
  [SerializeField]
  private TopBarMediator _topBarMediator;
  [SerializeField]
  private Spawner _spawner;
  [SerializeField]
  private ImagesHandler _imagesHandler;

  private void Start() {
    _topBarMediator.SubscribeAction(DownloadImage);
  }

  private void DownloadImage(string url) { //TODO refactor
    CLeanUp();

    Get(url, error => {
               Debug.Log("Error");
             }, htmlCode => {
                  var cycleProtection = 0;
                  while (B(htmlCode, cycleProtection)) {
                    cycleProtection++;
                    string imageUrl = GettingImageUrl(ref htmlCode);

                    if (TextureHandler.ExtractObject(imageUrl)) {
                      Texture texture = TextureHandler.GetObject(imageUrl);
                      InstantiateRawImage(imageUrl, texture);
                      continue;
                    }
                    
                    TryGetTexture(imageUrl);
                  }
                });
  }

  private static string GettingImageUrl(ref string htmlCode) {
    var textToFind = "<img";
    htmlCode = htmlCode[(htmlCode.IndexOf(textToFind, StringComparison.Ordinal) + textToFind.Length)..];
    textToFind = "src=\"";
    htmlCode = htmlCode[(htmlCode.IndexOf(textToFind, StringComparison.Ordinal) + textToFind.Length)..];
    string imageUrl = htmlCode[..htmlCode.IndexOf("\"", StringComparison.Ordinal)];
    return imageUrl;
  }

  private void CLeanUp() {
    _imagesHandler.Clear();
    _topBarMediator.ClearCapacity();
  }

  private static bool B(string htmlCode, int cycleProtection) {
    return HtmlHasImgTag(htmlCode) && cycleProtection < 100;
  }

  private static bool HtmlHasImgTag(string htmlCode) {
    return htmlCode.IndexOf("<img", StringComparison.Ordinal) != -1;
  }

  private void TryGetTexture(string imageUrl) {
    

    GetTexture(imageUrl, error => {
                           Debug.Log("Error: " + error);
                         }, texture => {
                              if (texture.IsValid()) {
                                InstantiateRawImage(imageUrl, texture);
                              } else {
                                Debug.Log("texture is not valid");
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