// Reversi
// Brian Hebert
//

using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Reversi
{
    public class AIDatabase
    {
        private Dictionary<string, int> BlackMoves = new Dictionary<string, int>();

        private Dictionary<string, SimulationNode> NodeMasterList = new Dictionary<string, SimulationNode>();

        private Queue<String> WorkNodes = new Queue<String>();
        private Queue<String> LeafNodes = new Queue<String>();

        private int SimulationCycles = 0;
        private int WhiteWinnerTotal = 0;
        private int BlackWinnerTotal = 0;
        private int TieTotal = 0;
        private int LeafTotal = 0;

        #region Getters and Setters

        public int getNodeMasterListCount() { return NodeMasterList.Count; }
        public int getWorkNodeCount() { return WorkNodes.Count; }
        public int getLeafTotal() { return LeafTotal; }

        #endregion

        // Returns a string representing the current database information
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

        // Infiniate list iterator for the parallel foreach loop
        private static IEnumerable<bool> IterateUntilFalse(Func<bool> condition)
        {
            while (condition()) yield return true;
        }

        // Builds the database of all potential game states
        public void BuildAIDatabase(BackgroundWorker WorkerThread, int BoardSize = 8, Boolean VisualizeResults = false, Boolean DisplayDebug = true)
        {
            /////////////////////////////////////////////////////////////
            //DEBUG BULLSHIT
            /////////////////////////////////////////////////////////////
            //SimulationDepth = 0;
            SimulationCycles = 0;

            DateTime SimulationClock = DateTime.Now;

            if (DisplayDebug)
            {
                Console.WriteLine("===============================\nBuilding AI Database (" + SimulationClock.ToLocalTime() + ")");
                WorkerThread.ReportProgress(Convert.ToInt32(DateTime.Now.Subtract(SimulationClock).Ticks), "===============================\nBuilding AI Database (" + SimulationClock.ToLocalTime() + ")\n");
            }
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

                if (DisplayDebug)
                {
                    //if (NodeMasterList.Count % 25000 == 0)
                    //    Console.WriteLine("(" + NodeMasterList.Count + ") (" + WorkNodes.Count + " queued) (" + LeafTotal + " end states)"); 
                }

                // If the BackgroundWorker.CancellationPending property is true, cancel
                if (WorkerThread.CancellationPending)
                {
                    Console.WriteLine("#####Database Build has been cancelled#####");
                    break;
                }

                if (SimulationCycles % 75 == 0)
                    WorkerThread.ReportProgress(Convert.ToInt32(DateTime.Now.Subtract(SimulationClock).Ticks), "");

                // Grab the next node ID off of the work queue
                ParentNodeID = WorkNodes.Dequeue();

                // Fetch the current game node from the master list
                ParentNode = NodeMasterList[ParentNodeID];

                // Set the child turn to be the next player
                ChildTurn = (ParentNode.getTurn() == ReversiApplication.WHITE) ? ReversiApplication.BLACK : ReversiApplication.WHITE;

                //if (NodeMasterList.Count % 10 == 0)
                //Console.WriteLine("Turn " + (ParentNode.Turn == WHITE ? "White" : "Black") + "\nScore: B-" + ParentNode.Board.FindScore(BLACK) + " W-" + ParentNode.Board.FindScore(WHITE)  + "\n======================\n" + ParentNode.Board.ToString() );

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
                        ChildBoard.PutPiece(CurrentMove.X, CurrentMove.Y, ChildTurn);
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
                TimeSpan SimulationElapsedTime = DateTime.Now.Subtract(SimulationClock);
                Console.WriteLine("===============================\nAI DB Build Complete\nSimulation Time: " + SimulationElapsedTime.ToString() + "\n\n" + DumpSimulationInfo());
                //gDebugTextBox.Text += "===============================\nAI DB Build Complete\nSimulation Time: " + SimulationElapsedTime.ToString() + "\n\n" + DumpSimulationInfo();
                WorkerThread.ReportProgress(Convert.ToInt32(DateTime.Now.Subtract(SimulationClock).Ticks), "===============================\nAI DB Build Complete\nSimulation Time: " + SimulationElapsedTime.ToString() + "\n\n" + DumpSimulationInfo());
            }
        }

        // Analyze the database that has been built by the AIDatabase method
        public void AnalyzeAIDatabase(BackgroundWorker WorkerThread, Boolean VisualizeResults = false, RichTextBox gDebugTextBox = null, Boolean DisplayDebug = true)
        {

            /////////////////////////////////////////////////////////////
            //DEBUG BULLSHIT
            /////////////////////////////////////////////////////////////
            DateTime SimulationClock = DateTime.Now;

            if (DisplayDebug)
            {
                Console.WriteLine("===============================\nAnalyzing AI Database (" + SimulationClock.ToLocalTime() + ")");
                // DebugTextBox.Text += "===============================\nAnalyzing AI Database (" + SimulationClock.ToLocalTime() + ")\n";
            }
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
            /////////////////////////////////////////////////////////////
            if (DisplayDebug)
            {
                TimeSpan SimulationElapsedTime = DateTime.Now.Subtract(SimulationClock);
                Console.WriteLine("(" + SimulationElapsedTime.ToString() + ") Database Stats Reset (" + LeafNodes.Count + " leaf nodes queued)");
                //gDebugTextBox.Text += "(" + SimulationElapsedTime.ToString() + ") Database Stats Reset (" + LeafNodes.Count + " leaf nodes queued)\n";
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
                    Console.WriteLine("#####Database Analysis has been cancelled#####");
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
            /////////////////////////////////////////////////////////////
            if (DisplayDebug)
            {
                TimeSpan SimulationElapsedTime = DateTime.Now.Subtract(SimulationClock);
                Console.WriteLine("===============================\nAI DB Analysis Complete\nSimulation Time: " + SimulationElapsedTime.ToString() + "\n\n");
                //gDebugTextBox.Text += "===============================\nAI DB Analysis Complete\nSimulation Time: " + SimulationElapsedTime.ToString() + "\n\n";
            }
            /////////////////////////////////////////////////////////////
        }
    }
}