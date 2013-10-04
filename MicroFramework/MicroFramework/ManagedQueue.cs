using System;
using Microsoft.SPOT;
using System.Collections;

namespace MicroFramework
{
    class ManagedQueue
    {
        private Queue queue = null;
        private object newest = null;

        public ManagedQueue()
        {
            queue = new Queue();
        }

        public object Add(object obj)
        {
            object tempObj = null;

            lock (queue.SyncRoot)
            {
                if (queue.Count == 6)
                    tempObj = queue.Dequeue();

                newest = obj;
                queue.Enqueue(obj);
            }

            return tempObj;
        }

        public object Oldest()
        {
            lock (queue.SyncRoot)
            {
                return queue.Peek();
            }
        }

        public object Newest()
        {
            lock (queue.SyncRoot)
            {
                return newest;
            }
        }

        public object GetQueue()
        {
            lock (queue.SyncRoot)
            {
                return queue.Clone();
            }
        }
    }
}
