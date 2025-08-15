using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CL_REUFT
{
    internal class Tetris
    {
        class Position //Position class to make life easier
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

    
    
       
        Position PlayerPosition = new Position(0,0); //The player's position
        short CurrentShape = 0; //The current shape...duh
        short CurrentRotation = 0; //The current rotation...duh
        
        List<int> board = new List<int>(); //The static board, ie where the tetronominoes are placed after a second or so of not moving
        List<List<Position>> Shapes = new List<List<Position>>(); //2D list of positions representing the tetronominoes
        
        

        public void init() 
        {
           
           Shapes.Clear();
            //Self explanatory
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
            //Set the cursor to the position defined in the shapes list plus the position of the player
            //This could be done with a for loop but since each tetronomino is only 4 cells in size there is no need
            //I think this is more readable anyway
            Console.SetCursorPosition(Shapes[CurrentShape][(CurrentRotation * 4)].GetX()+PlayerPosition.GetX(), Shapes[CurrentShape][(CurrentRotation * 4)].GetY()+PlayerPosition.GetY());
            Console.Write("1");
            Console.SetCursorPosition(Shapes[CurrentShape][(CurrentRotation * 4)+1].GetX() + PlayerPosition.GetX(), Shapes[CurrentShape][(CurrentRotation * 4)+1].GetY() + PlayerPosition.GetY());
            Console.Write("-");
            Console.SetCursorPosition(Shapes[CurrentShape][(CurrentRotation * 4) + 2].GetX() + PlayerPosition.GetX(), Shapes[CurrentShape][(CurrentRotation * 4) + 2].GetY() + PlayerPosition.GetY());
            Console.Write("-");
            Console.SetCursorPosition(Shapes[CurrentShape][(CurrentRotation * 4)+3].GetX() + PlayerPosition.GetX(), Shapes[CurrentShape][(CurrentRotation * 4)+3].GetY() + PlayerPosition.GetY());
            Console.Write("-");
           
        }
        private void DrawBoard() 
        {
            //Go through the static board and draw it
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
        private void GameLogic() //Kind of a redundant function, only here in case the codebase expands
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
