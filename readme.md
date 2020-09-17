# MLEventSystem - dll

## Description
MLEventSystem is a open source dll project for unity game engine. You can use this dll to manager your event and message.  

## How 2 use?  

### Step 
1. import dll asset to Plugins folder.
2. import namspace 'MLEventSystem' to your script.
3. make sure you hava a gameobject with EventManager script on your scene.
3. use it!

### Useage
MLEventSystem use `int` to define eventID, but recommand define your event type through System.Enum, just like this:
```csharp
public enum EventType : int
{
    Fire,
    Reborn,
    Dead
}
```
Then you can register your event to MLEventSystem.  
```csharp
EventManager.Instance.AddListener(EventType.Fire, (sender, args) => { /*DO YOUR THINGS*/ });
```
The listener delegate just like this:
```
public delegate void EventHandler(object sender, params object[] args);
```

You can use `Notification` to trigger your event.
```csharp
EventManager.Instance.Notification(EventType.ClickEven, this, arg);

//prototype
public void Notification(int eventID, object sender, params object[] args);
```