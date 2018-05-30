using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VeterinaryClinicManagment.Services;

namespace VeterinaryClinicManagment.Tests
{
    [TestClass]
    public class ToolsTest
    {
        [TestMethod]
        public void Sha256_WithAControlInput_ProducesExpectedHash()
        {
            string input = "Hello World";
            string output = Tools.ShaHash(input);
            Assert.AreEqual(output, "a591a6d40bf420404a011733cfb7b190d62c65bf0bcda32b57b277d9ad9f146e");
        }

        [TestMethod]
        public void FirstLetterToUpper_WithAllLettersToLower_ReturnsFirstInUpper()
        {
            string input1 = "hello";
            string input2 = "Hello";
            string input3 = "heLLo";
            string output1 = Tools.FirstLetterToUpper(input1);
            string output2 = Tools.FirstLetterToUpper(input2);
            string output3 = Tools.FirstLetterToUpper(input3);
            Assert.AreEqual(output1, "Hello");
            Assert.AreEqual(output2, "Hello");
            Assert.AreEqual(output3, "HeLLo");
        }
    }
}
