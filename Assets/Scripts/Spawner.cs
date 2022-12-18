using System;
using Helpers;
using UnityEngine;

public delegate RawImagePrefab SpawnDelegate(Texture texture); 

public class Spawner : MonoBehaviour {
  public static SpawnDelegate OnSpawn;
  [SerializeField]
  private RawImagePrefab _rawImagePrefab;
  [SerializeField]
  private Transform _transform;

  private void Awake() {
    SubscribeActions();
  }

  private void OnDestroy() {
    SubscribeActions(false);
  }

  private void SubscribeActions(bool sub = true) {
    OnSpawn = sub ? Spawn : null;
  }

  private RawImagePrefab Spawn(Texture texture) {
    return InstantiateImage(texture);
  }

  private RawImagePrefab InstantiateImage(Texture texture) {
    return Instantiate(_rawImagePrefab, _transform).Then(img => img.SetTexture(texture));
  }
}