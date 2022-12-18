using System.Collections.Generic;
using UnityEngine;

namespace Handlers {
  public sealed class ImagesHandler : MonoBehaviour {
    private readonly List<GameObject> _gameObjects = new();
    private readonly ObjectHandler<RawImagePrefab> imagesObjectHandler = new();

    public void AddImageObject(string objName, RawImagePrefab obj) {
      imagesObjectHandler.AddObjectNonStatic(objName, obj);
      _gameObjects.Add(obj.gameObject);
    }

    public void Clear() {
      if (_gameObjects.Count == decimal.Zero) {
        return;
      }

      foreach (GameObject obj in _gameObjects) {
        Destroy(obj);
      }

      _gameObjects.Clear();
    }
  }
}