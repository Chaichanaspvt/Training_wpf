using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using HalconDotNet;
using WPF_MVVM_Halcon.ViewModels;
namespace HEngine_Manager_ns
{
    public class HEngine_Manager
    {
        HObject Imageinput;
        string Path_Proc;
        HDevProcedure Proc;
        HDevProcedureCall Proc_Call;
        public HObject Regioninput,Regionout;
        HDevEngine MyEngine;
        HDevProgram Program;
        public bool wait_debug = false;
        bool onSever = false;
        public HEngine_Manager()

        {
            //Imageinput = data.DisplayImage;
            //Regioninput = data.DisplayRegion;
            //Path_Proc = data.Path_Proc;
            //wait_debug = data.waitng_debug;
            //Set path
            MyEngine = new HDevEngine();
            MyEngine.SetProcedurePath(Path.GetFullPath("../../Models/procedures/"));
            //MyEngine.SetEngineAttribute("execute_procedures_jit_compiled", "true");
            //Link to file HDev
            
        }
        public void Init()
        {
            string ProgarmPath = "../../Models/procedures/Exercise7.hdev";
            Program = new HDevProgram(ProgarmPath);
            Proc = new HDevProcedure(Program, "Threshold_and_Connection");
            Proc_Call = new HDevProcedureCall(Proc);
        }
       
        public void SetImage(HObject Img)
        {
            Imageinput = Img;
        }
        /// <summary>
        /// Process for Run HEngine
        /// </summary>
        public void Proce()
        {
            ResultContainer Result;
            //HTuple ResultTuple;
            if (Imageinput != null)
            {
                Result = new ResultContainer();
                try
                {
                    Proc_Call.SetWaitForDebugConnection(wait_debug);          
                    //Pass Parameter to be processed and execute ther procedure
                    Proc_Call.SetInputIconicParamObject("Image", Imageinput);
                    Proc_Call.Execute();
                    // access the processing results and store them in the container
                    //Result.InputImage = Imageinput;
                    //ResultTuple = Proc_Call.GetOutputCtrlParamTuple("ResultData");
                    //Result.Row = ResultTuple[0];
                    //Result.Column = ResultTuple[1];
                    //Result.Angle = ResultTuple[2];
                    //Result.ResultProc = ResultTuple[3];
                    Regionout = Proc_Call.GetOutputIconicParamRegion("ConnectedRegions");

                }
                catch (HDevEngineException Ex)
                {

                    MessageBox.Show(Ex.Message, "HDevEngine Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }



        }
        public void onDebug_Sever()
        {
            if (!onSever)
            {
                // Set debug parameters
                MyEngine.SetEngineAttribute("debug_port", 57786);
                //MyEngine.SetEngineAttribute("debug_password", textPassword.Text);
                //MyEngine.SetEngineAttribute("debug_wait_for_connection","true");

                // Start debug server
                MyEngine.StartDebugServer();
                MessageBox.Show("Sever Start", "Server Debug", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MyEngine.StopDebugServer();
                MessageBox.Show("Sever Stop", "Server Debug", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        public class ResultContainer
        {
            public HObject InputImage;
            public HObject RegionOut;
            public double Row;
            public double Column;
            public double Angle;
            public string ResultProc;
            public ResultContainer()
            {

            }
        }
    }
}
