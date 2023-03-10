using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TopBarMediator : MonoBehaviour {
  [SerializeField]
  private TMP_InputField _inputField;
  [SerializeField]
  private Button _getImages;
  [SerializeField]
  private TMP_Text _imagesCapacityText;
  private int _imagesCapacity;

  public void SubscribeAction(Action<string> getImages) {
    _getImages.onClick.AddListener(() => getImages?.Invoke(_inputField.text));
  }

  public void IncreaseImagesCapacity() {
    _imagesCapacity += 1;
    _imagesCapacityText.text = _imagesCapacity.ToString();
  }

  public void ClearCapacity() {
    _imagesCapacity = 0;
    _imagesCapacityText.text = _imagesCapacity.ToString();
  }
}