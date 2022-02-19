using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace ConfManager.UnitTest
{
    [TestClass]
    public class UnitTest
    {
        private const string FileName = "check_save.xml";

        [TestMethod()]
        public void CheckObjectNull()
        {
            Assert.ThrowsException<ConfManagerException>(() => ConfManager.Save(FileName, null));
        }

        [TestMethod]
        public void CheckPathNull()
        {
            Assert.ThrowsException<ConfManagerException>(() => ConfManager.Save(null, new ValidObject()));
        }

        [TestMethod]
        public void CheckPathValid()
        {
            Assert.ThrowsException<ConfManagerException>(() => ConfManager.Save(@"\", new ValidObject()));
        }

        [TestMethod]
        public void CheckNullSettings()
        {
            DeleteFile();
            Assert.IsNull(ConfManager.Load<ValidObject>(FileName));
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
            ConfManager.Save(FileName, obj);
            if (!Equals(obj, ConfManager.Load<T>(FileName)))
                return false;

            DeleteFile();
            obj.SaveSettings(FileName);
            return Equals(obj, ConfManager.Load<T>(FileName));
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
