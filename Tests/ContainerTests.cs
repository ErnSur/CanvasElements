using NUnit.Framework;
using QuickEye.Utility;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TestTools;

namespace QuickEye.Utility.Tests
{
    public class ContainerTests
    {
        private Transform _prefab;
        private Transform _containerTransform;
        private Container<Transform> _container;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _prefab = new GameObject("Prefab").transform;
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Object.Destroy(_prefab.gameObject);
        }

        [SetUp]
        public void Setup()
        {
            _containerTransform = new GameObject("Container").transform;
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(_containerTransform.gameObject);
        }

        [Test]
        public void AddNew_ReturnsContainerChild()
        {
            _container = new Container<Transform>(_containerTransform, _prefab);

            var child = _container.AddNew();

            Assert.AreEqual(_container.Transform, child.transform.parent);
        }

        [Test]
        public void Add_SetsNewItemParentToContainerTransform()
        {
            _container = new Container<Transform>(_containerTransform, _prefab);
            var child = new GameObject().transform;

            _container.Add(child);

            Assert.AreEqual(_container.Transform, child.transform.parent);
        }

        [UnityTest]
        [TestCase(0, ExpectedResult = null)]
        [TestCase(1, ExpectedResult = null)]
        [TestCase(3, ExpectedResult = null)]
        public IEnumerator Clear_DestroysAllContainerItems(int elementCount)
        {
            _container = new Container<Transform>(_containerTransform, _prefab);

            for (int i = 0; i < elementCount; i++)
            {
                _container.AddNew();
            }
            _container.Clear();
            yield return null;

            Assert.AreEqual(0, _containerTransform.childCount);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(3)]
        public void Clear_ReturnsAllItemsToPool(int elementCount)
        {
            _container = new PoolContainer<Transform>(_containerTransform, _prefab);

            for (int i = 0; i < elementCount; i++)
            {
                var item = _container.AddNew();
            }
            _container.Clear();

            var children = GetTransformChildren(_containerTransform);
            var allPoolItemsAreDeisabled =
                children.All(t => t.gameObject.activeSelf == false);

            for (int i = 0; i < _containerTransform.childCount; i++)
            {
                var child = _containerTransform.GetChild(i);
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
            _container = new Container<Transform>(_containerTransform, _prefab);

            for (int i = 0; i < elementCount; i++)
            {
                _container.AddNew();
            }
            _container.Clear();
            yield return null;
            Assert.AreEqual(0, _containerTransform.childCount);
        }

        private IEnumerable<Transform> GetTransformChildren(Transform parent)
        {
            foreach (Transform transform in parent)
            {
                yield return transform;
            }
        }
    }
}
