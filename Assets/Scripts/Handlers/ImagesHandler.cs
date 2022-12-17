using System.Collections.Generic;
using UnityEngine;

namespace Handlers {
  public sealed class ImagesHandler : MonoBehaviour {
    private ObjectHandler<RawImagePrefab> imagesObjectHandler = new ObjectHandler<RawImagePrefab>();
    private List<GameObject> _gameObjects = new();

    private static object locker = new ();
    
    public void AddImageObject(string name, RawImagePrefab obj) {
      imagesObjectHandler.AddObjectNonStatic(name, obj);
      _gameObjects.Add(obj.gameObject);
    }

    public void Clear() {
      if (_gameObjects.Count == decimal.Zero) {
        Debug.Log("list is empty");
        return;
      }

      foreach (var obj in _gameObjects) {
        Destroy(obj);
      }
      _gameObjects.Clear();
    }
  }
}