using System;
using System.Text;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using BashSoft.Contracts;

namespace BashSoft.DataStructures
{
    public class SimpleSortedList<T> : ISimpleOrderedBag<T> where T : IComparable<T>
    {
        private const int DefaultSize = 16;

        private T[] innerCollection;
        private int size;
        private int capacity;
        private IComparer<T> comparison;

        public SimpleSortedList()
        {
            this.innerCollection = new T[DefaultSize];
            this.comparison = Comparer<T>.Create((x, y) => x.CompareTo(y));
        }

        public SimpleSortedList(IComparer<T> comparer, int capacity)
            : this()
        {
            this.innerCollection = new T[capacity];
            this.comparison = comparer;
        }

        public SimpleSortedList(int capacity)
            : this(Comparer<T>.Create((x, y) => x.CompareTo(y)), capacity) { }

        public int Size => this.size;

        public int Capacity
        {
            get
            {
                return this.innerCollection.Length;
            }
        }

        public void Add(T element)
        {
            if (element == null)
            {
                throw new ArgumentNullException();
            }

            if (this.innerCollection.Length <= this.Size)
            {
                this.Resize();
            }

            this.innerCollection[size] = element;
            this.size++;
            Array.Sort(this.innerCollection, 0, this.size, this.comparison);
        }

        public void AddAll(ICollection<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException();
            }

            if (this.Size + collection.Count >= this.innerCollection.Length)
            {
                this.MultiResize(collection);
            }

            foreach (var element in collection)
            {
                this.innerCollection[this.Size] = element;
                this.size++;
            }

            Array.Sort(this.innerCollection, 0, this.size, this.comparison);
        }

        public bool Remove(T element)
        {
            if (element == null)
            {
                throw new ArgumentNullException();
            }

            if (!this.innerCollection.Contains(element))
            {
                throw new InvalidOperationException("The element is not present within the collection!");
            }

            var hasBeenRemoved = false;
            var indexOfRemovedElement = 0;

            for (var i = 0; i < this.Size; i++)
            {
                if (this.innerCollection[i].Equals(element))
                {
                    indexOfRemovedElement = i;
                    this.innerCollection[i] = default(T);
                    hasBeenRemoved = true;
                    break;
                }
            }

            if (hasBeenRemoved)
            {
                for (var i = indexOfRemovedElement; i < this.Size - 1; i++)
                {
                    this.innerCollection[i] = this.innerCollection[i + 1];
                }

                this.innerCollection[this.size - 1] = default(T);
                this.size--;
            }

            return hasBeenRemoved;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < this.Size; i++)
            {
                yield return this.innerCollection[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public string JoinWith(string joiner)
        {
            if (joiner == null)
            {
                throw new ArgumentNullException();
            }

            var builder = new StringBuilder();
            foreach (var element in this)
            {
                builder.Append(element);
                builder.Append(joiner);
            }

            builder.Remove(builder.Length - joiner.Length, joiner.Length);
            return builder.ToString();
        }

        private void MultiResize(ICollection<T> collection)
        {
            var newSize = this.innerCollection.Length * 2;

            while (this.Size + collection.Count >= newSize)
            {
                newSize *= 2;
            }

            T[] newCollection = new T[newSize];
            Array.Copy(this.innerCollection, newCollection, this.size);
            this.innerCollection = newCollection;
        }

        private void InitializeInnerCollection(int capacity)
        {
            if (size < 0)
            {
                throw new ArgumentException("Capacity cannot be negative!");
            }

            this.innerCollection = new T[capacity];
        }

        private void Resize()
        {
            T[] newCollection = new T[this.size * 2];
            Array.Copy(this.innerCollection, newCollection, this.Size);
            this.innerCollection = newCollection;
        }
    }
}
