using DefaultNamespace;
using UnityEngine;

public class Spawner : MonoBehaviour {
  [SerializeField]
  private RawImagePrefab _rawImagePrefab;
  [SerializeField]
  private Transform _transform;

  public RawImagePrefab Spawn(Texture texture) {
    return InstantiateImage(texture);
  }
  
  private RawImagePrefab InstantiateImage(Texture texture) {
    return Instantiate(_rawImagePrefab, _transform).Then(img => img.SetTexture(texture));
  }
}