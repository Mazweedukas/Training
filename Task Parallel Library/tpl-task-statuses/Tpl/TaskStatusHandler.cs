namespace Tpl;

public static class TaskStatusHandler
{
    public static Task CreateTaskWithCreatedStatus()
    {
        return new Task(() => Thread.Sleep(100));
    }

    public static Task CreateTaskWithWaitingForActivationStatus()
    {
        var completionSource = new TaskCompletionSource<int>();
        return completionSource.Task;
    }

    public static Task CreateTaskWithWaitingToRunStatus()
    {
        var scheduler = new ConcurrentExclusiveSchedulerPair().ExclusiveScheduler;

        var blockingTask = Task.Factory.StartNew(
            () =>
            {
                Thread.Sleep(100);
            }, CancellationToken.None,
            TaskCreationOptions.None,
            scheduler);

        var waitingTask = Task.Factory.StartNew(
            () =>
            {
                Thread.Sleep(100);
            }, CancellationToken.None,
            TaskCreationOptions.None,
            scheduler);

        return waitingTask;
    }

    public static Task CreateTaskWithRunningStatus()
    {
        var taskStarted = new TaskCompletionSource<bool>();

        var runningTask = Task.Run(() =>
        {
            taskStarted.TrySetResult(true);
            Thread.Sleep(100);
        });

        taskStarted.Task.Wait();
        return runningTask;
    }

    public static Task CreateTaskWithRanToCompletionStatus()
    {
        return CreateCompletedTask();
    }

    public static Task CreateTaskWithWaitingForChildrenToCompleteStatus()
    {
        var taskStarted = new TaskCompletionSource<bool>();

        var parentTask = Task.Factory.StartNew(
            () =>
            {
                Task.Factory.StartNew(
                    () =>
                    {
                        taskStarted.TrySetResult(true);
                        Thread.Sleep(100);
                    },
                    TaskCreationOptions.AttachedToParent);
            });

        taskStarted.Task.Wait();

        return parentTask;
    }

    public static Task CreateTaskWithIsCompletedStatus()
    {
        // Could return any completed task (success, cancelled, or faulted)
        return CreateCompletedTask();
    }

    public static Task CreateTaskWithIsCancelledStatus()
    {
        using var cts = new CancellationTokenSource();
        cts.Cancel();
        var cancelledTask = Task.Run(
        () =>
        {
            cts.Token.ThrowIfCancellationRequested();
        }, cts.Token);

        try
        {
            cancelledTask.Wait();
        }
        catch (AggregateException)
        {
            // Expected
        }

        return cancelledTask;
    }

    public static Task CreateTaskWithIsFaultedStatus()
    {
        Task exceptionTask = new Task(() =>
        {
            throw new InvalidOperationException();
        });

        try
        {
            exceptionTask.Start();
            exceptionTask.Wait();
        }
        catch (AggregateException)
        {
        }

        return exceptionTask;
    }

    private static Task CreateCompletedTask()
    {
        var task = Task.Run(() => Thread.Sleep(100));
        task.Wait();
        return task;
    }
}
