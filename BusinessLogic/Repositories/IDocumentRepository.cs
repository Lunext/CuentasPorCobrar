﻿

namespace CuentasPorCobrar.Shared;

public interface IDocumentRepository
{
    Task<Document?> CreateAsync(Document document);
    Task<IEnumerable<Document>> RetrieveAllAsync();
    Task<Document?> RetrieveAsync(Guid id);
    Task<Document?> UpdateAsync(Guid id, Document document);
    Task<bool?> DeleteAsync(Guid id); 

}

