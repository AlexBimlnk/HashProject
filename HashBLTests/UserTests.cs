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

            var dict1 = hashTable1.GetDict;

            //act
            hashTable1.Deserialize("HashData/file1.data");
            var dict2 = hashTable1.GetDict;

            //assert
            foreach (var key in dict1.Keys)
            {
                if (dict2.ContainsKey(key))
                {
                    for(int i = 0; i<dict2[key].Length; i++)
                    {
                        Assert.AreEqual(dict2[key][i], dict1[key][i]);
                    }
                }
                else
                    Assert.Fail();
            }

            //Assert.AreEqual(answer, answer);
        }

        [TestMethod()]
        public void HashUser_UIntSpeedTest()
        {
            //arrange
            uint i1 = uint.MaxValue;
            uint i2 = uint.MaxValue;
            uint i3 = uint.MaxValue;
            uint i4 = uint.MaxValue;
            uint i5 = uint.MaxValue;

            uint[] arrI = { i1, i2, i3, i4, i5 };

            uint j1 = uint.MaxValue;
            uint j2 = uint.MaxValue;
            uint j3 = uint.MaxValue;
            uint j4 = uint.MaxValue;
            uint j5 = uint.MaxValue;

            uint[] arrJ = { j1, j2, j3, j4, j5 };


            for(int i = 0; i<arrI.Length; i++)
            {
                Assert.AreEqual(arrI[i], arrJ[i]);
            }


        }

        [TestMethod()]
        public void HashUser_StringSpeedTest()
        {
            string R()
            {
                //arrange
                uint i1 = uint.MaxValue;
                uint i2 = uint.MaxValue;
                uint i3 = uint.MaxValue;
                uint i4 = uint.MaxValue;
                uint i5 = uint.MaxValue;

                string answer = "";
                answer += i1.ToString();
                answer += i2.ToString();
                answer += i3.ToString();
                answer += i4.ToString();
                answer += i5.ToString();

                return answer;
            }
            //arrange
            string a1 = R();
            string a2 = R();

            Assert.AreEqual(a1, a2);
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