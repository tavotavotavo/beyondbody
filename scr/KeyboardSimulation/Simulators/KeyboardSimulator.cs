using Domain;
using System.Collections.Generic;
using WindowsInput;

namespace KeyboardSimulation.Simulators
{
    public class KeyboardSimulator
    {
        public void PressKeys(IEnumerable<Word> words)
        {
            foreach (var word in words)
            {
                //Agregamos un espacio al final para que pueda seguir hablando y no le junte las palabras
                InputSimulator.SimulateTextEntry(string.Concat(word.Value, " "));
            }
        }
    }
}