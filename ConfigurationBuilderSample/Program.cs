using Microsoft.Extensions.Configuration;


namespace ConfigurationBuilderSample;

internal class Program
{
  private static void Main(string[] args)
  {
    // Build a config object, using env vars and JSON providers.
    IConfigurationRoot config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();

    // Get values from the config given their key and their target type.
    Settings? settings = config.GetRequiredSection("Settings").Get<Settings>();
    ConnectionStrings? connectionStrings = config.GetRequiredSection("ConnectionStrings").Get<ConnectionStrings>();

    // Write the values to the console.
    Console.WriteLine($"KeyOne = {settings?.KeyOne}");
    Console.WriteLine($"KeyTwo = {settings?.KeyTwo}");
    Console.WriteLine($"KeyThree:Message = {settings?.KeyThree?.Message}");

    settings?.IPAddressRange.ForEach(ip => Console.WriteLine(ip));

    Console.WriteLine($"Inventory = {connectionStrings?.Inventory}");

    PaletteNodeSection? paletteNodeSection = config.GetRequiredSection("PaletteNodeSection").Get<PaletteNodeSection>();
    Console.WriteLine($"PaletteFile: {paletteNodeSection?.PaletteFile}");
  }
}

// Application code which might rely on the config could start here.

// This will output the following:
//   KeyOne = 1
//   KeyTwo = True
//   KeyThree:Message = Oh, that's nice...