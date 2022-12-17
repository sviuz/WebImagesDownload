using System.Collections.Generic;

public class ObjectHandler<T> where T : class {
  private static readonly Dictionary<string, T> objects = new();

  public void AddObjectNonStatic(string name, T obj) {
    AddObject(name, obj);
  }
  public static void AddObject(string name, T obj) {
    if (objects.ContainsKey(name)) {
      return;
    }

    objects.Add(name, obj);
  }

  public static T GetObject(string name) {
    objects.TryGetValue(name, out T obj);
    return obj;
  }

  public static bool ExtractObject<T>(string name, out T obj) {
    bool condition()=>objects is not null && objects.ContainsKey(name);
    // obj = 

    return condition();
  }
}