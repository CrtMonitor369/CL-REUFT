using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CL_REUFT
{
    internal class Tetris
    {
        List<int> board = new List<int>();
        public void init() 
        {
            for (int i = 0; i < 200; i++) 
            {
            board.Add(0);
            }

            GameLoop();
        }

        private void RenderBoard() 
        {
            
            for (int i = 0; i < 20; i++) 
            {
                for (int j = 0; j < 10; j++)
                {
                Console.Write(board[i*10+j]);
                }
                Console.SetCursorPosition(0, i);
            }
            Console.ReadLine();
        }

        private void GameLoop() 
        {
            Console.Clear();
            RenderBoard();

        }



    }
}
