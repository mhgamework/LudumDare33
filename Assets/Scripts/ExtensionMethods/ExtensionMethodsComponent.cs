using System;
using System.Reflection;
using UnityEngine;
using System.Collections;

public static class ExtensionMethodsComponent
{
    /// <summary>
    /// Utilizes C# cheating to copy a component (including private variables).
    /// Might not work for some components (use at own risk). 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="comp"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static T GetCopyOf<T>(this Component comp, T other) where T : Component
    {
        Type type = comp.GetType();
        if (type != other.GetType()) return null; // type mis-match
        BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default | BindingFlags.DeclaredOnly;
        PropertyInfo[] pinfos = type.GetProperties(flags);
        foreach (var pinfo in pinfos)
        {
            if (pinfo.CanWrite)
            {
                try
                {
                    pinfo.SetValue(comp, pinfo.GetValue(other, null), null);
                }
                catch { } // In case of NotImplementedException being thrown. For some reason specifying that exception didn't seem to catch it, so I didn't catch anything specific.
            }
        }
        FieldInfo[] finfos = type.GetFields(flags);
        foreach (var finfo in finfos)
        {
            finfo.SetValue(comp, finfo.GetValue(other));
        }
        return comp as T;
    }


}

