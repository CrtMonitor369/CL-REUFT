using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CL_REUFT
{
    internal class Tetris
    {
        class Position
        {
            private int x;
            private int y; 
            public Position(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
            public int GetX() {  return x; }
            public int GetY() { return y; }
            public void SetX(int x) { this.x = x; }
            public void SetY(int y) { this.y = y; }

        }

        const short SizeOfATetronomino = 9;
        const short Rotations = 4;
        const short StartX =0;
        const short StartY=0;
        Position PlayerPosition = new Position(0,0);
        short CurrentShape = 1;
        short CurrentRotation = 0;
        float VariableForIncreasingYSlowly = 0f;
        List<int> board = new List<int>();
        List<List<Position>> Shapes = new List<List<Position>>();
        
        

        public void init() 
        {
           
           Shapes.Clear();
            Shapes.Add(new List<Position>() 
            {
            new Position(0,0),
            new Position(1,0),
            new Position(0,1),
            new Position(1,1),

            new Position(0,0),
            new Position(1,0),
            new Position(0,1),
            new Position(1,1),

            new Position(0,0),
            new Position(1,0),
            new Position(0,1),
            new Position(1,1),

            new Position(0,0),
            new Position(1,0),
            new Position(0,1),
            new Position(1,1),
            }
            );
            Shapes.Add(new List<Position>()
            {
            new Position(1,0),
            new Position(1,1),
            new Position(1,2),
            new Position(0,2),

            new Position(0,0),
            new Position(0,1),
            new Position(1,1),
            new Position(2,1),

            new Position(0,0),
            new Position(1,0),
            new Position(0,1),
            new Position(0,2),


            }
            );



            for (int i = 0; i < 200; i++) 
            {
            board.Add(0);
            }

            GameLoop();
        }
        private void DrawShape()
        {

            Console.SetCursorPosition(Shapes[CurrentShape][(CurrentRotation * 4)].GetX()+PlayerPosition.GetX(), Shapes[CurrentShape][(CurrentRotation * 4)].GetY()+PlayerPosition.GetY());
            Console.Write("-");
            Console.SetCursorPosition(Shapes[CurrentShape][(CurrentRotation * 4)+1].GetX() + PlayerPosition.GetX(), Shapes[CurrentShape][(CurrentRotation * 4)+1].GetY() + PlayerPosition.GetY());
            Console.Write("-");
            Console.SetCursorPosition(Shapes[CurrentShape][(CurrentRotation * 4) + 2].GetX() + PlayerPosition.GetX(), Shapes[CurrentShape][(CurrentRotation * 4) + 2].GetY() + PlayerPosition.GetY());
            Console.Write("-");
            Console.SetCursorPosition(Shapes[CurrentShape][(CurrentRotation * 4)+3].GetX() + PlayerPosition.GetX(), Shapes[CurrentShape][(CurrentRotation * 4)+3].GetY() + PlayerPosition.GetY());
            Console.Write("-");
           
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
            PlayerPosition.SetY(PlayerPosition.GetY() + 1);
            for (int i = 0; i < 4; i++) 
            {
                if(!(Shapes[CurrentShape][CurrentRotation * 4 + i].GetY()+PlayerPosition.GetY() < 20-Shapes[CurrentShape][CurrentRotation * 4 + i].GetY())) 
                {
                    PlayerPosition.SetY(PlayerPosition.GetY() - 1);
                }
               
            }
            

            if (Console.KeyAvailable)
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.LeftArrow:
                     
                        break;
                    case ConsoleKey.RightArrow:
                    
                        break;
                    case ConsoleKey.UpArrow:
                        CurrentRotation %= 3;
                        CurrentRotation += 1;

                        break;
                    

                }
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
                DrawShape();
                Thread.Sleep(50);
                GameLogic();
                
            }
        }



    }
}
