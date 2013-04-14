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
    // Class:       AI
    // Description: Stores the game simulation code used by the AI opponent to play the game
    public class AI
    {
        //private string AIDebug = "";
        private int color;

        private Dictionary<string, int> BlackMoves = new Dictionary<string, int>();

        private Dictionary<string, SimulationNode> NodeMasterList = new Dictionary<string, SimulationNode>();

        private Queue<String> WorkNodes = new Queue<String>();
        private Queue<String> LeafNodes = new Queue<String>();

        private int SimulationCycles = 0;
        //private int SimulationDepth = 0;
        //private int WinnerTotal = 0;
        private int WhiteWinnerTotal = 0;
        private int BlackWinnerTotal = 0;
        private int TieTotal = 0;
        //private int LoserTotal = 0;
        private int LeafTotal = 0;

        private bool ProcessingTurn = false;

        private Point NextMove = new Point(-1, -1);

        // This is an attempt to rate the value of each spot on the board
        private int[,] BoardValueMask = new int[,]
            {
	            {5,2,4,4,4,4,2,5},
   	            {2,0,1,1,1,1,0,2},
   	            {4,1,3,2,2,3,1,4},
   	            {4,1,2,0,0,2,1,4},
   	            {4,1,2,0,0,2,1,4},
   	            {4,1,3,2,2,3,1,4},
   	            {2,0,1,1,1,1,0,2},
	            {5,2,4,4,4,4,2,5}
            };

        public AI(int AIcolor)
        {
            color = AIcolor;
        }

        #region Getters and Setters

        public int getColor() { return color; }
        public int getNodeMasterListCount() { return NodeMasterList.Count; }
        public int getWorkNodeCount() { return WorkNodes.Count; }
        public int getLeafTotal() { return LeafTotal; }

        #endregion

        // Return a point representing the best possible next move for this AI
        public Point DetermineNextMove(Game SourceGame)
        {
            ProcessingTurn = true;

            AnalyzeBoard(SourceGame);
            //gAITurnWorker.RunWorkerAsync();

            while (ProcessingTurn) ;

            return NextMove;
        }

        // Determine the best move possible for the given game
        public void AnalyzeBoard(Game SourceGame)
        {
            Point[] PossibleMoves = SourceGame.getGameBoard().AvailableMoves(SourceGame.getCurrentTurn());

            if (PossibleMoves.Length < 1)
            {
                NextMove = new Point(-1, -1);
            }

            Point ChosenMove = PossibleMoves[0];
            Board SimBoard = new Board(ReversiForm.getCurrentGame().getGameBoard());
            double CurrentWeight, BestWeight = 0;

            //AIDebug += "\nPossible Moves:\n";
            foreach (Point CurrentPoint in PossibleMoves)
            {
                //AIDebug += "(" + CurrentPoint.X + "," + CurrentPoint.Y + ") Weight=" + BoardValueMask[CurrentPoint.X, CurrentPoint.Y] + "\n";
                SimBoard.CopyBoard(ReversiForm.getCurrentGame().getGameBoard());

                SimBoard.PutPiece(CurrentPoint.X, CurrentPoint.Y, SourceGame.getCurrentTurn());

                CurrentWeight = EvaluatePotentialMove(SimBoard, SourceGame.getCurrentTurn());

                if (CurrentWeight > BestWeight)
                    ChosenMove = CurrentPoint;
            }

            NextMove = ChosenMove;
            ProcessingTurn = false;
        }

        private double WeightMove(int X, int Y, int SimulationDepth, int WeightOverride = 0)
        {
            double SimStep = Math.Floor((double)SimulationDepth / 2);
            double MaxStep = Math.Floor((double)Properties.Settings.Default.MaxDepth / 2);

            return ((SimulationDepth % 2 == 0 ? -1 : 1) * (WeightOverride != 0 ? WeightOverride : BoardValueMask[X, Y]) * ((MaxStep - SimStep) / MaxStep));
        }

        private double EvaluatePotentialMove(Board CurrentBoard, int Turn, int SimulationDepth = 1)
        {
            /*if (SimulationCycles % 5000 == 0)
                Console.WriteLine("Simuldation Depth:" + SimulationDepth);*/

            if (SimulationDepth >= Properties.Settings.Default.MaxDepth)
            {
                return (0);
            }
            // If there are still moves left for the current player, start a new simulation for each of them
            else if (CurrentBoard.MovePossible(Turn))
            {
                Point[] PossibleMoves = CurrentBoard.AvailableMoves(Turn);
                double TotalWeight = 0;
                Board SimulationBoard;

                for (int lc = 0; lc < PossibleMoves.Length; lc++)
                {
                    // Make a copy of the current board
                    SimulationBoard = new Board(CurrentBoard);

                    // Place the current move on the new board
                    SimulationBoard.PutPiece(PossibleMoves[lc].X, PossibleMoves[lc].Y, Turn);

                    // Start a simulation for the next player with the updated board
                    TotalWeight += WeightMove(PossibleMoves[lc].X, PossibleMoves[lc].Y, SimulationDepth) + EvaluatePotentialMove(SimulationBoard, Turn == Properties.Settings.Default.WHITE ? Properties.Settings.Default.BLACK : Properties.Settings.Default.WHITE, SimulationDepth + 1);
                }
                return (TotalWeight);
            }
            // If there are no more moves for the current player, but the game is not over, start a new simulation for the other player
            else if (CurrentBoard.MovePossible(Turn == Properties.Settings.Default.WHITE ? Properties.Settings.Default.BLACK : Properties.Settings.Default.WHITE))
            {
                //Console.WriteLine( (Turn == Properties.Settings.Default.WHITE ? "White" : "Black") + " cannot move, passing");

                return (EvaluatePotentialMove(CurrentBoard, Turn == Properties.Settings.Default.WHITE ? Properties.Settings.Default.BLACK : Properties.Settings.Default.WHITE, SimulationDepth + 1));
            }
            // If there are no moves left in the game, collapse the simulation
            else
            {
                if (CurrentBoard.FindScore(color) > CurrentBoard.FindScore(color == Properties.Settings.Default.WHITE ? Properties.Settings.Default.BLACK : Properties.Settings.Default.WHITE))
                {
                    return (WeightMove(-1, -1, SimulationDepth, Properties.Settings.Default.VictoryWeight));
                }
                else
                {
                    return (WeightMove(-1, -1, SimulationDepth, Properties.Settings.Default.VictoryWeight * -1));
                }
            }
        }

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
            int ParentTurn = Properties.Settings.Default.WHITE;
            SimulationNode ParentNode = new SimulationNode(new Board(BoardSize), ParentTurn);
            String ParentNodeID = ParentNode.NodeID;
            String RootNodeID = ParentNode.NodeID;

            int ChildTurn = Properties.Settings.Default.BLACK;
            //String ChildNodeID;
            Board ChildBoard;
            SimulationNode ChildNode;

            // Seed the master node list with the the root node that contains the default game positions and settings
            NodeMasterList.Add(RootNodeID, ParentNode);

            // Seed the work list with the root node
            WorkNodes.Enqueue(RootNodeID);

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
                ChildTurn = (ParentNode.Turn == Properties.Settings.Default.WHITE) ? Properties.Settings.Default.BLACK : Properties.Settings.Default.WHITE;

                //if (NodeMasterList.Count % 10 == 0)
                //Console.WriteLine("Turn " + (ParentNode.Turn == Properties.Settings.Default.WHITE ? "White" : "Black") + "\nScore: B-" + ParentNode.Board.FindScore(Properties.Settings.Default.BLACK) + " W-" + ParentNode.Board.FindScore(Properties.Settings.Default.WHITE)  + "\n======================\n" + ParentNode.Board.ToString() );

                // Update the game board visual
                if (VisualizeResults)
                    ReversiForm.RefreshPieces(ParentNode.GameBoard);

                if (ParentNode.AvailableMoves.Length == 0)
                {
                    ParentNode.isPassTurn = true;

                    ChildNode = new SimulationNode(ParentNode.GameBoard, ChildTurn);

                    if (ChildNode.AvailableMoves.Length > 0)
                    {

                        if (NodeMasterList.ContainsKey(ChildNode.NodeID))
                        {
                            // Since the node already exists, just add the current parent to it's parent node list
                            if (!NodeMasterList[ChildNode.NodeID].ContainsParent(ParentNode.NodeID))
                                NodeMasterList[ChildNode.NodeID].AddParentNode(ParentNode.NodeID);
                        }
                        else
                        {
                            // Add the new node to the master list
                            NodeMasterList.Add(ChildNode.NodeID, ChildNode);

                            // Add the new node to the work list for eventual processing
                            WorkNodes.Enqueue(ChildNode.NodeID);
                        }

                        // Add this child to the parent's child node list
                        if (!ParentNode.ContainsChild(ChildNode.NodeID))
                            ParentNode.AddChildNode(ChildNode.NodeID);

                        // Add the new node to the work list for eventual processing
                        WorkNodes.Enqueue(ChildNode.NodeID);
                    }
                    else
                    {
                        ParentNode.isLeaf = true;
                        LeafTotal++;

                        if (ParentNode.GameBoard.FindScore(Properties.Settings.Default.BLACK) > ParentNode.GameBoard.FindScore(Properties.Settings.Default.WHITE))
                        {
                            ParentNode.BlackWins++;
                            BlackWinnerTotal++;
                        }
                        else if (ParentNode.GameBoard.FindScore(Properties.Settings.Default.BLACK) < ParentNode.GameBoard.FindScore(Properties.Settings.Default.WHITE))
                        {
                            ParentNode.WhiteWins++;
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
                    foreach (Point CurrentMove in ParentNode.AvailableMoves)
                    {
                        ChildBoard = new Board(ParentNode.GameBoard);
                        ChildBoard.PutPiece(CurrentMove.X, CurrentMove.Y, ChildTurn);
                        ChildNode = new SimulationNode(ChildBoard, ChildTurn);

                        // Add this child to the parent's child node list
                        if (!ParentNode.ContainsChild(ChildNode.NodeID))
                            ParentNode.AddChildNode(ChildNode.NodeID);

                        if (NodeMasterList.ContainsKey(ChildNode.NodeID))
                        {
                            // Since the node already exists, just add the current parent to it's parent node list
                            if (!NodeMasterList[ChildNode.NodeID].ContainsParent(ParentNode.NodeID))
                                NodeMasterList[ChildNode.NodeID].AddParentNode(ParentNode.NodeID);
                        }
                        else
                        {
                            // Add the new node to the master list
                            NodeMasterList.Add(ChildNode.NodeID, ChildNode);

                            // Add the new node to the work list for eventual processing
                            WorkNodes.Enqueue(ChildNode.NodeID);
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
                if (NodeMasterList[CurrentNodeID].isLeaf)
                    LeafNodes.Enqueue(NodeMasterList[CurrentNodeID].NodeID);

                NodeMasterList[CurrentNodeID].BlackWins = 0;
                NodeMasterList[CurrentNodeID].WhiteWins = 0;
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
                    ReversiForm.RefreshPieces( CurrentLeafNode.GameBoard );

                // Find who the winner of the leaf node is
                if (CurrentLeafNode.GameBoard.FindScore(Properties.Settings.Default.WHITE) > CurrentLeafNode.GameBoard.FindScore(Properties.Settings.Default.BLACK))
                    WinningColor = Properties.Settings.Default.WHITE;
                else if (CurrentLeafNode.GameBoard.FindScore(Properties.Settings.Default.WHITE) < CurrentLeafNode.GameBoard.FindScore(Properties.Settings.Default.BLACK))
                    WinningColor = Properties.Settings.Default.BLACK;
                else
                    WinningColor = -1;

                // If this is a tie, there is no reason to process it
                if ((WinningColor == Properties.Settings.Default.BLACK) || (WinningColor == Properties.Settings.Default.WHITE))
                {
                    // Seed the work list with the leaf
                    WorkNodes.Enqueue(LeafNodes.Dequeue());

                    while (WorkNodes.Count > 0)
                    {
                        CurrentWorkNodeID = WorkNodes.Dequeue();

                        if (WinningColor == Properties.Settings.Default.BLACK)
                            NodeMasterList[CurrentWorkNodeID].BlackWins++;
                        else
                            NodeMasterList[CurrentWorkNodeID].WhiteWins++;

                        foreach (String ParentNode in NodeMasterList[CurrentWorkNodeID].ParentNodes)
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