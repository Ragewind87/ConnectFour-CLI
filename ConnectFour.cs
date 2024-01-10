using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFourCLI {

    internal class ConnectFour {

        public static void Main(string[] args) {
            RunGame();
        }

        private static List<List<char>> createBoard(int rows, int columns) {
            List<List<char>> board = new List<List<char>>();
            for (int i = 0; i < rows; i++) {
                List<char> row = new List<char>();
                for (int k = 0; k < columns; k++) {
                    row.Add('.');
                }
                board.Add(row);
            }
            return board;
        }

        private static bool CheckForWin(List<List<char>> board, int lpY, int lpX, char piece) {
            //vertical win
            var count = 0;
            for (int y = lpY, x = lpX; y >= 0; y--) {
                if (board[y][x] == piece)
                    count++;
            }
            if (count >= 4)
                return true;

            //horizontal win
            count = 0;
            for (int y = lpY, x = lpX; x >= 0; x--) {
                if (board[y][x] == piece)
                    count++;
            }
            for (int y = lpY, x = lpX + 1; x < board[y].Count; x++) {
                if (board[y][x] == piece)
                    count++;
            }
            if (count >= 4)
                return true;

            //bot left to top right diag win
            count = 0;
            for (int y = lpY, x = lpX; y >= 0 && x < board[y].Count; y--, x++) {
                if (board[y][x] == piece)
                    count++;
            }
            for (int y = lpY + 1, x = lpX - 1; y < board.Count && x >= 0; y++, x--) {
                if (board[y][x] == piece)
                    count++;
            }
            if (count >= 4)
                return true;

            //bot right to top left diag win
            count = 0;
            for (int y = lpY, x = lpX; y >= 0 && x >= 0; y--, x--) {
                if (board[y][x] == piece)
                    count++;
            }
            for (int y = lpY + 1, x = lpX + 1; y < board.Count && x < board[y].Count; y++, x++) {
                if (board[y][x] == piece)
                    count++;
            }
            if (count >= 4)
                return true;

            return false;
        }

        private static bool PlacePieceAndCheckForWin(List<List<char>> board, int turn, int column) {
            for (int i = board.Count - 1; i >= 0; i--) {
                if (board[i][column] != 'O' && board[i][column] != 'X') {
                    board[i][column] = (turn == 1) ? 'O' : 'X';

                    if (CheckForWin(board, i, column, (turn == 1) ? 'O' : 'X'))
                        return true;

                    break;
                }
            }
            return false;
        }

        public static void RunGame() {
            int turn = 1;
            List<List<char>> board = createBoard(6, 7);
            while (true) {
                Console.WriteLine($"\nPlayer {turn}");
                printBoard(board);
                var input = Console.ReadLine();

                if (PlacePieceAndCheckForWin(board, turn, int.Parse(input))) {
                    Console.WriteLine();
                    printBoard(board);
                    Console.WriteLine($"----------------------\nPLAYER {turn} WINS\n----------------------");
                    Console.ReadLine();
                    break;
                }

                turn = (turn == 1) ? 2 : 1;
            }
        }

        private static void printBoard(List<List<char>> gameBoard) {
            foreach (List<char> row in gameBoard) {
                foreach (char cell in row) {
                    Console.Write(cell);
                }
                Console.Write('\n');
            }
        }
    }
}

