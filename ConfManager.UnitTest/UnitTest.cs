using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace ConfManager.UnitTest
{
    [TestClass]
    public class UnitTest
    {
        private const string FileName = @"c:\Windows\check_save.xml";

        [TestMethod()]
        public void CheckObjectNull()
        {
            Assert.ThrowsException<ConfManagerException>(() => ConfWriter.Write(FileName, null));
        }

        [TestMethod]
        public void CheckPathNull()
        {
            Assert.ThrowsException<ConfManagerException>(() => ConfWriter.Write(null, new ValidObject()));
        }

        [TestMethod]
        public void CheckPathValid()
        {
            Assert.ThrowsException<ConfManagerException>(() => ConfWriter.Write(@"\", new ValidObject()));
        }

        [TestMethod]
        public void CheckNullSettings()
        {
            DeleteFile();
            Assert.IsNull(ConfReader.Read<ValidObject>(FileName));
        }

        [TestMethod]
        public void CheckSaveAndLoad()
        {
            Assert.IsTrue(CheckObject(true));
            Assert.IsTrue(CheckObject("blabla"));
            Assert.IsTrue(CheckObject(1));

            ValidObject validObject = new ValidObject();
            validObject.Id = Guid.NewGuid();
            validObject.Children = new List<ChildObject>
            {
                new ChildObject {Id = Guid.NewGuid()},
                new ChildObject {Id = Guid.NewGuid()},
                new ChildObject {Id = Guid.NewGuid()}
            };

            Assert.IsTrue(CheckObject(validObject));
        }

        /// <summary>
        /// Serialize and Deserialize an object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool CheckObject<T>(T obj)
        {
            DeleteFile();
            ConfWriter.Write(FileName, obj);
            if (!Equals(obj, ConfReader.Read<T>(FileName)))
                return false;

            DeleteFile();
            obj.SaveSettings(FileName);
            return Equals(obj, ConfReader.Read<T>(FileName));
        }

        /// <summary>
        /// Delete file
        /// </summary>
        private void DeleteFile()
        {
            if (File.Exists(FileName))
                File.Delete(FileName);
        }
    }
}
