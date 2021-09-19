using DudCo.Wrappers;

namespace DudCo.SaveSystems
{
    public abstract class SaveDataSerializer : ISaveDataSerializer
    {
        public uint ID { get; set; }
        public ushort Version { get; set; }

        protected SaveDataSerializer (uint ID, ushort version)
        {
            this.ID = ID;
            this.Version = version;
        }

        public virtual void Save(IWriter writer)
        {
            writer.Write(ID);
            writer.Write(Version);
        }
        public virtual bool Load(IReader reader)
        {
            //ID has been read allready by the savesystem
            ushort savedVersion = reader.ReadUInt16();
            return true;
        }
    }
}