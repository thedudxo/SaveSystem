using DudCo.SaveSystems;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using static Tests.SaveSystems.FileElements;

namespace Tests.SaveSystems
{
    public class LoadTests
    {
        const string fakeDirectory = "P:\\oblivion\\somewhere\\";
        TestSaveSerializer testSerializer;
        MockLoadErrorHandler errorResult;

        MockWriter writer;
        MockReader reader;

        MockGameVersion runningVersion;

        MockFile File;

        IVersionReader versionReader;
        MockVersionWriter versionWriter;

        const int testVersion = 6;

        Dictionary<FileElements,object> fakeFile;
        Dictionary<uint, ISaveDataSerializer> serializers;

        [SetUp]
        public void SetUp()
        {
            errorResult = new MockLoadErrorHandler();
            
            runningVersion = new MockGameVersion(testVersion);

            testSerializer = new TestSaveSerializer(0, 1, "awesomeString");
            serializers = new Dictionary<uint, ISaveDataSerializer>();

            writer = new MockWriter();
            reader = new MockReader();

            File = new MockFile();

            fakeFile = new Dictionary<FileElements, object>
            {
                {fileStart, FileFormat.FileStart },
                {version, testVersion },
                {dataOpener, FileFormat.DataStart },
                {serializerID, testSerializer.ID },
                {serializerVersion, testSerializer.Version },
                {serializerValue, testSerializer.saved },
                {dataEnd, FileFormat.DataEnd },
                {fileEnd, FileFormat.FileEnd }
            };

            versionReader = new MockVersionReader();
            versionWriter = new MockVersionWriter(runningVersion);
        }

        SaveSystem CreateSaveSystem(string fakeFilePath = fakeDirectory + "fakeFile.jam")
        {
            serializers.Add(testSerializer.ID, testSerializer);

            reader.reading = fakeFile.Values.ToList();

            return new SaveSystem(fakeFilePath, runningVersion, serializers, errorResult, versionReader, versionWriter, writer, reader, new MockDirectoryWrapper(), File);
        }

        [Test]
        [TestCase(".jam")]
        [TestCase(".save")]
        [TestCase(".someTerribleExtension")]
        public void FileWith_CustomExtension_IsLoaded(string extension)
        {
            var saveSystem = CreateSaveSystem(fakeDirectory + extension);

            saveSystem.Load();

            Assert.AreEqual(testSerializer.saved, testSerializer.loaded);
        }

        [Test]
        public void FileWith_HigherGameVersion_IsErrorHandled()
        {
            fakeFile[version] = testVersion + 1;
            SaveSystem saveSystem = CreateSaveSystem();

            saveSystem.Load();

            Assert.True(errorResult.higherVersionHandled);
        }

        [Test]
        public void InvalidSerializerID_IsErrorHandled()
        {
            fakeFile[serializerID] = (uint)999999999;
            SaveSystem saveSystem = CreateSaveSystem();

            saveSystem.Load();

            Assert.True(errorResult.invaildSerializerIdWasHandled);
        }

        [Test]
        public void SerializerLoadsCorrectData()
        {
            SaveSystem saveSystem = CreateSaveSystem();

            saveSystem.Load();

            string saved = testSerializer.saved;
            string loaded = testSerializer.loaded;

            Assert.AreEqual(saved, loaded);
        }

        [Test]
        [TestCase(version, testVersion + 1, TestName = "HigherVersion")]
        [TestCase(serializerID, (uint)999999999, TestName ="UnknownSerializerID")]
        [TestCase(fileStart,'k', TestName ="InvalidFileStart")]
        public void ErrorHandledFile_IsNotLoaded(FileElements errorKey, object error)
        {
            fakeFile[errorKey] = error;
            SaveSystem saveSystem = CreateSaveSystem();

            saveSystem.Load();

            Assert.IsNull(testSerializer.loaded);
        }

        [Test]
        public void InvalidFileFormatting_IsErrorHandled()
        {
            fakeFile[fileStart] = '~';
            SaveSystem saveSystem = CreateSaveSystem();

            saveSystem.Load();

            Assert.True(errorResult.invalidFileFormatWasHandled);
        }

        [Test]
        public void FileWith_InvalidDataEndCharacter_IsErrorHandled()
        {
            fakeFile[dataEnd] = 'E';
            SaveSystem saveSystem = CreateSaveSystem();

            saveSystem.Load();

            Assert.True(errorResult.missingDataEndCharWasHandled);
        }

        List<object> AddDataToFile(List<object>data)
        {
            List<object> file = fakeFile.Values.ToList();
            int index = (int)dataEnd + 1;
            List<object> moreData = new List<object>();

            moreData.Add(FileFormat.DataStart);
            moreData.AddRange(data);
            moreData.Add(FileFormat.DataEnd);

            file.InsertRange(index, moreData);

            return file;
        }

        List<object> ListFromMockSerializer(TestSaveSerializer serializer)
            => new List<object>() { serializer.ID, serializer.Version, serializer.saved };

        [Test]
        public void TwoSerializerInstances_WithDifferentIDs_LoadCorrectData()
        {
            var serializer = new TestSaveSerializer(1, 1, "otherString");
            var data = ListFromMockSerializer(serializer);
            var file = AddDataToFile(data);
            serializers.Add(serializer.ID, serializer);

            SaveSystem saveSystem = CreateSaveSystem();
            reader.reading = file;

            saveSystem.Load();

            Assert.AreEqual(serializer.saved, serializer.loaded);
        }

        [Test]
        public void TwoSerializers_WithSameID_IsErrorHandled()
        {
            var serializer = new TestSaveSerializer(testSerializer.ID, 1, "otherString");
            var data = ListFromMockSerializer(serializer);

            var file = AddDataToFile(data);

            SaveSystem saveSystem = CreateSaveSystem();
            reader.reading = file;

            saveSystem.Load();

            Assert.True(errorResult.multipleOfSameIdWasHandled);
        }

        [Test]
        public void SerializerFailsLoading_IsErrorHandled()
        {
            var serializer = new FailLoadSerializer(1,1);
            var data = new List<object> { serializer.ID, serializer.Version };

            var file = AddDataToFile(data);

            SaveSystem saveSystem = CreateSaveSystem();
            reader.reading = file;
            serializers.Add(serializer.ID, serializer);

            saveSystem.Load();

            Assert.True(errorResult.failedSerializerLoadWasHandled);
        }

        private void LoadWithUndeternimedFileVersion()
        {
            versionReader = new FailVersionReader();
            SaveSystem saveSystem = CreateSaveSystem();

            saveSystem.Load();
        }
        [Test]
        public void UndeterminedFileVersion_IsErrorHandled()
        {
            LoadWithUndeternimedFileVersion();

            Assert.True(errorResult.undeterminedFileVersionWasHandled);
        }
        [Test]
        public void UndeterminedFileVersion_IsNotLoaded()
        {
            LoadWithUndeternimedFileVersion();

            Assert.IsNull(testSerializer.loaded);
        }

        [Test] 
        public void NonexistantFile_IsNotLoaded()
        {
            File.doesExist = false;
            var saveSystem = CreateSaveSystem();

            saveSystem.Load();

            Assert.IsNull(testSerializer.loaded);
        }
    }
}