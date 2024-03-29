// <copyright file="LogicTreeTest.cs">Copyright ©  2013</copyright>
using System;
using LogicEvalLib;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogicEvalLib.Tests
{
    /// <summary>This class contains parameterized unit tests for LogicTree</summary>
    [PexClass(typeof(LogicTree))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class LogicTreeExceptionTest
    {
        /// <summary>Test stub for Parse(String)</summary>
        [PexMethod(MaxRunsWithoutNewTests = 200)]
        public LogicNode ParseTest([PexAssumeUnderTest]LogicTree target, string parsestring)
        {
            LogicNode result = target.Parse(parsestring);
            return result;
            // TODO: add assertions to method LogicTreeTest.ParseTest(LogicTree, String)
        }
    }
}
