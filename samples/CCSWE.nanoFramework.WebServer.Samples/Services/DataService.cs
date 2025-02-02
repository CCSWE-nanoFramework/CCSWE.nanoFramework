namespace CCSWE.nanoFramework.WebServer.Samples.Services
{
    public interface IDataService
    {
        DataItem? GetData();
        void SetValue(string value);
    }

    public class DataService : IDataService
    {
        private DataItem? _data;

        public DataItem? GetData() => _data;

        public void SetValue(string value) => _data = new DataItem {Value = value};
    }

    public class DataItem
    {
        public string? Value { get; set; }
    }
}
