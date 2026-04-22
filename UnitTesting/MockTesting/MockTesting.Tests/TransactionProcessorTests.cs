using Moq;

namespace MockTesting.Tests;

[TestFixture]
public class TransactionProcessorTests
{
    private TransactionProcessor transactionProcessor;
    private Mock<IPermissionService> permissionServiceMock;
    private Mock<IAccountService> accountServiceMock;
    private Mock<ITransactionService> transactionServiceMock;
    private Mock<ILogger> loggerMock;

    [SetUp]
    public void Setup()
    {
        this.permissionServiceMock = new Mock<IPermissionService>();
        this.accountServiceMock = new Mock<IAccountService>();
        this.transactionServiceMock = new Mock<ITransactionService>();
        this.loggerMock = new Mock<ILogger>();

        this.transactionProcessor = new TransactionProcessor(
            this.permissionServiceMock.Object,
            this.accountServiceMock.Object,
            this.transactionServiceMock.Object,
            this.loggerMock.Object);
    }

    [Test]
    public void ProcessTransfer_SuccessfulTransfer_LogsAndReturnsTrue()
    {
        //Arrange
        int user1Id = 1;
        int user2Id = 2;
        _ = this.permissionServiceMock.Setup(p => p.HasTransferPermission(user1Id)).Returns(true);
        _ = this.accountServiceMock.Setup(a => a.GetBalance(user1Id)).Returns(1000);

        //Act
        var result = this.transactionProcessor.ProcessTransfer(user1Id, user2Id, 100);

        //Assert
        Assert.That(result, Is.True);

        this.transactionServiceMock.Verify(
            t => t.Transfer(user1Id, user2Id, 100),
            Times.Once);

        this.loggerMock.Verify(
            l => l.Log(It.IsAny<string>()),
            Times.AtLeastOnce);
    }

    [Test]
    public void ProcessTransfer_NoPermission_LogsAndReturnsFalse()
    {
        //Arrange
        int user1Id = 1;
        int user2Id = 2;
        int balance = 1000;

        _ = this.permissionServiceMock.Setup(e => e.HasTransferPermission(user1Id)).Returns(false);
        _ = this.accountServiceMock.Setup(e => e.GetBalance(user1Id)).Returns(balance);

        //Act
        var result = this.transactionProcessor.ProcessTransfer(user1Id, user2Id, 100);

        //Assert
        Assert.That(result, Is.False);

        this.transactionServiceMock.Verify(
            t => t.Transfer(user1Id, user2Id, 100),
            Times.Never);

        this.loggerMock.Verify(
            l => l.Log(It.IsAny<string>()),
            Times.AtLeastOnce);
    }

    [Test]
    public void ProcessTransfer_InsufficientBalance_LogsAndReturnsFalse()
    {
        //Arrange
        int user1Id = 1;
        int user2Id = 2;
        int balance = 50;

        _ = this.permissionServiceMock.Setup(e => e.HasTransferPermission(user1Id)).Returns(false);
        _ = this.accountServiceMock.Setup(e => e.GetBalance(user1Id)).Returns(balance);

        //Act
        var result = this.transactionProcessor.ProcessTransfer(user1Id, user2Id, 100);

        //Assert
        Assert.That(result, Is.False);

        this.transactionServiceMock.Verify(
            t => t.Transfer(user1Id, user2Id, 100),
            Times.Never);

        this.loggerMock.Verify(
            l => l.Log(It.IsAny<string>()),
            Times.AtLeastOnce);
    }

    [Test]
    public void ProcessTransfer_TransactionThrowsException_LogsAndReturnsFalse()
    {
        //Arrange
        int user1Id = 1;
        int user2Id = 2;
        int balance = 1000;
        int transferAmount = 100;
        var exception = new InvalidOperationException("Invalid Operation");

        _ = this.permissionServiceMock.Setup(e => e.HasTransferPermission(user1Id)).Returns(true);
        _ = this.accountServiceMock.Setup(e => e.GetBalance(user1Id)).Returns(balance);
        _ = this.transactionServiceMock.Setup(t => t.Transfer(user1Id, user2Id, transferAmount)).Throws(exception);

        //Act
        var result = this.transactionProcessor.ProcessTransfer(user1Id, user2Id, transferAmount);

        //Assert
        Assert.That(result, Is.False);

        this.transactionServiceMock.Verify(
            t => t.Transfer(user1Id, user2Id, transferAmount),
            Times.Once);

        this.loggerMock.Verify(
            l => l.Log(It.Is<string>(msg => msg.Contains("Invalid Operation"))),
            Times.AtLeastOnce);

        this.loggerMock.Verify(
            l => l.Log(It.IsAny<string>()),
            Times.AtLeastOnce);
    }

