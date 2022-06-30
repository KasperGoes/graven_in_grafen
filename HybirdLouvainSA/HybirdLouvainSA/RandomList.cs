using System;
using System.Collections.Generic;

namespace HybridLouvainSA
{
    public class RandomList<T>
    {
        public List<T> list; // Actual list

        Dictionary<T, int> indexValue; // Dictionary to store values in the list and its index in the actual list for fast removing

        int last_index; // Denotes index of last element

        public int Count; // Denotes the count of the elements in the list

        Random random;

        public RandomList()
        {
            list = new List<T>();
            last_index = -1;
            Count = 0;
            indexValue = new Dictionary<T, int>();
            random = new Random();
        }

        public void Add(T i)
        {
            if (last_index == list.Count - 1)
            {
                list.Add(i);
            }
            else
            {
                list[last_index + 1] = i;
            }

            indexValue[i] = last_index + 1;
            last_index++;
            Count++;
        }

        public void Remove(T value)
        {
            int index = indexValue[value];

            if (index != last_index)
            {
                swap(index, last_index);
                indexValue[list[index]] = index;
            }

            last_index--;
            Count--;
            indexValue.Remove(value);

        }

        private void swap(int i, int j)
        {
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }

        public dynamic get_random_element()
        {
            if (last_index == -1)
                return false;

            int random_index = random.Next(last_index + 1);
            T value = list[random_index];

            return value;
        }

        public bool contains(T value)
        {
            if (indexValue.ContainsKey(value))
                return true;
            else
                return false;
        }
    }
}

