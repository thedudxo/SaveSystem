namespace DudCo.SaveSystems
{
    public interface ILoadErrorHandler
    {
        //TODO: log what happened somewhere, then cancel the load operation

        /// <summary>
        /// Tried loading a file that was saved with a newer version of the program
        /// </summary>
        void HigherSaveFileVersion();

        /// <summary>
        /// Serializer ID present in the save file was not found in the dictionary of serializers
        /// </summary>
        void UnknownSerializerId();

        /// <summary>
        /// file contents was not formatted correctly
        /// </summary>
        void InvalidFileFormat();

        /// <summary>
        /// Two serializers were found in the file with the same ID
        /// </summary>
        void MultipleOfSameId();

        /// <summary>
        /// a serializer indicated it encountered an error while loading it's data
        /// </summary>
        void FailedSerializerLoad();

        /// <summary>
        /// Version reader returned null
        /// </summary>
        void UndeterminedFileVersion();

        /// <summary>
        /// the serializer has loaded everything correctly, but there was more unexpected data in the chunk
        /// </summary>
        void MissingDataEndChar();
    }
}