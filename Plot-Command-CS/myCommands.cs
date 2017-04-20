//-----------------------------------------------------------------------
// <copyright file="myCommands.cs" company="Goldsmith Engineering">
//     Copyright (c) Goldsmith Engineering. All rights reserved.
// </copyright>
// <author>Winston Goldsmith</author>
//-----------------------------------------------------------------------

using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;

// This line is not mandatory, but improves loading performances
[assembly: CommandClass(typeof(Plot_Command_CS.MyCommands))]

namespace Plot_Command_CS
{
    /// <summary>
    /// This class is instantiated by AutoCAD for each document when
    /// a command is called by the user the first time in the context
    /// of a given document. In other words, non static data in this class
    /// is implicitly per-document!
    /// </summary>
    public class MyCommands
    {
        // Modal Command with localized name

        /// <summary>
        /// Overrides AutoCAD PLOT command with PlotForm.cs form
        /// </summary>
        [CommandMethod("plot", CommandFlags.Modal)]
        public void MyCommand() // This method can have any name
        {
            PlotForm plotter = new PlotForm();
            plotter.Show();
        }
    }
}
