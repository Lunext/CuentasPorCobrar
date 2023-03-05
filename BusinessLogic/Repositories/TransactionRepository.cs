﻿using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Concurrent;

namespace CuentasPorCobrar.Shared;

public class TransactionRepository : ITransactionRepository
{
    private static ConcurrentDictionary<Guid, Transaction>? transactionCache;
    private CuentasporcobrardbContext context;

    public TransactionRepository(CuentasporcobrardbContext context)
    {
        this.context = context;
        if(transactionCache is null)
        {
            transactionCache = new ConcurrentDictionary<Guid, Transaction>(context.Transactions.ToDictionary(t => t.TransactionId));
        }
    }

    public Task<IEnumerable<Transaction>> RetrieveAllAsync()
    {
        return Task.FromResult
            (transactionCache is null ? Enumerable.Empty<Transaction>()
            : transactionCache.Values);
    }
    public Task<Transaction?> RetrieveByIdAsync(Guid id)
    {
        if (transactionCache is null) return null!; 
        transactionCache.TryGetValue(id, out Transaction? transaction);
        return Task.FromResult(transaction);
    }

    private Transaction UpdateCache(Guid id, Transaction transaction)
    {
        Transaction? old; 
        if(transactionCache is not null)
        {
            if(transactionCache.TryGetValue(id, out old))
            {
                if(transactionCache.TryUpdate(id, transaction, old))
                {
                    return transaction;
                }
            }
        }
        return null!;
    }

    public async Task<Transaction?> CreateAsync(Transaction transaction)
    {
        EntityEntry<Transaction> added = await context.Transactions.AddAsync(transaction);
        int affected = await context.SaveChangesAsync();

        if(affected == 1) 
        { 
            if(transactionCache is null) return transaction;

            return transactionCache.AddOrUpdate(transaction.TransactionId, transaction, UpdateCache);
        }
        else
        {
            return null; 
        }
    }

    public async Task<Transaction?> UpdateAsync(Guid id, Transaction transaction)
    {
        context.Transactions.Update(transaction);
        int affected = await context.SaveChangesAsync(); 
        if(affected == 1) 
        { 
            return UpdateCache(id, transaction);
        }
        return null; 
    }

    public async Task<bool?> DeleteAsync(Guid id)
    {
        Transaction? transaction = context.Transactions.Find(id);
        if (transaction is null) return null; 
        context.Transactions.Remove(transaction);
        int affected = await context.SaveChangesAsync();

        if(affected == 1)
        {
            if(transactionCache is null) return null;
            return transactionCache.TryRemove(id, out transaction);
        }
        else
        {
            return null;
        }
    }
}