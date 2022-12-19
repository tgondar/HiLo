using HiLow.Logic2.Interfaces;
using HiLow.Logic2.Logic;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiLow.Logic2.Tests
{
    public class GameLogicTests
    {
        public GameLogicTests()
        {
        }

        [Test]
        public void ShouldReturnAnArrayWith2Positions()
        {
            //Arrange
            Mock<IPlaceHolderLogic> pHolder = new Mock<IPlaceHolderLogic>();
            IGameLogic sut = new GameLogic(pHolder.Object);

            //Act
            int[] output = sut.Config();

            //Assert
            Assert.AreEqual(2, output.Length);
            Assert.Greater(output[0], 0);
            Assert.Greater(output[1], 0);
        }

        [TestCase(1, 20)]
        [TestCase(1, 5)]
        [TestCase(2, 21)]
        [TestCase(2, 6)]
        public void ShouldSendPlayerGuessingNumberAndCheckIfIsAMatch(int playerId, int guessingNumber)
        {
            //Arrange
            Mock<IPlaceHolderLogic> pHolder = new Mock<IPlaceHolderLogic>();
            IGameLogic sut = new GameLogic(pHolder.Object);

            //Act
            var output = sut.Play(playerId, guessingNumber);

            //Assert
            Assert.NotNull(output);
        }
    }
}
