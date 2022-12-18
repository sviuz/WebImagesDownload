using System;
using UnityEngine;

namespace Helpers {
  public static class Extensions {
    public static bool IsValid(this Texture self) {
      return self != null && self.isReadable;
    }

    public static T Then<T>(this T self, Action<T> action) {
      action?.Invoke(self);
      return self;
    }

    public static string ConvertAsImageUrl(this string htmlCode) {
      var textToFind = "<img";
      htmlCode = htmlCode[(htmlCode.IndexOf(textToFind, StringComparison.Ordinal) + textToFind.Length)..];
      textToFind = "src=\"";
      htmlCode = htmlCode[(htmlCode.IndexOf(textToFind, StringComparison.Ordinal) + textToFind.Length)..];
      string imageUrl = htmlCode[..htmlCode.IndexOf("\"", StringComparison.Ordinal)];
      return imageUrl;
    }

    public static bool ContainsImgTag(this string self) {
      return self.IndexOf("<img", StringComparison.Ordinal) != -1;
    }
  }
}