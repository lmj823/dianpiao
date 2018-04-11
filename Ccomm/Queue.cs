using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
namespace ebankGateway
{
    interface IMethodRequest
    {
        void Call();
    }

    public class ActiveQueue<T>
    {        
        const int maxCount = 10000; 
       

        public int Count
        {
            get
            {
                return queue.Count;
            }
        }

        Queue<T> queue = new Queue<T>();
        public void Enqueue(T item)
        {
            lock (this)
            {
                if (queue.Count >= maxCount)
                {
                    Monitor.Wait(this);
                }
                queue.Enqueue(item);
                Monitor.PulseAll(this);
            }
        }

        public T Dequeue()
        {
            lock (this)
            {
                if (queue.Count == 0)
                {
                    Monitor.Wait(this);
                }
                T item = queue.Dequeue();
                Monitor.PulseAll(this);
                return item;
            }
        }
    }



}
