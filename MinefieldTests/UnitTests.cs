using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minefield;
using Minefield.Enums;
using Minefield.Interfaces;
using Moq;
using System;

namespace MinefieldTests
{
    [TestClass]
    public class MinefieldTests
    {
        private const int _xLength = 10;
        private const int _yLength = 10;
        private const int _lives = 3;
        private const Difficulty _difficulty = Difficulty.Medium;

        private const string _gameOverText = "GAME OVER";
        private const string _congratulationsText = "CONGRATULATIONS";
        private const string _tryAgainText = "Press Enter To Try Again";

        private Mock<IField> _mockField;
        private Mock<IFieldRenderer> _mockRenderer;
        private Mock<IOutputWriter> _mockOutputWriter;

        private Field _field;

        [TestInitialize]
        public void TestInit()
        {
            _mockRenderer = new Mock<IFieldRenderer>();
            _mockField = new Mock<IField>();
            _mockOutputWriter = new Mock<IOutputWriter>();

            _field = new Field(_xLength, _yLength, _lives, _difficulty, _mockRenderer.Object, _mockOutputWriter.Object, 0);
        }

        [TestMethod]
        public void TestFieldSetup()
        { 
            _mockRenderer.Verify(m => m.Render(_field, _mockOutputWriter.Object), Times.Once);
        }

        [TestMethod]
        public void TestMoveViable()
        {
            var currentY = _field.CurrentPosition.YPosition;

            _field.Move(MoveOption.Up);

            Assert.IsTrue(currentY == _field.CurrentPosition.YPosition - 1);
        }

        [TestMethod]
        public void TestMoveUnViable()
        {
            var currentX = _field.CurrentPosition.XPosition;

            _field.Move(MoveOption.Left);

            Assert.IsTrue(currentX == _field.CurrentPosition.XPosition);
        }

        [TestMethod]
        public void TestGameOverNoLives()
        {
            var keyInfo = new ConsoleKeyInfo('a', ConsoleKey.Escape, false, false, false);
            _mockOutputWriter.Setup(m => m.ReadKey()).Returns(keyInfo);
            _field = new Field(_xLength, _yLength, 0, _difficulty, _mockRenderer.Object, _mockOutputWriter.Object, 0);
            var game = new Game(_field, _mockOutputWriter.Object);
            game.Start();

            _mockOutputWriter.Verify(m => m.WriteLine(_gameOverText), Times.Once);
            _mockOutputWriter.Verify(m => m.WriteLine(_tryAgainText), Times.Once);
        }

        [TestMethod]
        public void TestGameOverWon()
        {
            var keyInfo = new ConsoleKeyInfo('a', ConsoleKey.Escape, false, false, false);
            _mockOutputWriter.Setup(m => m.ReadKey()).Returns(keyInfo);
            _field = new Field(_xLength, _yLength, 1, _difficulty, _mockRenderer.Object, _mockOutputWriter.Object, 0, _yLength - 1);
            var game = new Game(_field, _mockOutputWriter.Object);
            game.Start();

            _mockOutputWriter.Verify(m => m.WriteLine(_congratulationsText), Times.Once);
            _mockOutputWriter.Verify(m => m.WriteLine(_tryAgainText), Times.Once);
        }
    }
}
