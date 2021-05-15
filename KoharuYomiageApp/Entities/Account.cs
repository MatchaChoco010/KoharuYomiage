namespace KoharuYomiageApp.Entities
{
    public abstract record Account
    {
        public Account(Username username, Instance instance)
        {
            Username = username;
            Instance = instance;
        }

        public Username Username { get; init; }
        public Instance Instance { get; init; }
        public AccountIdentifier AccountIdentifier => new(Username, Instance);

        public bool SameIdentityAs(Account other)
        {
            return AccountIdentifier == other.AccountIdentifier;
        }
    }
}
