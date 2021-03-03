using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;


namespace Tests
{
    public class CalculatorTests
    {

        [Test]
        public void TestAdd()
        {
            Assert.AreEqual(CalculatorController.PerformOperation(1, 2, InputOperation.ADD), 3);
        }

        [Test]
        public void TestSub()
        {
            Assert.AreEqual(CalculatorController.PerformOperation(1, 2, InputOperation.SUB), -1);
        }

        [Test]
        public void TestMul()
        {
            Assert.AreEqual(CalculatorController.PerformOperation(2, 3, InputOperation.MUL), 6);
        }

        [Test]
        public void TestDiv()
        {
            Assert.AreEqual(CalculatorController.PerformOperation(1, 2, InputOperation.DIV), 0.5);
        }

        [Test]
        public void TestPow()
        {
            Assert.AreEqual(CalculatorController.PerformOperation(3, 2, InputOperation.POW), 9);
        }

    }
}