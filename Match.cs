﻿using System;

namespace OOP_Lab_3
{
    class Match
    {
        protected static int MatchCounter { get; set; } = 0;   
        
        protected const int NO = 0;
        protected const int CROSS = 1;
        protected const int NOUGHT = 2;
        
        public int ID { get; protected set; }
        public User First { get; protected set;  }
        public User Second { get; protected set;  }
        public User Winner { get; protected set; }
        protected int[,] Field { get; set; }

        public Match() { }

        public Match(User first, User second)
        {
            if (first.ID == second.ID) throw new ArgumentException("User cannot play against themself");
            this.First = first;
            this.Second = second;
            Play();
        }

        protected void Play()
        {
            Field = new int[3, 3] {
            {NO, NO, NO},
            {NO, NO, NO},
            {NO, NO, NO}
            };

            for (int i = 1; i <= Field.Length; i++)
            {
                if (i % 2 != 0)
                {
                    if (NextTurn(First, CROSS)) break;
                }
                else
                {
                    if (NextTurn(Second, NOUGHT)) break;
                }
            }
            PrintField();
            if (Winner == null) Console.WriteLine("*****TIE!*****");
            else Console.WriteLine("*****" + Winner.Name + " WINS!*****");

            First.UserHistory.Add(this);
            Second.UserHistory.Add(this);
            MatchCounter++;
            ID = MatchCounter;
        }

        protected Boolean NextTurn(User player, int crossOrNought)
        {
            PrintField();
            Console.WriteLine(player.Name + "'s turn");
            MakeMove(crossOrNought);

            if (WinCheck() != NO) Winner = player;
            return WinCheck() != NO;
        }

        protected void PrintField()
        {
            for (int i = 0; i < Field.GetLength(0); i++)
            {
                for (int j = 0; j < Field.GetLength(1); j++)
                {
                    switch (Field[i, j])
                    {
                        case NO:
                            Console.Write("-");
                            break;
                        case CROSS:
                            Console.Write("X");
                            break;
                        case NOUGHT:
                            Console.Write("O");
                            break;
                    }
                }
                Console.WriteLine();
            }
        }

        protected void MakeMove(int crossOrNought)
        {
            int[] result = new int[2];

            while (true)
            {
                Console.Write("\tEnter the row: ");
                try
                {
                    result[0] = Convert.ToInt32(Console.ReadLine());
                    if (Field[result[0], 0] != NO && Field[result[0], 1] != NO && Field[result[0], 2] != NO) continue;
                }
                catch (Exception)
                { 
                    continue;
                }
                break;
            }

            while (true)
            {
                Console.Write("\tEnter the column: ");
                try
                {
                    result[1] = Convert.ToInt32(Console.ReadLine());
                    if (Field[result[0], result[1]] != NO) continue;
                }
                catch (Exception)
                {
                    continue;
                }
                break;
            }

            Field[result[0], result[1]] = crossOrNought;
        }

        protected int WinCheck()
        {
            int whoWon = NO;

            if (Field[0, 0] != NO && Field[0, 0] == Field[1, 1] && Field[0, 0] == Field[2, 2]) whoWon = Field[0, 0];
            else if (Field[0, 2] != NO && Field[0, 2] == Field[1, 1] && Field[0, 2] == Field[2, 0]) whoWon = Field[0, 2];
            for (int i = 0; i < Field.GetLength(0) && whoWon == NO; i++)
            {
                if (Field[i, 0] != NO && Field[i, 0] == Field[i, 1] && Field[i, 0] == Field[i, 2]) whoWon = Field[i, 0];
            }
            for (int i = 0; i < Field.GetLength(1) && whoWon == NO; i++)
            {
                if (Field[0, i] != NO && Field[0, i] == Field[1, i] && Field[0, i] == Field[2, i]) whoWon = Field[0, i];
            }

            return whoWon;
        }
    }
}
