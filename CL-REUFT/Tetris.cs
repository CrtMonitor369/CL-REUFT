using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CL_REUFT
{
    internal class Tetris
    {
        const short SizeOfATetronomino = 9;
        const short Rotations = 4;
        const short StartX =0;
        const short StartY=0;
        short x = 0;
        short y = 0;
        short CurrentShape = 0;
        short CurrentRotation = 0;
        float VariableForIncreasingYSlowly = 0f;
        List<int> board = new List<int>();

        List<List<int>> Shapes = new List<List<int>>();

        public void init() 
        {
            //Square block definition
            Shapes.Add(new List<int>() 
            {
                1,1,0,  //First rotation
                1,1,0,
                0,0,0,

                1,1,0,  //Second rotation
                1,1,0,
                0,0,0,

                1,1,0, //Third rotation
                1,1,0,
                0,0,0,

                1,1,0, //Fourth rotation
                1,1,0,
                0,0,0,
            });

            
            for (int i = 0; i < 200; i++) 
            {
            board.Add(0);
            }

            GameLoop();
        }
        private void DrawShape(short shape, short rotation) 
        {
            Console.SetCursorPosition(x,y);
            for (int i = rotation; i < 9; i++) {
                if (Shapes[shape][i] == 0) { Console.SetCursorPosition(x, y + 1); }
                else
                {
                    Console.Write("#");
                }
            }
        }
        private void DrawBoard() 
        {
            
            for (int i = 0; i < 20; i++) 
            {
                for (int j = 0; j < 10; j++)
                {
                Console.Write(board[i*10+j]);
                }
                Console.SetCursorPosition(0, i);
            }
           
        }
        private void PlayerLogic() 
        {
            
            if (Console.KeyAvailable)
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.LeftArrow:
                        x -= 1;
                        break;
                    case ConsoleKey.RightArrow:
                        x += 1;
                        break;

                }
            }
            switch (CurrentShape) {
                case 0:
                    switch (CurrentRotation) 
                    {
                        default:
                            if (y != 17)
                            {
                                VariableForIncreasingYSlowly += 0.25f;
                                if (VariableForIncreasingYSlowly > 1) { VariableForIncreasingYSlowly = 0; }
                                y += Convert.ToInt16(VariableForIncreasingYSlowly);
                            }
                            break;
                    }
                    break;

            }
            

        }
        private void GameLogic() 
        {
            PlayerLogic();
        }
     
        private void GameLoop() 
        {
            while (true)
            {
                Console.Clear();    
                DrawBoard();
                DrawShape(CurrentShape, CurrentRotation); //Shape and Rotation, these are not x and y positions
                GameLogic();
                Thread.Sleep(50);
            }
        }



    }
}
