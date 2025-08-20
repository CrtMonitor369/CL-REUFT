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
        public class Position //Position class to make life easier
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

        static int map_width;
        static int map_height;
        int Tile_AmountX = 20;
        int Tile_AmountY = 10;
        
        
       
        
        Player player;
        List<int> map_static;


        public void Init()
        {
            Console.WriteLine("WASD to move camera, arrow keys to move player");
            Console.WriteLine("Your goal is to get to the top at ~500 ft"); 
            Console.WriteLine("the player can walk over the walls by pressing the opposite key of where they want to go when on top of them");
            Console.WriteLine("There are special blocks scattered around, test out what they do, some of them may be of use while others may be disadvantageous");
            Console.ReadKey();
            player = new Player(new Position(50, 1000));
            map_height = 1500; //Redundancy to ensure the player doesn't go out of the map 
            map_width = 40;
            map_static = new List<int>();

            for (int i = 0; i < map_height * map_height; i++)
            {
                int randomNumber = RandomNumberGenerator.GetInt32(0, 100);
                if (randomNumber % 3 == 0)
                {
                    map_static.Add(2);
                    map_static.Add(0);
                    i += 1;
                    continue;
                }
                else if (randomNumber % 5 == 0)
                {
                    map_static.Add(1);
                    map_static.Add(0);
                    i += 1;
                    continue;
                }
                else if (randomNumber % 7 == 0)
                {
                    map_static.Add(3);
                }
                else if (randomNumber % 32 == 0)
                {
                    map_static.Add(4);
                }

                else
                {
                    map_static.Add(0);
                }
                


            }

            while (player.PlayerIsCollidingWithWhat(map_static) != 0) 
            {
            player.PlayerWorldSpace.SetX(player.PlayerWorldSpace.GetX()+1);
                player.PlayerWorldSpace.SetY(player.PlayerWorldSpace.GetY() + 1);
            }

            /* map_static[1000 * 50 + 50] = 1; //Middle of the screen at the starting point of the camera
             map_static[1000 * 50 + 51] = 1;
             map_static[1000 * 50 + 52] = 1;
             map_static[1000 * 50 + 53] = 1;
            */
            Gameloop();
        }
        static int Access1DListWith2DCoords(Position pos, List<int> map)
        {
            return map[map_width * pos.GetY() + pos.GetX()];
        }
        class Player 
        {
            public int dir = 0;
            public Position Camera = new Position(0, 0);
            public int ftTravelled = 0;
            public int animFrame = 0;

            public Position PlayerWorldSpace;
            public Position PlayerScreenSpace = new Position(8, 5);
            public Player(Position starting_position) 
            {
               
                PlayerWorldSpace = starting_position;
            }
            public void PlayerLogic(List<int> map_static)
            { //Excluding controls
                animFrame += 1;
                animFrame %= 2;
                
                
               
                if (Math.Abs((PlayerScreenSpace.GetX() - 8)) >= 2)
                {
                    if ((PlayerScreenSpace.GetX() - 8) > 0)
                    {
                        Camera.SetX(Camera.GetX() + 1);
                    }
                    else
                    {
                        Camera.SetX(Camera.GetX() - 1);
                    }
                    PlayerScreenSpace.SetX(8);

                }
                //Camera logic
                if (Math.Abs((PlayerScreenSpace.GetY() - 5)) >= 2)
                {
                    if ((PlayerScreenSpace.GetY() - 5) > 0)
                    {
                        Camera.SetY(Camera.GetY() + 1);
                    }
                    else
                    {
                        Camera.SetY(Camera.GetY() - 1);
                    }
                    PlayerScreenSpace.SetY(5);

                }
            }
               public  int PlayerIsCollidingWithWhat(List<int> map_static)
            {

                return Access1DListWith2DCoords(new Position(PlayerWorldSpace.GetX() + 8, PlayerWorldSpace.GetY() + 5), map_static);
            }
            
        }


        
        private void RenderBlockLayer() 
        {
        for(int i = player.Camera.GetY(); i < Tile_AmountY+player.Camera.GetY(); i++) 
            {
                for (int j = player.Camera.GetX(); j < Tile_AmountX+player.Camera.GetX(); j++)
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
                        case 4:
                            Console.Write("\u25B3");
                            break;


                    }
                }
               
                Console.WriteLine();
                
            }   
        }
        public void SnapBackToObject(Position Camera, Position WorldSpaceCords) 
        {
            Camera.SetX(WorldSpaceCords.GetX());
            Camera.SetY(WorldSpaceCords.GetY());
            player.PlayerScreenSpace.SetX(8);
            player.PlayerScreenSpace.SetY(5);
        }
        private void GameLogic() 
        {

             if (player.PlayerIsCollidingWithWhat(map_static) == 3)
                {
                    int randomNumber = RandomNumberGenerator.GetInt32(-100, 100);

                    player.PlayerWorldSpace.SetX(player.PlayerWorldSpace.GetX() + randomNumber);
                    player.PlayerWorldSpace.SetY(player.PlayerWorldSpace.GetY() + randomNumber);
                    while (player.PlayerIsCollidingWithWhat(map_static) != 0)
                    {
                        randomNumber = RandomNumberGenerator.GetInt32(-25, 25);
                        player.PlayerWorldSpace.SetX(player.PlayerWorldSpace.GetX() + randomNumber);
                    }
                    
                       player.ftTravelled += randomNumber;

                }

                if (player.PlayerIsCollidingWithWhat(map_static) == 1)
                {

                    
                    player.PlayerWorldSpace = new Position(50, 1000);
                    

                    player.ftTravelled = 0;

                }
            if (player.PlayerIsCollidingWithWhat(map_static) == 4)
            {


                player.PlayerWorldSpace = new Position(50, 300);


                player.ftTravelled = 300;

            }

            if (Console.KeyAvailable) 
            {
                
                switch (Console.ReadKey(true).Key) 
                {

                    
                    case ConsoleKey.D:
                        player.Camera.SetX(player.Camera.GetX()+1);
                        player.PlayerScreenSpace.SetX(player.PlayerScreenSpace.GetX()-1);
                        break;
                    case ConsoleKey.A:
                        player.Camera.SetX(player.Camera.GetX() - 1);
                        player.PlayerScreenSpace.SetX(player.PlayerScreenSpace.GetX() + 1);
                        break;
                    case ConsoleKey.W:
                        player.Camera.SetY(player.Camera.GetY() - 1);
                        player.PlayerScreenSpace.SetY(player.PlayerScreenSpace.GetY() + 1);
                        break;
                    case ConsoleKey.S:
                        player.Camera.SetY(player.Camera.GetY() + 1);
                        player.PlayerScreenSpace.SetY(player.PlayerScreenSpace.GetY()-1);
                        break;

                    case ConsoleKey.RightArrow:
                        SnapBackToObject(player.Camera, player.PlayerWorldSpace);
                        player.PlayerLogic(map_static);
                        

                        if (player.PlayerIsCollidingWithWhat(map_static) == 0)
                        {
                            player.PlayerScreenSpace.SetX(player.PlayerScreenSpace.GetX()+1);
                            
                            player.PlayerWorldSpace.SetX(player.PlayerWorldSpace.GetX() + 1);
                        }
                        else {
                            player.PlayerScreenSpace.SetX(player.PlayerScreenSpace.GetX() - 1);
                            player.PlayerWorldSpace.SetX(player.PlayerWorldSpace.GetX() - 1);
                        }
                            break;
                    case ConsoleKey.LeftArrow:
                        SnapBackToObject(player.Camera, player.PlayerWorldSpace);
                        if (player.PlayerIsCollidingWithWhat(map_static) == 0)
                        {

                            player.PlayerLogic(map_static);
                            player.PlayerScreenSpace.SetX(player.PlayerScreenSpace.GetX() - 1);
                            player.PlayerWorldSpace.SetX(player.PlayerWorldSpace.GetX() - 1);
                        }
                        else
                        {
                            player.PlayerScreenSpace.SetX(player.PlayerScreenSpace.GetX() + 1);
                            player.PlayerWorldSpace.SetX(player.PlayerWorldSpace.GetX() + 1);
                        }
                        break;

                    case ConsoleKey.UpArrow:
                        SnapBackToObject(player.Camera, player.PlayerWorldSpace);
                        if (player.ftTravelled >= 540)
                        {
                            WinScreen();

                        }
                        if (player.PlayerIsCollidingWithWhat(map_static) == 0)
                        {
                            player.ftTravelled += 1;

                            
                            player.PlayerLogic(map_static);
                            player.PlayerScreenSpace.SetY(player.PlayerScreenSpace.GetY() - 1);
                            player.PlayerWorldSpace.SetY(player.PlayerWorldSpace.GetY() - 1);
                        }
                        else
                        {
                            player.PlayerScreenSpace.SetY(player.PlayerScreenSpace.GetY() + 1);
                            player.PlayerWorldSpace.SetY(player.PlayerWorldSpace.GetY() + 1);
                        }


                        break;
                    case ConsoleKey.DownArrow:
                        
                        SnapBackToObject(player.Camera, player.PlayerWorldSpace);
                        if (player.PlayerIsCollidingWithWhat(map_static) == 0)
                        {
                            player.ftTravelled -= 1;

                            player.PlayerLogic(map_static);
                            player.PlayerScreenSpace.SetY(player.PlayerScreenSpace.GetY() + 1);
                            player.PlayerWorldSpace.SetY(player.PlayerWorldSpace.GetY() + 1);
                        }
                        else
                        {
                            player.PlayerScreenSpace.SetY(player.PlayerScreenSpace.GetY() - 1);
                            player.PlayerWorldSpace.SetY(player.PlayerWorldSpace.GetY() - 1);
                        }
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
       

        

        private void RenderPlayer() 
        {
            if (((player.PlayerScreenSpace.GetX() > 0) && (player.PlayerScreenSpace.GetX() < 20)) && ((player.PlayerScreenSpace.GetY() > 0) && (player.PlayerScreenSpace.GetY() < 10)))
            {
                Console.SetCursorPosition(player.PlayerScreenSpace.GetX(), player.PlayerScreenSpace.GetY());
                if (player.animFrame == 0)
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
            Console.Write(player.PlayerWorldSpace.GetX());
            Console.Write(" ");
            Console.Write(player.PlayerWorldSpace.GetY());
            Console.SetCursorPosition(2, 11);
            Console.Write(player.Camera.GetX());
            Console.Write(" ");
            Console.Write(player.Camera.GetY());

            Console.SetCursorPosition(8, 10);
            Console.Write("   ");
            Console.Write(player.PlayerIsCollidingWithWhat(map_static));
            Console.SetCursorPosition(0,0);
            
        }

        private void DrawUI() 
        {
            Console.SetCursorPosition(2, 10);
            Console.Write(player.ftTravelled);
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
