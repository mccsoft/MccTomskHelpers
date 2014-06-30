using System.Collections.Generic;
using System.Collections.Specialized;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helpers.Collections.Tests
{
    [TestClass]
    public class ChunkyListTests
    {
        [TestMethod]
         public void GetEnumerator_NoItems()
         {
             var chunkyList = new ChunkyList<float>();

             var wasInsideForeach = false;
             foreach (var item in chunkyList)
                 wasInsideForeach = true;

             Assert.IsFalse(wasInsideForeach);
         }

        [TestMethod]
        public void GetEnumerator_MaxBlockSizeOfThreeWithThreeItems()
        {
            var chunkyList = new ChunkyList<float> (3) { 1, 2, 3 };

            var wasInsideForeach = false;
            var iteratedItems = new List<float>();
            foreach (var item in chunkyList)
            {
                wasInsideForeach = true;
                iteratedItems.Add(item);
            }

            Assert.IsTrue(wasInsideForeach);
            CollectionAssert.AreEqual(new List<float> { 1, 2, 3 }, iteratedItems);
        }

        [TestMethod]
        public void GetEnumerator_MaxBlockSizeOfTwoWithThreeItems()
        {
            var chunkyList = new ChunkyList<float>(2) {1,2,3};
            var wasInsideForeach = false;
            var iteratedItems = new List<float>();

            foreach (var item in chunkyList)
            {
                wasInsideForeach = true;
                iteratedItems.Add(item);
            }

            Assert.IsTrue(wasInsideForeach);
            CollectionAssert.AreEqual(new List<float>() { 1, 2, 3 }, iteratedItems);
        }

        [TestMethod]
        public void GetEnumerator_OneBlockWithStartIndexAndEndIndex()
        {
            var chunkyList = new ChunkyList<float>(5) { 1, 2, 3, 4, 5 };
            var wasInsideForeach = false;
            var iteratedItems = new List<float>();

            var enumerator = chunkyList.GetEnumerator(1, 3);
            while (enumerator.MoveNext())
            {
                wasInsideForeach = true;
                iteratedItems.Add(enumerator.Current);
            }

            Assert.IsTrue(wasInsideForeach);
            CollectionAssert.AreEqual(new List<float>() { 2, 3, 4 }, iteratedItems);
        }

        [TestMethod]
        public void GetEnumerator_TwoBlocksWithStartIndexAndEndIndex()
        {
            var chunkyList = new ChunkyList<float>(3) { 1, 2, 3, 4, 5 };
            var wasInsideForeach = false;
            var iteratedItems = new List<float>();

            var enumerator = chunkyList.GetEnumerator(1, 3);
            while (enumerator.MoveNext())
            {
                wasInsideForeach = true;
                iteratedItems.Add(enumerator.Current);
            }

            Assert.IsTrue(wasInsideForeach);
            CollectionAssert.AreEqual(new List<float>() { 2, 3, 4 }, iteratedItems);
        }

        [TestMethod]
        public void CopyTo_WithoutStartIndex_AllElementsAreCopied()
        {
            var chunkyList = new ChunkyList<int>(3) {1, 2, 3, 4, 5};

            var array = new int[5];
            chunkyList.CopyTo(array, 0);

            array.Should().Equal(new int[] { 1, 2, 3, 4, 5 });
        }

        [TestMethod]
        public void CopyTo_WithStartIndex_AllElementsAreCopiedStartingFromStartIndex()
        {
            var chunkyList = new ChunkyList<int>(3) { 1, 2, 3, 4, 5 };

            var array = new int[7];
            chunkyList.CopyTo(array, 2);

            array.Should().Equal(new int[] { 0, 0, 1, 2, 3, 4, 5 });
        }
    }
}