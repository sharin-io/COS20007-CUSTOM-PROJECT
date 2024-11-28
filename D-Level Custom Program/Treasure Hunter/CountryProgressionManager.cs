using static Treasure_Hunter.Interface;
using Treasure_Hunter;

/// Manages the progression through different countries in the game.
public class CountryProgressionManager
{
    /// A list of country factories responsible for creating countries.
    private List<ICountryFactory> _countryFactories;

    /// Index of the current country in the progression.
    private int _currentCountryIndex;

    public CountryProgressionManager()
        {
            _countryFactories = new List<ICountryFactory>
            {
                new JapanCountryFactory(),
                new ChinaCountryFactory(),
                new MyanmarCountryFactory(),
                new ThailandCountryFactory()
            };
            _currentCountryIndex = 0;
        }

    /// Gets the first country in the progression.
    public Country GetFirstCountry()
    {
        return _countryFactories[0].CreateCountry();
    }

    /// Gets the next country in the progression, if available.
    public Country? GetNextCountry()
    {
        _currentCountryIndex++;
        return _currentCountryIndex < _countryFactories.Count
            ? _countryFactories[_currentCountryIndex].CreateCountry()
            : null;
    }

    /// Finds a country by its name.
    public Country? GetCountryByName(string countryName)
    {
        foreach (var factory in _countryFactories)
        {
            var country = factory.CreateCountry();
            if (country.Name.Equals(countryName, StringComparison.OrdinalIgnoreCase))
            {
                return country;
            }
        }
        return null;
    }

    /// Checks if there are more countries to progress to.
    public bool HasMoreCountries()
    {
        return _currentCountryIndex < _countryFactories.Count - 1;
    }

    /// Retrieves all countries in the progression.
    public List<Country> GetAllCountries()
    {
        List<Country> countries = new List<Country>();
        foreach (var factory in _countryFactories)
        {
            countries.Add(factory.CreateCountry());
        }
        return countries;
    }
}
