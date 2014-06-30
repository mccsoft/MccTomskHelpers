using System;
using System.Collections;
using System.Collections.Generic;

namespace Helpers.Collections
{
    public class ChunkyList<T> : IList<T>
    {
        public ChunkyList()
        {
            MaxBlockSize = 65536;
        }

        public ChunkyList(int maxBlockSize)
        {
            MaxBlockSize = maxBlockSize;
        }

        private List<T[]> _blocks = new List<T[]>();

        public int Count { get; private set; }

        public int MaxBlockSize { get; private set; }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return GetEnumeratorCore(0, Count - 1);
        }
        
        public IEnumerator<T> GetEnumerator(int startIndex, int endIndex)
        {
            return GetEnumeratorCore(startIndex, endIndex);
        }

        private IEnumerator<T> GetEnumeratorCore(int startIndex, int endIndex)
        {
            if (Count == 0)
                yield break;

            var index = startIndex;

            var startBlockIndex = GetBlockIndex(startIndex);

            var blocksCount = _blocks.Count;
            var isFirstEnumeratedBlock = true;
            for (var blockIndex = startBlockIndex; blockIndex <= blocksCount; blockIndex++)
            {
                var block = _blocks[blockIndex];

                long indexInsideBlock = 0;
                if (isFirstEnumeratedBlock)
                {
                    indexInsideBlock = GetIndexInsideBlock(startIndex);
                    isFirstEnumeratedBlock = false;
                }

                var blockLength = block.Length;
                while (indexInsideBlock < blockLength && index <= endIndex)
                {
                    yield return block[indexInsideBlock];

                    indexInsideBlock++;
                    index++;
                }

                if (index > endIndex)
                    yield break;
            }
        }

        public void Add(T item)
        {
            var indexInsideBlock = GetIndexInsideBlock(Count);
            if (indexInsideBlock == 0)
                _blocks.Add(new T[1]);
            else
            {
                var lastBlockIndex = _blocks.Count - 1;
                var lastBlock = _blocks[lastBlockIndex];
                if (indexInsideBlock >= lastBlock.Length)
                {
                    var newBlockSize = lastBlock.Length * 2;
                    if (newBlockSize >= MaxBlockSize)
                        newBlockSize = MaxBlockSize;

                    _blocks[lastBlockIndex] = new T[newBlockSize];
                    Array.Copy(lastBlock, _blocks[lastBlockIndex], lastBlock.Length);
                }
            }

            _blocks[GetBlockIndex(Count)][indexInsideBlock] = item;

            Count++;
        }

        public void AddRange(IEnumerable<T> items)
        {
            foreach (var item in items)
                Add(item);
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            var blockIndex = 0;
            foreach (var block in _blocks)
            {
                long length = block.Length;
                if (blockIndex == _blocks.Count - 1)
                    length = GetIndexInsideBlock(Count);

                Array.Copy(block, 0, array, blockIndex * MaxBlockSize + arrayIndex, length);

                blockIndex++;
            }
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public int IndexOf(T item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, T item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public T this[int index]
        {
            get
            {
                if (index >= Count)
                    throw new ArgumentOutOfRangeException("index");

                var blockIndex = GetBlockIndex(index);
                var block = _blocks[blockIndex];

                return block[GetIndexInsideBlock(index)];
            }
            set { throw new NotImplementedException(); }
        }

        private int GetBlockIndex(int index)
        {
            return index / MaxBlockSize;
        }

        private long GetIndexInsideBlock(int index)
        {
            return index % MaxBlockSize;
        }
    }
}