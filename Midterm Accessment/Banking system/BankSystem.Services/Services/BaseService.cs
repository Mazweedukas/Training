using BankSystem.EF.Entities;

namespace BankSystem.Services.Services;

public class BaseService : IDisposable
{
    public bool disposed { get; set; }

    public BankContext Context { get; set; }

    protected BaseService(BankContext context)
    {
        this.Context = context;
        this.Context.Database.EnsureCreated();
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            this.Context?.Dispose();
        }
    }
}
