using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace QuickEye.Utility
{
    [Serializable]
    public class Container<T> : IList<T>, IReadOnlyList<T> where T : Component
    {
        [SerializeField]
        private T itemPrefab;

        [SerializeField]
        protected Transform transform;

        [SerializeField]
        protected List<T> items = new List<T>();

        public Container() { }

        public Container(Transform transform, T itemPrefab)
        {
            this.transform = transform;
            this.itemPrefab = itemPrefab;
        }

        public T ItemPrefab => itemPrefab;

        public virtual Transform Transform
        {
            get => transform;
            set
            {
                if (transform == value)
                    return;

                transform = value;
                items.ForEach(i => i.transform.SetParent(transform));
            }
        }

        public int Count => items.Count;

        public bool IsReadOnly => false;

        public T this[int i]
        {
            get => items[i];
            set => items[i] = value;
        }

        public void Add(T item)
        {
            if (item.transform.parent != transform)
                item.transform.SetParent(transform);
            items.Add(item);
        }

        public void Insert(int index, T item)
        {
            if (item.transform.parent != transform)
                item.transform.SetParent(transform);
            items.Insert(index, item);
        }

        public bool Remove(T item)
        {
            if (items.Remove(item))
            {
                OnRemove(item);
                return true;
            }

            return false;
        }

        public void RemoveAt(int index)
        {
            Remove(items[index]);
        }

        public void Clear()
        {
            for (var i = items.Count - 1; i >= 0; i--) Remove(items[i]);
        }

        public bool Contains(T item)
        {
            return items.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            items.CopyTo(array, arrayIndex);
        }

        public int IndexOf(T item)
        {
            return items.IndexOf(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public T AddNew()
        {
            var item = GetNewItem();
            Add(item);
            return item;
        }

        protected virtual T GetNewItem()
        {
            return Object.Instantiate(itemPrefab, transform);
        }

        protected virtual void OnRemove(T item)
        {
            Object.Destroy(item.gameObject);
        }
    }
}