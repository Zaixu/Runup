using System;
using Microsoft.SPOT;
using System.Collections;

namespace MicroFramework
{
    /// <summary>
    /// Class Managing a Queue with certain number of entrys
    /// </summary>
    public class ManagedQueue
    {
        private Queue queue = null;
        private object newest = null;

        /// <summary>
        /// Constructor - Creates an ordinary queue to manage
        /// </summary>
        public ManagedQueue()
        {
            queue = new Queue();
        }

        /// <summary>
        /// Adds an object to the queue
        /// </summary>
        /// <param name="obj">Object to be added</param>
        /// <returns>Returns the object being pushed out</returns>
        public object Add(object obj)
        {
            object tempObj = null;
            //Lock queue for making changes
            lock (queue.SyncRoot)
            {
                //If number of queue is at limit, remove oldest
                if (queue.Count == 5)
                    tempObj = queue.Dequeue();
                //Assign newest variable to the object being added
                newest = obj;
                //Add newest object to queue
                queue.Enqueue(obj);
            }
            //Return pushed out 
            return tempObj;
        }

        /// <summary>
        /// Function that returns the oldest data point in queue
        /// </summary>
        /// <returns>Returns queue object</returns>
        public object Oldest()
        {
            //Lock queue
            lock (queue.SyncRoot)
            {
                //Return oldest data in queue
                return queue.Peek();
            }
        }

        /// <summary>
        /// Function that returns the newest data point in queue
        /// </summary>
        /// <returns>Returns queue object</returns>
        public object Newest()
        {
            //Lock queue
            lock (queue.SyncRoot)
            {
                //Return newest object
                return newest;
            }
        }

        /// <summary>
        /// Function that returns a clone of the queue.
        /// </summary>
        /// <returns></returns>
        public object GetQueue()
        {
            //Lock queue
            lock (queue.SyncRoot)
            {
                //Return clone of queue
                return queue.Clone();
            }
        }
    }
}
