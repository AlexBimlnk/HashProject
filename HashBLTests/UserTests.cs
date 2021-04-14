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
            var hashTable1 = User.HashUser(count);

            var dict1 = hashTable1.GetHashDict;

            //act
            hashTable1.Deserialize("HashData/file1.data");
            var dict2 = hashTable1.GetHashDict;

            //assert
            foreach (var key in dict1.Keys)
            {
                if (dict2.ContainsKey(key))
                {
                    for(int i = 0; i<dict2[key].HashedPassword.Length; i++)
                    {
                        Assert.AreEqual(dict2[key].HashedPassword[i], dict1[key].HashedPassword[i]);
                    }
                }
                else
                    Assert.Fail();
            }

            //Assert.AreEqual(answer, answer);
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