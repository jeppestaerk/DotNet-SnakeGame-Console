using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SnakeConsole
{
    internal struct Point
    {
        public override int GetHashCode()
        {
            unchecked
            {
                return (Col * 397) ^ Row;
            }
        }

        public int Col;
        public int Row;

        public Point(int col, int row)
        {
            this.Col = col;
            this.Row = row;
        }

        public bool Equals(Point other)
        {
            return Col == other.Col && Row == other.Row;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Point && Equals((Point)obj);
        }

        public static bool operator ==(Point p1, Point p2)
        {
            return p1.Equals(p2);
        }

        public static bool operator !=(Point p1, Point p2)
        {
            return !p1.Equals(p2);
        }
    }

    internal struct Directions
    {
        public static Point Right = new Point(1, 0);
        public static Point Left = new Point(-1, 0);
        public static Point Up = new Point(0, -1);
        public static Point Down = new Point(0, 1);
    }

    class Program
    {
        static void Main()
        {
            Console.Clear();
            int score = 0;
            int foodValue = 1000;
            int penalty = 0;
            int height = 30;
            int width = 60;
            int speed = 0;
            int count = 0;

            Random random = new Random();
            Draw.Init(width, height);

            List<Point> stones = new List<Point>();

            Queue<Point> snake = new Queue<Point>();
            snake.Enqueue(new Point(1,1));
            Point direction = Directions.Right;

            Point food;
            do
            {
                food = new Point(random.Next(1, width - 1), random.Next(1, height - 1));
            } while (snake.Contains(food) || stones.Contains(food));
            Draw.Food(food);

            

            while (true)
            {
                Point snakeHead = snake.Last();

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo userInput = Console.ReadKey();
                    if (userInput.Key == ConsoleKey.RightArrow && direction != Directions.Left)
                        direction = Directions.Right;
                    if (userInput.Key == ConsoleKey.LeftArrow && direction != Directions.Right)
                        direction = Directions.Left;
                    if (userInput.Key == ConsoleKey.UpArrow && direction != Directions.Down)
                        direction = Directions.Up;
                    if (userInput.Key == ConsoleKey.DownArrow && direction != Directions.Up)
                        direction = Directions.Down;
                }

                Point snakeNewHead = new Point(snakeHead.Col + direction.Col, snakeHead.Row + direction.Row);

                if (snakeNewHead.Col < 1)
                {
                    snakeNewHead.Col = width -1;
                    penalty += 100;
                    score -= 100;
                }
                if (snakeNewHead.Col >= width)
                {     
                    snakeNewHead.Col = 1;
                    penalty += 100;
                    score -= 100;
                }
                if (snakeNewHead.Row < 1)
                {
                    snakeNewHead.Row = height -1;
                    penalty += 100;
                    score -= 100;
                }
                if (snakeNewHead.Row >= height)
                {
                    snakeNewHead.Row = 1;
                    penalty += 100;
                    score -= 100;
                }

                if (snake.Contains(snakeNewHead))
                {
                    bool hit = true;
                    penalty += 1000;
                    score -= 1000;

                    Point hitPoint = new Point(snakeNewHead.Col, snakeNewHead.Row);
                    Draw.HitSnake(hitPoint);

                    Point newHeadRight = new Point(snakeHead.Col + Directions.Right.Col, snakeHead.Row + Directions.Right.Row);
                    Point newHeadLeft = new Point(snakeHead.Col + Directions.Left.Col, snakeHead.Row + Directions.Left.Row);
                    Point newHeadUp = new Point(snakeHead.Col + Directions.Up.Col, snakeHead.Row + Directions.Up.Row);
                    Point newHeadDown = new Point(snakeHead.Col + Directions.Down.Col, snakeHead.Row + Directions.Down.Row);

                    if ((snake.Contains(newHeadRight) || stones.Contains(newHeadRight)) && (snake.Contains(newHeadLeft) || stones.Contains(newHeadLeft)) && (snake.Contains(newHeadUp) || stones.Contains(newHeadUp)) && (snake.Contains(newHeadDown) || stones.Contains(newHeadDown)))
                    {
                        Draw.GameOver();
                        Thread.Sleep(5000);
                        Program.Main();
                    }

                    do
                    {
                        if (Console.KeyAvailable)
                        {
                            ConsoleKeyInfo userInput = Console.ReadKey();
                            if (userInput.Key == ConsoleKey.RightArrow)
                            {
                                if (!snake.Contains(newHeadRight) && (direction.Equals(Directions.Up) || direction.Equals(Directions.Down)))
                                {
                                    direction = Directions.Right;
                                    snakeNewHead = newHeadRight;
                                    hit = false;
                                }
                            }
                            if (userInput.Key == ConsoleKey.LeftArrow)
                            {
                                if (!snake.Contains(newHeadLeft) && (direction.Equals(Directions.Up) || direction.Equals(Directions.Down)))
                                {
                                    direction = Directions.Left;
                                    snakeNewHead = newHeadLeft;
                                    hit = false;
                                }
                            }
                            if (userInput.Key == ConsoleKey.UpArrow)
                            {
                                if (!snake.Contains(newHeadUp) && (direction.Equals(Directions.Left) || direction.Equals(Directions.Right)))
                                {
                                    direction = Directions.Up;
                                    snakeNewHead = newHeadUp;
                                    hit = false;
                                }
                            }
                            if (userInput.Key == ConsoleKey.DownArrow)
                            {
                                if (!snake.Contains(newHeadDown) && (direction.Equals(Directions.Left) || direction.Equals(Directions.Right)))
                                {
                                    direction = Directions.Down;
                                    snakeNewHead = newHeadDown;
                                    hit = false;
                                }
                            }
                        }
                    } while (hit);

                    Draw.Snake(hitPoint);
                }

                if (stones.Contains(snakeNewHead))
                {
                    bool hit = true;
                    penalty += 1000;
                    score -= 1000;

                    Point hitPoint = new Point(snakeNewHead.Col, snakeNewHead.Row);
                    Draw.HitStone(hitPoint);

                    Point newHeadRight = new Point(snakeHead.Col + Directions.Right.Col, snakeHead.Row + Directions.Right.Row);
                    Point newHeadLeft = new Point(snakeHead.Col + Directions.Left.Col, snakeHead.Row + Directions.Left.Row);
                    Point newHeadUp = new Point(snakeHead.Col + Directions.Up.Col, snakeHead.Row + Directions.Up.Row);
                    Point newHeadDown = new Point(snakeHead.Col + Directions.Down.Col, snakeHead.Row + Directions.Down.Row);

                    if ((snake.Contains(newHeadRight) || stones.Contains(newHeadRight)) && (snake.Contains(newHeadLeft) || stones.Contains(newHeadLeft)) && (snake.Contains(newHeadUp) || stones.Contains(newHeadUp)) && (snake.Contains(newHeadDown) || stones.Contains(newHeadDown)))
                    {
                        Draw.GameOver();
                        Draw.ClearCursor();
                        Thread.Sleep(5000);
                        Program.Main();
                    }

                    do
                    {
                        if (Console.KeyAvailable)
                        {
                            ConsoleKeyInfo userInput = Console.ReadKey();
                            if (userInput.Key == ConsoleKey.RightArrow)
                            {
                                if (!snake.Contains(newHeadRight) && (direction.Equals(Directions.Up) || direction.Equals(Directions.Down)))
                                {
                                    direction = Directions.Right;
                                    snakeNewHead = newHeadRight;
                                    hit = false;
                                }
                            }
                            if (userInput.Key == ConsoleKey.LeftArrow)
                            {
                                if (!snake.Contains(newHeadLeft) && (direction.Equals(Directions.Up) || direction.Equals(Directions.Down)))
                                {
                                    direction = Directions.Left;
                                    snakeNewHead = newHeadLeft;
                                    hit = false;
                                }
                            }
                            if (userInput.Key == ConsoleKey.UpArrow)
                            {
                                if (!snake.Contains(newHeadUp) && (direction.Equals(Directions.Left) || direction.Equals(Directions.Right)))
                                {
                                    direction = Directions.Up;
                                    snakeNewHead = newHeadUp;
                                    hit = false;
                                }
                            }
                            if (userInput.Key == ConsoleKey.DownArrow)
                            {
                                if (!snake.Contains(newHeadDown) && (direction.Equals(Directions.Left) || direction.Equals(Directions.Right)))
                                {
                                    direction = Directions.Down;
                                    snakeNewHead = newHeadDown;
                                    hit = false;
                                }
                            }
                        }
                    } while (hit);

                    Draw.Stone(hitPoint);
                }
                
                snake.Enqueue(snakeNewHead);
                Draw.Snake(snakeNewHead);

                if (snakeNewHead.Equals(food))
                {
                    count++;
                    score += foodValue;
                    foodValue = 1000;
                    if (speed < 100 && count == 10)
                    {
                        count = 0;    
                        speed += 10;
                        Point stone;
                        do
                        {
                            stone = new Point(random.Next(1, width - 1), random.Next(1, height - 1));
                        } while (snake.Contains(stone) || stones.Contains(stone) || food.Equals(stone));
                        stones.Add(stone);
                        Draw.Stone(stone);
                    }
                    do
                    {
                        food = new Point(random.Next(1, width - 1), random.Next(1, height - 1));
                    } while (snake.Contains(food) || stones.Contains(food));
                    Draw.Food(food);

                    Console.Beep();
                }
                else
                {
                    Point snakeTail = snake.Dequeue();
                    Draw.Clear(snakeTail);
                    foodValue -= 5;
                }

                if (foodValue <= 0)
                {
                    Draw.Clear(food);
                    foodValue = 1000;
                    do
                    {
                        food = new Point(random.Next(1, width - 1), random.Next(1, height - 1));
                    } while (snake.Contains(food) || stones.Contains(food));
                    Draw.Food(food);
                }

                Console.SetCursorPosition(1, 0);
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.Write("Score: " + score + " Food: " + foodValue + " Penalty: " + penalty + " Speed: " + speed + "     ");
                
                Draw.ClearCursor();
                Thread.Sleep(100 - speed);
            }
        }
    }
}