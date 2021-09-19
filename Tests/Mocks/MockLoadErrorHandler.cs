using DudCo.SaveSystems;

namespace Tests.SaveSystems
{
    class MockLoadErrorHandler : ILoadErrorHandler
    {
        public bool
            higherVersionHandled = false,
            invaildSerializerIdWasHandled = false,
            invalidFileFormatWasHandled = false,
            missingDataEndCharWasHandled = false,
            multipleOfSameIdWasHandled = false,
            failedSerializerLoadWasHandled = false,
            undeterminedFileVersionWasHandled = false
            ;

        void ILoadErrorHandler.HigherSaveFileVersion() => higherVersionHandled = true;
        void ILoadErrorHandler.UnknownSerializerId() => invaildSerializerIdWasHandled = true;
        void ILoadErrorHandler.InvalidFileFormat() => invalidFileFormatWasHandled = true;
        void ILoadErrorHandler.MissingDataEndChar() => missingDataEndCharWasHandled = true;
        void ILoadErrorHandler.MultipleOfSameId() => multipleOfSameIdWasHandled = true;
        void ILoadErrorHandler.FailedSerializerLoad() => failedSerializerLoadWasHandled = true;
        void ILoadErrorHandler.UndeterminedFileVersion() => undeterminedFileVersionWasHandled = true;
    }
}

