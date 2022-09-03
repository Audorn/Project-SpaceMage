using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Extensions
{
    public static class EnumExtensions
    {
        public static T Previous<T>(this T src, bool wrap = true) where T : struct
        {
            if (!typeof(T).IsEnum) throw new ArgumentException(String.Format("Argument {0} is not an Enum", typeof(T).FullName));

            T[] Arr = (T[])Enum.GetValues(src.GetType());
            int i = Array.IndexOf<T>(Arr, src) - 1;
            return (wrap && i == -1) ? Arr[Arr.Length - 1] : 
                   (i == -1) ? Arr[0] : Arr[i];
        }

        public static T Next<T>(this T src, bool wrap = true) where T : struct
        {
            if (!typeof(T).IsEnum) throw new ArgumentException(String.Format("Argument {0} is not an Enum", typeof(T).FullName));

            T[] Arr = (T[])Enum.GetValues(src.GetType());
            int i = Array.IndexOf<T>(Arr, src) + 1;
            return (wrap && i == Arr.Length) ? Arr[0] : 
                   (i == Arr.Length) ? Arr[Arr.Length - 1] : Arr[i];
        }
    }
}