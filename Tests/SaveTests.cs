using NUnit.Framework;
using DudCo.SaveSystems;
using System.Collections.Generic;

namespace Tests.SaveSystems
{
    public class SaveTests
    {
        TestSaveSerializer testSerializer;

        const string saveFile = "Fake File";
        readonly MockGameVersion testVersion = new MockGameVersion(3);
        MockVersionReader versionReader;
        MockVersionWriter versionWriter;
        MockWriter writer;
        MockReader reader;

        const int testSerializerID = 0;
        const ushort testSerializerVersion = 1;
        const string testSerializerValue = "SomeString";

        const int versionLength = 1;
        const int dataStart = versionLength + 1;



        SaveSystem CreateSaveSystem(Dictionary<uint, ISaveDataSerializer> systems)
        {
            ILoadErrorHandler errorResult = new MockLoadErrorHandler();
            versionWriter = new MockVersionWriter(testVersion);
            versionReader = new MockVersionReader();
            writer = new MockWriter();
            reader = new MockReader();

            return new SaveSystem(saveFile, testVersion, systems, errorResult, versionReader, versionWriter, writer, reader, new MockDirectoryWrapper(), new MockFile());
        }


        public void SaveWithTestSystem()
        {
            testSerializer = new TestSaveSerializer(testSerializerID, testSerializerVersion, testSerializerValue);

            var systems = new Dictionary<uint, ISaveDataSerializer>
                {
                    {testSerializer.ID, testSerializer }
                };

            CreateSaveSystem(systems).Save();
        }

        void SaveWithEmptySystem()
        {
            var systems = new Dictionary<uint, ISaveDataSerializer> {};
            CreateSaveSystem(systems).Save();
        }

        [Test] public void _1_FileStartsWithSquareBracket()
        {
            SaveWithEmptySystem();

            Assert.AreEqual(FileFormat.FileStart, writer.wrote[0]);
        }

        [Test] public void _2_GameVersion()
        {
            SaveWithEmptySystem();

            Assert.AreEqual(testVersion.version, writer.wrote[1]);
        }

        [Test] public void _3_FileEndsWithSquareBracket()
        {
            SaveWithEmptySystem();

            int last = writer.wrote.Count - 1;
            Assert.AreEqual(FileFormat.FileEnd, writer.wrote[last]);
        }

        [Test] public void _4_DataStartsWithCurlyBrace()
        {
            SaveWithTestSystem();

            Assert.AreEqual(FileFormat.DataStart, writer.wrote[dataStart]);
        }

        [Test] public void _5_DataIdentifier()
        {
            SaveWithTestSystem();

            Assert.AreEqual(testSerializerID, writer.wrote[dataStart + 1]);
        }

        [Test] public void _6_SerializerVersion()
        {
            SaveWithTestSystem();

            Assert.AreEqual(testSerializerVersion, writer.wrote[dataStart + 2]);
        }

        [Test] public void _7_SerializerData()
        {
            SaveWithTestSystem();

            Assert.AreEqual(testSerializerValue, writer.wrote[dataStart + 3]);
        }

        [Test] public void _8_DataEndsWithCurlyBrace()
        {
            SaveWithTestSystem();

            Assert.AreEqual(FileFormat.DataEnd, writer.wrote[dataStart + 4]);
        }
    }
}