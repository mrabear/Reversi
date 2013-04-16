// Reversi
// Brian Hebert
//

using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;

namespace Reversi
{

    // Class:       AI
    // Description: Stores the game simulation code used by the AI opponent to play the game
    public class AI
    {
        // Color constants
        private static int BLACK  = Properties.Settings.Default.BLACK;
        private static int WHITE  = Properties.Settings.Default.WHITE;
        private static int EMPTY  = Properties.Settings.Default.EMPTY;
        private static int YELLOW = Properties.Settings.Default.YELLOW;
        private static int RED    = Properties.Settings.Default.RED;
        private static int GREEN  = Properties.Settings.Default.GREEN;

        private Dictionary<string, int> BlackMoves = new Dictionary<string, int>();

        private Dictionary<string, SimulationNode> NodeMasterList = new Dictionary<string, SimulationNode>();
        
        private Queue<String> WorkNodes = new Queue<String>();
        private Queue<String> LeafNodes = new Queue<String>();

        private int SimulationCycles = 0;
        private int WhiteWinnerTotal = 0;
        private int BlackWinnerTotal = 0;
        private int TieTotal = 0;
        private int LeafTotal = 0;
        private int color;

        private bool ProcessingTurn = false;

        private Point NextMove = new Point(-1, -1);

        // This is an attempt to rate the value of each spot on the board
        private int[,] BoardValueMask = new int[,]
            {
	            {9,2,5,5,5,5,2,9},
   	            {2,0,0,0,0,0,0,2},
   	            {5,0,3,1,1,3,0,5},
   	            {5,0,1,0,0,1,0,5},
   	            {5,0,1,0,0,1,0,5},
   	            {5,0,3,1,1,3,0,6},
   	            {2,0,0,0,0,0,0,2},
	            {9,2,5,5,5,5,2,9}
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
            Board SimBoard = new Board(SourceGame.getGameBoard());
            Dictionary<Point, double> MoveResults = new Dictionary<Point, double>();

            foreach (Point CurrentPoint in PossibleMoves)
                ReversiForm.DrawPiece(YELLOW, CurrentPoint.X, CurrentPoint.Y);

            Thread.Sleep(2000);

            Console.WriteLine("#### New Turn Analysis ####\nCurrentBoard:\n" + SimBoard.ToString() + "" );

            Parallel.ForEach(PossibleMoves, CurrentPoint =>
            {
                SimBoard.CopyBoard(SourceGame.getGameBoard());
                SimBoard.PutPiece(CurrentPoint.X, CurrentPoint.Y, SourceGame.getCurrentTurn());

                double[] EvalResult = new double[Properties.Settings.Default.MaxDepth];

                EvaluatePotentialMove(ref EvalResult, SimBoard, SourceGame.getCurrentTurn());

                // Serializes the theads to make sure the update functions properly
                lock (this)
                {
                    Console.WriteLine(" Depth| Sign * Value *  Weight  =  Score");
                    Console.WriteLine("|------------------------------------------|");

                    double MoveWeight = WeightMove(EvalResult);

                    Console.WriteLine("|------------------------------------------|");

                    MoveResults.Add(CurrentPoint, MoveWeight);
                    Console.WriteLine("\t Point (" + CurrentPoint.X + "," + CurrentPoint.Y + ")\t\tscore=" + MoveWeight + "\n");
                }
            } );

            foreach(Point ResultMove in MoveResults.Keys)
                if (MoveResults[ResultMove] > MoveResults[ChosenMove])
                    ChosenMove = ResultMove;

            Console.WriteLine("Point (" + ChosenMove.X + "," + ChosenMove.Y + ") Chosen\n");

            ReversiForm.ClearBoardPieces(PossibleMoves);

            Thread.Sleep(1000);
            NextMove = ChosenMove;
            ProcessingTurn = false;
        }

