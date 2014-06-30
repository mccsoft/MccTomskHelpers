﻿using System.IO;
using System.Reflection;

namespace Helpers.Core
{
    public class PathHelper
    {
        public static string GetAbsolutePathBasedOnExecutingAssemblyLocation(string relativePath)
        {
            return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), relativePath);
        }
    }
}