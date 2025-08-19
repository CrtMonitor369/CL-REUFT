using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CL_REUFT
{
    internal class GetToTheTop
    {
        //Heavily WIP, odds are this won't be done 
        class Position //Position class to make life easier
        {
            private int x;
            private int y;
            public Position(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
            public int GetX() { return x; }
            public int GetY() { return y; }
            public void SetX(int x) { this.x = x; }
            public void SetY(int y) { this.y = y; }

        }

        int map_width;
        int map_height;
        int Tile_AmountX = 20;
        int Tile_AmountY = 10;
        int animFrame = 0;
        int ftTravelled = 0;
        Position PlayerScreenSpacePosition = new Position(8, 5);
        Position PlayerWorldSpacePosition = new Position(10,590);
        
        Position Camera;
        List<int> map_static;
        private void Modify1DListWith2DCo0rds(Position pos, List<int> map, int tile) 
        {
        
        }
        private int Access1DListWith2DCoords(Position pos, List<int> map) 
        {
            return map[map_width*pos.GetY()+pos.GetX()];
        }
        

        private void SpawnBarrel() 
        {
        
        }
        public void Init() 
        {
            
            map_height = 600;
            map_width = 20;
            map_static = new List<int>();
            Camera = new Position(10,10);
            for (int i = 0; i < map_height*map_height; i++) 
            {
                int randomNumber = RandomNumberGenerator.GetInt32(0,100);
                if (randomNumber % 3 == 0) 
                {
                    map_static.Add(2);
                    map_static.Add(0);
                    i += 1;
                    continue;
                }
               else if (randomNumber == 55) 
                {
                    map_static.Add(3);
                }


                else
                {
                    map_static.Add(0);
                }
                 

            }

           /* map_static[1000 * 50 + 50] = 1; //Middle of the screen at the starting point of the camera
            map_static[1000 * 50 + 51] = 1;
            map_static[1000 * 50 + 52] = 1;
            map_static[1000 * 50 + 53] = 1;
           */
            Gameloop();
        }

        private void RenderBlockLayer() 
        {
        for(int i = Camera.GetY(); i < Tile_AmountY+Camera.GetY(); i++) 
            {
                for (int j = Camera.GetX(); j < Tile_AmountX+Camera.GetX(); j++)
                {
                   
                    switch (Access1DListWith2DCoords(new Position(j, i), map_static))
                    {
                        case 0:
                            Console.Write('\u2591');
                            break;
                        case 1:
                            Console.Write('\u2573');
                       
                            break;
                        case 2:
                            Console.Write('\u2593');
                            break;
                        case 3:
                            Console.Write("\u25A9");
                            break;
                        

                    }
                }
               
                Console.WriteLine();
                
            }   
        }
        private void SnapBackToObject(Position Camera, Position WorldSpaceCords) 
        {
            Camera.SetX(WorldSpaceCords.GetX());
            Camera.SetY(WorldSpaceCords.GetY());
            PlayerScreenSpacePosition.SetX(8);
            PlayerScreenSpacePosition.SetY(5);
        }
        private void GameLogic() 
        {
            animFrame += 1;
            animFrame %= 2;

            
            Position PreviousPosition = PlayerWorldSpacePosition;
            if (Console.KeyAvailable) 
            {
                switch (Console.ReadKey(true).Key) 
                {
                    
                    case ConsoleKey.D:
                        Camera.SetX(Camera.GetX()+1);
                        PlayerScreenSpacePosition.SetX(PlayerScreenSpacePosition.GetX()-1);
                        break;
                    case ConsoleKey.A:
                        Camera.SetX(Camera.GetX() - 1);
                        PlayerScreenSpacePosition.SetX(PlayerScreenSpacePosition.GetX() + 1);
                        break;
                    case ConsoleKey.W:
                        Camera.SetY(Camera.GetY() - 1);
                        PlayerScreenSpacePosition.SetY(PlayerScreenSpacePosition.GetY() + 1);
                        break;
                    case ConsoleKey.S:
                        Camera.SetY(Camera.GetY() + 1);
                        PlayerScreenSpacePosition.SetY(PlayerScreenSpacePosition.GetY()-1);
                        break;

                    case ConsoleKey.RightArrow:
                        SnapBackToObject(Camera, PlayerWorldSpacePosition);
                        PlayerLogic();

                       
                        PlayerScreenSpacePosition.SetX(PlayerScreenSpacePosition.GetX() + 1);
                        PlayerWorldSpacePosition.SetX(PlayerWorldSpacePosition.GetX() + 1);

                        break;
                    case ConsoleKey.LeftArrow:
                        SnapBackToObject(Camera, PlayerWorldSpacePosition);
                        

                        PlayerLogic();
                        PlayerScreenSpacePosition.SetX(PlayerScreenSpacePosition.GetX() - 1);
                        PlayerWorldSpacePosition.SetX(PlayerWorldSpacePosition.GetX() - 1);
                        break;

                    case ConsoleKey.UpArrow:
                        ftTravelled += 1;
                        SnapBackToObject(Camera, PlayerWorldSpacePosition);
                        if (ftTravelled >= 540) 
                        {
                            WinScreen();
                           
                        }

                        PlayerLogic();
                        PlayerScreenSpacePosition.SetY(PlayerScreenSpacePosition.GetY() - 1);
                        PlayerWorldSpacePosition.SetY(PlayerWorldSpacePosition.GetY() - 1);
                        break;
                    case ConsoleKey.DownArrow:
                        SnapBackToObject(Camera, PlayerWorldSpacePosition);
                        ftTravelled -= 1;

                        PlayerLogic();
                        PlayerScreenSpacePosition.SetY(PlayerScreenSpacePosition.GetY() + 1);
                        PlayerWorldSpacePosition.SetY(PlayerWorldSpacePosition.GetY() + 1);
                        break;
                    

                }
                
            }
            
        }
        private void RenderSpriteLayer() 
        {

        RenderPlayer();
        }

      
        private void WinScreen() 
        {
        Console.Clear();
        Console.Write("You win!");
        Console.ReadKey();
        }
        private void PlayerLogic()
        { //Excluding controls

            if (PlayerIsCollidingWithWhat() == 2)
            {

            }
            if (PlayerIsCollidingWithWhat() == 3)
            {
                int randomNumber = RandomNumberGenerator.GetInt32(0, 100);
                PlayerWorldSpacePosition = new Position(randomNumber, randomNumber);

                while (PlayerIsCollidingWithWhat() != 0) 
                {
                    randomNumber = RandomNumberGenerator.GetInt32(0, 100);
                    PlayerWorldSpacePosition = new Position(randomNumber, randomNumber);
                }
                ftTravelled += randomNumber;
                
            }
            if (Math.Abs((PlayerScreenSpacePosition.GetX() - 8)) >= 2)
            {
                if ((PlayerScreenSpacePosition.GetX() - 8) > 0)
                {
                    Camera.SetX(Camera.GetX()+1);
                }
                else 
                {
                    Camera.SetX(Camera.GetX() - 1);
                }
                    PlayerScreenSpacePosition.SetX(8);
                
            }
            //Camera logic
            if (Math.Abs((PlayerScreenSpacePosition.GetY() - 5)) >= 2)
            {
                if ((PlayerScreenSpacePosition.GetY() - 5) > 0)
                {
                    Camera.SetY(Camera.GetY() + 1);
                }
                else
                {
                    Camera.SetY(Camera.GetY() - 1);
                }
                PlayerScreenSpacePosition.SetY(5);

            }

           
            



        }

        private int PlayerIsCollidingWithWhat() 
        {

            return Access1DListWith2DCoords(new Position(PlayerWorldSpacePosition.GetX() + 8, PlayerWorldSpacePosition.GetY() + 5), map_static);
        }

        private void RenderPlayer() 
        {
            if (((PlayerScreenSpacePosition.GetX() > 0) && (PlayerScreenSpacePosition.GetX() < 20)) && ((PlayerScreenSpacePosition.GetY() > 0) && (PlayerScreenSpacePosition.GetY() < 10)))
            {
                Console.SetCursorPosition(PlayerScreenSpacePosition.GetX(), PlayerScreenSpacePosition.GetY());
                if (animFrame == 0)
                {
                    Console.Write('\u12F3');
                }
                else
                {
                    Console.Write('\u12F6');
                }
            }
        //Console.SetCursorPosition(0, 0);
        }

        private void DebugInfo() 
        {
            Console.SetCursorPosition(2, 10);
            Console.Write(PlayerWorldSpacePosition.GetX());
            Console.Write(" ");
            Console.Write(PlayerWorldSpacePosition.GetY());
            Console.SetCursorPosition(2, 11);
            Console.Write(Camera.GetX());
            Console.Write(" ");
            Console.Write(Camera.GetY());

            Console.SetCursorPosition(8, 10);
            Console.Write(PlayerIsCollidingWithWhat());
            Console.SetCursorPosition(0,0);
            
        }

        private void DrawUI() 
        {
            Console.SetCursorPosition(2, 10);
            Console.Write(ftTravelled);
            Console.Write("  FT travelled");
        }
        private void Gameloop() 
        {
            while (true)
            {
                Console.Clear();
              
                RenderBlockLayer();
                RenderSpriteLayer();
                GameLogic();
                //DebugInfo();
                DrawUI();
                Thread.Sleep(50);


                Console.SetCursorPosition(20, 29);
                Console.Write(" ");

            }
        }


    }
}
