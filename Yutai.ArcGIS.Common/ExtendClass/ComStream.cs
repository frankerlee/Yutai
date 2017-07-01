using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Yutai.ArcGIS.Common.ExtendClass
{
    public class ComStream : Stream
    {
        /// <summary>
        /// </summary>
        private IStream theOrigStream;

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
            get
            {
                System.Runtime.InteropServices.ComTypes.STATSTG sTATSTG;
                if (this.theOrigStream == null)
                {
                    throw new ObjectDisposedException("theStream");
                }
                this.theOrigStream.Stat(out sTATSTG, 1);
                return sTATSTG.cbSize;
            }
        }

        public override long Position
        {
            get { return this.Seek((long) 0, SeekOrigin.Current); }
            set { this.Seek(value, SeekOrigin.Begin); }
        }

        public ComStream(ref IStream theStream)
        {
            if (theStream == null)
            {
                throw new ArgumentNullException("theStream");
            }
            this.theOrigStream = theStream;
        }

        public override void Close()
        {
            if (this.theOrigStream != null)
            {
                this.theOrigStream.Commit(0);
                Marshal.ReleaseComObject(this.theOrigStream);
                this.theOrigStream = null;
                GC.SuppressFinalize(this);
            }
            base.Close();
        }

        [DllImport("OLE32.DLL", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int CreateStreamOnHGlobal(int hGlobalMemHandle, bool fDeleteOnRelease, out IStream pOutStm);

        public override void Flush()
        {
            if (this.theOrigStream == null)
            {
                throw new ObjectDisposedException("theStream");
            }
            this.theOrigStream.Commit(0);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (this.theOrigStream == null)
            {
                throw new ObjectDisposedException("theStream");
            }
            int num = 0;
            object obj = num;
            GCHandle gCHandle = GCHandle.Alloc(obj, GCHandleType.Pinned);
            try
            {
                IntPtr intPtr = gCHandle.AddrOfPinnedObject();
                if (offset == 0)
                {
                    this.theOrigStream.Read(buffer, count, intPtr);
                    num = (int) obj;
                }
                else
                {
                    byte[] numArray = new byte[count - 1];
                    this.theOrigStream.Read(numArray, count, intPtr);
                    num = (int) obj;
                    Array.Copy(numArray, 0, buffer, offset, num);
                }
            }
            finally
            {
                if (gCHandle.IsAllocated)
                {
                    gCHandle.Free();
                }
            }
            return num;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            if (this.theOrigStream == null)
            {
                throw new ObjectDisposedException("theStream");
            }
            long num = (long) 0;
            object obj = num;
            GCHandle gCHandle = GCHandle.Alloc(obj, GCHandleType.Pinned);
            try
            {
                IntPtr intPtr = gCHandle.AddrOfPinnedObject();
                this.theOrigStream.Seek(offset, (int) origin, intPtr);
                num = (long) obj;
            }
            finally
            {
                if (gCHandle.IsAllocated)
                {
                    gCHandle.Free();
                }
            }
            return num;
        }

        public override void SetLength(long value)
        {
            if (this.theOrigStream == null)
            {
                throw new ObjectDisposedException("theStream");
            }
            this.theOrigStream.SetSize(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (this.theOrigStream == null)
            {
                throw new ObjectDisposedException("theStream");
            }
            if (offset == 0)
            {
                IntPtr intPtr = new IntPtr();
                IntPtr intPtr1 = new IntPtr();
                this.theOrigStream.Write(buffer, count, intPtr);
                this.theOrigStream.Seek((long) 0, 0, intPtr1);
            }
            else
            {
                int length = (int) buffer.Length - offset;
                byte[] numArray = new byte[length];
                Array.Copy(buffer, offset, numArray, 0, length);
                this.theOrigStream.Write(numArray, length, IntPtr.Zero);
            }
        }
    }
}