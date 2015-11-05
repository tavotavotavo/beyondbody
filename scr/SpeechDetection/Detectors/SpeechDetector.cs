using Domain;
using System;
using System.Collections.Generic;
using System.Speech.Recognition;

namespace SpeechDetection.Detectors
{
    public class SpeechDetector
    {
        private SpeechRecognitionEngine recognizer;
        private EventHandler<SpeechRecognizedEventArgs> handler;
        private Action<IEnumerable<Word>> action;

        public void Setup(Action<IEnumerable<Word>> action)
        {
            this.action = action;
            try
            {
                this.recognizer =
                  new SpeechRecognitionEngine(
                    new System.Globalization.CultureInfo("es-ES"));
            }
            catch (Exception)
            {
                this.recognizer =
                  new SpeechRecognitionEngine(
                    new System.Globalization.CultureInfo("en-US"));
            }

            recognizer.SetInputToDefaultAudioDevice();

            recognizer.LoadGrammarAsync(new DictationGrammar());

            this.handler +=
              new EventHandler<SpeechRecognizedEventArgs>((object sender, SpeechRecognizedEventArgs e) =>
              {
                  IEnumerable<Word> words = new List<Word>()
                      {
                        new Word(e.Result.Text)
                      };

                  this.action(words);
              });

            recognizer.SpeechRecognized += this.handler;
        }

        public void StartDetecting()
        {
            recognizer.SetInputToDefaultAudioDevice();

            recognizer.UnloadAllGrammars();
            recognizer.LoadGrammarAsync(new DictationGrammar());

            this.recognizer.RecognizeAsync(RecognizeMode.Multiple);
        }

        public void StopDetecting()
        {
            this.recognizer.RecognizeAsyncStop();
        }
    }
}