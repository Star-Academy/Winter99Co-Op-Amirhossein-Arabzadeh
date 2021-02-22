using System;

namespace InvertedIndexLibrary
{
    public class InputGetter : IInputGetter
    {
        public string GetInput()
        {
            Console.WriteLine("Please Type Your plusSigned Words and MinusSigned and unSigned words");
            string input = Console.ReadLine();
            return input;
        }
    }
}