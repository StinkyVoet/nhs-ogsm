namespace nhs_ogsm.Data;

public static class ChartTestData
{
    public static List<string> Text = new List<string>()
    {
        "1", "2", "3", "4", "5", "6", "7","8","9","10","11","12","13",
        "14","15","16","17","18","19","20","21","22","23","24","25","26","27","28","29","30","31"
    };
    public static List<DataItem> SimpleBar = new List<DataItem>() {
        new DataItem() { Name = "1", Value = 100 },
        new DataItem() { Name = "2", Value = 100 },
        new DataItem() { Name = "3", Value = 100 },
        new DataItem() { Name = "4", Value = 95 },
        new DataItem() { Name = "5", Value = 95 },
        new DataItem() { Name = "6", Value = 85 },
        new DataItem() { Name = "7", Value = 85 },
        new DataItem() { Name = "8", Value = 85 },
        new DataItem() { Name = "9", Value = 85 },
        new DataItem() { Name = "10", Value = 70 },
        new DataItem() { Name = "11", Value = 70 },
        new DataItem() { Name = "12", Value = 60 },
        new DataItem() { Name = "13", Value = 60 },
        new DataItem() { Name = "14", Value = 60 },
        new DataItem() { Name = "15", Value = 60 },
        new DataItem() { Name = "16", Value = 60 },
        new DataItem() { Name = "17", Value = 60 },
        new DataItem() { Name = "18", Value = 55 },
        new DataItem() { Name = "19", Value = 55 },
        new DataItem() { Name = "20", Value = 55 },
        new DataItem() { Name = "21", Value = 55 },
        new DataItem() { Name = "22", Value = 55 },
        new DataItem() { Name = "23", Value = 55 },
        new DataItem() { Name = "24", Value = 55 },
        new DataItem() { Name = "25", Value = 55 },
        new DataItem() { Name = "26", Value = 40 },
        new DataItem() { Name = "27", Value = 40 },
        new DataItem() { Name = "28", Value = 35 },
        new DataItem() { Name = "29", Value = 20 },
        new DataItem() { Name = "30", Value = 20 },
        new DataItem() { Name = "31", Value = 20 }
    };

    public static List<string> GroupedLabels = new List<string>() { "1900", "1950", "1999", "2050" };
    public static List<decimal?> Grouped1 = new List<decimal?>() { 133, 221, 783, 2478 };
    public static List<decimal?> Grouped2 = new List<decimal?>() { 408, 547, 675, 734 };

    public static List<string> CallbackLabels = new List<string>() { "Q1", "Q2", "Q3", "Q4" };
    public static List<decimal?> CallbackValues = new List<decimal?> { 50000, 60000, 70000, 1800000 };
}

