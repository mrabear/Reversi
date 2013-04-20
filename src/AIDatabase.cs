/// <summary>
/// Reversi.AIDatabase.cs
/// </summary>

using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Reversi
{
    /// <summary>
    /// Used to calculate the AI turn database
    /// </summary>
    public class AIDatabase
    {
        private static Dictionary<string, int> BlackMoves = new Dictionary<string, int>();

        private static Dictionary<string, SimulationNode> NodeMasterList = new Dictionary<string, SimulationNode>();

        private static Queue<String> WorkNodes = new Queue<String>();
        private static Queue<String> LeafNodes = new Queue<String>();

        private static int SimulationCycles = 0;
        private static int WhiteWinnerTotal = 0;
        private static int BlackWinnerTotal = 0;
        private static int TieTotal = 0;
        private static int LeafTotal = 0;

        /// <summary>
        /// Returns a string dump of the current database information
        /// </summary>
        /// <returns>A string dump of the current database information</returns>
        public String DumpSimulationInfo()
        {
            return ("============================\n" +
                    "Dumping AI DB Info\n" +
                    "============================\n" +
                    "Total Nodes: " + NodeMasterList.Count + "\n" +
                    "Total Leaf Nodes: " + LeafTotal + "\n" +
                    "Total Black Winners: " + BlackWinnerTotal + "\n" +
                    "Total White Winners: " + WhiteWinnerTotal + "\n");
        }

        /// <summary>
        /// Infiniate list iterator for the parallel foreach loop
        /// </summary>
        private static IEnumerable<bool> IterateUntilFalse(Func<bool> condition)
        {
            while (condition()) yield return true;
        }

        /// <summary>
        /// Builds the database of all potential game states
        /// </summary>
        /// <param name="WorkerThread">The background worker to use</param>
        /// <param name="BoardSize">The size of the board to simulate</param>
        /// <param name="VisualizeResults">True if the board analysis should be displayed</param>
        /// <param name="DisplayDebug">True if debug information should be displayed</param>
        public void BuildAIDatabase(BackgroundWorker WorkerThread, int BoardSize = 8, Boolean VisualizeResults = false, Boolean DisplayDebug = true)
        {
            /////////////////////////////////////////////////////////////
            //DEBUG BULLSHIT
            SimulationCycles = 0;

            DateTime SimulationClock = DateTime.Now;

            ReversiForm.reportDebugMessage("===============================\nBuilding AI Database (" + SimulationClock.ToLocalTime() + ")\n", overwrite: true );
            ReversiForm.updateDatabaseProgress(DateTime.Now.Subtract(SimulationClock), WorkNodes.Count, NodeMasterList.Count, LeafTotal);
            /////////////////////////////////////////////////////////////

            // Reset the database and work queues
            LeafNodes = new Queue<String>();
            WorkNodes = new Queue<String>();
            NodeMasterList = new Dictionary<string, SimulationNode>();

            //Board CurrentBoard = new Board();
            int ParentTurn = ReversiApplication.WHITE;
            SimulationNode ParentNode = new SimulationNode(new Board(BoardSize), ParentTurn);
            String ParentNodeID = ParentNode.getNodeID();
            String RootNodeID = ParentNode.getNodeID();

            int ChildTurn = ReversiApplication.BLACK;
            //String ChildNodeID;
            Board ChildBoard;
            SimulationNode ChildNode;

            // Seed the master node list with the the root node that contains the default game positions and settings
            NodeMasterList.Add(RootNodeID, ParentNode);

            // Seed the work list with the root node
            WorkNodes.Enqueue(RootNodeID);

            //Parallel.ForEach(IterateUntilFalse(WorkNodes.Count > 0), =>
            while (WorkNodes.Count > 0)
            {

                /////////////////////////////////////////////////////////////
                //DEBUG BULLSHIT
                /////////////////////////////////////////////////////////////
                SimulationCycles++;

                // If the BackgroundWorker.CancellationPending property is true, cancel
                if (WorkerThread.CancellationPending)
                {
                    ReversiForm.reportDebugMessage("#####Database Build has been cancelled#####", updateConsole: true );
                    break;
                }

                if (SimulationCycles % 75 == 0)
                    ReversiForm.updateDatabaseProgress(DateTime.Now.Subtract(SimulationClock), WorkNodes.Count, NodeMasterList.Count, LeafTotal);

                // Grab the next node ID off of the work queue
                ParentNodeID = WorkNodes.Dequeue();

                // Fetch the current game node from the master list
                ParentNode = NodeMasterList[ParentNodeID];

                // Set the child turn to be the next player
                ChildTurn = (ParentNode.getTurn() == ReversiApplication.WHITE) ? ReversiApplication.BLACK : ReversiApplication.WHITE;

                // Update the game board visual
                if (VisualizeResults)
                    ReversiForm.RefreshPieces(ParentNode.getGameBoard());

                if (ParentNode.getAvailableMoves().Length == 0)
                {
                    ParentNode.setIsPassTurn(true);

                    ChildNode = new SimulationNode(ParentNode.getGameBoard(), ChildTurn);

                    if (ChildNode.getAvailableMoves().Length > 0)
                    {

                        if (NodeMasterList.ContainsKey(ChildNode.getNodeID()))
                        {
                            // Since the node already exists, just add the current parent to it's parent node list
                            if (!NodeMasterList[ChildNode.getNodeID()].ContainsParent(ParentNode.getNodeID()))
                                NodeMasterList[ChildNode.getNodeID()].AddParentNode(ParentNode.getNodeID());
                        }
                        else
                        {
                            // Add the new node to the master list
                            NodeMasterList.Add(ChildNode.getNodeID(), ChildNode);

                            // Add the new node to the work list for eventual processing
                            WorkNodes.Enqueue(ChildNode.getNodeID());
                        }

                        // Add this child to the parent's child node list
                        if (!ParentNode.ContainsChild(ChildNode.getNodeID()))
                            ParentNode.AddChildNode(ChildNode.getNodeID());

                        // Add the new node to the work list for eventual processing
                        WorkNodes.Enqueue(ChildNode.getNodeID());
                    }
                    else
                    {
                        ParentNode.setIsLeaf(true);
                        LeafTotal++;

                        if (ParentNode.getGameBoard().FindScore(ReversiApplication.BLACK) > ParentNode.getGameBoard().FindScore(ReversiApplication.WHITE))
                        {
                            ParentNode.setBlackWins(ParentNode.getBlackWins() + 1);
                            BlackWinnerTotal++;
                        }
                        else if (ParentNode.getGameBoard().FindScore(ReversiApplication.BLACK) < ParentNode.getGameBoard().FindScore(ReversiApplication.WHITE))
                        {
                            ParentNode.setWhiteWins(ParentNode.getWhiteWins() + 1);
                            WhiteWinnerTotal++;
                        }
                        else
                        {
                            TieTotal++;
                        }
                    }
                }
                else
                {
                    foreach (Point CurrentMove in ParentNode.getAvailableMoves())
                    {
                        ChildBoard = new Board(ParentNode.getGameBoard());
                        ChildBoard.PutPiece(CurrentMove, ChildTurn);
                        ChildNode = new SimulationNode(ChildBoard, ChildTurn);

                        // Add this child to the parent's child node list
                        if (!ParentNode.ContainsChild(ChildNode.getNodeID()))
                            ParentNode.AddChildNode(ChildNode.getNodeID());

                        if (NodeMasterList.ContainsKey(ChildNode.getNodeID()))
                        {
                            // Since the node already exists, just add the current parent to it's parent node list
                            if (!NodeMasterList[ChildNode.getNodeID()].ContainsParent(ParentNode.getNodeID()))
                                NodeMasterList[ChildNode.getNodeID()].AddParentNode(ParentNode.getNodeID());
                        }
                        else
                        {
                            // Add the new node to the master list
                            NodeMasterList.Add(ChildNode.getNodeID(), ChildNode);

                            // Add the new node to the work list for eventual processing
                            WorkNodes.Enqueue(ChildNode.getNodeID());
                        }
                    }
                }
                // Clear all of the moves from the nodes working list
                ParentNode.ClearMoves();
            }

            /////////////////////////////////////////////////////////////
            //DEBUG BULLSHIT
            /////////////////////////////////////////////////////////////
            if (DisplayDebug)
            {
                ReversiForm.updateDatabaseProgress(DateTime.Now.Subtract(SimulationClock), WorkNodes.Count, NodeMasterList.Count, LeafTotal);
                ReversiForm.reportDebugMessage("===============================\nAI DB Build Complete\nSimulation Time: " + DateTime.Now.Subtract(SimulationClock) + "\n\n" + DumpSimulationInfo(), updateConsole: true);
            }
        }

        // Analyze the database that has been built by the AIDatabase method
        // LEGACY CODE THAT DOESN'T REALLY WORK
        public void AnalyzeAIDatabase(BackgroundWorker WorkerThread, Boolean VisualizeResults = false, RichTextBox gDebugTextBox = null, Boolean DisplayDebug = true)
        {

            /////////////////////////////////////////////////////////////
            //DEBUG BULLSHIT
            DateTime SimulationClock = DateTime.Now;

            if (DisplayDebug)
                ReversiForm.reportDebugMessage("===============================\nAnalyzing AI Database (" + SimulationClock.ToLocalTime() + ")", updateConsole: true);
            /////////////////////////////////////////////////////////////

            // Reset all previous analysis values and queue all of the leaf nodes to process
            LeafNodes = new Queue<String>();
            WorkNodes = new Queue<String>();

            foreach (String CurrentNodeID in NodeMasterList.Keys)
            {
                if (NodeMasterList[CurrentNodeID].getIsLeaf())
                    LeafNodes.Enqueue(NodeMasterList[CurrentNodeID].getNodeID());

                NodeMasterList[CurrentNodeID].setBlackWins(0);
                NodeMasterList[CurrentNodeID].setWhiteWins(0);
            }

            /////////////////////////////////////////////////////////////
            //DEBUG BULLSHIT
            if (DisplayDebug)
            {
                TimeSpan SimulationElapsedTime = DateTime.Now.Subtract(SimulationClock);
                ReversiForm.reportDebugMessage("(" + SimulationElapsedTime.ToString() + ") Database Stats Reset (" + LeafNodes.Count + " leaf nodes queued)", updateConsole: true);
            }
            /////////////////////////////////////////////////////////////

            SimulationNode CurrentLeafNode;
            String CurrentWorkNodeID;
            int WinningColor;

            while (LeafNodes.Count > 0)
            {
                // If the BackgroundWorker.CancellationPending property is true, cancel
                if (WorkerThread.CancellationPending)
                {
                    ReversiForm.reportDebugMessage("#####Database Analysis has been cancelled#####");
                    break;
                }

                // Grab the next leaf node from the leaf queue
                CurrentLeafNode = NodeMasterList[LeafNodes.Dequeue()];

                // Update the game board visual
                if (VisualizeResults)
                    ReversiForm.RefreshPieces(CurrentLeafNode.getGameBoard());

                // Find who the winner of the leaf node is
                if (CurrentLeafNode.getGameBoard().FindScore(ReversiApplication.WHITE) > CurrentLeafNode.getGameBoard().FindScore(ReversiApplication.BLACK))
                    WinningColor = ReversiApplication.WHITE;
                else if (CurrentLeafNode.getGameBoard().FindScore(ReversiApplication.WHITE) < CurrentLeafNode.getGameBoard().FindScore(ReversiApplication.BLACK))
                    WinningColor = ReversiApplication.BLACK;
                else
                    WinningColor = -1;

                // If this is a tie, there is no reason to process it
                if ((WinningColor == ReversiApplication.BLACK) || (WinningColor == ReversiApplication.WHITE))
                {
                    // Seed the work list with the leaf
                    WorkNodes.Enqueue(LeafNodes.Dequeue());

                    while (WorkNodes.Count > 0)
                    {
                        CurrentWorkNodeID = WorkNodes.Dequeue();

                        if (WinningColor == ReversiApplication.BLACK)
                            NodeMasterList[CurrentWorkNodeID].setBlackWins(NodeMasterList[CurrentWorkNodeID].getBlackWins() + 1);
                        else
                            NodeMasterList[CurrentWorkNodeID].setWhiteWins(NodeMasterList[CurrentWorkNodeID].getWhiteWins() + 1);

                        foreach (String ParentNode in NodeMasterList[CurrentWorkNodeID].getParentNodes())
                            WorkNodes.Enqueue(ParentNode);
                    }
                }
            }

            /////////////////////////////////////////////////////////////
            //DEBUG BULLSHIT
            if (DisplayDebug)
            {
                TimeSpan SimulationElapsedTime = DateTime.Now.Subtract(SimulationClock);
                ReversiForm.reportDebugMessage("===============================\nAI DB Analysis Complete\nSimulation Time: " + SimulationElapsedTime.ToString() + "\n\n", updateConsole: true);
            }
            /////////////////////////////////////////////////////////////
        }
    }
}