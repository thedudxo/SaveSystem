using DudCo.Wrappers;

namespace DudCo.SaveSystems
{
    public interface ISaveDataSerializer
    {
        uint ID { get; set; }
        ushort Version { get; set; }
        void Save(IWriter writer);
        bool Load(IReader reader);
    }
}