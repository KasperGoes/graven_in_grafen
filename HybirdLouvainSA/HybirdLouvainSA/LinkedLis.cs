using System;
using System.Collections.Generic;

namespace HybridLouvainSA
{
    public class LinkedList
    {
        public Elem head;
        public Elem tail;
        public Elem current_element;

        // Initialize empty list
        public LinkedList()
        {
            
            head = new Elem(int.MaxValue);
            tail = new Elem(int.MinValue);
            current_element = head;
            head.next = tail;
            tail.prev = head;
        }

        // Add new element to list
        public void Add(int i)
        {
            
            Elem elem = new Elem(i);
            elem.next = current_element.next;
            elem.prev = current_element;
            current_element.next.prev = elem;
            current_element.next = elem;
            current_element = elem;
        }

        // Remove current_element element from list
        public void Remove()
        {
            if (current_element != head)
            {
                current_element.prev.next = current_element.next;
                current_element.next.prev = current_element.prev;
                current_element = current_element.prev;
            }
        }

        // Adds the given list at the end of the linked list
        public void add_linkedlist_end(LinkedList list)
        {
            current_element.next = list.head.next;
            list.head.next.prev = current_element;
            current_element = list.tail.prev;
            this.tail = list.tail;
        }

        // Adds all elements to a partition dictionary
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


