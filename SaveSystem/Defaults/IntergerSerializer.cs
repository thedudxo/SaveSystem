using DudCo.Wrappers;

namespace DudCo.SaveSystems
{
    public class IntergerSerializer : SaveDataSerializer
    {
        public int value = 0;

        public IntergerSerializer(uint ID) : base(ID, version:1) {}

        public override bool Load(IReader reader)
        {
            base.Load(reader);

            value = reader.ReadInt32();

            return true;
        }

        public override void Save(IWriter writer)
        {
            base.Save(writer);
            writer.Write(value);
        }
    }
}