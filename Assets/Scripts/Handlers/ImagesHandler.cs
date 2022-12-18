using System.Collections.Generic;
using UnityEngine;

namespace Handlers {
  public sealed class ImagesHandler :  ObjectHandler<RawImagePrefab> {
    private static List<GameObject> _gameObjects = new();
    // private static ObjectHandler<RawImagePrefab> imagesObjectHandler = new();

    public static void AddImageObject(string objName, RawImagePrefab obj) {
      AddObject(objName, obj);
      _gameObjects.Add(obj.gameObject);
    }

    public static void Clear() {
      if (_gameObjects.Count == decimal.Zero) {
        return;
      }

      foreach (GameObject obj in _gameObjects) {
        Object.Destroy(obj);
      }

      _gameObjects.Clear();
    }
  }
}