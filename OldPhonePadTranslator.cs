using System;
using System.Collections.Generic;
using System.Text;

namespace OldPhonePadApp
{
    public class OldPhonePadTranslator
    {
        // Converts key press sequences into text messages
        public static string OldPhonePad(string input)
        {
            if (string.IsNullOrEmpty(input)) return "";

            // Defines the letter groups associated with each number key
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

            var output = new StringBuilder();   // Stores the final message
            var buffer = new StringBuilder();   // Holds repeated key presses temporarily
            char? previousCharacter = null;     // Keeps track of the last key pressed

            foreach (char c in input)
            {
                if (c == '#')
                {
                    // Marks the end of input, adds the last buffered character if there is one
                    if (buffer.Length > 0)
                        output.Append(GetCharFromBuffer(buffer.ToString(), keyMap));
                    break;
                }
                else if (c == '*')
                {
                    // Clears the current sequence or deletes the last typed character
                    if (buffer.Length > 0)
                        buffer.Clear(); // Resets ongoing key presses
                    else if (output.Length > 0)
                        output.Remove(output.Length - 1, 1); // Erases the most recently added letter
                }
                else if (c == ' ')
                {
                    // Indicates a pause, commits the buffered sequence to the output
                    if (buffer.Length > 0)
                    {
                        output.Append(GetCharFromBuffer(buffer.ToString(), keyMap));
                        buffer.Clear();
                    }
                }
                else if (char.IsDigit(c))
                {
                    // If a new key is pressed, process the previous sequence first
                    if (previousCharacter != null && c != previousCharacter)
                    {
                        if (buffer.Length > 0)
                        {
                            output.Append(GetCharFromBuffer(buffer.ToString(), keyMap));
                            buffer.Clear();
                        }
                    }

                    buffer.Append(c);
                    previousCharacter = c;
                }
                else
                {
                    // Handles unexpected characters by inserting a placeholder '?'
                    output.Append('?');
                    buffer.Clear();
                    previousCharacter = null;
                }
            }

            return output.ToString();
        }

        // Determines the correct letter from a series of key presses (e.g., "222" → "C")
        private static char GetCharFromBuffer(string buffer, Dictionary<char, string> keyMap)
        {
            if (string.IsNullOrEmpty(buffer)) return '?';

            char key = buffer[0];

            // Invalidates input if the sequence includes mixed digits (e.g., "264")
            foreach (char c in buffer)
            {
                if (c != key)
                    return '?';
            }

            // Ensures the key exists and actually represents letters (skips '1')
            if (!keyMap.ContainsKey(key) || keyMap[key].Length == 0)
                return '?';

            string chars = keyMap[key];
            int index = (buffer.Length - 1) % chars.Length;

            return chars[index];
        }
    }
}
