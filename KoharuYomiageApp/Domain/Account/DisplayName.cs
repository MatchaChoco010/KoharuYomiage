namespace KoharuYomiageApp.Domain.Account
{
    public record DisplayName
    {
        public DisplayName(string instance)
        {
            Value = instance;
        }

        public string Value { get; }
    }
}
