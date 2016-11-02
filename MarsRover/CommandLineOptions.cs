using CommandLine;
using CommandLine.Text;

namespace MarsRover
{
    public class CommandLineOptions: CommandLineOptionsBase    
    {
        private string _optionError;

        [Option("f", "filename", Required = true, HelpText = "Import file name")]
        public string FileName { get; set; }

        [Option("d", "debug", Required = false, HelpText = "Debug mode")]
        public string Debug { 
            get { return DebugFlag.ToString().ToLower(); }
            set
                {
                    switch (value.ToLower())
                    {
                        case "true":
                            DebugFlag = true;
                            break;
                        case "false":
                            DebugFlag = false;
                            break;

                        default:
                            _optionError = string.Format(@"Debug ""{0}"" not valid, use ""true"" or ""false"" ", value);
                            break;
                    }
                }
        }

        public bool DebugFlag { get; set; }

        public bool HasError { get { return _optionError != null; } }

        [HelpOption(HelpText = "Display this help screen.")]
        public string GetUsage()
        {
            var help = new HelpText
            {
                Heading = "Mars Rovers",
                AdditionalNewLineAfterOption = true,
                AddDashesToOption = true
            };

            if (HandleParsingErrorsInHelp(help))
            {
                help.AddPreOptionsLine("Usage: MarsRover -f<filename>  -d<debug>");
                help.AddOptions(this);
            }
            else
            {
                help.AddPostOptionsLine("\n\n");
            }

            return help;
        }

        private bool HandleParsingErrorsInHelp(HelpText help)
        {
            string errors = null;
            if (this.LastPostParsingState.Errors.Count > 0)
            {
                errors = help.RenderParsingErrorsText(this, 2);
            }
            if (_optionError != null)
            {
                errors += "  " + _optionError;
            }
            if (!string.IsNullOrEmpty(errors))
            {
                help.AddPreOptionsLine("\nERROR(S):");
                help.AddPreOptionsLine(errors);
                help.AddPreOptionsLine("\n");
                return true;
            }

            return false;
        }
    }
}
