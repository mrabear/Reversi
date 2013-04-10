using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Reversi
{
	/// <summary>
	/// Summary description for Form Reversi.
	/// </summary>
	/// 

	public class ReversiForm : System.Windows.Forms.Form
	{
		private class Board
		{
			private Piece[,] BoardPieces = new Piece[8,8];
			private Image BlackPieceImage;
			private Image WhitePieceImage;
			private Image NoPieceImage;

			public string DebugMovesAvailable;

			public Board( )
			{
				ClearBoard();
			    BlackPieceImage = Image.FromFile( @"C:\Documents and Settings\Brian Hebert\My Documents\Visual Studio Projects\Reversi\black_piece.bmp" );
			    WhitePieceImage = Image.FromFile( @"C:\Documents and Settings\Brian Hebert\My Documents\Visual Studio Projects\Reversi\white_piece.bmp" );
			    NoPieceImage = Image.FromFile( @"C:\Documents and Settings\Brian Hebert\My Documents\Visual Studio Projects\Reversi\no_piece.bmp" );
			}

			public Boolean InBounds( int x, int y )
			{
				if ( ( x > 7 ) || ( y > 7 ) || ( x < 0 ) || ( y < 0 ) ) 
				{
					return false;
				}
				else
				{
					return true;
				}
			}

			public void CopyBoard( Board SourceBoard )
			{
				for( int olc = 0; olc < 8; olc++ )
				{
					for( int ilc = 0; ilc < 8; ilc++ )
					{
						BoardPieces[ilc,olc] = new Piece( SourceBoard.PieceAt( ilc, olc ).color, SourceBoard.PieceAt( ilc, olc ).GetX(), SourceBoard.PieceAt( ilc, olc ).GetY() ) ;
					}				
				}
			}

			public string ToString()
			{
				string boardString = "";
				for( int olc = 0; olc < 8; olc++ )
				{	
					for( int ilc = 0; ilc < 8; ilc++ )
					{

						boardString += PieceAt( ilc, olc ).color;
					}
					boardString += "\n";
				}

				return boardString;
			}

			public Piece PieceAt( int x, int y )
			{	
				if( ( x < 0 ) || ( x > 7 ) || ( y < 0 ) || ( y > 7 ) )
				{
					return null;
				}
				return( BoardPieces[x,y] );
			}

			public Boolean MakeMove( Game CurrentGame, int x, int y, int color, Boolean MakeMove )
			{

				int CurrentTurn = color ;
				int NextTurn;
				if ( CurrentTurn == 1 )
				{
					NextTurn = 2;
				} 
				else 
				{
					NextTurn = 1;
				}

				// Check for already existing piece
				if( PieceAt( x, y ).color != 0 )
				{
					//DebugText.Text = "A piece is already on that space";
					return false;
				}

				Boolean findStatus = false;
				Boolean takeStatus = false;
			
				//DebugText.Text = "Current Game Turn: " + CurrentGame.CurrentTurn + "\nNext Game Turn: " + CurrentGame.NextTurn + "\n\n";
				for( int olc = Math.Max( y-1, 0 ) ; olc <= Math.Min( y+1, 7 ) ; olc++ )
				{
					for( int ilc = Math.Max( x-1, 0 ) ; ilc <= Math.Min( x+1, 7 ) ; ilc++ )
					{
						// If the piece is placed next to an enemy piece, track it
						if( PieceAt( ilc, olc ).color == ( NextTurn ) )
						{
							//DebugText.Text += "Found adjacent opponent piece at " + ilc + "," + olc + " (" + MainBoard.PieceAt( ilc, olc ).color + "=" + CurrentGame.NextTurn + ")\n";
						
							findStatus = true;

							int newX = ilc;
							int newY = olc;
							int dirX = ilc - x;
							int dirY = olc - y;

							Board TempBoard = new Board();
							TempBoard.CopyBoard( this );

							//DebugText.Text += "Direction X: " + dirX + "\nDirection Y: " + dirY + "\n    Start Trace From: " + newX + "," + newY + "\n";
							while( TempBoard.PieceAt( newX, newY ).color == NextTurn )
							{
								TempBoard.PutPiece( newX, newY, CurrentTurn );
								newX += dirX;
								newY += dirY;
								//DebugText.Text += "    Next piece is: " + newX + "," + newY + "\n";
							
								if ( !TempBoard.InBounds( newX, newY ) )
								{
									break;
								}
							}
							if ( TempBoard.InBounds( newX, newY ) )
							{
								if( TempBoard.PieceAt( newX, newY ).color == CurrentTurn )
								{
									//DebugText.Text += "\nPieces taken, mainboard updated\n";
									if ( MakeMove )
									{
										TempBoard.PutPiece( x, y, color );
										this.CopyBoard( TempBoard );
									}
									//MainBoard.RefreshPieces( BoardPicture.CreateGraphics() );
									takeStatus = true;
								}
							}
						} 
						else 
						{
							//DebugText.Text += "Checked " + ilc + "," + olc + " (color=" + MainBoard.PieceAt( ilc, olc ).color + ")\n";
						}
					}
				}
			
				if( ( !findStatus ) && ( MakeMove ) )
				{
					//DebugText.Text = "You must place your piece adjacent to an opponents piece.";
				}
				if( ( !takeStatus ) && ( MakeMove ) )
				{
					//DebugText.Text = "You must place capture at least one piece on each turn.";
				}

				return takeStatus;
			}

			public Point[] AvailableMoves( Game CurrentGame )
			{
				Point[] Moves = new Point[64];
				int foundMoves = 0;
				for( int olc = 0; olc < 8; olc++ )
				{	
					for( int ilc = 0; ilc < 8; ilc++ )
					{
						if( PieceAt( ilc, olc ).color == 0 )
						{
							if( MakeMove( CurrentGame, ilc, olc, CurrentGame.CurrentTurn, false ) )
							{
								Moves[ foundMoves ] = new Point( ilc, olc ); 
								foundMoves++;
							}
						}
					}
				}

				Point[] FinalMoves = new Point[ foundMoves ];
				for( int lc = 0 ; lc < FinalMoves.Length ; lc++ )
				{
					FinalMoves[ lc ] = Moves[ lc ];
				}

				return FinalMoves;				
			}

			public Boolean MovePossible( Game CurrentGame, int color )
			{
				for( int olc = 0; olc < 8; olc++ )
				{	
					for( int ilc = 0; ilc < 8; ilc++ )
					{
						if( PieceAt( ilc, olc ).color == 0 )
						{
							if( MakeMove( CurrentGame , ilc, olc, color, false ) )
							{
								DebugMovesAvailable = "(" + ilc + "," + olc + ")" ;
								return true;
							}
						}
					}
				}

				return false;
			}

			public void DrawPiece( int x, int y, Graphics BoardGfx )
			{
				Piece CurrentPiece = PieceAt( x, y );
				Image PieceImage;
				if( CurrentPiece.color == 1 )
				{
					PieceImage = WhitePieceImage;
				}
				else if( CurrentPiece.color == 2 )
				{
					PieceImage = BlackPieceImage;
				} 
				else
				{
					PieceImage = NoPieceImage;
				}

				BoardGfx.DrawImage( PieceImage, x * 40 + 1, y * 40 + 1, PieceImage.Width, PieceImage.Height );
			}

			public Piece PutPiece( int x, int y, int color )
			{
				if ( ( color == 1 ) || ( color == 2 ) || ( color == 0 ) )
				{
					BoardPieces[x,y] = new Piece( color, x, y );
					return( BoardPieces[x,y] );
				}

				return( null );
			}

			public void ClearBoard()
			{
				for( int olc = 0; olc < 8; olc++ )
				{
					for( int ilc = 0; ilc < 8; ilc++ )
					{
						BoardPieces[olc,ilc] = new Piece( 0 );
					}				
				}
				PutPiece( 3, 3, 1 );
				PutPiece( 4, 4, 1 );
				PutPiece( 3, 4, 2 );
				PutPiece( 4, 3, 2 );
			}

			public void RefreshPieces( Graphics g )
			{
				for( int olc = 0; olc < 8; olc++ )
				{	
					for( int ilc = 0; ilc < 8; ilc++ )
					{
						if( PieceAt( ilc, olc ).color != 0 )
						{
							DrawPiece( ilc, olc, g );
						}
					}
				}
			}

			public int FindScore( int color )
			{
				int score = 0;
				for( int olc = 0; olc < 8; olc++ )
				{	
					for( int ilc = 0; ilc < 8; ilc++ )
					{
						if( PieceAt( ilc, olc ).color == color )
						{
							score++;
						}
					}
				}
				return score ;
			}
			
		}

		private class AI
		{
			public string AIDebug = "";
			public int color;

			public AI( int AIcolor )
			{		
				color = AIcolor;	
			}

			public Point Move( Game CurrentGame )
			{
				AIDebug = "---------------------\nStarting AI Move Sequence:\nAI is " + 
					      CurrentGame.GetTurnString( color ) + "\n" +
					      "AI is set to difficulty level " + CurrentGame.Difficulty + "\n" +
					      "\nInherited Game Board:\n" + CurrentGame.GameBoard.ToString() + "\n";

				Point[] PossibleMoves = CurrentGame.GameBoard.AvailableMoves( CurrentGame );

				AIDebug += "\nPossible Moves:\n";
				for( int lc = 0 ; lc < PossibleMoves.Length ; lc++ )
				{
					AIDebug += "(" + PossibleMoves[ lc ].X + "," + PossibleMoves[ lc ].Y + ")\n";
				}
	
				AIDebug += "\nTotal Possible Moves: " + PossibleMoves.Length + "\n";
				if( PossibleMoves.Length < 1 )
				{
					return new Point( -1, -1 );
				}
				
				// This is just a gameplay hack to get by...all the AI is doing at this point is
				// gathering a list of all possible moves and then picking the first move off of
				// that list.  This line should be replaced with algorithims to determine which
				// of the available moves is best.
           		Point ChosenMove = PossibleMoves[0];

				AIDebug += "\nMove Chosen: (" + ChosenMove.X + "," + ChosenMove.Y + ")\n";

				return ChosenMove;
			}
		}

		private class Piece
		{
			
			public int color = new int();
			private int x = -1;
			private int y = -1;

			public Piece( int PieceColor )
			{
				// None  = 0
				// White = 1
				// Black = 2
				if ( PieceColor == 1 ) 
				{
					color = 1;
				} 
				else if ( PieceColor == 2 ) 
				{
					color = 2;
				} 
				else 
				{
					color = 0;
				}
			}

			public Piece( int PieceColor, int pX, int pY )
			{
				// None  = 0
				// White = 1
				// Black = 2
				if ( PieceColor == 1 ) 
				{
					color = 1;
				} 
				else if ( PieceColor == 2 ) 
				{
					color = 2;
				} 
				else 
				{
					color = 0;
				}
				x = pX;
				y = pY;
			}

			public int GetX() { return x; }
			public int GetY() { return y; }
		}

		private static Boolean PvC = false;
		private static int AIDifficulty = 1;

		private class Game
		{
			public int CurrentTurn;
			public int NextTurn;
			public int Difficulty;
			public Boolean VsComputer = false;
			public Board GameBoard;
			public int Winner;
			public Boolean IsComplete = false;
			public Boolean ProcessMoves = true;
			public AI AI;

			public Game()
			{
				CurrentTurn = 1;
				NextTurn = 2;
				Difficulty = AIDifficulty;
				VsComputer = PvC;
				GameBoard = new Board();
				IsComplete = false;
				AI = new AI( 2 );
			}

			public void SwitchTurn()
			{
				if( CurrentTurn == 1 ) 
				{
					CurrentTurn = 2;
					NextTurn = 1;
				} 
				else 
				{
					CurrentTurn = 1;
					NextTurn = 2;
				}
			}

			public string GetTurnString( int color )
			{
				if( color == 1 ) 
				{
					return( "White" );
				} 
				else if ( color == 2 )
				{
					return( "Black" );
				} 
				else 
				{
					return( "Illegal Color!" );
				}
			}
		}


		#region Form Designer Variables

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.Windows.Forms.Label Title;
		private System.Windows.Forms.Label DebugText;
		private System.Windows.Forms.PictureBox BoardPicture;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label TurnLabel;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem DiffMenu_Easy;
		private System.Windows.Forms.MenuItem DiffMenu_Normal;
		private System.Windows.Forms.MenuItem DiffMenu_Hard;
		private System.Windows.Forms.MenuItem DiffMenu_VeryHard;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.MenuItem PvPMenu;
		private System.Windows.Forms.MenuItem PvCMenu;
		private System.Windows.Forms.MenuItem ExitMenu;
		private System.Windows.Forms.MenuItem NewGameMenu;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem DebugSkip;
		private System.Windows.Forms.MenuItem DebugProcess;
		private System.Windows.Forms.Label ScoreText;
		private System.Windows.Forms.RichTextBox DebugAITrace;
		private System.Windows.Forms.Label AITraceLabel;
		#endregion
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem DebugScenario_NoWhite;
		private System.Windows.Forms.MenuItem DebugScenario_NoBlack;
		private System.Windows.Forms.MenuItem DebugScenario_TieGame;
		
		// The Global Game Object
		private Game CurrentGame;

		public ReversiForm()
		{
			InitializeComponent();
			
			CurrentGame = new Game();

			TurnLabel.Text = "Current Turn: " + CurrentGame.GetTurnString( CurrentGame.CurrentTurn ) + "\n" ;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ReversiForm));
			this.BoardPicture = new System.Windows.Forms.PictureBox();
			this.Title = new System.Windows.Forms.Label();
			this.DebugText = new System.Windows.Forms.Label();
			this.TurnLabel = new System.Windows.Forms.Label();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.NewGameMenu = new System.Windows.Forms.MenuItem();
			this.ExitMenu = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.PvPMenu = new System.Windows.Forms.MenuItem();
			this.PvCMenu = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.DiffMenu_Easy = new System.Windows.Forms.MenuItem();
			this.DiffMenu_Normal = new System.Windows.Forms.MenuItem();
			this.DiffMenu_Hard = new System.Windows.Forms.MenuItem();
			this.DiffMenu_VeryHard = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.DebugSkip = new System.Windows.Forms.MenuItem();
			this.DebugProcess = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.DebugScenario_NoWhite = new System.Windows.Forms.MenuItem();
			this.DebugScenario_NoBlack = new System.Windows.Forms.MenuItem();
			this.DebugScenario_TieGame = new System.Windows.Forms.MenuItem();
			this.ScoreText = new System.Windows.Forms.Label();
			this.DebugAITrace = new System.Windows.Forms.RichTextBox();
			this.AITraceLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// BoardPicture
			// 
			this.BoardPicture.Image = ((System.Drawing.Bitmap)(resources.GetObject("BoardPicture.Image")));
			this.BoardPicture.Location = new System.Drawing.Point(136, 56);
			this.BoardPicture.Name = "BoardPicture";
			this.BoardPicture.Size = new System.Drawing.Size(320, 320);
			this.BoardPicture.TabIndex = 0;
			this.BoardPicture.TabStop = false;
			this.BoardPicture.Paint += new System.Windows.Forms.PaintEventHandler(this.BoardPaint);
			this.BoardPicture.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BoardPicture_MouseUp);
			// 
			// Title
			// 
			this.Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Title.Location = new System.Drawing.Point(136, 16);
			this.Title.Name = "Title";
			this.Title.Size = new System.Drawing.Size(320, 32);
			this.Title.TabIndex = 1;
			this.Title.Text = "Reversi";
			this.Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.Title.Click += new System.EventHandler(this.Title_Click);
			// 
			// DebugText
			// 
			this.DebugText.Location = new System.Drawing.Point(8, 288);
			this.DebugText.Name = "DebugText";
			this.DebugText.Size = new System.Drawing.Size(112, 128);
			this.DebugText.TabIndex = 2;
			// 
			// TurnLabel
			// 
			this.TurnLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.TurnLabel.Location = new System.Drawing.Point(16, 48);
			this.TurnLabel.Name = "TurnLabel";
			this.TurnLabel.Size = new System.Drawing.Size(88, 112);
			this.TurnLabel.TabIndex = 3;
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1,
																					  this.menuItem2,
																					  this.menuItem3});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.NewGameMenu,
																					  this.ExitMenu});
			this.menuItem1.Text = "File";
			// 
			// NewGameMenu
			// 
			this.NewGameMenu.Index = 0;
			this.NewGameMenu.Text = "&New Game";
			this.NewGameMenu.Click += new System.EventHandler(this.NewGameMenu_Click);
			// 
			// ExitMenu
			// 
			this.ExitMenu.Index = 1;
			this.ExitMenu.Text = "E&xit";
			this.ExitMenu.Click += new System.EventHandler(this.ExitMenu_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.PvPMenu,
																					  this.PvCMenu,
																					  this.menuItem9,
																					  this.menuItem8});
			this.menuItem2.Text = "Game Setup";
			// 
			// PvPMenu
			// 
			this.PvPMenu.Checked = true;
			this.PvPMenu.Index = 0;
			this.PvPMenu.Text = "Player vs Player";
			this.PvPMenu.Click += new System.EventHandler(this.PvPMenu_Click);
			// 
			// PvCMenu
			// 
			this.PvCMenu.Index = 1;
			this.PvCMenu.Text = "Player vs Computer";
			this.PvCMenu.Click += new System.EventHandler(this.PvCMenu_Click);
			// 
			// menuItem9
			// 
			this.menuItem9.Index = 2;
			this.menuItem9.Text = "-";
			// 
			// menuItem8
			// 
			this.menuItem8.Index = 3;
			this.menuItem8.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.DiffMenu_Easy,
																					  this.DiffMenu_Normal,
																					  this.DiffMenu_Hard,
																					  this.DiffMenu_VeryHard});
			this.menuItem8.Text = "Computer Difficulty";
			// 
			// DiffMenu_Easy
			// 
			this.DiffMenu_Easy.Index = 0;
			this.DiffMenu_Easy.Text = "&Easy";
			this.DiffMenu_Easy.Click += new System.EventHandler(this.DiffMenu_EasyClick);
			// 
			// DiffMenu_Normal
			// 
			this.DiffMenu_Normal.Checked = true;
			this.DiffMenu_Normal.Index = 1;
			this.DiffMenu_Normal.Text = "&Normal";
			this.DiffMenu_Normal.Click += new System.EventHandler(this.DiffMenu_NormalClick);
			// 
			// DiffMenu_Hard
			// 
			this.DiffMenu_Hard.Index = 2;
			this.DiffMenu_Hard.Text = "&Hard";
			this.DiffMenu_Hard.Click += new System.EventHandler(this.DiffMenu_HardClick);
			// 
			// DiffMenu_VeryHard
			// 
			this.DiffMenu_VeryHard.Index = 3;
			this.DiffMenu_VeryHard.Text = "&Very Hard";
			this.DiffMenu_VeryHard.Click += new System.EventHandler(this.DiffMenu_VeryHardClick);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 2;
			this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.DebugSkip,
																					  this.DebugProcess,
																					  this.menuItem4});
			this.menuItem3.Text = "Debug";
			// 
			// DebugSkip
			// 
			this.DebugSkip.Index = 0;
			this.DebugSkip.Text = "SKip Turn";
			this.DebugSkip.Click += new System.EventHandler(this.DebugSkip_Click);
			// 
			// DebugProcess
			// 
			this.DebugProcess.Checked = true;
			this.DebugProcess.Index = 1;
			this.DebugProcess.Text = "Process Piece Captures";
			this.DebugProcess.Click += new System.EventHandler(this.DebugProcess_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 2;
			this.menuItem4.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.DebugScenario_NoWhite,
																					  this.DebugScenario_NoBlack,
																					  this.DebugScenario_TieGame});
			this.menuItem4.Text = "New Game Scenario";
			// 
			// DebugScenario_NoWhite
			// 
			this.DebugScenario_NoWhite.Index = 0;
			this.DebugScenario_NoWhite.Text = "White cannot move";
			this.DebugScenario_NoWhite.Click += new System.EventHandler(this.DebugScenario_NoWhite_Click);
			// 
			// DebugScenario_NoBlack
			// 
			this.DebugScenario_NoBlack.Index = 1;
			this.DebugScenario_NoBlack.Text = "Black cannot move";
			// 
			// DebugScenario_TieGame
			// 
			this.DebugScenario_TieGame.Index = 2;
			this.DebugScenario_TieGame.Text = "Tie Game";
			// 
			// ScoreText
			// 
			this.ScoreText.Location = new System.Drawing.Point(16, 168);
			this.ScoreText.Name = "ScoreText";
			this.ScoreText.Size = new System.Drawing.Size(88, 104);
			this.ScoreText.TabIndex = 4;
			// 
			// DebugAITrace
			// 
			this.DebugAITrace.Location = new System.Drawing.Point(480, 56);
			this.DebugAITrace.Name = "DebugAITrace";
			this.DebugAITrace.Size = new System.Drawing.Size(248, 368);
			this.DebugAITrace.TabIndex = 5;
			this.DebugAITrace.Text = "";
			// 
			// AITraceLabel
			// 
			this.AITraceLabel.Location = new System.Drawing.Point(552, 40);
			this.AITraceLabel.Name = "AITraceLabel";
			this.AITraceLabel.Size = new System.Drawing.Size(112, 16);
			this.AITraceLabel.TabIndex = 6;
			this.AITraceLabel.Text = "Component AI Trace";
			// 
			// ReversiForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(760, 438);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.AITraceLabel,
																		  this.DebugAITrace,
																		  this.ScoreText,
																		  this.TurnLabel,
																		  this.DebugText,
																		  this.Title,
																		  this.BoardPicture});
			this.Menu = this.mainMenu1;
			this.Name = "ReversiForm";
			this.Text = "Reversi";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new ReversiForm());
		}

		private void ProcessTurn( Game CurrentGame, int x, int y )
		{
			if( !CurrentGame.IsComplete )
			{
				if( !( ( CurrentGame.VsComputer ) && ( CurrentGame.CurrentTurn == CurrentGame.AI.color ) ) )
				{
					if ( CurrentGame.GameBoard.MovePossible( CurrentGame, CurrentGame.CurrentTurn ) ) 
					{
						if( CurrentGame.GameBoard.MakeMove( CurrentGame, x, y, CurrentGame.CurrentTurn, CurrentGame.ProcessMoves ) )
						{
							CurrentGame.SwitchTurn();
						}
					} 
					else
					{
						CurrentGame.SwitchTurn();
					}
				}

				if( ( CurrentGame.VsComputer ) && ( CurrentGame.CurrentTurn == CurrentGame.AI.color ) )
				{
					while( CurrentGame.GameBoard.MovePossible( CurrentGame, CurrentGame.AI.color ) )
					{
					
						Point AIMove = CurrentGame.AI.Move( CurrentGame );

						DebugAITrace.Text = CurrentGame.AI.AIDebug;
						DebugAITrace.Text += "\nOutside class...\nPlacing " + CurrentGame.GetTurnString( CurrentGame.CurrentTurn ) + " AI piece at (" + AIMove.X + "," + AIMove.Y + ")\n";

						CurrentGame.GameBoard.MakeMove( CurrentGame, AIMove.X, AIMove.Y, CurrentGame.CurrentTurn, CurrentGame.ProcessMoves );

						DebugAITrace.Text += "\nResult Board:\n" + CurrentGame.GameBoard.ToString() + "\n\n" ;

						if( CurrentGame.GameBoard.MovePossible( CurrentGame, CurrentGame.NextTurn ) )
						{
							DebugAITrace.Text += "------------\n" + CurrentGame.GetTurnString( CurrentGame.NextTurn ) + " can move to space " + CurrentGame.GameBoard.DebugMovesAvailable + "...ending ai turn\n------------\n";
							CurrentGame.SwitchTurn();
							break;
						} 
						else 
						{
							DebugAITrace.Text += "------------\n" + CurrentGame.NextTurn + " CANNOT MOVE!  AI moving again\n------------\n";
							//CurrentGame.SwitchTurn();
							CurrentGame.GameBoard.RefreshPieces( BoardPicture.CreateGraphics() );
						}
					}
					DebugAITrace.Text += "------------\nAI " + CurrentGame.GetTurnString( CurrentGame.AI.color ) + " turn over!  allowing human player to move\n############\n";
				}

				CurrentGame.GameBoard.RefreshPieces( BoardPicture.CreateGraphics() );

				DetermineWinner( CurrentGame );
			}
			
			if( CurrentGame.IsComplete )
			{
				if( CurrentGame.Winner == 0 ) 
				{
					DebugText.Text = "The game ended in a tie!!!";
				} 
				else 
				{
					DebugText.Text = CurrentGame.GetTurnString( CurrentGame.Winner ) + " is the winner!!!";
				}
			}
			
			TurnLabel.Text = "Current Turn: " + CurrentGame.GetTurnString( CurrentGame.CurrentTurn ) + "\n" ;
		}

		private Boolean DetermineWinner( Game CurrentGame )
		{
			int WhiteScore = CurrentGame.GameBoard.FindScore( 1 );
			int BlackScore = CurrentGame.GameBoard.FindScore( 2 );

			ScoreText.Text = "Current Score:\n" + " White: " + WhiteScore + "\n Black: " + BlackScore;

			if( WhiteScore == 0 )
			{
				CurrentGame.IsComplete = true;
				CurrentGame.Winner = 2;
			}
			else if ( BlackScore == 0 )
			{
				CurrentGame.IsComplete = true;
				CurrentGame.Winner = 1;
			} 
			else if ( ( ( WhiteScore + BlackScore ) == 64 ) || 
				( ( !CurrentGame.GameBoard.MovePossible( CurrentGame, CurrentGame.CurrentTurn ) ) && ( !CurrentGame.GameBoard.MovePossible( CurrentGame, CurrentGame.NextTurn ) ) ) ) 
			{
				CurrentGame.IsComplete = true;
				if ( BlackScore > WhiteScore ) 
				{
					CurrentGame.Winner = 2;
				} 
				else if ( BlackScore < WhiteScore )
				{
					CurrentGame.Winner = 1;
				} 
				else
				{
					CurrentGame.Winner = 0;
				}

			}

			return( CurrentGame.IsComplete );
		}

		private void ProcessMove_OLD( Game CurrentGame, int x, int y )
		{
			if ( ( CurrentGame.GameBoard.MakeMove( CurrentGame, x, y, CurrentGame.CurrentTurn, CurrentGame.ProcessMoves ) ) && !CurrentGame.IsComplete )
			{
				DebugText.Text = "";

				//Piece CurrentPiece = CurrentGame.GameBoard.PutPiece( x, y, CurrentGame.CurrentTurn );
				
				if( ( !CurrentGame.GameBoard.MovePossible( CurrentGame, CurrentGame.NextTurn ) ) && ( CurrentGame.GameBoard.MovePossible( CurrentGame, CurrentGame.CurrentTurn ) ) )
				{
					DebugText.Text = CurrentGame.GetTurnString( CurrentGame.CurrentTurn ) + " cannot move!";
					//CurrentGame.SwitchTurn();
				}
				else if( CurrentGame.VsComputer )
				{
					do 
					{
						CurrentGame.SwitchTurn();

						Point AIMove = CurrentGame.AI.Move( CurrentGame );
						DebugAITrace.Text += CurrentGame.AI.AIDebug;
						DebugAITrace.Text += "\nOutside class...\nPlacing " + CurrentGame.GetTurnString( CurrentGame.CurrentTurn ) + " AI piece at (" + AIMove.X + "," + AIMove.Y + ")\n";
				
						if( ( AIMove.X >= 0 ) && ( AIMove.X <= 7 ) && ( AIMove.Y <= 7 ) && ( AIMove.Y >= 0 ) )
						{
							CurrentGame.GameBoard.MakeMove( CurrentGame, AIMove.X, AIMove.Y, CurrentGame.CurrentTurn, CurrentGame.ProcessMoves );
							CurrentGame.GameBoard.RefreshPieces( BoardPicture.CreateGraphics() );
						}
				
						CurrentGame.SwitchTurn();
					} while( ( !CurrentGame.GameBoard.MovePossible( CurrentGame, CurrentGame.CurrentTurn ) ) && ( CurrentGame.GameBoard.MovePossible( CurrentGame, CurrentGame.NextTurn ) ) );
				} 
				else 
				{
					CurrentGame.SwitchTurn();
				}
			}

			int WhiteScore = CurrentGame.GameBoard.FindScore( 1 );
			int BlackScore = CurrentGame.GameBoard.FindScore( 2 );

			ScoreText.Text = "Current Score:\n" + " White: " + WhiteScore + "\n Black: " + BlackScore;

			if( WhiteScore == 0 )
			{
				CurrentGame.IsComplete = true;
				CurrentGame.Winner = 2;
			}
			else if ( BlackScore == 0 )
			{
				CurrentGame.IsComplete = true;
				CurrentGame.Winner = 1;
			} 
			else if ( ( ( WhiteScore + BlackScore ) == 64 ) || 
				      ( ( !CurrentGame.GameBoard.MovePossible( CurrentGame, CurrentGame.CurrentTurn ) ) && ( !CurrentGame.GameBoard.MovePossible( CurrentGame, CurrentGame.NextTurn ) ) ) ) 
			{
				CurrentGame.IsComplete = true;
				if ( BlackScore > WhiteScore ) 
				{
					CurrentGame.Winner = 2;
				} 
				else if ( BlackScore < WhiteScore )
				{
					CurrentGame.Winner = 1;
				} 
				else
				{
					CurrentGame.Winner = 0;
				}

			}

			if( CurrentGame.IsComplete )
			{
				if( CurrentGame.Winner == 0 ) 
				{
					DebugText.Text = "The game ended in a tie!!!";
				} 
				else 
				{
					DebugText.Text = CurrentGame.GetTurnString( CurrentGame.Winner ) + " is the winner!!!";
				}
			}

			TurnLabel.Text = "Current Turn: " + CurrentGame.GetTurnString( CurrentGame.CurrentTurn ) + "\n" ;
			CurrentGame.GameBoard.RefreshPieces( BoardPicture.CreateGraphics() );
		}

		private void BoardPicture_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			int x = (e.X + 1)/40;
			int y = (e.Y + 1)/40;
			
			ProcessTurn( CurrentGame, x, y );
			
			//DebugText.Text = CurrentGame.GameBoard.ToString();
		}

		private void BoardPaint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Graphics g = e.Graphics;

			base.OnPaint(e);
			
			if ( CurrentGame.GameBoard != null ) 
			{
				CurrentGame.GameBoard.RefreshPieces( g );	
			}
		}

		private void DiffMenu_EasyClick(object sender, System.EventArgs e)
		{
			DiffMenu_Easy.Checked = true;
			DiffMenu_Normal.Checked = false;
			DiffMenu_Hard.Checked = false;
			DiffMenu_VeryHard.Checked = false;
			AIDifficulty = 0;
		}

		private void DiffMenu_NormalClick(object sender, System.EventArgs e)
		{
			DiffMenu_Easy.Checked = false;
			DiffMenu_Normal.Checked = true;
			DiffMenu_Hard.Checked = false;
			DiffMenu_VeryHard.Checked = false;		
			AIDifficulty = 1;
		}

		private void DiffMenu_HardClick(object sender, System.EventArgs e)
		{
			DiffMenu_Easy.Checked = false;
			DiffMenu_Normal.Checked = false;
			DiffMenu_Hard.Checked = true;
			DiffMenu_VeryHard.Checked = false;		
			AIDifficulty = 2;
		}

		private void DiffMenu_VeryHardClick(object sender, System.EventArgs e)
		{
			DiffMenu_Easy.Checked = false;
			DiffMenu_Normal.Checked = false;
			DiffMenu_Hard.Checked = false;
			DiffMenu_VeryHard.Checked = true;
			AIDifficulty = 3;
		}

		private void PvPMenu_Click(object sender, System.EventArgs e)
		{
			PvPMenu.Checked = true;
			PvCMenu.Checked = false;
			PvC = false;
		}

		private void PvCMenu_Click(object sender, System.EventArgs e)
		{
			PvPMenu.Checked = false;
			PvCMenu.Checked = true;
			PvC = true;
		}

		private void ExitMenu_Click(object sender, System.EventArgs e)
		{
			ReversiForm.ActiveForm.Close();
		}

		private void NewGameMenu_Click(object sender, System.EventArgs e)
		{
			CurrentGame = new Game();
			BoardPicture.Invalidate();
		}

		private void DebugSkip_Click(object sender, System.EventArgs e)
		{
			CurrentGame.SwitchTurn();
			TurnLabel.Text = "Current Turn: " + CurrentGame.GetTurnString( CurrentGame.CurrentTurn ) + "\n" ;		
		}

		private void DebugProcess_Click(object sender, System.EventArgs e)
		{
			CurrentGame.ProcessMoves = !CurrentGame.ProcessMoves;
			DebugProcess.Checked = !DebugProcess.Checked;
		}

		private void DebugScenario_NoWhite_Click(object sender, System.EventArgs e)
		{
			CurrentGame = new Game();
			DebugAITrace.Text = "";
			CurrentGame.GameBoard.ClearBoard();
			CurrentGame.GameBoard.PutPiece( 3, 3, 0 );
			CurrentGame.GameBoard.PutPiece( 4, 4, 0 );
			CurrentGame.GameBoard.PutPiece( 3, 4, 0 );
			CurrentGame.GameBoard.PutPiece( 4, 3, 0 );
			CurrentGame.GameBoard.PutPiece( 0, 0, 2 );
			CurrentGame.GameBoard.PutPiece( 0, 1, 2 );
			CurrentGame.GameBoard.PutPiece( 0, 2, 2 );
			CurrentGame.GameBoard.PutPiece( 0, 3, 2 );
			CurrentGame.GameBoard.PutPiece( 0, 4, 2 );
			CurrentGame.GameBoard.PutPiece( 0, 5, 2 );
			CurrentGame.GameBoard.PutPiece( 0, 6, 2 );
			CurrentGame.GameBoard.PutPiece( 0, 7, 2 );
			CurrentGame.GameBoard.PutPiece( 1, 0, 1 );
			CurrentGame.GameBoard.PutPiece( 1, 1, 1 );
			CurrentGame.GameBoard.PutPiece( 1, 2, 1 );
			CurrentGame.GameBoard.PutPiece( 1, 3, 1 );
			CurrentGame.GameBoard.PutPiece( 1, 4, 1 );
			CurrentGame.GameBoard.PutPiece( 1, 5, 1 );
			CurrentGame.GameBoard.PutPiece( 1, 6, 1 );
			CurrentGame.GameBoard.PutPiece( 1, 7, 1 );
			CurrentGame.CurrentTurn = 1;
			CurrentGame.NextTurn = 2;
			CurrentGame.GameBoard.RefreshPieces( BoardPicture.CreateGraphics() );
			BoardPicture.Invalidate();
			ProcessTurn( CurrentGame, 0, 0 );
		}

		private void Title_Click(object sender, System.EventArgs e)
		{
		
		}

	}
}
