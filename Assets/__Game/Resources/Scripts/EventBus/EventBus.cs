using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace __Game.Resources.Scripts.EventBus
{

  public static class EventBusUtility
  {
    public static IReadOnlyList<Type> EventTypes { get; private set; }
    public static IReadOnlyList<Type> StaticEventBusesTypes { get; private set; }

#if UNITY_EDITOR
    public static PlayModeStateChange PlayModeState { get; private set; }

    [InitializeOnLoadMethod]
    public static void InitializeEditor()
    {
      EditorApplication.playModeStateChanged -= HandleEditorStateChange;
      EditorApplication.playModeStateChanged += HandleEditorStateChange;
    }

    private static void HandleEditorStateChange(PlayModeStateChange state)
    {
      PlayModeState = state;

      if (PlayModeState == PlayModeStateChange.EnteredEditMode)
        ClearAllBuses();
    }
#endif

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Init()
    {
      var assemblies = AppDomain.CurrentDomain.GetAssemblies();
      Type[] assemblyCSharp = null;
      Type[] assemblyCSharpFirstPass = null;

      for (int i = 0; i < assemblies.Length; i++)
      {
        if (assemblies[i].GetName().Name == "Assembly-CSharp")
          assemblyCSharp = assemblies[i].GetTypes();
        else if (assemblies[i].GetName().Name == "Assembly-CSharp-firstpass")
          assemblyCSharpFirstPass = assemblies[i].GetTypes();

        if (assemblyCSharp != null && assemblyCSharpFirstPass != null)
          break;
      }

      List<Type> eventTypes = new List<Type>();

      if (assemblyCSharp != null)
      {
        for (int i = 0; i < assemblyCSharp.Length; i++)
        {
          var type = assemblyCSharp[i].GetType();

          if (typeof(IEvent) != type && typeof(IEvent).IsAssignableFrom(type))
          {
            eventTypes.Add(type);
          }
        }
      }

      if (assemblyCSharpFirstPass != null)
      {
        for (int i = 0; i < assemblyCSharpFirstPass.Length; i++)
        {
          var type = assemblyCSharpFirstPass[i].GetType();

          if (typeof(IEvent) != type && typeof(IEvent).IsAssignableFrom(type))
          {
            eventTypes.Add(type);
          }
        }
      }

      EventTypes = eventTypes;

      List<Type> staticEventBusesTypes = new List<Type>();
      var typedef = typeof(EventBus<>);

      for (int i = 0; i < EventTypes.Count; i++)
      {
        var type = EventTypes[i];
        var genType = typedef.MakeGenericType(type);

        staticEventBusesTypes.Add(genType);
      }

      StaticEventBusesTypes = staticEventBusesTypes;
    }

    public static void ClearAllBuses()
    {
      for (int i = 0; i < StaticEventBusesTypes.Count; i++)
      {
        var type = StaticEventBusesTypes[i];
        var clearMethod = type.GetMethod("Clear", BindingFlags.Static | BindingFlags.NonPublic);

        if (clearMethod != null)
        {
          clearMethod.Invoke(null, null);
        }
      }
    }
  }

  public static class EventBus<T> where T : struct, IEvent
  {
    private static EventBinding<T>[] bindings = new EventBinding<T>[64];
    private static readonly List<Callback> callbacks = new List<Callback>();
    private static int count;

    public class Awaiter : EventBinding<T>
    {
      public bool EventRaised { get; private set; }
      public T Payload { get; private set; }

      public Awaiter() : base((Action)null)
      {
        ((IEventBinding<T>)this).OnEvent = OnEvent;
      }

      private void OnEvent(T ev)
      {
        EventRaised = true;
        Payload = ev;
      }
    }

    private struct Callback
    {
      public Action onEventNoArg;
      public Action<T> onEvent;
    }

    private static void Clear()
    {
      bindings = new EventBinding<T>[64];
      callbacks.Clear();
      count = 0;
    }

    public static void Register(EventBinding<T> binding)
    {
      if (binding.Registered)
        return;

      if (bindings.Length <= count)
      {
        EventBinding<T>[] newArray = new EventBinding<T>[bindings.Length * 2];
        Array.Copy(bindings, newArray, bindings.Length);

        bindings = newArray;
      }

      binding.InternalIndex = count;
      bindings[count] = binding;

      count++;
    }

    public static void AddCallback(Action callback)
    {
      if (callback == null)
        return;

      callbacks.Add(new Callback() { onEventNoArg = callback });
    }

    public static void AddCallback(Action<T> callback)
    {
      if (callback == null)
        return;

      callbacks.Add(new Callback() { onEvent = callback });
    }

    public static void Unregister(EventBinding<T> binding)
    {
#if UNITY_EDITOR
      if (EventBusUtility.PlayModeState == PlayModeStateChange.ExitingPlayMode)
        return;
#endif
      int index = binding.InternalIndex;

      if (index == -1 || index > count)
      {
        // Binding invalid
        return;
      }

      if (bindings[index] != binding)
      {
        // Binding invalid
        return;
      }

      if (index == count - 1)
      {
        bindings[count - 1] = null;
        binding.InternalIndex = -1;
        count--;

        return;
      }

      int lastIndex = count - 1;
      var last = bindings[lastIndex];

      bindings[index] = last;
      bindings[lastIndex] = null;

      if (last != null)
        last.InternalIndex = index;
      binding.InternalIndex = -1;

      count--;
    }

    public static void Raise()
    {
      Raise(default);
    }

    public static void Raise(T ev)
    {
#if UNITY_EDITOR
      if (EventBusUtility.PlayModeState == PlayModeStateChange.ExitingPlayMode)
        return;
#endif
      for (int i = 0; i < count; i++)
      {
        IEventBinding<T> internalBind = bindings[i];
        internalBind.OnEvent?.Invoke(ev);
        internalBind.OnEventArgs?.Invoke();
      }

      for (int i = 0; i < callbacks.Count; i++)
      {
        callbacks[i].onEvent?.Invoke(ev);
        callbacks[i].onEventNoArg?.Invoke();
      }

      callbacks.Clear();
    }

    public static string GetDebugInfoString()
    {
      return "Bindings: " + count + " BufferSize: " + bindings.Length + "\n"
             + "Callbacks: " + callbacks.Count;
    }

    public static Awaiter NewAwaiter()
    {
      // TODO: do it non alloc
      return new Awaiter();
    }
  }
}