using System.IO;
using System.Threading;

namespace MccTomskHelpers.Collections
{
    public static class StreamExtensions
    {
        public static void CopyTo(this Stream source, Stream destination, int bufferSize, CancellationToken cancellationToken)
        {
            //Unfortunately .NET Framework doesn't have synchronous version of CopyTo with CancellationToken argument.
            //So we have modified default implementation
            //See http://stackoverflow.com/questions/15612074/copyto-with-cancellationtoken-argument for more details
            var buffer = new byte[bufferSize];
            int count;
            while ((count = source.Read(buffer, 0, buffer.Length)) != 0)
            {
                cancellationToken.ThrowIfCancellationRequested();
                destination.Write(buffer, 0, count);
            }
        }
    }
}