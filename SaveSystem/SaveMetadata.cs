using System.Collections.Generic;

namespace DudCo.SaveSystems
{

    internal class SaveMetadata
    {
        internal string file;
        internal IVersion programVersion;
        internal VersionSerializer versionSerializer;
        internal Dictionary<uint, ISaveDataSerializer> serializers;
        internal ILoadErrorHandler loadError;

        internal SaveMetadata(string file, IVersion version, Dictionary<uint, ISaveDataSerializer> serializers, ILoadErrorHandler loadError, VersionSerializer versionSerializer)
        {
            this.file = file;
            this.programVersion = version;
            this.serializers = serializers;
            this.loadError = loadError;
            this.versionSerializer = versionSerializer;
        }
    }
    internal struct VersionSerializer
    {
        internal IVersionReader reader;
        internal IVersionWriter writer;

        internal VersionSerializer(IVersionReader versionReader, IVersionWriter versionWriter)
        {
            this.reader = versionReader;
            this.writer = versionWriter;
        }
    }
}