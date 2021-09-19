using DudCo.Wrappers;
using System.Collections.Generic;

namespace DudCo.SaveSystems
{
    public class SaveSystem : ISaveSystem
    {
        readonly SaveFile saveFile;
        readonly LoadedFile loadedFile;
        SaveMetadata metadata;
        IWrappedDirectory Directory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file">The full path, filename, and extension</param>
        /// <param name="version">The current running version of the program</param>
        /// <param name="serializers">Dictionary of serializers keyed by their IDs</param>
        /// <param name="errorHandler">A class to handle program flow when a load error is encountered</param>
        /// <param name="versionReader"></param>
        /// <param name="versionWriter"></param>
        public SaveSystem(string file, IVersion version, Dictionary<uint, ISaveDataSerializer> serializers, ILoadErrorHandler errorHandler, IVersionReader versionReader, IVersionWriter versionWriter)
        {
            metadata = CreateMetadata(file, version, serializers, errorHandler, versionReader, versionWriter);

            saveFile = new SaveFile(metadata, new WriterFactory());
            loadedFile = new LoadedFile(metadata, new ReaderFactory(), new WrappedFile());

            Directory = new WrappedDirectory();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customWriter">Custom IWriter implementation to use instead of the default binaryWriter</param>
        /// <param name="customReader">Custom IReader implementation to use instead of the default binaryReader</param>
        public SaveSystem(string file, IVersion version, Dictionary<uint, ISaveDataSerializer> serializers, ILoadErrorHandler errorHandler, IVersionReader versionReader, IVersionWriter versionWriter, IWriter customWriter, IReader customReader, IWrappedDirectory Directory, IWrappedFile File)
        {
            metadata = CreateMetadata(file, version, serializers, errorHandler, versionReader, versionWriter);

            saveFile = new SaveFile(metadata, new WriterFactory(customWriter));
            loadedFile = new LoadedFile(metadata, new ReaderFactory(customReader), File);

            this.Directory = Directory;
        }
        private static SaveMetadata CreateMetadata(string file, IVersion version, Dictionary<uint, ISaveDataSerializer> serializers, ILoadErrorHandler errorHandler, IVersionReader versionReader, IVersionWriter versionWriter)
        {
            var versionSerializer = new VersionSerializer(versionReader, versionWriter);
            var metadata = new SaveMetadata(file, version, serializers, errorHandler, versionSerializer);
            return metadata;
        }

        /// <summary>
        /// Create or overwrite the target save file.
        /// </summary>
        public void Save()
        {
            SetFile(metadata.file); // make sure the directory exists
            saveFile.Create();
        }

        /// <summary>
        /// load the target save file
        /// </summary>
        public void Load()
        {
            loadedFile.Distribute();
        }

        /// <summary>
        /// Change the target save file. Creates the directory if it doesn't exist.
        /// </summary>
        /// <param name="file">Full path to the savefile.</param>
        public void SetFile(string file)
        {
            string targetDirectory = System.IO.Path.GetDirectoryName(file);
            if (Directory.Exists(targetDirectory) == false) 
                Directory.Create(targetDirectory);

            metadata.file = file;
        }
    }
}