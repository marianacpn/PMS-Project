using System.Data;

namespace Application.Common.Interfaces
{
    public interface IReadFileService
    {
        DataTableCollection Read(byte[] content);
    }
}
