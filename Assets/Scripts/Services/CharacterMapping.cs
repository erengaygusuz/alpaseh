using System;
using System.Collections.Generic;
using System.Text;

namespace FTRGames.Alpaseh.Services
{
    public sealed class CharacterMapping
    {
        private readonly Dictionary<char, char> characterMap = new Dictionary<char, char>();

        private CharacterMapping(
            string languageId,
            string mappingName,
            string sourceCharacters,
            string targetCharacters,
            bool isRequired)
        {
            sourceCharacters = sourceCharacters ?? string.Empty;
            targetCharacters = targetCharacters ?? string.Empty;

            Validate(languageId, mappingName, sourceCharacters, targetCharacters, isRequired);

            for (int i = 0; i < sourceCharacters.Length; i++)
            {
                characterMap[sourceCharacters[i]] = targetCharacters[i];
            }
        }

        public static CharacterMapping Optional(
            string languageId,
            string mappingName,
            string sourceCharacters,
            string targetCharacters)
        {
            return new CharacterMapping(
                languageId,
                mappingName,
                sourceCharacters,
                targetCharacters,
                false);
        }

        public static CharacterMapping Required(
            string languageId,
            string mappingName,
            string sourceCharacters,
            string targetCharacters)
        {
            return new CharacterMapping(
                languageId,
                mappingName,
                sourceCharacters,
                targetCharacters,
                true);
        }

        public bool TryGetValue(char sourceCharacter, out char targetCharacter)
        {
            return characterMap.TryGetValue(sourceCharacter, out targetCharacter);
        }

        public string ReplaceCharacters(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            var builder = new StringBuilder(text.Length);

            for (int i = 0; i < text.Length; i++)
            {
                builder.Append(
                    characterMap.TryGetValue(text[i], out char replacement)
                        ? replacement
                        : text[i]);
            }

            return builder.ToString();
        }

        private static void Validate(
            string languageId,
            string mappingName,
            string sourceCharacters,
            string targetCharacters,
            bool isRequired)
        {
            if (isRequired && string.IsNullOrWhiteSpace(sourceCharacters))
            {
                throw new InvalidOperationException(
                    $"Language '{languageId}' is missing its {mappingName} source characters in the language catalog.");
            }

            if (sourceCharacters.Length == targetCharacters.Length)
            {
                return;
            }

            throw new InvalidOperationException(
                $"Language '{languageId}' {mappingName} source and target character counts do not match.");
        }
    }
}
