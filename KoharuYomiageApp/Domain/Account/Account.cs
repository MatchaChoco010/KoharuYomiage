namespace KoharuYomiageApp.Domain.Account
{
    public abstract record Account
    {
        protected Account(Username username, Instance instance)
        {
            Username = username;
            Instance = instance;
        }

        public Username Username { get; }
        public Instance Instance { get; }
        public AccountIdentifier AccountIdentifier => new(Username, Instance);

        public bool SameIdentityAs(Account other)
        {
            return AccountIdentifier == other.AccountIdentifier;
        }
    }
}
