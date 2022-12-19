using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiLow.Logic2.Interfaces
{
    public interface IGameLogic
    {
        int[] Config();
        bool Play(int playerId, int guessingNumber);
        bool PlaceHolder(bool input);
    }
}