    [Test]
    public void ProcessTransfer_InvalidFromUserId_ThrowsArgumentException()
    {
        //Arrange
        int user1Id = 0;
        int user2Id = 2;
        int transferAmount = 100;

        //Assert
        Assert.That(() => this.transactionProcessor.ProcessTransfer(user1Id, user2Id, transferAmount), Throws.ArgumentException);
    }

    [Test]
    public void ProcessTransfer_InvalidToUserId_ThrowsArgumentException()
    {
        //Arrange
        int user1Id = 1;
        int user2Id = 0;
        int transferAmount = 100;

        //Assert
        Assert.That(() => this.transactionProcessor.ProcessTransfer(user1Id, user2Id, transferAmount), Throws.ArgumentException);
    }

    [Test]
    public void ProcessTransfer_SameUserIds_ThrowsArgumentException()
    {
        //Arrange
        int user1Id = 1;
        int user2Id = 1;
        int transferAmount = 100;

        //Assert
        Assert.That(() => this.transactionProcessor.ProcessTransfer(user1Id, user2Id, transferAmount), Throws.ArgumentException);
    }

    [Test]
    public void ProcessTransfer_InvalidAmount_ThrowsArgumentException()
    {
        //Arrange
        int user1Id = 1;
        int user2Id = 2;
        int transferAmount = -100;

        //Assert
        Assert.That(() => this.transactionProcessor.ProcessTransfer(user1Id, user2Id, transferAmount), Throws.ArgumentException);
    }

    [Test]
    public void Constructor_NullDependencies_ThrowsArgumentNullException()
    {
        Assert.That(() => new TransactionProcessor(
            null,
            this.accountServiceMock.Object,
            this.transactionServiceMock.Object,
            this.loggerMock.Object), Throws.ArgumentNullException);

        Assert.That(() => new TransactionProcessor(
            this.permissionServiceMock.Object,
            null,
            this.transactionServiceMock.Object,
            this.loggerMock.Object), Throws.ArgumentNullException);

        Assert.That(() => new TransactionProcessor(
            this.permissionServiceMock.Object,
            this.accountServiceMock.Object,
            null,
            this.loggerMock.Object), Throws.ArgumentNullException);

        Assert.That(() => new TransactionProcessor(
            this.permissionServiceMock.Object,
            this.accountServiceMock.Object,
            this.transactionServiceMock.Object,
            null), Throws.ArgumentNullException);
    }

    [Test]
    public void ProcessTransfer_WithMaxIntValues_ShouldSucceed()
    {
        //Arrange
        int user1Id = 1;
        int user2Id = 2;
        int transferAmount = int.MaxValue;
        int balance = int.MaxValue;

        _ = this.permissionServiceMock.Setup(p => p.HasTransferPermission(user1Id)).Returns(true);
        _ = this.accountServiceMock.Setup(a => a.GetBalance(user1Id)).Returns(balance);

        //Act
        var result = this.transactionProcessor.ProcessTransfer(user1Id, user2Id, transferAmount);

        //Assert
        Assert.That(result, Is.True);

        this.transactionServiceMock.Verify(
            t => t.Transfer(user1Id, user2Id, transferAmount),
            Times.Once);

        this.loggerMock.Verify(
            l => l.Log(It.IsAny<string>()),
            Times.AtLeastOnce);
    }

    [Test]
    public void ProcessTransfer_WithMinValidValues_ShouldSucceed()
    {
        //Arrange
        int user1Id = 1;
        int user2Id = 2;
        int transferAmount = 1;
        int balance = 1;

        _ = this.permissionServiceMock.Setup(p => p.HasTransferPermission(user1Id)).Returns(true);
        _ = this.accountServiceMock.Setup(a => a.GetBalance(user1Id)).Returns(balance);

        //Act
        var result = this.transactionProcessor.ProcessTransfer(user1Id, user2Id, transferAmount);

        //Assert
        Assert.That(result, Is.True);

        this.transactionServiceMock.Verify(
            t => t.Transfer(user1Id, user2Id, transferAmount),
            Times.Once);

        this.loggerMock.Verify(
            l => l.Log(It.IsAny<string>()),
            Times.AtLeastOnce);
    }

    [Test]
    public void ProcessTransfer_LogMessages_ShouldContainCorrectInformation()
    {
        //Arrange
        int user1Id = 1;
        int user2Id = 2;
        int transferAmount = 1;
        int balance = 1;

        var logs = new List<string>();

        _ = this.loggerMock.Setup(l => l.Log(It.IsAny<string>())).Callback<string>(msg => logs.Add(msg));
        _ = this.permissionServiceMock.Setup(p => p.HasTransferPermission(user1Id)).Returns(true);
        _ = this.accountServiceMock.Setup(a => a.GetBalance(user1Id)).Returns(balance);

        //Act
        var result = this.transactionProcessor.ProcessTransfer(user1Id, user2Id, transferAmount);

        //Assert
        Assert.That(result, Is.True);

        Assert.That(logs.Count, Is.GreaterThanOrEqualTo(2));

        Assert.That(logs.Any(l => l.Contains("started", StringComparison.OrdinalIgnoreCase)), Is.True);
        Assert.That(logs.Any(l => l.Contains("successfully", StringComparison.OrdinalIgnoreCase)), Is.True);
        Assert.That(logs.Any(l => l.Contains($"{transferAmount}", StringComparison.OrdinalIgnoreCase)), Is.True);
        Assert.That(logs.Any(l => l.Contains($"{user1Id}", StringComparison.OrdinalIgnoreCase)), Is.True);
        Assert.That(logs.Any(l => l.Contains($"{user2Id}", StringComparison.OrdinalIgnoreCase)), Is.True);
    }

