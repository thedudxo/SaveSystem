using DudCo.Wrappers;
using DudCo.SaveSystems;

namespace Tests.SaveSystems
{
    internal class FailVersionReader : IVersionReader
    {
        public IVersion ReadFromFile(IReader reader)
        {
            return null;
        }
    }
}