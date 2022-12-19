using HiLow.Logic2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiLow.Logic2.Logic
{
    public class GameLogic : IGameLogic
    {
        private readonly Dictionary<int, int> _playerLadder = new();
        private int _min = 0;
        private int _max = 0;
        private int _secretNumber;
        private IPlaceHolderLogic _placeHolder;

        public GameLogic(IPlaceHolderLogic placeHolder)
        {
            _placeHolder = placeHolder;
        }

        public int[] Config()
        {
            if (_min == 0 && _max == 0)
            {
                _min = new Random().Next(10);
                _max = new Random().Next(11, 20);

                _secretNumber = new Random().Next(_min, _max);
            }

            return new int[] { _min, _max };
        }

        public bool Play(int playerId, int guessingNumber)
        {
            CheckIfPlayerExistsIfNotAddsIt(playerId);

            if (guessingNumber == _secretNumber)
            {
                return true;
            }
            else
            {
                _playerLadder[playerId] = _playerLadder[playerId]++;
                return false;
            }
        }

        public bool PlaceHolder(bool input)
        {
            return _placeHolder.PlaceHolderFunction(input);
        }

        private void CheckIfPlayerExistsIfNotAddsIt(int playerId)
        {
            if (_playerLadder.ContainsKey(playerId) == false)
            {
                _playerLadder.Add(playerId, 1);
            }
        }
    }
}
