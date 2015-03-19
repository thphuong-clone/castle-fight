using System;
using Windows.ApplicationModel.Activation;
using Windows.Media.SpeechRecognition;
using System.Threading.Tasks;

namespace Cortana
{
    class SpeechHelper
    {
        private static SpeechRecognizer speechRecognizer;

        public static void HandleSpeechCommand(IActivatedEventArgs args)
        {
            var commandArgs = args as VoiceCommandActivatedEventArgs;
            SpeechRecognitionResult result = commandArgs.Result;

            string receivedText = result.Text;

            Interop.cortanaReceivedText = receivedText;
        }

        public static async Task InitialiseSpeechRecognition()
        {
            speechRecognizer = new SpeechRecognizer();

            var dictationGrammar = new SpeechRecognitionTopicConstraint(SpeechRecognitionScenario.Dictation, "dictation");
            speechRecognizer.UIOptions.AudiblePrompt = "Your command";
            speechRecognizer.UIOptions.ExampleText = "All attack";
            speechRecognizer.UIOptions.IsReadBackEnabled = false;
            speechRecognizer.Constraints.Add(dictationGrammar);

            await speechRecognizer.CompileConstraintsAsync();
        }

        public static async void StartRecognising(object sender, EventArgs e)
        {
            Interop.receivingVoice = true;

            //start
            SpeechRecognitionResult result = await speechRecognizer.RecognizeWithUIAsync();
            
            //pass result
            Interop.cortanaReceivedText = result.Text;
            Interop.receivingVoice = false;

        }
    }
}
