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
  private Button _clear;
  [SerializeField]
  private TMP_Text _imagesCapacityText;

  private int _imagesCapacity;

  public void SubscribeGetImagesButton(Action<string> getImages) {
    _getImages.onClick.AddListener(() => getImages(_inputField.text));
  }

  public void SubscribeClearButton(Action clean) {
    _clear.onClick.AddListener(() => clean());
  }

  public void IncreaseImagesCapacity() {
    _imagesCapacity += 1;
    _imagesCapacityText.text = $"Images: {_imagesCapacity.ToString()}";
  }

  public void ClearCapacity() {
    _imagesCapacity = 0;
    _imagesCapacityText.text = $"Images: {_imagesCapacity.ToString()}";
    _inputField.text = String.Empty;
  }
}