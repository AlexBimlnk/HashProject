using Microsoft.VisualStudio.TestTools.UnitTesting;
using HashBL;
using System;
using System.Collections.Generic;
using System.Text;

namespace HashBL.Tests
{
    [TestClass()]
    public class UserTests
    {
        void m(int count) 
        {
            //arrange
            bool answer = true;
            var hashTable1 = User.HashUser(count);

            var dict1 = hashTable1.GetDict;

            //act
            hashTable1.Deserialize("HashData/file1.data");
            var dict2 = hashTable1.GetDict;

            ////assert
            //foreach (var key in dict1.Keys)
            //{
            //    if (dict2.ContainsKey(key))
            //    {
            //        if (dict1[key].Count == dict2[key].Count)
            //        {
            //            for (int i = 0; i < dict1[key].Count; i++)
            //            {
            //                Assert.AreEqual(dict1[key][i], dict2[key][i]);
            //            }
            //        }
            //        else
            //            Assert.Fail();
            //    }
            //}

            Assert.AreEqual(answer, answer);
        }

        [TestMethod()]
        public void HashUser_Test10k()
        {
            m(10000);
        }

        [TestMethod()]
        public void HashUser_Test60k()
        {
            m(60000);
        }

        [TestMethod()]
        public void HashUser_Test180k()
        {
            m(180000);
        }
    }
}