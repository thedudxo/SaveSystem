using DudCo.Wrappers;
using System.Collections.Generic;

namespace Tests.SaveSystems
{
    class MockReader : IReader
    {
        public List<object> hasRead = new List<object>();
        public List<object> reading;
        int position = -1;

        bool closed;
        bool disposed;
        public bool ClosedAndDisposed => closed && disposed;

        public void Close()
        {
            closed = true;
        }

        public void Dispose()
        {
            disposed = true;
        }

        public int PeekChar()
        {
            return (int)reading[position];
        }

        public int Read()
        {
            position++; 
            return (int)reading[position];
        }

        public int Read(byte[] buffer, int index, int count)
        {
            position++; 
            return (int)reading[position];
        }

        public int Read(char[] buffer, int index, int count)
        {
            position++; 
            return (int)reading[position];
        }

        public bool ReadBoolean()
        {
            position++; 
            return (bool)reading[position];
        }

        public byte ReadByte()
        {
            position++; 
            return (byte)reading[position];
        }

        public byte[] ReadBytes(int count)
        {
            position++; 
            return (byte[])reading[position];
        }

        public char ReadChar()
        {
            position++; 
            return (char)reading[position];
        }

        public char[] ReadChars(int count)
        {
            position++; 
            return (char[])reading[position];
        }

        public decimal ReadDecimal()
        {
            position++; 
            return (decimal)reading[position];
        }

        public double ReadDouble()
        {
            position++; 
            return (double)reading[position];
        }

        public short ReadInt16()
        {
            position++; 
            return (short)reading[position];
        }

        public int ReadInt32()
        {
            position++; 
            return (int)reading[position];
        }

        public long ReadInt64()
        {
            position++; 
            return (long)reading[position];
        }

        public sbyte ReadSByte()
        {
            position++; 
            return (sbyte)reading[position];
        }

        public float ReadSingle()
        {
            position++; 
            return (float)reading[position];
        }

        public string ReadString()
        {
            position++; 
            return (string)reading[position];
        }

        public ushort ReadUInt16()
        {
            position++; 
            return (ushort)reading[position];
        }

        public uint ReadUInt32()
        {
            position++; 
            return (uint)reading[position];
        }

        public ulong ReadUInt64()
        {
            position++; 
            return (ulong)reading[position];
        }
    }
}