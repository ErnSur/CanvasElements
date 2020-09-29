using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace QuickEye.Utility.Tests
{
    public class ContainerTests
    {
        private Container<Transform> container;
        private Transform containerTransform;
        private Transform prefab;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            prefab = new GameObject("Prefab").transform;
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Object.Destroy(prefab.gameObject);
        }

        [SetUp]
        public void Setup()
        {
            containerTransform = new GameObject("Container").transform;
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(containerTransform.gameObject);
        }

        [Test]
        public void AddNew_ReturnsContainerChild()
        {
            container = new Container<Transform>(containerTransform, prefab);

            var child = container.AddNew();

            Assert.AreEqual(container.Transform, child.transform.parent);
        }

        [Test]
        public void Add_SetsNewItemParentToContainerTransform()
        {
            container = new Container<Transform>(containerTransform, prefab);
            var child = new GameObject().transform;

            container.Add(child);

            Assert.AreEqual(container.Transform, child.transform.parent);
        }

        [UnityTest]
        [TestCase(0, ExpectedResult = null)]
        [TestCase(1, ExpectedResult = null)]
        [TestCase(3, ExpectedResult = null)]
        public IEnumerator Clear_DestroysAllContainerItems(int elementCount)
        {
            container = new Container<Transform>(containerTransform, prefab);

            for (var i = 0; i < elementCount; i++) container.AddNew();
            container.Clear();
            yield return null;

            Assert.AreEqual(0, containerTransform.childCount);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(3)]
        public void Clear_ReturnsAllItemsToPool(int elementCount)
        {
            container = new PoolContainer<Transform>(containerTransform, prefab);

            for (var i = 0; i < elementCount; i++)
            {
                var item = container.AddNew();
            }

            container.Clear();

            var children = GetTransformChildren(containerTransform);
            var allPoolItemsAreDeisabled =
                children.All(t => t.gameObject.activeSelf == false);

            for (var i = 0; i < containerTransform.childCount; i++)
            {
                var child = containerTransform.GetChild(i);
                Debug.Log($"c {i}");
                Assert.IsFalse(child.gameObject.activeSelf);
            }

            //Assert.IsTrue(allPoolItemsAreDeisabled);
        }

        [UnityTest]
        [TestCase(3, ExpectedResult = null)]
        [TestCase(2, ExpectedResult = null)]
        public IEnumerator Clear_DestroyAll(int c)
        {
            var elementCount = 3;
            container = new Container<Transform>(containerTransform, prefab);

            for (var i = 0; i < elementCount; i++) container.AddNew();
            container.Clear();
            yield return null;
            Assert.AreEqual(0, containerTransform.childCount);
        }

        private IEnumerable<Transform> GetTransformChildren(Transform parent)
        {
            foreach (Transform transform in parent) yield return transform;
        }
    }
}