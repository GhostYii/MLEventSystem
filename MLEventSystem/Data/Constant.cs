﻿namespace MLEventSystem
{
    internal class Constance
    {
        public const string DEBUG_NAME = "[MLEventSystem]";
    }

    /// <summary>
    /// 事件处理委托
    /// </summary>
    public delegate void EventHandler(object sender, params object[] args);

    
}
