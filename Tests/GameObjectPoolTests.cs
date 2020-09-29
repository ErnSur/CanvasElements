using NUnit.Framework;
using UnityEngine;

namespace QuickEye.Utility.Tests
{
    public class GameObjectPoolTests
    {
        private Transform original;
        private Transform parent;
        private GameObjectPool<Transform> pool;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            original = new GameObject("Prefab").transform;
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Object.Destroy(original.gameObject);
            pool = null;
        }

        [SetUp]
        public void Setup()
        {
            parent = new GameObject("Container").transform;
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(parent.gameObject);
            pool = null;
        }

        [Test]
        public void Should_ReturnInactiveGameObject_WhenRent()
        {
            pool = new GameObjectPool<Transform>(null, original, 0);

            var instance = pool.Rent();

            Assert.IsNotNull(instance);
            Assert.IsFalse(instance.gameObject.activeSelf);
        }

        [TestCase(0)]
        [TestCase(5)]
        public void Should_CreateInactiveInstances_When_CtorWithStartSize(int startSize)
        {
            pool = new GameObjectPool<Transform>(null, original, startSize);

            Assert.AreEqual(startSize, pool.CountAvailable);
        }

        [TestCase(0)]
        [TestCase(5)]
        public void Should_PoolObjectsBeContainerChildren(int startSize)
        {
            pool = new GameObjectPool<Transform>(parent, original, startSize);

            Assert.AreEqual(startSize, parent.childCount);
        }

        [TestCase(0, 5)]
        [TestCase(0, 0)]
        [TestCase(5, 3)]
        public void Should_GetRentedCount(int startSize, int rentCount)
        {
            pool = new GameObjectPool<Transform>(parent, original, startSize);

            for (var i = 0; i < rentCount; i++)
                pool.Rent();

            Assert.AreEqual(rentCount, pool.CountRented);
        }

        [TestCase(0, 5)]
        [TestCase(2, 1)]
        [TestCase(5, 3)]
        public void Should_NotCreateNewObject_WhenUnusedObjectExists(int startSize, int rentCount)
        {
            pool = new GameObjectPool<Transform>(parent, original, startSize);

            for (var i = 0; i < rentCount; i++)
                pool.Rent();


            Assert.AreEqual(Mathf.Max(startSize, rentCount), pool.CountAll);
        }
    }
}