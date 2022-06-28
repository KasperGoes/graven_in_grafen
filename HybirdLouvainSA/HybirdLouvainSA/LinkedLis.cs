using System;
using System.Collections.Generic;

namespace HybridLouvainSA
{
    public class LinkedList
    {
        public Elem head;
        public Elem tail;
        public Elem current_element;

        public LinkedList()
        {
            // Initialize empty list
            head = new Elem(int.MaxValue);
            tail = new Elem(int.MinValue);
            current_element = head;
            head.next = tail;
            tail.prev = head;
        }

        public void Add(int i)
        {
            // Add new element to list
            Elem elem = new Elem(i);
            elem.next = current_element.next;
            elem.prev = current_element;
            current_element.next.prev = elem;
            current_element.next = elem;
            current_element = elem;
        }

        public void Remove()
        {
            // Remove current_element element from list
            if (current_element != head) //&& current_element != tail)
            {
                current_element.prev.next = current_element.next;
                current_element.next.prev = current_element.prev;
                current_element = current_element.prev;
            }
        }

        public void add_linked_end(LinkedList list)
        {
            current_element.next = list.head.next;
            list.head.next.prev = current_element;
            current_element = list.tail.prev;
            this.tail = list.tail;
        }

        public Dictionary <int,int> add_to_partition(int community, Dictionary<int,int> partition)
        {
            Elem start = head.next;

            while(start != tail)
            {
                partition.Add(start.value, community);
                start = start.next;
            }

            return partition;
        }

        public class Elem
        {
            public int value;
            public Elem next;
            public Elem prev;

            public Elem(int value)
            {
                this.value = value;
            }
        }
    }
}


