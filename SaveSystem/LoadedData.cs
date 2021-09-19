using System.Collections.Generic;
using System;
using DudCo.Wrappers;

namespace DudCo.SaveSystems
{
    internal class LoadedData
    {
        private const char FileEndChar = ']';
        private const char DataEndChar = '}';
        private const char DataStartChar = '{';
        IReader reader;
        SaveMetadata metadata;

        Stack<uint> loadedIDs = new Stack<uint>();
        uint serializerID;
        bool moreToRead = true;
        bool loadedSucsessfully = true;

        internal LoadedData(IReader reader, SaveMetadata metadata)
        {
            this.reader = reader;
            this.metadata = metadata;
        }

        internal bool Distribute()
        {
            Func<bool>[] steps =
            {
                ReadStartOfDataOrEndOfFile,
                ReadID,
                RunSerializerLoadMethod,
                ReadEndOfData,
            };

            while (moreToRead)
            {
                Run(steps);
            }

            return loadedSucsessfully;
        }

        private void Run(Func<bool>[] steps)
        {
            foreach (var step in steps)
            {
                if (step() == false)
                {
                    moreToRead = false;
                    loadedSucsessfully = false;
                    break;
                };
            }
        }

        private bool ReadStartOfDataOrEndOfFile()
        {
            char starter = reader.ReadChar();

            if (starter == FileEndChar)
            {
                moreToRead = false;
                return false;
            }

            if (starter != DataStartChar) 
                return false;

            return true;
        }

        private bool ReadID()
        {
            serializerID = reader.ReadUInt32();

            if (loadedIDs.Contains(serializerID))
            {
                metadata.loadError.MultipleOfSameId();
                return false;
            }

            loadedIDs.Push(serializerID);

            bool knownID = metadata.serializers.ContainsKey(serializerID);
            if (knownID == false) metadata.loadError.UnknownSerializerId();

            return knownID;
        }

        private bool RunSerializerLoadMethod()
        {
            ISaveDataSerializer serializer = metadata.serializers[serializerID];

            bool sucsess = serializer.Load(reader);
            if (sucsess == false) metadata.loadError.FailedSerializerLoad();

            return sucsess;
        }

        private bool ReadEndOfData()
        {
            char c = reader.ReadChar();
            bool correctChar = c == DataEndChar;
            if (correctChar == false) metadata.loadError.MissingDataEndChar();
            return correctChar;
        }
    }
}