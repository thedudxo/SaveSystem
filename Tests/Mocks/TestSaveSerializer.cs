using DudCo.SaveSystems;

namespace Tests.SaveSystems
{
    class TestSaveSerializer : SaveDataSerializer
    {
        public string saved;
        public string loaded;

        public TestSaveSerializer(uint ID, ushort version, string data) : base(ID, version)
        {
            saved = data;
        }

        public override void Save(IWriter writer)
        {
            base.Save(writer);

            writer.Write(saved);
        }

        public override bool Load(IReader reader)
        {
            base.Load(reader);

            loaded = reader.ReadString();

            return true;
        }
    }
}
