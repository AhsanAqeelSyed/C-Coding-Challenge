using System;

namespace OldPhonePadApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Test 1: " + OldPhonePadTranslator.OldPhonePad("33#"));                      // E
            Console.WriteLine("Test 2: " + OldPhonePadTranslator.OldPhonePad("227*#"));                   // B
            Console.WriteLine("Test 3: " + OldPhonePadTranslator.OldPhonePad("4433555 555666#"));         // HELLO
            Console.WriteLine("Test 4: " + OldPhonePadTranslator.OldPhonePad("8 88777444666*664#"));      // ?????
            Console.WriteLine("Test 5: " + OldPhonePadTranslator.OldPhonePad("222 2 22#"));               // CAB
        }
    }
}
