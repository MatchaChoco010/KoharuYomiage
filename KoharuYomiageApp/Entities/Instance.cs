using System;

namespace KoharuYomiageApp.Entities
{
    public record Instance
    {
        public Instance(string instance)
        {
            if (Uri.CheckHostName(instance) is UriHostNameType.Unknown)
            {
                throw new ArgumentException("Invalid Instance");
            }

            Value = instance;
        }

        public string Value { get; }
    }
}
