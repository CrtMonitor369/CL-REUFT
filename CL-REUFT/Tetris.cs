using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
        short CurrentShape = 6; //The current shape...duh
        short CurrentRotation = 0; //The current rotation...duh
        bool ApplyingGravityToBoard=false;
        List<int> board = new List<int>(); //The static board, ie where the tetronominoes are placed after a second or so of not moving
        List<List<Position>> Shapes = new List<List<Position>>(); //2D list of positions representing the tetronominoes
        float timer;
        bool onFloor;
        int score;
        bool Sandmode = false;
        bool dead;
        public void init() 
        {
            bool dead = false;
            Console.WriteLine("enable sandmode? y/n");
            if (Console.ReadLine().ToUpper() == "Y") {Sandmode = true;}
            score= 0;
            onFloor = false;
           timer=0;
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

            new Position(0,0),
            new Position(1,0),
            new Position(2,0),
            new Position(2,1),


            }
            );

            Shapes.Add(new List<Position>()
            {
            new Position(0,0),
            new Position(0,1),
            new Position(0,2),
            new Position(0,3),

            new Position(0,0),
            new Position(1,0),
            new Position(2,0),
            new Position(3,0),

            new Position(1,0),
            new Position(1,1),
            new Position(1,2),
            new Position(1,3),

            new Position(0,0),
            new Position(1,0),
            new Position(2,0),
            new Position(3,0),


            }
           );
            Shapes.Add(new List<Position>()
            {
            new Position(2,0),
            new Position(0,1),
            new Position(1,1),
            new Position(2,1),

            new Position(0,0),
            new Position(0,1),
            new Position(0,2),
            new Position(1,2),

            new Position(0,2),
            new Position(0,1),
            new Position(1,1),
            new Position(2,1),

            new Position(0,0),
            new Position(1,0),
            new Position(1,1),
            new Position(1,2),


            }
           );

            Shapes.Add(new List<Position>()
            {
            new Position(0,0),
            new Position(1,0),
            new Position(1,1),
            new Position(2,1),

            new Position(1,0),
            new Position(1,1),
            new Position(2,1),
            new Position(2,2),

            new Position(0,1),
            new Position(1,1),
            new Position(1,2),
            new Position(2,2),

            new Position(1,0),
            new Position(1,1),
            new Position(2,1),
            new Position(2,2),


            }
           );
            Shapes.Add(new List<Position>()
            {
            new Position(1,0),
            new Position(2,0),
            new Position(0,1),
            new Position(1,1),

            new Position(0,0),
            new Position(0,1),
            new Position(1,1),
            new Position(1,2),

            new Position(0,1),
            new Position(1,1),
            new Position(1,2),
            new Position(2,2),

            new Position(1,0),
            new Position(1,1),
            new Position(2,1),
            new Position(2,2),


            }
           );
            Shapes.Add(new List<Position>()
            {
            new Position(1,0),
            new Position(0,1),
            new Position(1,1),
            new Position(2,1),

            new Position(0,0),
            new Position(0,1),
            new Position(1,1),
            new Position(0,2),

            new Position(0,0),
            new Position(1,0),
            new Position(2,0),
            new Position(1,1),

            new Position(1,0),
            new Position(0,1),
            new Position(1,1),
            new Position(1,2),


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
            Console.Write("\u25A9");
            Console.SetCursorPosition(Shapes[CurrentShape][(CurrentRotation * 4)+1].GetX() + PlayerPosition.GetX(), Shapes[CurrentShape][(CurrentRotation * 4)+1].GetY() + PlayerPosition.GetY());
            Console.Write("\u25A9");
            Console.SetCursorPosition(Shapes[CurrentShape][(CurrentRotation * 4) + 2].GetX() + PlayerPosition.GetX(), Shapes[CurrentShape][(CurrentRotation * 4) + 2].GetY() + PlayerPosition.GetY());
            Console.Write("\u25A9");
            Console.SetCursorPosition(Shapes[CurrentShape][(CurrentRotation * 4)+3].GetX() + PlayerPosition.GetX(), Shapes[CurrentShape][(CurrentRotation * 4)+3].GetY() + PlayerPosition.GetY());
            Console.Write("\u25A9");
           
        }
        private void DrawBoard() 
        {
            //Go through the static board and draw it
            for (int i = 0; i < 20; i++) 
            {
                for (int j = 0; j < 10; j++)
                {
                    if (board[i * 10 + j] == 0)
                    {
                        Console.Write("\u2591");
                    }
                    else
                    {
                        Console.Write("\u2588");
                    }
                }
                Console.SetCursorPosition(0, i);
            }
           
        }
        private void PlayerLogic() //mostly works, occasional glitch where player clips through blocks, no clue why, but it works 100% of the time 95% of the time
        

        {
            //Funky code full of magic numbers, this could certainly be done in a smarter way
            if (ApplyingGravityToBoard != true)
            {
                Position LastPos = PlayerPosition;
                for (int i = 0; i < 4; i++)
                {
                    int playerY = (Shapes[CurrentShape][CurrentRotation * 4 + i].GetY() + PlayerPosition.GetY());
                    int playerX = Shapes[CurrentShape][CurrentRotation * 4 + i].GetX() + PlayerPosition.GetX();
                    if (board[(10 * playerY + playerX)] != 0)
                    {
                        PlayerPosition = LastPos;
                        break;
                    }
                }
                LastPos = PlayerPosition;
                if (timer > 2)
                {
                    PlayerPosition.SetY(PlayerPosition.GetY() + 1);
                    timer = 0;
                }
                timer++;
                for (int i = 0; i < 4; i++)
                {
                    int playerY = (Shapes[CurrentShape][CurrentRotation * 4 + i].GetY() + PlayerPosition.GetY()) + 1;
                    int playerX = Shapes[CurrentShape][CurrentRotation * 4 + i].GetX() + PlayerPosition.GetX();




                    if (playerY > 19)
                    {
                        PlayerPosition.SetY(PlayerPosition.GetY() - 1);
                        onFloor = true;
                    }
                    else
                    {

                        int elementBelow = 0;
                        if (playerY < 19)
                        {
                            elementBelow = board[(10 * (playerY + 1) + playerX)];
                        }
                        if (elementBelow == 1)
                        {
                            onFloor = true;
                        }
                        /*if (elementAtCurrentPosition == 1 && onFloor==false)
                        {
                            PlayerPosition.SetY(PlayerPosition.GetY() - 1);

                            onFloor = true;

                        }*/
                    }




                }
                if (onFloor)
                {
                    for (int i = 0; i < 4; i++)
                    {

                        int playerX = Shapes[CurrentShape][CurrentRotation * 4 + i].GetX() + PlayerPosition.GetX();
                        int playerY = (Shapes[CurrentShape][CurrentRotation * 4 + i].GetY() + PlayerPosition.GetY() + 1);
                        board[(10 * playerY + playerX)] = 1;

                        if (i == 3)
                        {
                            

                            PlayerPosition.SetX(0);
                            PlayerPosition.SetY(0);
                            playerX = Shapes[CurrentShape][CurrentRotation * 4 + i].GetX() + PlayerPosition.GetX();
                            playerY = (Shapes[CurrentShape][CurrentRotation * 4 + i].GetY() + PlayerPosition.GetY() + 1);
                            int elementBelow = board[(10 * (playerY + 1) + playerX)];
                            if (elementBelow != 0) 
                            {
                                dead = true;
                                break;
                            }
                            timer = 0;
                            CurrentRotation = 0;
                            CurrentShape = Convert.ToInt16(RandomNumberGenerator.GetInt32(7));
                            onFloor = false;
                        }


                    }
                }


                if (Console.KeyAvailable)
                {
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.LeftArrow:
                            for (int i = 0; i < 4; i++)
                            {
                                int playerY = (Shapes[CurrentShape][CurrentRotation * 4 + i].GetY() + PlayerPosition.GetY() + 1);
                                int playerX = Shapes[CurrentShape][CurrentRotation * 4 + i].GetX() + PlayerPosition.GetX();
                                int ElementToTheLeft = 0;

                                if (playerX > 0)
                                {
                                    ElementToTheLeft = board[(10 * playerY + playerX - 1)];
                                }


                                //int playerY = (Shapes[CurrentShape][CurrentRotation * 4 + i].GetY() + PlayerPosition.GetY()) + 1;
                                if (playerX - 1 < 0) { break; }
                                if (i == 3 && ElementToTheLeft != 1)
                                {
                                    PlayerPosition.SetX(PlayerPosition.GetX() - 1);
                                }
                                else if (ElementToTheLeft == 1)
                                { break; }

                            }
                            break;
                        case ConsoleKey.RightArrow:
                            for (int i = 0; i < 4; i++)
                            {
                                int playerY = (Shapes[CurrentShape][CurrentRotation * 4 + i].GetY() + PlayerPosition.GetY() + 1);
                                int playerX = Shapes[CurrentShape][CurrentRotation * 4 + i].GetX() + PlayerPosition.GetX();
                                int ElementToTheRight = 0;

                                if (playerX < 9)
                                {
                                    ElementToTheRight = board[(10 * playerY + playerX + 1)];
                                }

                                //int playerY = (Shapes[CurrentShape][CurrentRotation * 4 + i].GetY() + PlayerPosition.GetY()) + 1;
                                if (playerX + 1 > 9) { break; }
                                if (i == 3 && ElementToTheRight != 1)
                                {
                                    PlayerPosition.SetX(PlayerPosition.GetX() + 1);
                                }

                            }
                            break;
                        case ConsoleKey.UpArrow:

                            int RotTest = CurrentRotation + 1;
                            RotTest %= 3; 
                            bool validRotation=true;
                            for (int i = 0; i < 4; i++)
                            {
                                
                                int playerX = Shapes[CurrentShape][RotTest * 4 + i].GetX() + PlayerPosition.GetX();
                                int playerY = (Shapes[CurrentShape][RotTest * 4 + i].GetY() + PlayerPosition.GetY()) + 1;
                                int ElementToTheLeft = board[(10 * playerY + playerX - 1)];
                                int ElementToTheRight = board[(10 * playerY + playerX + 1)];
                                if (playerX > 9)
                                {
                                    validRotation = false;
                                    //PlayerPosition.SetX(PlayerPosition.GetX() - 1);
                                }
                                if (playerX < 0)
                                {
                                    validRotation = false;
                                    //PlayerPosition.SetX(PlayerPosition.GetX() + 1);
                                }

                                if (ElementToTheLeft != 0) { validRotation = false; }
                                if(ElementToTheRight!=0) { validRotation = false; }

                            }
                            CurrentRotation %= 3;
                            if (validRotation)
                            {
                                
                                CurrentRotation += 1;
                            }

                            break;


                    }
                }



            }
        }
        private void GameLogic() 
        {

            if (Sandmode) 
            {
            ApplyGravityToLines();
            }
            ClearLines();
            PlayerLogic();
            //CheckIfLineToBeClearedAndClearAlsoApplyGravityToBlocks();
        }
        private void ClearLines() //This works but the apply gravity method seems to result in odd line clearing behaviour, I don't have a tetris game to reference so I'll just guess it's ok
        {
            for (int i = 0; i < 20; i++) 
            {
                int block_amount = 0;
                for (int j = 0; j < 10; j++) 
                {
                    if(board[i * 10 + j] != 0) 
                    {

                        block_amount += 1;
                    }
                    
                }
                if (block_amount == 10) 
                {
                   
                    for (int j = 0; j < 10; j++)
                    {
                        if (Sandmode == false)
                        {
                            ApplyGravityToLines();
                        }


                        board[i * 10 + j] = 0;
                        
                    }
                }
            }
        }
        private void ApplyGravityToLines() //I'm not entirely sure that this is entirely correct
        {
            for(int i = 0;i < 20; i++) 
            {
                for (int j = 0; j < 10; j++)
                {
                    if (i != 19)
                    {
                        if (board[i * 10 + j] == 1 && board[(i + 1) * 10 + j] == 0)
                        {
                            board[i * 10 + j] = 0;
                            board[(i + 1) * 10 + j] = 1;
                            score += 1;

                        }
                    }
                }
            }
        }
        
        private void DrawScore() 
        {
            Console.SetCursorPosition(10, 20);
            Console.Write(score);
        }
        private void GameLoop() 
        {
            while (dead!=true)
            {
                Console.Clear();    
                DrawBoard();
                DrawShape();
                Thread.Sleep(50);
                GameLogic();
                DrawScore();
               
                
                
            }
            //Console.Clear();
            Console.SetCursorPosition(10, 20);
            Console.Write(' ');
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Thread.Sleep(20);
                    Console.Write("#");
                }
                Console.SetCursorPosition(0, i);
            }


            Console.WriteLine(score);
            Console.ReadKey();
        }



    }
}


