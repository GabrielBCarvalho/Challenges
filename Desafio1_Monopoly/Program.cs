using System;
using System.Text.RegularExpressions;

namespace Desafio1_Monopoly
{
    class Program
    {
        public class Player
        {
            private int position, money;
            private bool isArrested;

            public Player(int position, int money, bool isArrested)
            {  
                this.position = position;
                this.money = money;
                this.isArrested = isArrested;
            }

            public int Position
            {
                get { return position; }
                set { position = value; }
            }

            public int Money
            {
                get { return money; }
                set { money = value; }
            }

            public bool IsArrested
            {
                get { return isArrested; }
                set { isArrested = value; }
            }
        }
        

        static void Main(string[] args)
        {
            Console.WriteLine("Qual a posição atual do jogador? (Considere um número inteiro)");
            int position = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Quanto dinheiro ele tem? (Considere um número inteiro)");
            int money = Convert.ToInt32(Console.ReadLine());

            bool isArrested = false;
            Console.WriteLine("Ele está preso? S/N");
            string arrested = Console.ReadLine();
            if(arrested == "s" || arrested == "S")
                isArrested = true;
            else if(arrested == "n" || arrested == "N")
                isArrested = false;

            Player player = default_move(position, isArrested, money);

            Console.WriteLine("Fim da jogada: ");
            Console.WriteLine("     Posicao do jogador: " + player.Position.ToString());
            Console.WriteLine("     Dinheiro do jogador: " + player.Money.ToString());
            Console.WriteLine("     Está preso? " + player.IsArrested.ToString());
        }


        public static Player default_move(int piece, bool prison_try, int money)
        {
            int firstDice, secondDice;
            Random rnd = new Random();

            if(!prison_try)  // Não está preso
            {
                int counterPlays = 1;

                while(true)
                {
                    int sum = 0;
                    if(counterPlays < 4)
                    {
                        Console.WriteLine(counterPlays.ToString() + "ª jogada");
                        firstDice  = rnd.Next(1, 7);
                        Console.WriteLine("     Primeiro dado: " + firstDice.ToString());
                        secondDice = rnd.Next(1, 7);
                        Console.WriteLine("     Segundo dado: " + secondDice.ToString());

                        sum += (firstDice + secondDice);
                        counterPlays++;
                        if(firstDice != secondDice)
                        {
                            Console.WriteLine("     O jogador andará " + sum.ToString());
                            Player player = new Player(piece + sum, money, false);
                            return player;
                        }
                        else
                        {
                            Console.WriteLine("     O jogador andará " + sum.ToString() + " e pode jogar de novo.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("     O jogador tirou 3 doubles e está preso.");
                        Player player = new Player(piece + sum, money, true);
                        return player;
                    }
                }
            }
            else
            {
                int i = 1;
                int sum = 0;
                bool noPayment = false;

                while(i <= 3)
                {
                    Console.WriteLine(i.ToString() + "ª jogada");
                    firstDice  = rnd.Next(1, 7);
                    Console.WriteLine("     Primeiro dado: " + firstDice.ToString());
                    secondDice = rnd.Next(1, 7);
                    Console.WriteLine("     Segundo dado: " + secondDice.ToString());

                    if(firstDice == secondDice)
                    {
                        sum = firstDice + secondDice;
                        noPayment = true;
                        break;
                    }
                    i++;
                }

                if(noPayment)
                {
                    Player player = new Player(piece + sum, money, false);
                    return player;
                }
                else
                {
                    Player player = new Player(piece + sum, money - 50, false);
                    return player;
                }
            }
        }
    }   
}
