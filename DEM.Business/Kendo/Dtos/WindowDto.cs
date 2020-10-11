using System;
using System.Collections.Generic;
using System.Text;

namespace DEM.App.Kendo
{
    public class WindowDto
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public bool Draggable { get; set; }
        public bool Resizable { get; set; }
        public int Width { get; set; }
        public string[] Actions { get; set; }
        public string ActivateEvent { get; set; }
        public string OpenEvent { get; set; }
        public string CloseEvent { get; set; }
        public string RefreshEvent { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string DataBridge { get; set; }
    }
}