using System;

namespace battleship_dotnet_master
{
    class Battleship {
        public int xpos;
        public int ypos;
        public string align; // horizontal or vertical
        //public bool hit;
        public int length;

        public Battleship() {
            xpos = -1;
            ypos = -1;
            align = "h"; 
            //hit = false;
            length = 0;
        }
    }
    class Program
    {        
        static bool addship(Battleship ship, string[,] map) {
            /*  add a ship into the map
                Return: 
                    ture - added successfully 
                    false - invalid position 
            */             
            if (ship.xpos>=10 || ship.ypos>=10 || ship.xpos<0 || ship.ypos<0) {
                return false;
            }
            switch (ship.align) {
                case "h":
                    if (ship.xpos + ship.length < 10) {
                        for (int x=ship.xpos; x<ship.xpos+ship.length; x++) {
                            map[x, ship.ypos] = "A";
                        }
                    }
                    return true;
                    //break;

                case "v":
                    if (ship.ypos + ship.length < 10) {
                        for (int y=ship.ypos; y<ship.ypos+ship.length; y++) {
                            map[ship.xpos, y] = "A";
                        }
                    }
                    return true;
                    //break;
            }

            return false;
        }       

        static void PrintMap(string[,] map, string title) {
            /*  print out the map with a title
                
            */             
            Console.WriteLine(title);
            for (int i=0; i<10; i++) {
                for (int j=0; j<10; j++) {
                    Console.Write(map[i,j]);
                    
                }
                Console.WriteLine();
            }
        }           

        static bool isAttacked(int x, int y, string[,] map) {      
            /*  verify if the position has been attacked
                Return: 
                    ture - attacked
                    false - available spot
            */                 
            if (map[x, y] == "X" || map[x, y] == "H") {
                return true;
            } else {
                return false;
            }
        }

        static bool isHit(int x, int y, string[,] map) {        
            /*  verify if there is a ship at the position
                Return: 
                    ture - hit
                    false - miss
            */                
            if (map[x, y] == "A") {
                map[x, y] = "H";
                return true;
            } else {
                return false;
            }
        }        
          
        static bool attacking() {
            /*  the player attacks a position, verify if the position has been attacked or not 
                Return: 
                    ture - hit
                    false - miss
            */            
            string userinput;
            int x, y;
            bool valid = false;

            do {
                Console.WriteLine("Which Spot would you like to attack?");
                do {
                    Console.WriteLine("Input X position (0~9): ");                
                    userinput = Console.ReadLine();   
                    if (!int.TryParse(userinput, out x)) {
                        x = -1;
                    }         
                } while (!(x>=0 && x<=9));
                do {
                    Console.WriteLine("Input Y position (0~9): ");                
                    userinput = Console.ReadLine();   
                    if (!int.TryParse(userinput, out y)) {
                        y = -1;
                    }        
                } while (!(y>=0 && y<=9));    
 
                if (isAttacked(x, y, mymap2)) {
                    Console.WriteLine("({0}, {1}) has been attacked! Please choose another spot.", x, y);                     
                } else {
                    Console.WriteLine("Attacking ({0}, {1})!", x, y); 
                    mymap2[x, y] = "X";     
                    valid = true;               
                }                             
            } while (!valid);            

            if (isHit(x, y, enermymap)) {
                winner = "You";
                return true;
            } else {
                return false;
            }            
        }   

        static bool pcattacking() {
            /*  the program attacks a position randomly
                Return: 
                    ture - hit
                    false - miss
            */
            int x, y;
            Random rnd = new Random();            

            do {
                x = rnd.Next(10);
                y = rnd.Next(10);                                          
            } while (isAttacked(x, y, mymap));        
            
            Console.WriteLine("PC is attacking ({0}, {1})!", x, y); 
            if (isHit(x, y, mymap)) {
                winner = "Computer";
                return true;
            } else {
                mymap[x, y] = "X";    
                return false;
            }
        }                 
        
        // initial setup         
        static string [,] mymap = new string[10,10];
        static string [,] enermymap = new string[10,10];      
        static string [,] mymap2 = new string[10,10];   
        static string winner = "";       

        static void Main(string[] args)
        {   
            Battleship myship = new Battleship() {
                xpos = 3,
                ypos = 3,
                length = 5
            };
            Battleship enermyship = new Battleship() {
                xpos = 4,
                ypos = 4,
                length = 5,
                align = "v"
            };              
            string useraction;   

            Console.WriteLine("Battleship");
            // initial board setup
            for (int i=0; i<10; i++) {
                for (int j=0; j<10; j++) {
                    mymap[i,j] = "~";
                    enermymap[i,j] = "~";
                    mymap2[i,j] = "O";
                }
            }
            addship(myship, mymap);
            addship(enermyship, enermymap);            

            // game starts
            do {
                PrintMap(mymap, "My Map");
                PrintMap(mymap2, "Attacking History");
                //PrintMap(enermymap, "Enermy's Map");                
                Console.WriteLine("Your Turn! Press (1) to attack, press (2) to quit the game...");
                useraction = Console.ReadLine();
                if (useraction == "1") {
                    Console.WriteLine("Attacking!");
                    if (attacking() || pcattacking()) {
                        // hit a ship
                        break;
                    } 
                } else if (useraction == "2") {
                    Console.WriteLine("Quit!");    
                } else {
                    Console.WriteLine("Sorry, unknown action. Please input the correct action.");                       
                }       
            } while (useraction != "2");

            // print the outcome
            if (winner != "") {
                Console.WriteLine("Game Over! The winner is {0}!", winner);   
                PrintMap(mymap, "My Map");   
                PrintMap(enermymap, "Enermy's Map");                               
            }
        }
    }
}
