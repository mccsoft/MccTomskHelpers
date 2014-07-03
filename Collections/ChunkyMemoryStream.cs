using System;
using System.Collections.Generic;
using System.IO;

namespace MccTomskHelpers.Collections
{
    /// <summary>
    /// ChunkyMemoryStream is a re-implementation of MemoryStream that uses a dynamic list of byte arrays as a backing store, instead of a single byte array, the allocation
    /// of which will fail for relatively small streams as it requires contiguous memory.
    /// More details here: http://www.codeproject.com/Articles/348590/A-replacement-for-MemoryStream
    /// </summary>
    /// 
    public class ChunkyMemoryStream : Stream
    {
        public ChunkyMemoryStream()
        {
            Position = 0;
        }

        public ChunkyMemoryStream(byte[] source)
        {
            this.Write(source, 0, source.Length);
            Position = 0;
        }

        public ChunkyMemoryStream(byte[] source, long blockSize)
        {
            _blockSize = blockSize;
            this.Write(source, 0, source.Length);
            Position = 0;
        }

        public ChunkyMemoryStream(int length)
        {
            SetLength(length);
            Position = length;
            var d = Block;   //access block to prompt the allocation of memory
            Position = 0;
        }

        public ChunkyMemoryStream(int length, long blockSize)
        {
            SetLength(length);
            Position = length;
            _blockSize = blockSize;
            var d = Block;   //access block to prompt the allocation of memory
            Position = 0;
        }

        public ChunkyMemoryStream(long blockSize)
        {
            Position = 0;
            _blockSize = blockSize;
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { return true; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override long Length
        {
            get { return _length; }
        }

        public override long Position { get; set; }

        private long _length = 0;

        private long _blockSize = 65536;
        public long BlockSize
        {
            get { return _blockSize; }
        }

        private List<byte[]> _blocks = new List<byte[]>();
      
        /// <summary>
        /// The block of memory currently addressed by Position
        /// </summary>
        private byte[] Block
        {
            get
            {
                while (_blocks.Count <= BlockId)
                    _blocks.Add(new byte[_blockSize]);
                return _blocks[(int)BlockId];
            }
        }
        /// <summary>
        /// The id of the block currently addressed by Position
        /// </summary>
        private long BlockId
        {
            get { return Position / _blockSize; }
        }
        /// <summary>
        /// The offset of the byte currently addressed by Position, into the block that contains it
        /// </summary>
        private long BlockOffset
        {
            get { return Position % _blockSize; }
        }

        public override void Flush()
        {
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            var lcount = (long)count;

            if (lcount < 0)
                throw new ArgumentOutOfRangeException("count", lcount, "Number of bytes to copy cannot be negative.");

            long remaining = (_length - Position);
            if (lcount > remaining)
                lcount = remaining;

            if (buffer == null)
                throw new ArgumentNullException("buffer", "Buffer cannot be null.");
            if (offset < 0)
                throw new ArgumentOutOfRangeException("offset", offset, "Destination offset cannot be negative.");

            int read = 0;
            long copysize = 0;
            do
            {
                copysize = Math.Min(lcount, (_blockSize - BlockOffset));
                Buffer.BlockCopy(Block, (int)BlockOffset, buffer, offset, (int)copysize);
                lcount -= copysize;
                offset += (int)copysize;

                read += (int)copysize;
                Position += copysize;

            } while (lcount > 0);

            return read;

        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Begin:
                    Position = offset;
                    break;
                case SeekOrigin.Current:
                    Position += offset;
                    break;
                case SeekOrigin.End:
                    Position = Length - offset;
                    break;
            }
            return Position;
        }

        public override void SetLength(long value)
        {
            _length = value;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            var initialPosition = Position;
            int copySize;
            try
            {
                do
                {
                    copySize = Math.Min(count, (int)(_blockSize - BlockOffset));

                    EnsureCapacity(Position + copySize);

                    Buffer.BlockCopy(buffer, offset, Block, (int)BlockOffset, copySize);
                    count -= copySize;
                    offset += copySize;

                    Position += copySize;

                } while (count > 0);
            }
            catch (Exception e)
            {
                Position = initialPosition;
                throw e;
            }
        }

        public override int ReadByte()
        {
            if (Position >= _length)
                return -1;

            var b = Block[BlockOffset];
            Position++;

            return b;
        }

        public override void WriteByte(byte value)
        {
            EnsureCapacity(Position + 1);
            Block[BlockOffset] = value;
            Position++;
        }

        protected void EnsureCapacity(long intendedLength)
        {
            if (intendedLength > _length)
                _length = (intendedLength);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        #region Public Additional Helper Methods

        /// <summary>
        /// Returns the entire content of the stream as a byte array. This is not safe because the call to new byte[] may 
        /// fail if the stream is large enough. Where possible use methods which operate on streams directly instead.
        /// </summary>
        /// <returns>A byte[] containing the current data in the stream</returns>
        public byte[] ToArray()
        {
            var firstPosition = Position;
            Position = 0;
            var destination = new byte[Length];
            Read(destination, 0, (int)Length);
            Position = firstPosition;
            return destination;
        }

        /// <summary>
        /// Reads length bytes from source into the this instance at the current position.
        /// </summary>
        /// <param name="source">The stream containing the data to copy</param>
        /// <param name="length">The number of bytes to copy</param>
        public void ReadFrom(Stream source, long length)
        {
            var buffer = new byte[4096];
            int read;
            do
            {
                read = source.Read(buffer, 0, (int)Math.Min(4096, length));
                length -= read;
                this.Write(buffer, 0, read);

            } while (length > 0);
        }

        /// <summary>
        /// Writes the entire stream into destination, regardless of Position, which remains unchanged.
        /// </summary>
        /// <param name="destination">The stream to write the content of this stream to</param>
        public void WriteTo(Stream destination)
        {
            var initialPosition = Position;
            Position = 0;
            this.CopyTo(destination);
            Position = initialPosition;
        }

        #endregion
    }
}