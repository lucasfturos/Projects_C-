namespace Program
{
    using System;
    public struct Point
    {
        public int x, y;
    }
    public class Program
    {
        const int WIDTH = 20;
        const int HEIGHT = 50;

        public Point[] tail = new Point[100];
        public Point apple, snake;
        public int nTail, score, flag, i, j, k;
        public bool defeat;
        public char[,] camp = new char[WIDTH, HEIGHT];
        public Random r = new Random();

        public void setup()
        {
            score = 0;
            snake = new Point
            {
                x = (int)WIDTH / 2,
                y = (int)HEIGHT / 2
            };
            defeat = false;
            apple = new Point
            {
                x = r.Next(1, WIDTH - 1),
                y = r.Next(1, HEIGHT - 1)
            };
        }

        public void clean()
        {
            for (i = 0; i < WIDTH; i++)
            {
                for (j = 0; j < HEIGHT; j++)
                {
                    if (i == 0 || i == WIDTH - 1 || j == 0 || j == HEIGHT - 1)
                    {
                        camp[i, j] = '#';
                    }
                    else
                    {
                        camp[i, j] = ' ';
                    }

                    if (j == snake.y && i == snake.x)
                    {
                        camp[i, j] = '0';
                    }
                    else if (i == apple.x && j == apple.y)
                    {
                        camp[i, j] = '@';
                    }
                    else
                    {
                        for (k = 0; k < nTail; k++)
                        {
                            if (tail[k].x == i && tail[k].y == j)
                            {
                                camp[i, j] = '0';
                            }
                        }
                    }
                }
            }
        }

        public void draw()
        {
            Console.Write("\f\u001bc\x1b[3J");
            Console.WriteLine("Cobrinha em ASCII\n");
            for (i = 0; i < WIDTH; i++)
            {
                for (j = 0; j < HEIGHT; j++)
                {
                    Console.Write(camp[i, j]);

                }
                Console.WriteLine();
            }
            Console.WriteLine("\nPontos: " + score);
            Console.WriteLine("Click X para Sair\n");
        }

        public void input()
        {
            var choice = Console.ReadKey(true).Key;

            switch (choice)
            {
                case ConsoleKey.A:
                    flag = 1;
                    break;
                case ConsoleKey.S:
                    flag = 2;
                    break;
                case ConsoleKey.D:
                    flag = 3;
                    break;
                case ConsoleKey.W:
                    flag = 4;
                    break;
                case ConsoleKey.X:
                    defeat = true;
                    Console.Write("\f\u001bc\x1b[3J");
                    break;
            }
        }

        public void logic()
        {
            Point prev1, prev2;
            prev1 = new Point
            {
                x = tail[0].x,
                y = tail[0].y
            };

            tail[0] = new Point
            {
                x = snake.x,
                y = snake.y
            };

            for (k = 1; k < nTail; k++)
            {
                prev2 = new Point
                {
                    x = tail[k].x,
                    y = tail[k].y
                };

                tail[k] = new Point
                {
                    x = prev1.x,
                    y = prev1.y
                };

                prev1 = new Point
                {
                    x = prev2.x,
                    y = prev2.y
                };
            }

            switch (flag)
            {
                case 1:
                    snake.y--;
                    break;
                case 2:
                    snake.x++;
                    break;
                case 3:
                    snake.y++;
                    break;
                case 4:
                    snake.x--;
                    break;
                default:
                    break;
            }

            if (snake.x < 1 || snake.x > WIDTH - 2 || snake.y < 1 || snake.y > HEIGHT - 2)
            {
                defeat = true;
            }

            for (k = 0; k < nTail; k++)
            {
                if (tail[k].x == snake.x && tail[k].y == snake.y)
                {
                    defeat = true;
                }
            }

            if (snake.x == apple.x && snake.y == apple.y)
            {
                score += 10;
                apple = new Point
                {
                    x = r.Next(1, WIDTH - 1),
                    y = r.Next(1, HEIGHT - 1)
                };

                nTail++;
            }
        }

        static void Main(String[] argv)
        {
            Program p = new Program();
            p.setup();
            while (!p.defeat)
            {
                p.clean();
                p.draw();
                p.input();
                p.logic();
                Task.Delay(80).Wait();
            }
        }
    }
}