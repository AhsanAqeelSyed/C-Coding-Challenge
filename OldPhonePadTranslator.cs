using System;
using System.Collections.Generic;
using System.Text;

namespace OldPhonePadApp
{
    public class OldPhonePadTranslator
    {
        public static string OldPhonePad(string input)
        {
            if (string.IsNullOrEmpty(input)) return "";

            var keyMap = new Dictionary<char, string>
            {
                { '1', "" },
                { '2', "ABC" },
                { '3', "DEF" },
                { '4', "GHI" },
                { '5', "JKL" },
                { '6', "MNO" },
                { '7', "PQRS" },
                { '8', "TUV" },
                { '9', "WXYZ" },
                { '0', " " },
            };

            var output = new StringBuilder();
            var buff = new StringBuilder();
            char? previousCharacter = null;

            foreach (char c in input)
            {
                if (c == '#')
                {
                    if (buff.Length > 0)
                        output.Append(GetCharFromBuffer(buff.ToString(), keyMap));
                    break;
                }
                else if (c == '*')
                {
                    if (buff.Length > 0)
                        buff.Clear();
                    else if (output.Length > 0)
                        output.Remove(output.Length - 1, 1);
                }
                else if (c == ' ')
                {
                    if (buff.Length > 0)
                    {
                        output.Append(GetCharFromBuffer(buff.ToString(), keyMap));
                        buff.Clear();
                    }
                }
                else if (char.IsDigit(c))
                {
                    if (previousCharacter != null && c != previousCharacter)
                    {
                        if (buff.Length > 0)
                        {
                            output.Append(GetCharFromBuffer(buff.ToString(), keyMap));
                            buff.Clear();
                        }
                    }
                    buff.Append(c);
                    previousCharacter = c;
                }
                else
                {
                    output.Append('?');
                    buff.Clear();
                    previousCharacter = null;
                }
            }

            return output.ToString();
        }

       private static char GetCharFromBuffer(string buffer, Dictionary<char, string> keyMap)
{
    if (string.IsNullOrEmpty(buffer)) return '?';

    char key = buffer[0];

    foreach (char c in buffer)
    {
        if (c != key)
            return '?';
    }

    if (!keyMap.ContainsKey(key) || keyMap[key].Length == 0)
        return '?';

    string chars = keyMap[key];
    int index = (buffer.Length - 1) % chars.Length;

    return chars[index];
}

    }
}
