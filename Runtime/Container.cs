using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuickEye.Utility
{
    [Serializable]
    public class Container<T> : IList<T>, IReadOnlyList<T> where T : Component
    {
        [SerializeField]
        private T _itemPrefab;

        [SerializeField]
        protected Transform _transform;

        [SerializeField]
        protected List<T> _items = new List<T>();

        public T ItemPrefab => _itemPrefab;

        public virtual Transform Transform
        {
            get => _transform;
            set
            {
                if (_transform == value)
                    return;

                _transform = value;
                _items.ForEach(i => i.transform.SetParent(_transform));
            }
        }

        public int Count => _items.Count;

        public bool IsReadOnly => false;

        public T this[int i]
        {
            get => _items[i];
            set => _items[i] = value;
        }
        public Container() { }
        public Container(Transform transform, T itemPrefab)
        {
            _transform = transform;
            _itemPrefab = itemPrefab;
        }

        public T AddNew()
        {
            var item = GetNewItem();
            Add(item);
            return item;
        }

        public void Add(T item)
        {
            if (item.transform.parent != _transform)
                item.transform.SetParent(_transform);
            _items.Add(item);
        }

        public void Insert(int index, T item)
        {
            if (item.transform.parent != _transform)
                item.transform.SetParent(_transform);
            _items.Insert(index, item);
        }

        public bool Remove(T item)
        {
            if (_items.Remove(item))
            {
                OnRemove(item);
                return true;
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            Remove(_items[index]);
        }

        public void Clear()
        {
            for (int i = _items.Count-1; i >= 0; i--)
            {
                Remove(_items[i]);
            }
        }

        public bool Contains(T item) => _items.Contains(item);

        public void CopyTo(T[] array, int arrayIndex) => _items.CopyTo(array, arrayIndex);

        public int IndexOf(T item) => _items.IndexOf(item);

        public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        protected virtual T GetNewItem()
        {
            return UnityEngine.Object.Instantiate(_itemPrefab, _transform);
        }

        protected virtual void OnRemove(T item)
        {
            UnityEngine.Object.Destroy(item.gameObject);
        }
    }
}