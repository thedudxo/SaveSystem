using System.IO;
using DudCo.Wrappers;

namespace DudCo.SaveSystems
{
    public interface IVersionReader
    {
        IVersion ReadFromFile(IReader reader);
    }

    public interface IVersionWriter
    {
        void WriteToFile(IWriter writer);
    }

    public interface IVersion : System.IComparable
    {
    }
}