using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCalculator
{
    class Heap<T>
    {
        public int Amount { get; private set; }
        private T[] data;

        public Heap()
        {
            data = new T[20];
            Amount = 0;
        }

        public void Push(T newElem)
        {
            if (Amount == data.Length)
            {
                T[] temp_data = new T[data.Length];
                for (int i = 0; i < data.Length; i++)
                {
                    temp_data[i] = data[i];
                }
                data = new T[data.Length + 20];
                for (int i = 0; i < (data.Length - 20); i++)
                {
                    data[i] = temp_data[i];
                }
            }
            data[Amount] = newElem;
            Amount++;
        }

        public T Pop()
        {
            T toReturn = data[Amount - 1];
            data[Amount - 1] = default(T);
            Amount--;
            return toReturn;
        }

        public void Clear()
        {
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = default(T);
            }
            Amount = 0;
        }

        public override string ToString()
        {
            string toReturn = "";
            for (int i = Amount - 1; i > -1; i--)
            {
                toReturn += (data[i].ToString() + " ");
            }
            return toReturn;
        }

    }
}
