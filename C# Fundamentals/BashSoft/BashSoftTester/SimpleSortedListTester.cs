using System;
using System.Linq;
using System.Collections.Generic;

using NUnit.Framework;

using BashSoft.Contracts;
using BashSoft.DataStructures;

namespace BashSoftTester
{
    public class SimpleSortedListTester
    {
        private ISimpleOrderedBag<string> names;

        [SetUp]
        public void InitializeTestDependencies()
        {
            this.names = new SimpleSortedList<string>();
        }

        [Test]
        public void TestEmptyCtor()
        {
            var expectedListCapacity = 16;
            var expectedListSize = 0;

            Assert.That(this.names.Capacity, Is.EqualTo(expectedListCapacity));
            Assert.That(this.names.Size, Is.EqualTo(expectedListSize));
        }

        [Test]
        [TestCase(20)]
        public void TestCtorWithInitialCapacity(int listSize)
        {
            var expectedListCapacity = 20;
            var expectedListSize = 0;

            this.names = new SimpleSortedList<string>(listSize);
            Assert.That(this.names.Capacity, Is.EqualTo(expectedListCapacity));
            Assert.That(this.names.Size, Is.EqualTo(expectedListSize));
        }

        [Test]
        [TestCase(30)]
        public void TestCtorWithAllParams(int listSize)
        {
            var expectedListCapacity = 30;
            var expectedListSize = 0;

            this.names = new SimpleSortedList<string>(StringComparer.OrdinalIgnoreCase, listSize);
            Assert.That(this.names.Capacity, Is.EqualTo(30));
            Assert.That(this.names.Size, Is.EqualTo(expectedListSize));
        }

        [Test]
        public void TestAddIncreasesSize()
        {
            var expectedListSize = 1;

            this.names.Add("Ivan");
            Assert.That(this.names.Size, Is.EqualTo(expectedListSize));
        }

        [Test]
        public void TestAddNullThrowsException()
        {
            Assert.That(() => this.names.Add(null), Throws.ArgumentNullException);
        }

        [Test]
        public void TestAddUnsortedDataIsHeldSorted()
        {
            var firstExpectedElement = "Balkan";
            var lastExpectedElement = "Rosen";

            this.names.Add("Rosen");
            this.names.Add("Georgi");
            this.names.Add("Balkan");

            Assert.That(names.First(), Is.EqualTo(firstExpectedElement));
            Assert.That(names.Last(), Is.EqualTo(lastExpectedElement));
        }

        [Test]
        public void TestAddingMoreThanInitialCapacity()
        {
            var expectedListSize = 20;
            var defaultListCapacity = 16;

            for (var i = 0; i < 20; i++)
            {
                this.names.Add(i.ToString());
            }

            Assert.That(this.names.Size, Is.EqualTo(expectedListSize));
            Assert.That(this.names.Capacity, Is.Not.EqualTo(defaultListCapacity));
        }

        [Test]
        public void TestAddingAllFromCollectionIncreasesSize()
        {
            var expectedListSize = 2;

            var tempList = new List<string>() { "Ivan", "Maria" };

            this.names.AddAll(tempList);

            Assert.That(this.names.Size, Is.EqualTo(expectedListSize));
        }

        [Test]
        public void TestAddingFromNullThrowsException()
        {
            Assert.That(() => this.names.AddAll(null), Throws.ArgumentNullException);
        }

        [Test]
        public void TestAddAllKeepsSorted()
        {
            var firstExpectedElement = "Balkan";
            var lastExpectedElement = "Rosen";

            var tempList = new List<string>() { "Rosen", "Georgi", "Balkan" };

            this.names.AddAll(tempList);

            Assert.That(names.First(), Is.EqualTo(firstExpectedElement));
            Assert.That(names.Last(), Is.EqualTo(lastExpectedElement));
        }

        [Test]
        public void TestRemoveValidElementDecreasesSize()
        {
            var expectedListSize = 0;

            this.names.Add("Test");

            this.names.Remove("Test");

            Assert.That(this.names.Size, Is.EqualTo(expectedListSize));
        }

        [Test]
        public void TestRemoveMultipleElementsDecreasesSize()
        {
            var expectedListSize = 1;

            this.names.Add("Ivan");
            this.names.Add("Maria");
            this.names.Add("Georgi");

            this.names.Remove("Ivan");
            this.names.Remove("Georgi");

            Assert.That(this.names.Size, Is.EqualTo(expectedListSize));
        }

        [Test]
        public void TestRemoveValidElementRemovesSelectedOne()
        {
            var elementToRemove = "Maria";
            var expectedResult = false;

            this.names.Add("Ivan");
            this.names.Add("Maria");
            this.names.Add("Georgi");

            this.names.Remove(elementToRemove);

            Assert.That(() => this.names.Contains(elementToRemove), Is.EqualTo(expectedResult));
        }

        [Test]
        public void TestRemovingNullThrowsException()
        {
            Assert.That(() => this.names.Remove(null), Throws.ArgumentNullException);
        }

        [Test]
        [TestCase("Ivan")]
        public void TestAttemptToRemoveNullElementThrowsException(string elementToRemove)
        {
            Assert.That(() => this.names.Remove(elementToRemove), Throws.InvalidOperationException);
        }

        [Test]
        [TestCase("Ivan", "Maria")]
        public void TestJoinWithNull(params string[] elementsToAdd)
        {
            this.names.AddAll(elementsToAdd);

            Assert.That(() => this.names.JoinWith(null), Throws.ArgumentNullException);
        }

        [Test]
        [TestCase("Ivan", "Maria", "Georgi")]
        public void TestJoinWorksFine(params string[] elementsToAdd)
        {
            Assert.That(() => this.names.AddAll(elementsToAdd), Throws.Nothing);
        }
    }
}
