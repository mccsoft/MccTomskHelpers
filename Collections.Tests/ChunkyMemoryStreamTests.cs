using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helpers.Collections.Tests
{
    [TestClass]
    public class ChunkyMemoryStreamTests
    {
        private static byte[] GenerateBinaryArray()
        {
            var array = new byte[256];
            for (int i = 0; i < 256; i++)
                array[i] = (byte) i;
            return array;
        }

        [TestMethod]
        public void Length_EmptyStream_ReturnsZero()
        {
            var chunkyStream = new ChunkyMemoryStream();

            Assert.AreEqual(chunkyStream.Length, 0);
        }

        [TestMethod]
        public void Length_ContainsBytesArrayOf10Elements_Return10()
        {
            var chunkyStream = new ChunkyMemoryStream();

            chunkyStream.Write(new byte[10], 0, 10);

            Assert.AreEqual(chunkyStream.Length, 10);
        }

        [TestMethod]
        public void WriteRead_PassArray_ReadsSameArray()
        {
            var chunkyStream = new ChunkyMemoryStream();
            var input = GenerateBinaryArray();

            chunkyStream.Write(input, 0, input.Length);
            chunkyStream.Position = 0;
            byte[] output = new byte[chunkyStream.Length];
            chunkyStream.Read(output, 0, (int)chunkyStream.Length);

            CollectionAssert.AreEqual(input, output);
        }

        [TestMethod]
        public void Write_PassArrayWithOffset_ReturnArrayWithCorrectElements()
        {
            var chunkyStream = new ChunkyMemoryStream();
            var input = GenerateBinaryArray();

            var expected = new byte[input.Length - 2];
            System.Array.Copy(input, 1, expected, 0, input.Length - 2);

            chunkyStream.Write(input, 1, input.Length-2);
            chunkyStream.Position = 0;
            var output = new byte[chunkyStream.Length];
            chunkyStream.Read(output, 0, (int)chunkyStream.Length);
            
            CollectionAssert.AreEqual(expected, output);
        }

        [TestMethod]
        public void CanAllocate1KBlocks()
        {
            var chunkyStream = new ChunkyMemoryStream();

            for (int i = 0; i < 1000; i++)
                chunkyStream.Write(new byte[65536], 0, 65536);

            Assert.AreEqual(chunkyStream.Length, 65536000);
        }

        [TestMethod]
        public void ToArray_PassInputArray_ReturnArrayWhichIsEqualToInputArray()
        {
            var inputArray = GenerateBinaryArray();
            var chunkyStream = new ChunkyMemoryStream(inputArray);

            var resultArray = chunkyStream.ToArray();

            CollectionAssert.AreEqual(inputArray, resultArray);
        }
    }
}