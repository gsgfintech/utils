﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.Teirlinck.Utils
{
    public static class CollectionUtils
    {
        public static bool IsNullOrEmpty<T>(IEnumerable<T> enumerable)
        {
            return ((enumerable == null) || (enumerable.Count<T>() == 0));
        }
    }
}
