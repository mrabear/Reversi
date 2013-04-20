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
        private String NodeID;              // The unique identifier of this node

        private Point[] AvailableMoves;     // The list available moves that haven't been simulated yet

        private Boolean isLeaf;             // TRUE if the node represnts a game end state
        private Boolean isTrunk;            // TRUE if the node is the initial game starting position
        private Boolean isPassTurn;         // TRUE if the node represents a game board where the current player has to pass

        private Board GameBoard;            // The board state that this node was generated from

        private int Turn;                   // The player who is moving in this node

        private int WhiteWins;              // The potential number of White victory states that this node can lead to
        private int BlackWins;              // The potential number of Black victory states that this node can lead to

        private List<String> ChildNodes;    // The list of game nodes that can be created from the current one (i.e. player moves from the current state to one of the children)
        private List<String> ParentNodes;   // The list of game nodes that can create the current one (i.e. player moves from one of the parent states to the current state)

        /// <summary>
        /// Creates a new SimulationNode instance
        /// </summary>
        /// <param name="SourceBoard">The game board to build this node with</param>
        /// <param name="SourceTurn">The turn that this node represents</param>
        /// <param name="SetTrunk">(optional) set to true if this is the first element</param>
        /// <param name="SetLeaf">(optional) set to true if this is a victory state</param>
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

        #region Getters and Setters

        public String getNodeID() { return NodeID; }
        public int getTurn() { return Turn; }
        public Board getGameBoard() { return GameBoard; }
        public Point[] getAvailableMoves() { return AvailableMoves; }
        public int getBlackWins() { return BlackWins; }
        public void setBlackWins(int newBlackWins) { BlackWins = newBlackWins; }
        public int getWhiteWins() { return WhiteWins; }
        public void setWhiteWins(int newWhiteWins) { WhiteWins = newWhiteWins; }
        public Boolean getIsLeaf() { return isLeaf; }
        public void setIsLeaf(Boolean newLeaf) { isLeaf = newLeaf; }
        public List<String> getParentNodes() { return ParentNodes; }
        public void setIsPassTurn(Boolean newPassTurn) { isPassTurn = newPassTurn; }

        #endregion

        /// <summary>
        /// Adds the given node to the parent node list
        /// </summary>
        /// <param name="NodeID">The node to add</param>
        public void AddParentNode(String NodeID)
        {
            ParentNodes.Add(NodeID);
        }

        /// <summary>
        /// Adds the given node to the child node list
        /// </summary>
        /// <param name="NodeID">The node to add</param>
        public void AddChildNode(String NodeID)
        {
            ChildNodes.Add(NodeID);
        }

        /// <summary>
        /// Returns true if the given node is a child of this node
        /// </summary>
        /// <param name="NodeID">The child node to check</param>
        /// <returns>True if the given node is a child of this node</returns>
        public Boolean ContainsChild(String NodeID)
        {
            return (ChildNodes.Contains(NodeID));
        }

        /// <summary>
        /// Returns true if the given node is a parent of this node
        /// </summary>
        /// <param name="NodeID">The child node to check</param>
        /// <returns>True if the given node is a parent of this node</returns>
        public Boolean ContainsParent(String NodeID)
        {
            return (ParentNodes.Contains(NodeID));
        }

        /// <summary>
        /// Resets the list of available moves
        /// </summary>
        public void ClearMoves()
        {
            AvailableMoves = new Point[0];
        }

        /// <summary>
        /// Initializes the basic object variables
        /// </summary>
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