using DudCo.SaveSystems;
using NUnit.Framework;
using System.Collections.Generic;

namespace Tests.SaveSystems
{
    class SaveSystemTests
    {
        SaveSystem saveSystem;
        MockDirectoryWrapper Directory;

        [SetUp]
        public void SetUp()
        {
            Directory = new MockDirectoryWrapper();
            var version = new MockGameVersion(1);

            saveSystem = new SaveSystem
                (
                "junk",
                version,
                new Dictionary<uint, ISaveDataSerializer>(),
                new MockLoadErrorHandler(),
                new MockVersionReader(),
                new MockVersionWriter(version),
                new MockWriter(),
                new MockReader(),
                Directory,
                new MockFile()
                );
        }

        [Test]
        public void CreateDirectory_If_ItDoesntExist()
        {
            Directory.doesExist = false;

            saveSystem.SetFile("some/new/Dir/");

            Assert.True(Directory.created);
        }

        [Test]
        public void DontCreateDirectory_If_ItExists()
        {
            Directory.doesExist = true;

            saveSystem.SetFile("some/existing/Dir/");

            Assert.False(Directory.created);
        }

        [Test]
        public void CreateDirectory_IfGivenFile_InNonexistantDirectory()
        {
            Directory.doesExist = false;

            saveSystem.SetFile("some\\nonexsitant\\dir\\save.jam");

            Assert.AreEqual("some\\nonexsitant\\dir", Directory.createdDirectory);
        }
    }
}