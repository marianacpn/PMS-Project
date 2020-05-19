namespace Application.Common.Exceptions
{
    public class NotFoundException : NotLoggableException
    {
        public NotFoundException(string name, object key)
            : base($"Entidade \"{name}\" ({key}) não encontrada.")
        {
        }
    }
}
