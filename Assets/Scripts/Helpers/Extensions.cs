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

    public static bool ContainsImgTag(this string self) {
      return self.IndexOf("<img", StringComparison.Ordinal) != -1;
    }
  }
}