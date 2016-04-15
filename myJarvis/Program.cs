using System;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Threading;

namespace myJarvis
{
    class Program
    {
        static ManualResetEvent _completed = null;

        static void Main(string[] args)
        {
            //Speech Recognition Engine object instance
            SpeechRecognitionEngine sRecognizer = new SpeechRecognitionEngine();
            Grammar testGrammar = new Grammar(new GrammarBuilder("test"));
            testGrammar.Name = "testGram";
            sRecognizer.LoadGrammar(testGrammar);
            Grammar exitGrammar = new Grammar(new GrammarBuilder("exit"));
            sRecognizer.LoadGrammar(exitGrammar);

            sRecognizer.SpeechRecognized += SRecognizer_SpeechRecognized;

            sRecognizer.SetInputToDefaultAudioDevice(); // sets Input to the pc's default audio device
            sRecognizer.RecognizeAsync(RecognizeMode.Multiple);
        }

        private static void SRecognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result.Text == "test") // this contains the recognized text.
            {
                Console.WriteLine("The test was successful");
            }
            else if(e.Result.Text == "exit")
            {
                _completed.Set();
            }
        }
    }
}
