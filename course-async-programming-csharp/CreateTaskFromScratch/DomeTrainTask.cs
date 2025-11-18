namespace CreateTaskFromScratch;

public class DomeTrainTask
{
    private readonly Lock _lock = new();
    
    private bool _completed;
    
    private Exception? _exception;

    public bool IsCompleted
    {
        get
        {
            lock (_lock)
            {
                return _completed;
            }
        }
    }
    
    public static DomeTrainTask Run(Action action)
    {
        DomeTrainTask task = new();

        ThreadPool.QueueUserWorkItem(_ =>
        {
            try
            {
                action();
                task.SetResult();
            }
            catch (Exception e)
            {
                task.SetException(e);
            }
        });

        return task;
    }

    public void SetResult()
    {
        lock (_lock)
        {
            if(_completed)
                throw new InvalidOperationException("Task already completed. Cannot set result of a completed DomeTrainTask.");
            
            _completed = true;
        }
    }
    
    public void SetException(Exception exception)
    {
        lock (_lock)
        {
            if (_completed)
                throw new InvalidOperationException("Task already completed. Cannot set exception of a completed DomeTrainTask.");
            
            _exception = exception;
        }
    }
}