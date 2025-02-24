using MediatR;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using TodoApi.Application.Features.Todos.Queries;
using TodoApi.Core;
using Xunit;

public class UpdateTodoCommandHandlerTests
{
    private readonly Mock<ITodoRepository> _repositoryMock;
    private readonly UpdateTodoCommandHandler _handler;

    public UpdateTodoCommandHandlerTests()
    {
        _repositoryMock = new Mock<ITodoRepository>();
        _handler = new UpdateTodoCommandHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_TodoItemExists_UpdatesTodoItem()
    {
        // Arrange
        var command = new UpdateTodoCommand
        {
            Id = 1,
            Title = "Updated Title",
            Description = "Updated Description",
            IsCompleted = true
        };

        var todoItem = new TodoItem
        {
            Id = 1,
            Title = "Original Title",
            Description = "Original Description",
            IsCompleted = false
        };

        _repositoryMock.Setup(repo => repo.GetByIdAsync(command.Id)).ReturnsAsync(todoItem);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(Unit.Value, result);
        Assert.Equal(command.Title, todoItem.Title);
        Assert.Equal(command.Description, todoItem.Description);
        Assert.Equal(command.IsCompleted, todoItem.IsCompleted);
        _repositoryMock.Verify(repo => repo.UpdateAsync(todoItem), Times.Once);
    }

    [Fact]
    public async Task Handle_TodoItemDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        var command = new UpdateTodoCommand { Id = 1 };
        _repositoryMock.Setup(repo => repo.GetByIdAsync(command.Id)).ReturnsAsync((TodoItem)null);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
    }
}