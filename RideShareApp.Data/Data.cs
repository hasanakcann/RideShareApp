using Newtonsoft.Json;
using RideShareApp.Core.Entity;

namespace RideShareApp.Data;

public class Data
{
    private static readonly string _dataFilePath = Path.Combine(AppContext.BaseDirectory, "data.json");
    private static readonly Lazy<Data> _instance = new(() => LoadData());

    public List<User> Users { get; set; } = new();
    public List<TravelPlan> TravelPlans { get; set; } = new();
    public List<Contract> Contracts { get; set; } = new();

    private static readonly JsonSerializerSettings _jsonSettings = new()
    {
        Formatting = Formatting.Indented,
        NullValueHandling = NullValueHandling.Ignore
    };

    private Data() { }

    public static Data Instance => _instance.Value;

    private static Data LoadData()
    {
        try
        {
            if (!File.Exists(_dataFilePath))
            {
                var emptyData = new Data();
                SaveToFile(emptyData);
                return emptyData;
            }

            var content = File.ReadAllText(_dataFilePath);
            if (string.IsNullOrWhiteSpace(content))
            {
                var emptyData = new Data();
                SaveToFile(emptyData);
                return emptyData;
            }

            return JsonConvert.DeserializeObject<Data>(content) ?? new Data();
        }
        catch
        {
            return new Data();
        }
    }

    private static void SaveToFile(Data data)
    {
        try
        {
            var json = JsonConvert.SerializeObject(data, _jsonSettings);
            File.WriteAllText(_dataFilePath, json);
        }
        catch { }
    }

    public static void Save()
    {
        if (_instance.IsValueCreated)
            SaveToFile(_instance.Value);
    }
}
