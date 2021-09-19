using DudCo.SaveSystems;
using DudCo.Wrappers;

namespace Tests.SaveSystems
{
    class FailLoadSerializer : SaveDataSerializer
    {
        public FailLoadSerializer(uint ID, ushort version) : base(ID, version)
        {
        }

        public override bool Load(IReader reader)
        {
            base.Load(reader);

            return false;
        }
    }
}
