using DudCo.SaveSystems;
using System.Collections.Generic;

namespace Tests.SaveSystems
{
    class MockWriter : IWriter
    {
        public List<object> wrote = new List<object>();

        public bool Disposed;
        public bool Closed;
        public bool DisposedAndClosed => Disposed && Closed;

        public void Close() => Closed = true;

        public void Dispose() => Disposed = true;

        public void Finished()
        {
            throw new System.NotImplementedException();
        }

        public void Write(bool value) => wrote.Add(value);

        public void Write(byte value) => wrote.Add(value);

        public void Write(byte[] buffer) => wrote.Add(buffer);


        public void Write(byte[] buffer, int index, int count)
        {
            throw new System.NotImplementedException();
        }

        public void Write(char ch) => wrote.Add(ch);

        public void Write(char[] chars) => wrote.Add(chars);

        public void Write(char[] chars, int index, int count)
        {
            throw new System.NotImplementedException();
        }

        public void Write(decimal value) => wrote.Add(value);

        public void Write(double value) => wrote.Add(value);

        public void Write(float value) => wrote.Add(value);

        public void Write(int value) => wrote.Add(value);

        public void Write(long value) => wrote.Add(value);

        public void Write(sbyte value) => wrote.Add(value);

        public void Write(short value) => wrote.Add(value);

        public void Write(string value) => wrote.Add(value);

        public void Write(uint value) => wrote.Add(value);

        public void Write(ulong value) => wrote.Add(value);

        public void Write(ushort value) => wrote.Add(value);
    }
}