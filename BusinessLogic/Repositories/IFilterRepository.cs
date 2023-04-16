
//Used to difference CuentasPorCobrar.Transaction from the
//reserved word Transaction

namespace CuentasPorCobrar.Shared;

public interface IFilterRepository <T>
{
    

    Task<IEnumerable<T>>? RetrieveFilterDate(DateTime firstDate, DateTime lastDate);
}

