using NUnit.Framework;
using UnityEngine;

namespace QuickEye.Utility.Tests
{
    public class GameObjectPoolTests
    {
        private Transform _original;
        private Transform _parent;
        private GameObjectPool<Transform> _pool;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _original = new GameObject("Prefab").transform;
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Object.Destroy(_original.gameObject);
            _pool = null;
        }

        [SetUp]
        public void Setup()
        {
            _parent = new GameObject("Container").transform;
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(_parent.gameObject);
            _pool = null;
        }

        [Test]
        public void Should_ReturnInactiveGameObject_WhenRent()
        {
            _pool = new GameObjectPool<Transform>(null, _original, 0);

            var instance = _pool.Rent();

            Assert.IsNotNull(instance);
            Assert.IsFalse(instance.gameObject.activeSelf);
        }

        [TestCase(0)]
        [TestCase(5)]
        public void Should_CreateInactiveInstances_When_CtorWithStartSize(int startSize)
        {
            _pool = new GameObjectPool<Transform>(null, _original, startSize);

            Assert.AreEqual(startSize, _pool.CountAvailable);
        }

        [TestCase(0)]
        [TestCase(5)]
        public void Should_PoolObjectsBeContainerChildren(int startSize)
        {
            _pool = new GameObjectPool<Transform>(_parent, _original, startSize);

            Assert.AreEqual(startSize, _parent.childCount);
        }

        [TestCase(0, 5)]
        [TestCase(0, 0)]
        [TestCase(5, 3)]
        public void Should_GetRentedCount(int startSize, int rentCount)
        {
            _pool = new GameObjectPool<Transform>(_parent, _original, startSize);

            for (int i = 0; i < rentCount; i++)
                _pool.Rent();

            Assert.AreEqual(rentCount, _pool.CountRented);
        }

        [TestCase(0, 5)]
        [TestCase(2, 1)]
        [TestCase(5, 3)]
        public void Should_NotCreateNewObject_WhenUnusedObjectExists(int startSize, int rentCount)
        {
            _pool = new GameObjectPool<Transform>(_parent, _original, startSize);

            for (int i = 0; i < rentCount; i++)
                _pool.Rent();


            Assert.AreEqual(Mathf.Max(startSize, rentCount), _pool.CountAll);
        }
    }
}