        private double WeightMove(double[] BandedWeightTable)
        {
            double CurrentStep, MaxStep = Math.Floor((double)Properties.Settings.Default.MaxDepth / 2);
            double Penalty, SubTotal, WeightedTotal = 0;
            int Sign;

            for (int SimDepth = 0; SimDepth < BandedWeightTable.Length ; SimDepth++)
            {
                // Current weighted tier 
                CurrentStep = Math.Floor((double)SimDepth / 2);

                // The penalty to assign to this simulation depth
                //Penalty = (( MaxStep - CurrentStep ) / Math.Pow( MaxStep, 2 ));

                // 0.7*e^(-0.293*x)
                Penalty = ( CurrentStep == 0 ) ? 1 : 0.5 * Math.Exp(-1 * CurrentStep);

                // The sign to apply to this analysis (+ for beneficial moves, - for opponent moves)
                Sign = (SimDepth % 2 == 0 ? 1 : -1);

                // The average of all of the values for the current simulation depth
                //Average = (BandedWeightTable[SimDepth].TotalWeight / BandedWeightTable[SimDepth].NodeCount);
                SubTotal = Sign * BandedWeightTable[SimDepth] * Penalty;

                // The end calculation
                WeightedTotal += SubTotal;

                Console.WriteLine(String.Format("|  " + SimDepth.ToString().PadLeft(2) + " | " + Sign.ToString().PadLeft(3) + "  *" + BandedWeightTable[SimDepth].ToString().PadLeft(4) + "   *" + Penalty.ToString("0.00000").PadLeft(9) + " =" + SubTotal.ToString("0.00000").PadLeft(9)) + " |");
            }

            return (WeightedTotal);
        }

        public class BandedWeightRow
        {
            public int NodeCount;
            public double TotalWeight;

            public BandedWeightRow()
            {
                NodeCount = 0;
                TotalWeight = 0;
            }

            public BandedWeightRow(double NewWeight)
            {
                NodeCount = 1;
                TotalWeight = NewWeight;
            }

            public BandedWeightRow(int NewWeight)
            {
                NodeCount = 1;
                TotalWeight = NewWeight;
            }           
        }

        private double ScoreMove(Board CurrentBoard, Point NewPiece)
        {
            return BoardValueMask[NewPiece.X, NewPiece.Y];
        }

