using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace ClearMindWindows.RequestModels
{
    public class PythonModel
    {
        public PythonModel() 
        {

        }
        public void StartRecording()
        {
            ScriptEngine engine = Python.CreateEngine();
            engine.ExecuteFile(@"test.py");
        }
    }
}
