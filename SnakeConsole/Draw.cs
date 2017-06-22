using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeConsole
{
    static class Draw
    {
        private static int _width = 0;
        private static int _height = 0;
        private static int _highScore = 0;

        public static void ClearCursor()
        {
            Console.SetCursorPosition(0, _height + 1);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public static void Init(int width, int height)
        {
            _width = width;
            _height = height;
            Console.WindowHeight = height + 2;
            Console.WindowWidth = width + 2;
            Console.BufferHeight = height + 2;
            Console.BufferWidth = width + 2;
            Console.CursorVisible = false;
            Console.Title = "Snake by StaerkIO";
            Console.Clear();

            Boader(width, height);
        }

        public static void Clear(Point point)
        {
            Console.SetCursorPosition(point.Col, point.Row);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(" ");
            ClearCursor();
        }

        public static void HitSnake(Point point)
        {
            Console.SetCursorPosition(point.Col, point.Row);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.Write("X");
            ClearCursor();
        }

        public static void Snake(Point point)
        {
            Console.SetCursorPosition(point.Col, point.Row);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.Write("+");
            ClearCursor();
        }

        public static void Food(Point point)
        {
            Console.SetCursorPosition(point.Col, point.Row);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write("#");
            ClearCursor();
        }

        public static void Value(int value)
        {
            string v;
            if (value >= 0)
            {
                v = " +" + value.ToString();
            }
            else
            {
                v = value.ToString();
            }
            Console.SetCursorPosition(_width - v.Length, 0);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.Write(v);
            ClearCursor();
        }

        public static void Stone(Point point)
        {
            Console.SetCursorPosition(point.Col, point.Row);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.Write(" ");
            ClearCursor();
        }

        public static void HitStone(Point point)
        {
            Console.SetCursorPosition(point.Col, point.Row);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.Write("X");
            ClearCursor();
        }

        public static void GameOver(int score)
        {
            if (score > _highScore)
            {
                _highScore = score;
            }
            string go = " GAME OVER! Score: " + score + " ";
            Console.SetCursorPosition(_width/2 - go.Length/2, _height/2);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write(go);
            ClearCursor();

        }

        public static void HighScore()
        {
                string hs = "Highscore: " + _highScore;
                Console.SetCursorPosition(_width - hs.Length, _height);
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.Write(hs);
                ClearCursor();
        }

        private static void Boader(int width, int height)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.BackgroundColor = ConsoleColor.DarkYellow;

            for (int i = 0; i <= width; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write(" ");
                Console.SetCursorPosition(i, height);
                Console.Write(" ");
            }

            for (int i = 0; i < height; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(" ");
                Console.SetCursorPosition(width, i);
                Console.Write(" ");
            }
            string foot = "Tore Bieler EDITON";
            Console.SetCursorPosition(1, height);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.Write(foot);
            ClearCursor();
            HighScore();
        }
    }
}
