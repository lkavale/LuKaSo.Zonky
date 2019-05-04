using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LuKaSo.Zonky.Models
{
    public class FilterOptions
    {
        private readonly IDictionary<string, string> _options;

        public FilterOptions()
        {
            _options = new Dictionary<string, string>();
        }

        public void Add(string type, string value)
        {
            if (_options.ContainsKey(type))
            {
                _options[type] = value;
                return;
            }

            _options.Add(type, value);
        }

        public void Remove(string type)
        {
            if (_options.ContainsKey(type))
            {
                _options.Remove(type);
            }
        }

        public IReadOnlyDictionary<string, string> ToDictionary()
        {
            return new ReadOnlyDictionary<string, string>(_options);
        }
    }
}
