﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace QuickEye.Utility
{
    [Serializable]
    public class GameObjectPool<T> where T : Component
    {
        [SerializeField]
        private T original;

        [SerializeField]
        private Transform parent;

        [SerializeField]
        private int startSize;

        private readonly Stack<T> available = new Stack<T>();
        private readonly HashSet<T> rented = new HashSet<T>();

        // Empty constructor is needed for proper unity serialization
        public GameObjectPool() { }

        public GameObjectPool(Transform parent, T original, int size)
        {
            this.parent = parent;
            this.original = original;
            startSize = size;
            Initialize();
        }

        public int CountAll => CountRented + CountAvailable;
        public int CountRented => rented.Count;
        public int CountAvailable => available.Count;

        public T Original => original;
        public Transform Parent => parent;

        public void Initialize()
        {
            for (var i = 0; i < startSize; i++)
                available.Push(CreateObject());
        }

        public virtual T Rent()
        {
            var obj = available.Count > 0 ? available.Pop() : CreateObject();
            rented.Add(obj);
            return obj;
        }

        public void Return(T obj)
        {
            if (available.Contains(obj))
            {
                Debug.LogWarning("Trying to return already released object");
                return;
            }

            rented.Remove(obj);

            obj.gameObject.SetActive(false);
            obj.transform.SetParent(parent);

            available.Push(obj);
        }

        public void ReturnAll()
        {
            foreach (var obj in rented)
                Return(obj);
        }

        private T CreateObject()
        {
            var newObject = Object.Instantiate(original, parent);
            newObject.gameObject.SetActive(false);
            return newObject;
        }
    }
}