namespace Web_Blog.Models.Interfaces
{
    public interface IContactRepository
    {
        Task AddContactAsync(Contact contact);
    }
}
