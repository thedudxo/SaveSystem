namespace DudCo.SaveSystems
{
    public interface IWriter : System.IDisposable
    {
        void Close();
        void Write(bool value);
        void Write(byte value);
        void Write(byte[] buffer);
        void Write(byte[] buffer, int index, int count);
        void Write(char ch);
        void Write(char[] chars);
        void Write(char[] chars, int index, int count);
        void Write(decimal value);
        void Write(double value);
        void Write(float value);
        void Write(int value);
        void Write(long value);
        void Write(sbyte value);
        void Write(short value);
        void Write(string value);
        void Write(uint value);
        void Write(ulong value);
        void Write(ushort value);
        void Finished(); // calls close and dispose
    }
}