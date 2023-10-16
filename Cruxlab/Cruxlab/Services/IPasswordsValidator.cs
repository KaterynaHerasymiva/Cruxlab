using System.Threading.Tasks;

namespace Cruxlab.Services;

public interface IPasswordsValidator
{
    Task<int> ValidatePasswordAsync(string pathToFile);
}