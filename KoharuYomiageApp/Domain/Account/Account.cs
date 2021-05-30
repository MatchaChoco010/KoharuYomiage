namespace KoharuYomiageApp.Domain.Account
{
    public abstract record Account
    {
        protected Account(Username username, Instance instance,
            IsReadingPostsFromThisAccount isReadingPostsFromThisAccount)
        {
            Username = username;
            Instance = instance;
            IsReadingPostsFromThisAccount = isReadingPostsFromThisAccount;
        }

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
