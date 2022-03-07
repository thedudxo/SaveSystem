using System.IO;
using System.Text;

namespace DudCo.SaveSystems
{
    public class WrappedWriter : BinaryWriter, IWriter
    {
        public WrappedWriter(Stream input) : base(input) { }
        public WrappedWriter(Stream input, Encoding encoding) : base(input, encoding) { }
        public WrappedWriter(Stream input, Encoding encoding, bool leaveOpen) : base(input, encoding, leaveOpen) { }

        public void Finished()
        {
            Close();
            Dispose();
        }
    }
}
