using UnityEditor;
using UnityEngine;

namespace MLEventSystem.Editor
{
    internal class EventSystemEditor
    {
        [MenuItem("GameObject/MLTools/EventSystem/EventManager", false, 12)]
        private static void CreateInstance()
        {
            if (GameObject.FindObjectOfType<EventManager>())
            {
                Log.PrintWarning("EventManager already exist. It's a singeton script.");
                return;
            }

            GameObject go = new GameObject("ML Event Manager", typeof(EventManager));
        }

    }
}
