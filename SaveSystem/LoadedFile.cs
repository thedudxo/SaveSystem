using DudCo.Wrappers;
using System;

namespace DudCo.SaveSystems
{
    internal class LoadedFile
    {
        SaveMetadata metadata;
        ILoadErrorHandler LoadError => metadata.loadError;

        IReader reader;
        bool openedFile = false;
        readonly ReaderFactory readerFactory;
        readonly IWrappedFile File;

        internal LoadedFile(SaveMetadata metadata, ReaderFactory readerFactory, IWrappedFile File)
        {
            this.metadata = metadata;
            this.readerFactory = readerFactory;
            this.File = File;
        }

        internal void Distribute()
        {
            Func<bool>[] steps =
            {
                CheckFileExists,
                CreateReader,
                ReadStartOfFile,
                ReadSavedGameVersion,
                DistributeToSerializers,
            };

            Run(steps);
        }

        private void Run(Func<bool>[] steps)
        {
            try
            {
                foreach (var step in steps)
                    if (step() == false) break;
            }
            finally
            {
                DisposeReader();
            }
        }

        private bool CheckFileExists()
        {
            return File.Exists(metadata.file);
        }

        private bool CreateReader()
        {
            reader = readerFactory.CreateReader(metadata.file);
            openedFile = true;
            return true;
        }

        private void DisposeReader()
        {
            if (openedFile == false) return;
            reader.Close();
            reader.Dispose();
        }

        private bool ReadStartOfFile()
        {
            char fileStart = reader.ReadChar();
            bool valid = fileStart == '[';
            if(valid == false) LoadError.InvalidFileFormat();
            return valid;
        }

        private bool ReadSavedGameVersion()
        {
            IVersionReader versionReader = metadata.versionSerializer.reader;
            IVersion gameVersion = metadata.programVersion;

            IVersion fileVersion = versionReader.ReadFromFile(reader); 

            if (fileVersion == null) 
            {
                LoadError.UndeterminedFileVersion();
                return false;
            }

            bool saveVersionHigher = gameVersion.CompareTo(fileVersion) == -1;
            if (saveVersionHigher) LoadError.HigherSaveFileVersion();

            bool valid = saveVersionHigher == false;
            return valid;
        }

        private bool DistributeToSerializers()
        {
            var data = new LoadedData(reader, metadata);

            return data.Distribute();
        }
    }
}