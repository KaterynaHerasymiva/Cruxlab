using System;
using System.IO;
using System.Threading.Tasks;

namespace Cruxlab.Services;

public class PasswordsValidator : IPasswordsValidator
{
    public async Task<int> ValidatePasswordAsync(string pathToFile)
    {
        if (!File.Exists(pathToFile))
        {
            throw new ArgumentException(nameof(pathToFile));
        }

        try
        {
            var text = await File.ReadAllTextAsync(pathToFile);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            // TODO log error.
        }

        return 0;
    }
}