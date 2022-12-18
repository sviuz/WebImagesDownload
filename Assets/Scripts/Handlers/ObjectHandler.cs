using System.Collections.Generic;

namespace Handlers {
  public class ObjectHandler<T> where T : class {
    private static readonly Dictionary<string, T> objects = new();

    public static void AddObject(string name, T obj) {
      objects.TryAdd(name, obj);
    }

    public static bool ExtractObject(string name, out T obj) {
      objects.TryGetValue(name, out obj);

      return objects is not null && objects.ContainsKey(name);
    }

    public void AddObjectNonStatic(string name, T obj) {
      AddObject(name, obj);
    }
  }
}