﻿using bayonet.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bayonet.Core.Common
{
    public static class BayonetHelper
    {
        public static string FormatStoryType(string typeString)
        {
            return typeString.ToLower();
        }

        public static bool ValidateStoryType(string typeString)
        {
            return Enum.GetNames(typeof(StoryType)).Contains(typeString);
        }

        public static bool ValidateId(string id)
        {
            if(String.IsNullOrEmpty(id) || String.IsNullOrWhiteSpace(id))
            {
                return false;
            }
            return true;
        }
    }
}