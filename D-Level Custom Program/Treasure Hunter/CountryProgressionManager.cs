using System;
using System.Collections.Generic;
using System.Linq;
using static Treasure_Hunter.Interface;

namespace Treasure_Hunter
{
    public class CountryProgressionManager
    {
        private List<ICountryFactory> _countryFactories;
        private int _currentCountryIndex;

        public CountryProgressionManager()
        {
            _countryFactories = new List<ICountryFactory>
            {
                new JapanCountryFactory(),
                new ChinaCountryFactory()
                // Easily add more country factories here
            };
            _currentCountryIndex = 0;
        }

        public Country GetFirstCountry()
        {
            return _countryFactories[0].CreateCountry();
        }

        public Country? GetNextCountry()
        {
            _currentCountryIndex++;
            return _currentCountryIndex < _countryFactories.Count
                ? _countryFactories[_currentCountryIndex].CreateCountry()
                : null;
        }

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

        public bool HasMoreCountries()
        {
            return _currentCountryIndex < _countryFactories.Count - 1;
        }

        public List<Country> GetAllCountries()
        {
            // Create a list of countries by using each factory
            List<Country> countries = new List<Country>();
            foreach (var factory in _countryFactories)
            {
                countries.Add(factory.CreateCountry());
            }
            return countries;
        }
    }
}