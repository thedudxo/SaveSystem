using System.IO;
using System.Text;

namespace DudCo.SaveSystems
{
    public class WrappedReader : BinaryReader, IReader 
    {
        public WrappedReader(Stream input) : base(input) { }
        public WrappedReader(Stream input, Encoding encoding) :base(input, encoding) { }
        public WrappedReader(Stream input, Encoding encoding, bool leaveOpen) : base(input, encoding, leaveOpen) { }
    }
}
