namespace KoharuYomiageApp.Entities
{
    public record AccountIdentifier
    {
        public AccountIdentifier(Username username, Instance instance)
        {
            Value = username.Value + "@" + instance.Value;
        }

        public string Value { get; init; }
    }
}
