﻿using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;

namespace PoeFilterX.Business.Extensions
{
    /// <summary>
    /// Extends <see cref="IConfiguration"/>
    /// </summary>
    public static class ConfigurationExtensions
    {
        public static string[] InjectEnvironment(this IConfiguration config, IReadOnlyList<string> args)
        {
            return InjectEnvironmentInternal(config, args).ToArray();
        }

        private static IEnumerable<string> InjectEnvironmentInternal(this IConfiguration config, IReadOnlyList<string> args)
        {
            var enviroRegex = new Regex("%\\w+%");
            for (var n = 0; n < args.Count; n++)
            {
                var arg = args[n];
                while (enviroRegex.IsMatch(arg))
                {
                    var match = enviroRegex.Matches(arg)[0];
                    var key = arg.Substring(match.Index, match.Length);
                    var keyInner = key[1..^1];
                    var value = config[keyInner];
                    if (value == null)
                    {
                        throw new ParserException($"Unrecognized environment variable '{keyInner}'");
                    }

                    arg = arg.Replace(key, value);
                }

                yield return arg;

            }
        }
    }
}
