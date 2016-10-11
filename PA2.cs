using System;
using static System.Console;

namespace Bme121.Pa2
{
    // Procedural-programming implementation of the game Connect Four.
    // Note: Connect Four is (C) Hasbro, Inc.
    // This version is for educational use only.
    
    static partial class Program
    {
        // For random moves by AI players.
        static Random randomNumberGenerator = new Random( );
        
        // The game board and intuitive names for its size.
        // Each element of the game board is initially null.
        // In valid play, an element may become "O" or "X".
        static string[ , ] gameBoard = new string[ 6, 7 ]; 
        static readonly int gameRows = gameBoard.GetLength( 0 );
        static readonly int gameCols = gameBoard.GetLength( 1 );
        
        // The playing piece colors can be altered.
        static readonly ConsoleColor fgColor = ForegroundColor;
        static readonly ConsoleColor xColor = ConsoleColor.Cyan;
        static readonly ConsoleColor oColor = ConsoleColor.Magenta;
        
        // Save the two player's names and kinds (human or AI).
        static string xName, oName;
        static string xKind, oKind;
        
        // The symbol (O or X), name, and kind of the current player.
        static string currentPlayer;
        static string currentPlayerName;
        static string currentPlayerKind;
    
        // Play the game, largely by calling game methods.
        static void Main( )
        {
            WriteLine( );
            WriteLine( "BME 121 Connect Four!" );
            WriteLine( );
            WriteLine( "The game of Connect Four is (C) Hasbro, Inc." );
            WriteLine( "This version is for educational use only." );
            WriteLine( );
            WriteLine( "Play by stacking your token in any column with available space." );
            WriteLine( "Win with four-in-a-row vertically, horizontally, or diagonally." );
            
            DrawGameBoard( );
            
            GetPlayerNames( );
            GetPlayerKinds( );
            GetFirstPlayer( );
            
            while( ! IsBoardFull( ) )
            {
                GetPlayerMove( );
                DrawGameBoard( );
                
                if( CurrentPlayerWins( ) )
                {
                    WriteLine( );
                    Write( $"{currentPlayerName} - " );
                    ColorWrite( currentPlayer );
                    WriteLine( " - wins!" );
                    WriteLine( );
                    return;
                }
                
                SwitchPlayers( );
            }
            WriteLine( );
            WriteLine( "Game is a draw!" );
            WriteLine( );
        }
        
        // Get the displayed names of the two players.
        static void GetPlayerNames( )
        {
            WriteLine( );
            Write( "Enter player O name: " );
            oName = ReadLine( );
            Write( "Enter player X name: " );
            xName = ReadLine( );
        }
        
        // Get the kinds (human or ai) of the two players.
        static void GetPlayerKinds( )
        {
            WriteLine( );
            
            while( true )
            {
                Write( "Enter player O kind [human ai]: " );
                oKind = ReadLine( ).ToLower( );
                if( oKind == "human" ) break;
                if( oKind == "ai" ) break;
                WriteLine( "Must be one of 'human' or 'ai'." );
                WriteLine( "Please try again." );
            }
            
            while( true )
            {
                Write( "Enter player X kind [human ai]: " );
                xKind = ReadLine( ).ToLower( );
                if( xKind == "human" ) break;
                if( xKind == "ai" ) break;
                WriteLine( "Must be one of 'human' or 'ai'." );
                WriteLine( "Please try again." );
            }
        }
        
        // Get and set up the player who will play first.
        static void GetFirstPlayer( )
        {
            WriteLine( );
            
            while( true )
            {
                Write( "Enter first to play [O X]: " );
                currentPlayer = ReadLine( ).ToUpper( );
                if( currentPlayer == "O" ) break;
                if( currentPlayer == "X" ) break;
                WriteLine( "Must be one of 'O' or 'X'." );
                WriteLine( "Please try again." );
            }
            
            if( currentPlayer == "O" )
            {
                currentPlayerName = oName;
                currentPlayerKind = oKind;
            }
            
            if( currentPlayer == "X" )
            {
                currentPlayerName = xName;
                currentPlayerKind = xKind;
            }
        }
        
        // Get and perform the desired move by the current player.
        static void GetPlayerMove( )
        {
            if( currentPlayerKind == "ai" ) 
            {
                WriteLine( );
                Write( $"{currentPlayerName} - " );
                ColorWrite( currentPlayer );
                Write( " - choose a column: " );
                int column = SelectRandomColumn( );
                System.Threading.Thread.Sleep( 1000 );
                Write( column );
                System.Threading.Thread.Sleep( 1000 );
                WriteLine( );
                PlayInColumn( column );
            }
            
            if( currentPlayerKind == "human" )
            {
                while( true )
                {
                    WriteLine( );
                    Write( $"{currentPlayerName} - " );
                    ColorWrite( currentPlayer );
                    Write( " - choose a column: " );
                    int column;
                    if( ! int.TryParse( ReadLine( ), out column ) || ! IsValidPlay( column ) )
                    {
                        WriteLine( "Not a valid column or column is full." );
                        WriteLine( "Please try again." );
                    }
                    else
                    {
                        PlayInColumn( column );
                        break;
                    }
                }
            }
        }
        
