using System;
using System.Collections.Generic;
using System.Linq;

// Base class for identifiable objects with multiple identifiers
public class IdentifiableObject
{
    private List<string> _identifiers;

    public IdentifiableObject(string[] idents)
    {
        _identifiers = new List<string>();
        foreach (string ident in idents)
        {
            AddIdentifier(ident);
        }
    }

    public bool AreYou(string name)
    {
        return _identifiers.Any(i => i.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    public string FirstID => _identifiers.FirstOrDefault() ?? string.Empty;

    public void AddIdentifier(string id)
    {
        if (!_identifiers.Contains(id.ToLower()))
        {
            _identifiers.Add(id.ToLower());
        }
    }
}