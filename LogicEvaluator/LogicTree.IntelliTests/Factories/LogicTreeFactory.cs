using LogicEvalLib;
// <copyright file="LogicTreeFactory.cs">Copyright ©  2013</copyright>

using System;
using Microsoft.Pex.Framework;

namespace LogicEvalLib
{
    /// <summary>A factory for LogicEvalLib.LogicTree instances</summary>
    public static partial class LogicTreeFactory
    {
        /// <summary>A factory for LogicEvalLib.LogicTree instances</summary>
        [PexFactoryMethod(typeof(LogicTree))]
        public static LogicTree Create()
        {
            LogicTree logicTree = new LogicTree();
            return logicTree;

            // TODO: Edit factory method of LogicTree
            // This method should be able to configure the object in all possible ways.
            // Add as many parameters as needed,
            // and assign their values to each field by using the API.
        }
    }
}
