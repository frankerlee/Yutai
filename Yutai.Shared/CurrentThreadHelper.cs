﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Yutai.Shared
{
    public static class CurrentThreadHelper
    {
        public static void DumpThreadInfo()
        {
            var t = Thread.CurrentThread;

            var state = t.GetApartmentState();
            Debug.Print("{0}; Apartment: {1}", t.ManagedThreadId, state);
            Debug.Print("Is pooled thread: " + t.IsThreadPoolThread);
        }
    }
}