        private void EvaluatePotentialMove(ref double[] BandedWeightTable,Board CurrentBoard, int Turn, int SimulationDepth = 0)
        {
            if (SimulationDepth < Properties.Settings.Default.MaxDepth)
            {
                // If there are still moves left for the current player, start a new simulation for each of them
                if (CurrentBoard.MovePossible(Turn))
                {
                    Point[] PossibleMoves = CurrentBoard.AvailableMoves(Turn);
                    Board SimulationBoard;
                    Point BestPoint = PossibleMoves[0];

                    // If it is the opponents turn only pick their best moves
                    if ( (SimulationDepth % 2 != 0) )
                        foreach (Point CurrentPoint in PossibleMoves)
                            if (ScoreMove( CurrentBoard, CurrentPoint ) > ScoreMove( CurrentBoard, BestPoint ))
                                BestPoint = CurrentPoint;

                    for (int index = 0; index < PossibleMoves.Length; index++)
                    {
                        if (ScoreMove(CurrentBoard, PossibleMoves[index]) >= ScoreMove( CurrentBoard, BestPoint ))
                        {
                            // Make a copy of the current board
                            SimulationBoard = new Board(CurrentBoard);

                            // Place the current move on the new board
                            SimulationBoard.PutPiece(PossibleMoves[index].X, PossibleMoves[index].Y, Turn);

                            if (ScoreMove(CurrentBoard, PossibleMoves[index]) > BandedWeightTable[SimulationDepth])
                                BandedWeightTable[SimulationDepth] = ScoreMove(CurrentBoard, PossibleMoves[index]);

                            // Start a simulation for the next player with the updated board
                            EvaluatePotentialMove(ref BandedWeightTable, SimulationBoard, Turn == WHITE ? BLACK : WHITE, SimulationDepth + 1);
                        }
                    }
                }
                // If there are no more moves for the current player, but the game is not over, start a new simulation for the other player
                else if (CurrentBoard.MovePossible(Turn == WHITE ? BLACK : WHITE))
                {
                    EvaluatePotentialMove(ref BandedWeightTable, CurrentBoard, Turn == WHITE ? BLACK : WHITE, SimulationDepth + 1);
                }
                // If there are no moves left in the game, collapse the simulation
                else
                {
                    if (CurrentBoard.FindScore(color) > CurrentBoard.FindScore(color == WHITE ? BLACK : WHITE))
                        BandedWeightTable[SimulationDepth] = Properties.Settings.Default.VictoryWeight;
                    else
                        BandedWeightTable[SimulationDepth] = Properties.Settings.Default.VictoryWeight * -1;
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

        // Infiniate list iterator for the parallel foreach loop
        private static IEnumerable<bool> IterateUntilFalse(Func<bool> condition)
        {
            while (condition()) yield return true;
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
            int ParentTurn = WHITE;
            SimulationNode ParentNode = new SimulationNode(new Board(BoardSize), ParentTurn);
            String ParentNodeID = ParentNode.getNodeID();
            String RootNodeID = ParentNode.getNodeID();

            int ChildTurn = BLACK;
            //String ChildNodeID;
            Board ChildBoard;
            SimulationNode ChildNode;

            // Seed the master node list with the the root node that contains the default game positions and settings
            NodeMasterList.Add(RootNodeID, ParentNode);

            // Seed the work list with the root node
            WorkNodes.Enqueue(RootNodeID);

            //Parallel.ForEach(IterateUntilFalse(WorkNodes.Count > 0), =>
            while(WorkNodes.Count > 0)
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
                ChildTurn = (ParentNode.getTurn() == WHITE) ? BLACK : WHITE;

                //if (NodeMasterList.Count % 10 == 0)
                //Console.WriteLine("Turn " + (ParentNode.Turn == WHITE ? "White" : "Black") + "\nScore: B-" + ParentNode.Board.FindScore(BLACK) + " W-" + ParentNode.Board.FindScore(WHITE)  + "\n======================\n" + ParentNode.Board.ToString() );

                // Update the game board visual
                if (VisualizeResults)
                    ReversiForm.RefreshPieces(ParentNode.getGameBoard());

                if (ParentNode.getAvailableMoves().Length == 0)
                {
                    ParentNode.setIsPassTurn( true );

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
                        ParentNode.setIsLeaf( true );
                        LeafTotal++;

                        if (ParentNode.getGameBoard().FindScore(BLACK) > ParentNode.getGameBoard().FindScore(WHITE))
                        {
                            ParentNode.setBlackWins( ParentNode.getBlackWins() + 1 );
                            BlackWinnerTotal++;
                        }
                        else if (ParentNode.getGameBoard().FindScore(BLACK) < ParentNode.getGameBoard().FindScore(WHITE))
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

                NodeMasterList[CurrentNodeID].setBlackWins( 0 );
                NodeMasterList[CurrentNodeID].setWhiteWins( 0 );
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
                if (CurrentLeafNode.getGameBoard().FindScore(WHITE) > CurrentLeafNode.getGameBoard().FindScore(BLACK))
                    WinningColor = WHITE;
                else if (CurrentLeafNode.getGameBoard().FindScore(WHITE) < CurrentLeafNode.getGameBoard().FindScore(BLACK))
                    WinningColor = BLACK;
                else
                    WinningColor = -1;

                // If this is a tie, there is no reason to process it
                if ((WinningColor == BLACK) || (WinningColor == WHITE))
                {
                    // Seed the work list with the leaf
                    WorkNodes.Enqueue(LeafNodes.Dequeue());

                    while (WorkNodes.Count > 0)
                    {
                        CurrentWorkNodeID = WorkNodes.Dequeue();

                        if (WinningColor == BLACK)
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