using DudCo.Wrappers;

namespace DudCo.SaveSystems
{
    public class SaveFile
    {
        SaveMetadata metadata;
        WriterFactory writerFactory;

        internal SaveFile(SaveMetadata metadata, WriterFactory writerFactory)
        {
            this.metadata = metadata;
            this.writerFactory = writerFactory;
        }

        public void Create()
        {
            using (IWriter writer = writerFactory.CreateWriter(metadata.file))
            {
                SaveToFile(writer);
            }
        }

        private void SaveToFile(IWriter writer)
        {
            writer.Write('[');

            WriteRunningVersion(writer);
            WriteSaveData(writer);

            writer.Write(']');
        }

        private void WriteRunningVersion(IWriter writer)
        {
            IVersionWriter versionWriter = metadata.versionSerializer.writer;
            versionWriter.WriteToFile(writer);
        }

        private void WriteSaveData(IWriter writer)
        {
            foreach (var data in metadata.serializers)
            {
                ISaveDataSerializer serializer = data.Value;

                writer.Write('{');
                serializer.Save(writer);
                writer.Write('}');
            }
        }
    }
}