        // Detect whether the current player has won by looking for a vertical,
        // horizontal, or diagonal run of four of the current player's symbols.
        static bool CurrentPlayerWins( )
        {
            // TO DO (6):  Replace the following line with the correct computation.

            //Checking for 4 in a row by detecting if four adjacent, diagonal, or vertical cells in a row are filled with the same symbol
            for (int j = 0; j < gameRows; j++)
            {
                for (int i = 0; i < gameCols-3;i++)
                {
                    if (gameBoard[j, i] != null && gameBoard[j, i] == gameBoard[j, i + 1] && gameBoard[j, i] == gameBoard[j, i + 2] && gameBoard[j, i] == gameBoard[j, i + 3])
                    {
                        return true;
                    }
                }
            }
            for (int i = 0; i < gameCols; i++)
            {
                for (int j = 0; j < gameRows-3; j++)
                {
                    if (gameBoard[j, i] != null && gameBoard[j, i] == gameBoard[j + 1, i] && gameBoard[j, i] == gameBoard[j + 2, i] && gameBoard[j, i] == gameBoard[j + 3, i])
                    {
                        return true;
                    }
                }
            }
            for (int j = 0; j < gameRows - 3; j++)
            {
                for (int i = 0; i < gameCols - 3; i++)
                {
                    if (gameBoard[j, i] != null && gameBoard[j, i] == gameBoard[j + 1, i + 1] && gameBoard[j, i] == gameBoard[j + 2, i + 2] && gameBoard[j, i] == gameBoard[j + 3, i + 3])
                    {
                        return true;
                    }
                }
            }

            for (int j = 0; j < gameRows - 3; j++)
            {
                for (int i = gameCols - 4; i < gameCols; i++)
                {
                    if (gameBoard[j, i] != null && gameBoard[j, i] == gameBoard[j + 1, i - 1] && gameBoard[j, i] == gameBoard[j + 2, i - 2] && gameBoard[j, i] == gameBoard[j + 3, i - 3])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        
        // Detect whether the game board is completely filled.
        static bool IsBoardFull( )
        {   
            //detects whether or not the top row of every column is full
            for (int i = 0; i < gameCols; i++)
            {
                if (gameBoard[gameRows - 1, i] == null)
                    return false;
            }
            return true;
        }
        
        // Detect whether given column is on the board and has space remaining.
        static bool IsValidPlay( int col )
        {
            //detects whether or not the top row of the column is full
            if (gameBoard[gameRows - 1, col] == null)
                return true;
            else
                return false;
        }
        
        // Play current player's symbol on top of existing plays in the selected column.
        static void PlayInColumn( int col )
        {
            for (int i = 0; i < gameRows; i++)
            {
                //places character token in the first empty row in the chosen column
                if (gameBoard[i,col] == null)
                {
                    gameBoard[i, col] = currentPlayer;
                    break; 
                }
            }
            //static string[,] gameBoard = new string[6, 7];
            //static readonly int gameRows = gameBoard.GetLength(0);
            //static readonly int gameCols = gameBoard.GetLength(1);
            //WriteLine(gameRows);
            // TO DO (3):  Perform the correct computation.
        }

        // Select a column at random until a valid play is found.
        static int SelectRandomColumn( )
        {
            //returns a random number between 0 and the number of columns
            return randomNumberGenerator.Next(0, gameCols) ;
            
            // use a while loop to determine that the column determined is valid (IsValidPlay)
            //static string[,] gameBoard = new string[6, 7];
            //static readonly int gameRows = gameBoard.GetLength(0);
            //static readonly int gameCols = gameBoard.GetLength(1);
        }

        // Change the current player from player O to player X or vice versa.
        static void SwitchPlayers( )
        {
            WriteLine("Test");
            //Switches player from O to X or vice versa
            if (currentPlayer == "X") 
            {
                currentPlayer = "O";
                currentPlayerName = oName;
                currentPlayerKind = oKind;
            }
            else
            {
                currentPlayer = "X";
                currentPlayerName = xName;
                currentPlayerKind = xKind;
            }
        }

        // Display the current game board on the console.
        // This version uses only ASCII characters for portability.
        static void DrawGameBoard( )
        {
            WriteLine( );
            for( int row = gameRows - 1; row >= 0; row -- )
            {
                Write( "   |" );
                for( int col = 0; col < gameCols; col ++ ) Write( "   |" ); 
                WriteLine( );
                Write( $"{row,2} |" );
                for( int col = 0; col < gameCols; col ++ ) 
                {
                    Write( " " );
                    ColorWrite( gameBoard[ row, col ] );
                    Write( " |" );
                }
                WriteLine( );
            }
            Write( "   |" );
            for( int col = 0; col < gameCols; col ++ ) Write( "___|" ); 
            WriteLine( );
            WriteLine( );
            Write( "    " );
            for( int col = 0; col < gameCols; col ++ ) Write( $"{col,2}  " ); 
            WriteLine( );
        }
        
        // Display O or X in their special color.
        static void ColorWrite( string symbol )
        {
            if( symbol == "O" ) ForegroundColor = oColor;
            if( symbol == "X" ) ForegroundColor = xColor;
            // Empty cells in the game board use null but
            // we still want them to display using one space.
            Write( $"{symbol,1}" );
            ForegroundColor = fgColor;
        }
    }
}
