/// <summary>
/// Reversi.ReversiForm.FormUtil.cs
/// </summary>

using System;
using System.Management;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Reversi
{
    /// <summary>
    /// A subclass of ReversiForm, used to manipulate the various form elements
    /// </summary>
    public class DebugUtil
    {
        /// <summary>
        /// Updates the current game and form elements with the current simulation max depth
        /// </summary>
        public static void UpdateMaxDepth(){}

        /// <summary>
        /// Thread safe delegates for setting the debug window with the new text
        /// </summary>
        /// <param name="newText">The information to report</param>
        public delegate void setDebugTextDelagate(string newText);

        /// <summary>
        /// Thread safe delegates for appending to the debug window with the new text
        /// </summary>
        /// <param name="newText">The information to report</param>
        public delegate void appendDebugTextDelagate(string newText);

        /// <summary>
        /// Thread safe way to update the debug message box
        /// </summary>
        /// <param name="newDebugMsg">The information to report</param>
        /// <param name="updateConsole">(optional: false) True to update the console</param>
        /// <param name="updateWindow">(optional: true) True to update the debug window</param>
        /// <param name="overwrite">(optional: false) To reset the debug window</param>
        public static void ReportDebugMessage(String newDebugMsg, bool updateConsole = false, bool updateWindow = true, bool overwrite = false)
        {
            /*
            if (gDebugLogCheckBox.Checked && updateWindow)
                if (overwrite)
                    gDebugText.Invoke(new setDebugTextDelagate(SetDebugText), newDebugMsg + Environment.NewLine);
                else
                    gDebugText.Invoke(new appendDebugTextDelagate(AppendDebugText), newDebugMsg + Environment.NewLine);

            if (updateConsole)
                Console.WriteLine(newDebugMsg);
             */
        }

        /// <summary>
        /// Thread safe way to clear the debug message box
        /// </summary>
        public static void ClearDebugMessage()
        {
            //gDebugText.Invoke(new setDebugTextDelagate(SetDebugText), "");
        }
    }
}