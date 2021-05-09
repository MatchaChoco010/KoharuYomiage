namespace KoharuYomiageApp.Application.LoadTalker.UseCases
{
    public abstract record LoadTalkerStatus
    {
        public record Success : LoadTalkerStatus;

        public record Failure : LoadTalkerStatus;
    }
}
