using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MLEventSystem
{
    /// <summary>
    /// 事件管理器
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class EventManager : Singleton<EventManager>
    {
        private Dictionary<int, List<EventHandler>> listeners = new Dictionary<int, List<EventHandler>>();

        /// <summary>
        /// 添加事件监听
        /// </summary>
        public void AddListener(int eventID, EventHandler listener)
        {
            List<EventHandler> events = null;
            if (listeners.TryGetValue(eventID, out events))
            {
                events.Add(listener);
                return;
            }

            events = new List<EventHandler>();
            events.Add(listener);
            listeners.Add(eventID, events);
        }

        /// <summary>
        /// 
        /// </summary>
        public void AddListener<T>(T eventID, EventHandler listener) where T : Enum
        {
            List<EventHandler> events = null;
            int id = Convert.ToInt32(eventID);
            if (listeners.TryGetValue(id, out events))
            {
                events.Add(listener);
                return;
            }

            events = new List<EventHandler>();
            events.Add(listener);
            listeners.Add(id, events);
        }

        /// <summary>
        /// 通知事件
        /// </summary>
        public void Notification(int eventID, object sender, params object[] args)
        {
            List<EventHandler> events = null;

            if (!listeners.TryGetValue(eventID, out events))
            {
                Log.Print($"[notification]: no event({eventID}) exist.");
                return;
            }

            foreach (var handler in listeners[eventID])
            {
                if (!handler.Equals(null))
                    handler(sender, args);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Notification<T>(T eventID, object sender, params object[] args) where T : Enum
        {
            List<EventHandler> events = null;
            int id = Convert.ToInt32(eventID);
            if (!listeners.TryGetValue(id, out events))
            {
                Log.Print($"[notification]: no event({eventID}) exist.");
                return;
            }

            foreach (var handler in listeners[id])
            {
                if (!handler.Equals(null))
                    handler(sender, args);
            }
        }

        /// <summary>
        /// 移除事件所有监听
        /// </summary>
        public void RemoveEvent(int eventID)
        {
            List<EventHandler> handlers = null;
            if (!listeners.TryGetValue(eventID, out handlers))
                Log.Print($"[remove event]: no event({eventID}) exist.");

            listeners.Remove(eventID);
        }

        /// <summary>
        /// 
        /// </summary>
        public void RemoveEvent<T>(T eventID) where T : Enum
        {
            List<EventHandler> handlers = null;
            int id = Convert.ToInt32(eventID);
            if (!listeners.TryGetValue(id, out handlers))
                Log.Print($"[remove event]: no event({eventID}) exist.");

            listeners.Remove(id);
        }

        /// <summary>
        /// 移除事件中某个监听
        /// </summary>
        public void RemoveEvent(int eventID, EventHandler handler)
        {
            List<EventHandler> handlers = null;
            if (!listeners.TryGetValue(eventID, out handlers))
            {
                Log.Print($"[remove event]: no event({eventID}) exist.");
                return;
            }
            else
            {
                if (handlers.Contains(handler))
                    handlers.Remove(handler);
                else
                    Log.Print($"[remove event]: no EventHandler({handler}) exists in event({eventID}).");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void RemoveEvent<T>(T eventID, EventHandler handler) where T : Enum
        {
            List<EventHandler> handlers = null;
            int id = Convert.ToInt32(eventID);
            if (!listeners.TryGetValue(id, out handlers))
            {
                Log.Print($"[remove event]: no event({eventID}) exist.");
                return;
            }
            else
            {
                if (handlers.Contains(handler))
                    handlers.Remove(handler);
                else
                    Log.Print($"[remove event]: no EventHandler({handler}) exists in event({eventID}).");
            }
        }

        /// <summary>
        /// 移除所有事件中无效的监听
        /// </summary>
        public void RemoveRedundancies()
        {
            Dictionary<int, List<EventHandler>> tmp = new Dictionary<int, List<EventHandler>>();

            foreach (var item in listeners)
            {
                for (int i = item.Value.Count; i <= 0; i--)
                {
                    if (item.Value[i].Equals(null))
                        item.Value.RemoveAt(i);
                }

                if (item.Value.Count > 0)
                    tmp.Add(item.Key, item.Value);
            }

            listeners.Clear();
            listeners = tmp;

        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnAwake()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene s, LoadSceneMode mode)
        {
            RemoveRedundancies();
        }
    }

}
