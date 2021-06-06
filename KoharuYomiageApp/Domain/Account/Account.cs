namespace KoharuYomiageApp.Domain.Account
{
    public abstract record Account
    {
        protected Account(Username username, Instance instance, DisplayName displayName,
            IsReadingPostsFromThisAccount isReadingPostsFromThisAccount)
        {
            DisplayName = displayName;
            Username = username;
            Instance = instance;
            IsReadingPostsFromThisAccount = isReadingPostsFromThisAccount;
        }

        public DisplayName DisplayName { get; }
        public Username Username { get; }
        public Instance Instance { get; }
        public AccountIdentifier AccountIdentifier => new(Username, Instance);
        public IsReadingPostsFromThisAccount IsReadingPostsFromThisAccount { get; set; }

        public bool SameIdentityAs(Account other)
        {
            return AccountIdentifier == other.AccountIdentifier;
        }
    }
}
