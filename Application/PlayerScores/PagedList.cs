using System.Data;
using Dapper;

namespace Application.PlayerScores;

public class PagedList<T>
{
    private const int MaxPageSize = 10;

    private PagedList(List<T> items, int page, int pageSize, int totalCount)
    {
        Items = items;
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
    }

    public List<T> Items { get; }

    public int Page { get; }

    public int PageSize { get; }

    public int TotalCount { get; }

    public bool HasNextPage => Page * PageSize < TotalCount;

    public bool HasPreviousPage => Page > 1 && (Page - 1) * PageSize < TotalCount + PageSize;

    public static async Task<PagedList<T>> CreateAsync(
        string sqlQuery,
        int page,
        int pageSize,
        IDbConnection connection,
        DynamicParameters? sqlQueryParam = null)
    {
        var safePageSize = Math.Min(pageSize, MaxPageSize);
        var sqlCount = string.Concat("SELECT COUNT(*)\n",
            sqlQuery.AsSpan(sqlQuery.IndexOf("FROM", StringComparison.OrdinalIgnoreCase)));
        var totalCount = await connection.QueryFirstOrDefaultAsync<int>(sqlCount, sqlQueryParam);

        var sql = sqlQuery + "\nOFFSET :SkipRows ROWS FETCH NEXT :RowsPerPage ROWS ONLY";


        var dynamicParam = new DynamicParameters();
        dynamicParam.AddDynamicParams(
            new
            {
                SkipRows = (page - 1) * safePageSize,
                RowsPerPage = safePageSize
            });
        dynamicParam.AddDynamicParams(sqlQueryParam);
        var resultSet = await connection
            .QueryAsync<T>(sql, dynamicParam);

        var items = resultSet.ToList();

        return new PagedList<T>(items, page, safePageSize, totalCount);
    }
}
