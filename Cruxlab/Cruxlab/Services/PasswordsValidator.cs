using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Cruxlab.Services;

public class PasswordsValidator : IPasswordsValidator
{
    public async Task<int> ValidatePasswordAsync(string pathToFile)
    {
        int totalCounter = 0;

        if (!File.Exists(pathToFile))
        {
            throw new ArgumentException(nameof(pathToFile));
        }

        try
        {
            var text = await File.ReadAllTextAsync(pathToFile);
            string[] lines = text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            foreach (var line in lines)
            {
                try
                {
                    var record = line.Split(Array.Empty<char>(), StringSplitOptions.RemoveEmptyEntries);
                    char symbol;

                    if (record.Length != 3)
                    {
                        Debug.WriteLine("Invalid record.");
                        continue;
                    }

                    if (record[0].Length == 1)
                    {
                        symbol = record[0][0];
                    }
                    else
                    {
                        Debug.WriteLine("Failed to get input symbol.");
                        continue;
                    }

                    if (record[1].Length >= 3 && record[1].EndsWith(':'))
                    {
                        var str = record[1].TrimEnd(':');
                        var range = str.Split('-');
                        if (range.Length == 2)
                        {
                            var min = int.Parse(range[0]);
                            var max = int.Parse(range[1]);

                            var count = record[2].Count(t => t == symbol);
                            if (count >= min && count <= max)
                            {
                                totalCounter++;
                            }
                        }
                        else
                        {
                            Debug.WriteLine("Failed to get interval.");
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Failed to get interval.");
                    Debug.WriteLine(e);
                }
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine("Failed to split file by lines.");
            Debug.WriteLine(e);
        }

        return totalCounter;
    }
}