namespace BlogAtor.Store.Sql.Data;

using BlogAtor.Core.Entity;

internal class DbDataItem : DataItem, IDbEntity<DataItem>
{
    public DbDataItem() {}
    public DbDataItem(DataItem entity)
    {
        Id = entity.Id;
        DataSourceId = entity.DataSourceId;
        CollectorId = entity.CollectorId;

        Uid = entity.Uid;

        Title = entity.Title;
        Link = entity.Link;
        UpdatedDate = entity.UpdatedDate;
        CollectedDate = entity.CollectedDate;
    }
    public DbDataSource DataSource { get; set; }
    public DbSourceCollector Collector { get; set; }
    public ICollection<DbDataContent> Contents { get; set; }

    public DataItem ToEntity() => new DataItem()
    {
        Id = Id,
        DataSourceId = DataSourceId,
        CollectorId = CollectorId,

        Uid = Uid,

        Title = Title,
        Link = Link,
        UpdatedDate = UpdatedDate,
        CollectedDate = CollectedDate
    };
}