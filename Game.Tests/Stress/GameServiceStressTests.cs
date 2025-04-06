using AutoMapper;
using FluentAssertions;
using Game.Application.Services;
using Game.Domain.Interfaces.Repositories;
using Game.Domain.Entities;
using Moq;
using System.Diagnostics;
using Xunit;
using Game.Tests.Services;

namespace Game.Tests.Stress
{
    public class GameServiceStressTest : BaseServiceTest
    {
        private GameService _gameService;
        private readonly IMapper _mapper;
        private readonly Mock<IGameRepository> _gameRepositoryMock = new Mock<IGameRepository>();

        public GameServiceStressTest()
        {
            _mapper = CreateIMapper();
            _gameService = new GameService(_gameRepositoryMock.Object, _mapper);
        }

        [Fact(DisplayName = "StressTest_GetBoardStateById")]
        public async Task StressTest_GetBoardStateById()
        {
            // Arrange
            var validBoardState = new BoardState()
            {
                Grid = new Grid()
                {
                    Width = 10,
                    Height = 10,
                    Cells = new int[,] { { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 } }
                }
            };
            _gameRepositoryMock.Setup(s => s.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(validBoardState);

            var id = Guid.NewGuid();
            var cancellationToken = CancellationToken.None;
            var stopwatch = new Stopwatch();

            // Act
            stopwatch.Start();
            for (int i = 0; i < 1000; i++)
            {
                var result = await _gameService.GetByIdAsync(id, cancellationToken);
                result.Should().NotBeNull();
                result.Success.Should().BeTrue();
            }
            stopwatch.Stop();

            // Assert
            var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
            elapsedMilliseconds.Should().BeLessThan(5000); // Example: the test must be completed in less than 5 seconds
        }

        [Fact(DisplayName = "StressTest_GetNextState")]
        public async Task StressTest_GetNextState()
        {
            // Arrange
            var validBoardState = new BoardState()
            {
                Grid = new Grid()
                {
                    Width = 10,
                    Height = 10,
                    Cells = new int[,] { { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 } }
                }
            };
            _gameRepositoryMock.Setup(s => s.GetNextStateAsync(It.IsAny<CancellationToken>())).ReturnsAsync(validBoardState);

            var cancellationToken = CancellationToken.None;
            var stopwatch = new Stopwatch();

            // Act
            stopwatch.Start();
            for (int i = 0; i < 1000; i++)
            {
                var result = await _gameService.GetNextStateAsync(cancellationToken);
                result.Should().NotBeNull();
                result.Success.Should().BeTrue();
            }
            stopwatch.Stop();

            // Assert
            var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
            elapsedMilliseconds.Should().BeLessThan(5000); // Example: the test must be completed in less than 5 seconds
        }

        [Fact(DisplayName = "StressTest_UploadBoard")]
        public async Task StressTest_UploadBoard()
        {
            // Arrange
            var validGrid = new Grid()
            {
                Width = 10,
                Height = 10,
                Cells = new int[,] { { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 } }
            };
            _gameRepositoryMock.Setup(s => s.UploadAsync(It.IsAny<Grid>(), It.IsAny<CancellationToken>())).ReturnsAsync(Guid.NewGuid().ToString());

            var cancellationToken = CancellationToken.None;
            var stopwatch = new Stopwatch();

            // Act
            stopwatch.Start();
            for (int i = 0; i < 1000; i++)
            {
                var result = await _gameService.UploadAsync(validGrid, cancellationToken);
                result.Should().NotBeNull();
                result.Success.Should().BeTrue();
            }
            stopwatch.Stop();

            // Assert
            var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
            elapsedMilliseconds.Should().BeLessThan(5000); // Example: the test must be completed in less than 5 seconds
        }

        // Including Load and Peak tests for GetByIdAsync method

        [Fact(DisplayName = "LoadTest_GetBoardStateById")]
        public async Task LoadTest_GetBoardStateById()
        {
            // Arrange
            var validBoardState = new BoardState()
            {
                Grid = new Grid()
                {
                    Width = 10,
                    Height = 10,
                    Cells = new int[,] { { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 } }
                }
            };
            _gameRepositoryMock.Setup(s => s.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(validBoardState);

            var id = Guid.NewGuid();
            var cancellationToken = CancellationToken.None;
            var stopwatch = new Stopwatch();

            // Act
            stopwatch.Start();
            for (int i = 0; i < 10000; i++) // Simulate a higher load
            {
                var result = await _gameService.GetByIdAsync(id, cancellationToken);
                result.Should().NotBeNull();
                result.Success.Should().BeTrue();
            }
            stopwatch.Stop();

            // Assert
            var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
            elapsedMilliseconds.Should().BeLessThan(30000); // Example: the test must be completed in less than 30 seconds
        }

        [Fact(DisplayName = "PeakTest_GetBoardStateById")]
        public async Task PeakTest_GetBoardStateById()
        {
            // Arrange
            var validBoardState = new BoardState()
            {
                Grid = new Grid()
                {
                    Width = 10,
                    Height = 10,
                    Cells = new int[,] { { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 } }
                }
            };
            _gameRepositoryMock.Setup(s => s.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(validBoardState);

            var id = Guid.NewGuid();
            var cancellationToken = CancellationToken.None;
            var stopwatch = new Stopwatch();

            // Act
            stopwatch.Start();
            for (int i = 0; i < 1000; i++) // Simulate a peak load
            {
                var result = await _gameService.GetByIdAsync(id, cancellationToken);
                result.Should().NotBeNull();
                result.Success.Should().BeTrue();
            }
            stopwatch.Stop();

            // Assert
            var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
            elapsedMilliseconds.Should().BeLessThan(2000); // Example: the test must be completed in less than 2 seconds
        }

    }
}
