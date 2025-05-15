namespace JQueryAJAX.WebApp.Exceptions
{
    internal class MovieNotFoundException : Exception
    {
        public MovieNotFoundException() : base("Movie is not found") { }

        public MovieNotFoundException(string message) : base(message) { }
    }
}
