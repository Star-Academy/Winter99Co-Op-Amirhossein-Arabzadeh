using System;

namespace Phase10Library
{
    public class InputGetter : IInputGetter
    {
        public string GetInput()
        {
            Console.WriteLine("Please Type Your plusSigned Words and MinusSigned and unSigned words");
            return Console.ReadLine()?.ToLower();
        }
    }
}