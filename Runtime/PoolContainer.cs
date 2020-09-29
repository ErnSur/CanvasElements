using System;
using UnityEngine;

namespace QuickEye.Utility
{
    [Serializable]
    public class PoolContainer<T> : Container<T>, ISerializationCallbackReceiver
        where T : Component
    {
        [NonSerialized]
        public GameObjectPool<T> pool;

        public PoolContainer() { }
        public PoolContainer(Transform transform, T itemPrefab) : base(transform, itemPrefab)
        {
            pool = new GameObjectPool<T>(transform, itemPrefab, 0);
        }

        protected override T GetNewItem()
        {
            var item = pool.Rent();
            item.gameObject.SetActive(true);
            return item;
        }

        protected override void OnRemove(T item)
        {
            pool.Return(item);
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            pool = new GameObjectPool<T>(Transform, ItemPrefab, 0);
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize() { }
    }
}
