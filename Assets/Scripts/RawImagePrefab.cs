using UnityEngine;
using UnityEngine.UI;

public class RawImagePrefab : MonoBehaviour {
  [SerializeField]
  private RawImage _rawImage;

  public void SetTexture(Texture texture) {
    if (texture == null) {
      Debug.Log("texture is empty");
      return;
    }

    if (!texture.isReadable) {
      Debug.Log("texture is unreadable");
      return;
    }

    _rawImage.texture = texture;
  }
}