    [Test]
    public void ProcessTransfer_TransactionId_ShouldBeUnique()
    {
        //Arrange
        int user1Id = 1;
        int user2Id = 2;
        int transferAmount = 1;
        int balance = 1;

        var transactionStrings = new List<string>();

        _ = this.loggerMock.Setup(l => l.Log(It.Is<string>(t => t.Contains("started")))).Callback<string>(msg => transactionStrings.Add(msg));
        _ = this.permissionServiceMock.Setup(p => p.HasTransferPermission(user1Id)).Returns(true);
        _ = this.accountServiceMock.Setup(a => a.GetBalance(user1Id)).Returns(balance);

        //Act
        var result1 = this.transactionProcessor.ProcessTransfer(user1Id, user2Id, transferAmount);
        var result2 = this.transactionProcessor.ProcessTransfer(user1Id, user2Id, transferAmount);

        //Assert
        Assert.That(result1, Is.True);
        Assert.That(result2, Is.True);

        Assert.That(transactionStrings.Count, Is.EqualTo(2));

        Assert.That(transactionStrings[0].Split(" ")[1], Is.Not.EqualTo(transactionStrings[1].Split(" ")[1]));
    }

    [TestCase(false, 100, 100, "Permission denied")]
    [TestCase(true, 100, 50, "Insufficient funds. Current balance:")]
    public void ProcessTransfer_ErrorMessages_ShouldBeProperlyFormatted(bool hasPermission, int transferAmount, int balance, string reason)
    {
        //Arrange
        int user1Id = 1;
        int user2Id = 2;

        var errorMessage = "";

        _ = this.loggerMock
        .Setup(l => l.Log(It.IsAny<string>()))
        .Callback<string>(msg =>
        {
            if (msg.Contains("failed", StringComparison.OrdinalIgnoreCase))
            {
                errorMessage = msg;
            }
        });

        _ = this.permissionServiceMock.Setup(p => p.HasTransferPermission(user1Id)).Returns(hasPermission);
        _ = this.accountServiceMock.Setup(a => a.GetBalance(user1Id)).Returns(balance);

        //Act
        var result1 = this.transactionProcessor.ProcessTransfer(user1Id, user2Id, transferAmount);

        //Assert
        Assert.That(result1, Is.False);

        this.loggerMock.Verify(
            l => l.Log(It.Is<string>(l => l.Contains("failed"))),
            Times.AtLeastOnce);

        Assert.That(errorMessage, Does.Contain("Transaction"));
        Assert.That(errorMessage, Does.Contain("failed:"));
        Assert.That(errorMessage, Does.Contain(reason));
    }

    [TestCase(true, 100, 100, "failed with error:")]
    public void ProcessTransfer_ExceptionMessages_ShouldBeProperlyFormatted(bool hasPermission, int transferAmount, int balance, string errorMessage)
    {
        //Arrange
        int user1Id = 1;
        int user2Id = 2;

        var exceptionMessage = "";

        _ = this.loggerMock
        .Setup(l => l.Log(It.IsAny<string>()))
        .Callback<string>(msg =>
        {
            if (msg.Contains("failed with error:", StringComparison.OrdinalIgnoreCase))
            {
                exceptionMessage = msg;
            }
        });

        _ = this.permissionServiceMock.Setup(p => p.HasTransferPermission(user1Id)).Returns(hasPermission);
        _ = this.accountServiceMock.Setup(a => a.GetBalance(user1Id)).Returns(balance);
        _ = this.transactionServiceMock.Setup(t => t.Transfer(user1Id, user2Id, transferAmount)).Throws(new ArgumentException("Mock Exception"));

        //Act
        var result1 = this.transactionProcessor.ProcessTransfer(user1Id, user2Id, transferAmount);

        //Assert
        Assert.That(result1, Is.False);

        this.loggerMock.Verify(
            l => l.Log(It.Is<string>(l => l.Contains("failed"))),
            Times.AtLeastOnce);

        Assert.That(exceptionMessage, Does.Contain("Transaction"));
        Assert.That(exceptionMessage, Does.Contain(errorMessage));
        Assert.That(exceptionMessage, Does.Contain("Mock Exception"));
    }
}
