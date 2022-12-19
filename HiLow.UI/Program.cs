// See https://aka.ms/new-console-template for more information

using HiLow.Logic2.Interfaces;
using HiLow.Logic2.Logic;
using Microsoft.Extensions.DependencyInjection;
using static System.Net.Mime.MediaTypeNames;

namespace HiLow.UI // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static int _currentPlayer = 0;
        static int _players = 0;

        static void Main(string[] args)
        {
            var collection = new ServiceCollection();
            collection.AddSingleton<IGameLogic, GameLogic>();
            collection.AddSingleton<IPlaceHolderLogic, PlaceHolderLogic>();

            IServiceProvider serviceProvider = collection.BuildServiceProvider();
            var gameLogic = serviceProvider.GetService<IGameLogic>();

            if (gameLogic == null)
            {
                Console.WriteLine("Something went really wrong...");
                return;
            }

            WellcomeMessageAndTotalPlayers();

            int[] minMax = gameLogic.Config();
            Console.WriteLine($"Min: {minMax[0]}, Max: {minMax[1]}");

            bool guessed = false;
            int guessingNumber;

            while (guessed == false)
            {
                GetNextPlayer();

                Console.WriteLine($"Player {_currentPlayer}, guess the number: ");
                int.TryParse(Console.ReadLine(), out guessingNumber);

                if (guessingNumber >= minMax[0] && guessingNumber <= minMax[1])
                {
                    guessed = gameLogic.Play(_currentPlayer, guessingNumber);

                    Message(guessed);
                }
                else
                {
                    Console.WriteLine($"The number needs to be >= {minMax[0]} and <= {minMax[1]}");
                    _currentPlayer--;
                }
            }
        }

        private static void WellcomeMessageAndTotalPlayers()
        {
            Console.Write("Hello, welcome to the Hi-Lo game, set the number of players: ");
            while (_players == 0)
            {
                int.TryParse(Console.ReadLine(), out _players);

                if (_players == 0)
                {
                    Console.WriteLine("Not supported value for number of players");
                    Console.Write("number of players: ");
                }
            }
        }

        private static void Message(bool guessing)
        {
            if (guessing)
            {
                Console.WriteLine($"Congratulations Player {_currentPlayer} you've discovered the secret number.");
            }
            else
            {
                Console.WriteLine("That's not the secret number.");
            }
        }

        private static void GetNextPlayer()
        {
            _currentPlayer++;

            if (_currentPlayer > _players)
            {
                _currentPlayer = 1;
            }
        }
    }
}