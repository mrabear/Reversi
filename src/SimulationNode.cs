// Reversi
// Brian Hebert
//

using System;
using System.Drawing;
using System.Collections.Generic;

namespace Reversi
{
    // Represents a sinlge game state with N-number of connections to and from the tree of all possible game states
    public class SimulationNode
    {
        public String NodeID;                 // The unique identifier of this node

        public Point[] AvailableMoves;         // The list available moves that haven't been simulated yet

        public Boolean isLeaf;                 // TRUE if the node represnts a game end state
        public Boolean isTrunk;                // TRUE if the node is the initial game starting position
        public Boolean isPassTurn;             // TRUE if the node represents a game board where the current player has to pass

        public Board GameBoard;              // The board state that this node was generated from

        public int Turn;                   // The player who is moving in this node

        public int WhiteWins;              // The potential number of White victory states that this node can lead to
        public int BlackWins;              // The potential number of Black victory states that this node can lead to

        public List<String> ChildNodes;      // The list of game nodes that can be created from the current one (i.e. player moves from the current state to one of the children)
        public List<String> ParentNodes;     // The list of game nodes that can create the current one (i.e. player moves from one of the parent states to the current state)

        public SimulationNode(Board SourceBoard, int SourceTurn, Boolean SetTrunk = false, Boolean SetLeaf = false)
        {
            // Initialize variable defaults
            this.Initialize();

            // Map constructor inputs to variables
            Turn = SourceTurn;
            GameBoard = new Board(SourceBoard);
            isTrunk = SetTrunk;
            isLeaf = SetLeaf;

            // Generate a list of all possible moves for the given player
            AvailableMoves = GameBoard.AvailableMoves(Turn);

            // Generate a unique ID for the node
            NodeID = GameBoard.GetID(Turn);
        }

        public void AddParentNode(String NodeID)
        {
            ParentNodes.Add(NodeID);
        }

        public void AddChildNode(String NodeID)
        {
            ChildNodes.Add(NodeID);
        }

        public Boolean ContainsChild(String NodeID)
        {
            return (ChildNodes.Contains(NodeID));
        }

        public Boolean ContainsParent(String NodeID)
        {
            return (ParentNodes.Contains(NodeID));
        }

        public void ClearMoves()
        {
            AvailableMoves = new Point[0];
        }

        public void Initialize()
        {
            BlackWins = 0;
            WhiteWins = 0;
            ChildNodes = new List<String>();
            ParentNodes = new List<String>();
            isLeaf = false;
            isTrunk = false;
        }
    }
}