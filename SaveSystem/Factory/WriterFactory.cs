using System.IO;
using DudCo.Wrappers;

namespace DudCo.SaveSystems
{
    public class WriterFactory
    {
        readonly IWriter customWriter;
        readonly bool usingCustomWriter = false;

        public WriterFactory(IWriter customWriter)
        {
            this.customWriter = customWriter;
            usingCustomWriter = true;
        }
        public WriterFactory() {}

        public IWriter CreateWriter(string filename)
        {
            if (usingCustomWriter) 
                return customWriter;

            return CreateDefaultWriter(filename);
        }

        private IWriter CreateDefaultWriter(string filename)
        {
            var stream = File.Open(filename, FileMode.Create);
            return new WrappedWriter(stream);
        }
    }
}