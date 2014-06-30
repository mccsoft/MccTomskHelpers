using System;

namespace Helpers.Core
{
    public class GcHelper
    {
        /// <summary>
        /// For 99.99999% of cases you don't need to explicitly call garbage collector.
        /// But sometimes you have to, especially if you treat large objects. Large objects are placed in Large Objects Heap (LOH)
        /// which is collected only after 2nd generation of Small Objects Heap (SOH) is collected which is not enough in some cases
        /// To get rid of OutOfMemoryException for this kind of cases we have to call methods placed below.
        /// More info here: http://stackoverflow.com/questions/10016541/garbage-collection-not-happening-even-when-needed
        /// </summary>
        public static void AggressivelyCollect()
        {
            //The reason why GC.Collect is called twice: http://stackoverflow.com/questions/3829928/under-what-circumstances-we-need-to-call-gc-collect-twice
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
        }
    }
}
