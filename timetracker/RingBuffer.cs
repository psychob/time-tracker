using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace timetracker
{
    internal class RingBuffer<T> : IEnumerable<T> where T : struct
    {
        List<T> Buffer;
        int Limit;

        public int Count
        {
            get
            {
                return Buffer.Count;
            }
        }

        public RingBuffer(int limit)
        {
            Limit = limit;
            Buffer = new List<T>(limit);
        }

        public Nullable<T> Add(T value)
        {
            if (Buffer.Count == Limit)
            {
                // remove first, and then append
                T v = Buffer[0];

                Buffer.RemoveAt(0);
                Buffer.Add(value);

                return v;
            }
            else
            {
                Buffer.Add(value);
                return null;
            }
        }

        public bool Top(out T value)
        {
            if (Buffer.Count == 0)
            {
                value = default(T);
                return false;
            }
            else
            {
                value = Buffer[Buffer.Count - 1];
                return true;
            }
        }

        public bool Bottom(out T value)
        {
            if (Buffer.Count == 0)
            {
                value = default(T);
                return false;
            }
            else
            {
                value = Buffer[0];
                return true;
            }
        }

        public int RemoveIf(Predicate<T> p)
        {
            int ret = 0;

            for (var it = 0; it < Buffer.Count;)
            {
                if (p(Buffer[it]))
                {
                    Buffer.RemoveAt(it);
                    ret++;
                }
                else
                {
                    it++;
                }
            }

            return ret;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)Buffer).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)Buffer).GetEnumerator();
        }
    }
}
