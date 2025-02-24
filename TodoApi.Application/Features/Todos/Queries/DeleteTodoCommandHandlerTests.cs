using MediatR;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using TodoApi.Application.Features.Todos.Queries;
using TodoApi.Core;
using Xunit;

public class DeleteTodoCommandHandlerTests
{
    private readonly Mock<ITodoRepository> _repositoryMock;
    private readonly DeleteTodoCommandHandler _handler;

    public DeleteTodoCommandHandlerTests()
    {
        _repositoryMock = new Mock<ITodoRepository>();
        _handler = new DeleteTodoCommandHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_TodoItemExists_DeletesTodoItem()
    {
        // Arrange
        var command = new DeleteTodoCommand(1);
        var todoItem = new TodoItem { Id = 1 };

        _repositoryMock.Setup(repo => repo.GetByIdAsync(command.Id)).ReturnsAsync(todoItem);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(Unit.Value, result);
        _repositoryMock.Verify(repo => repo.DeleteAsync(command.Id), Times.Once);
    }

    [Fact]
    public async Task Handle_TodoItemDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        var command = new DeleteTodoCommand(1);
        _repositoryMock.Setup(repo => repo.GetByIdAsync(command.Id)).ReturnsAsync((TodoItem)null);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
    }
}