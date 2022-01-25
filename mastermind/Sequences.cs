using System;
using System.Threading;

namespace mastermind
{
    public class Sequences
    {
        public static void Win()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.SetCursorPosition(0, Console.CursorTop);
            for (int w = 0; w < 50; w++)
            {
                Console.Write("#");
                Thread.Sleep(50);
            }
            Console.WriteLine();
            for (int w = 0; w < 25; w++)
            {
                Console.WriteLine("               ");
                Thread.Sleep(100);
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.WriteLine("YOU WON!!!!!!!!");
                Thread.Sleep(100);
                Console.SetCursorPosition(0, Console.CursorTop - 1);
            }
            Console.WriteLine();
            for (int w = 0; w < 55; w++)
            {
                Console.Write("#");
                Thread.Sleep(50);
            }
            Console.WriteLine();
            Console.WriteLine("Press any key to finish");
            Console.ReadKey();
        }

        public static void Lose()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("No more attempts");
            Thread.Sleep(1000);
            for (int w = 0; w < 4; w++) 
            {
                Console.WriteLine("GAME OVER");
                Thread.Sleep(250);
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.WriteLine("         ");
                Thread.Sleep(250);
                Console.SetCursorPosition(0, Console.CursorTop - 1); 
            }
            Console.WriteLine("Press any key to finish");
            Console.ReadKey();
        }

        public static void Intro()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Welcome to....");
            Console.SetCursorPosition(0, 4);
            Thread.Sleep(250);
            Console.WriteLine(
                "                  ███╗░░░███╗░█████╗░░██████╗████████╗███████╗██████╗░███╗░░░███╗██╗███╗░░██╗██████╗░");
            Thread.Sleep(250);
            Console.WriteLine(
                "                  ████╗░████║██╔══██╗██╔════╝╚══██╔══╝██╔════╝██╔══██╗████╗░████║██║████╗░██║██╔══██╗");
            Thread.Sleep(250);
            Console.WriteLine(
                "                  ██╔████╔██║███████║╚█████╗░░░░██║░░░█████╗░░██████╔╝██╔████╔██║██║██╔██╗██║██║░░██║");
            Thread.Sleep(250);
            Console.WriteLine(
                "                  ██║╚██╔╝██║██╔══██║░╚═══██╗░░░██║░░░██╔══╝░░██╔══██╗██║╚██╔╝██║██║██║╚████║██║░░██║");
            Thread.Sleep(250);
            Console.WriteLine(
                "                  ██║░╚═╝░██║██║░░██║██████╔╝░░░██║░░░███████╗██║░░██║██║░╚═╝░██║██║██║░╚███║██████╔╝");
            Thread.Sleep(250);
            Console.WriteLine(
                "                  ╚═╝░░░░░╚═╝╚═╝░░╚═╝╚═════╝░░░░╚═╝░░░╚══════╝╚═╝░░╚═╝╚═╝░░░░░╚═╝╚═╝╚═╝░░╚══╝╚═════╝░");
            Thread.Sleep(250);
        }
    }
}