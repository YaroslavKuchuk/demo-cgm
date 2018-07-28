using CGM.Scanner;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CGM2SVG
{
    public class CGM2SVG
    {
        // Fields
        [AccessedThroughProperty("_handler")]
        private CGM2SVGhandler _handler;
        private ConvertCompleteEventHandler ConvertCompleteEvent;
        private long currentFileSize;
        private ArrayList elementlist;
        private string mycgmpath;
        private TranslateOptions options;
        private ProgressChangeEventHandler ProgressChangeEvent;
        private Reference @ref;

        //// Events
        //public event ConvertCompleteEventHandler ConvertComplete {
        //  [MethodImpl(MethodImplOptions.Synchronized)]
        //  add {
        //    this.ConvertCompleteEvent = Delegate.Combine(this.ConvertCompleteEvent, obj);
        //  }
        //  [MethodImpl(MethodImplOptions.Synchronized)]
        //  remove {
        //    this.ConvertCompleteEvent = Delegate.Remove(this.ConvertCompleteEvent, obj);
        //  }
        //}

        //public event ProgressChangeEventHandler ProgressChange {
        //  [MethodImpl(MethodImplOptions.Synchronized)]
        //  add {
        //    this.ProgressChangeEvent = Delegate.Combine(this.ProgressChangeEvent, obj);
        //  }
        //  [MethodImpl(MethodImplOptions.Synchronized)]
        //  remove {
        //    this.ProgressChangeEvent = Delegate.Remove(this.ProgressChangeEvent, obj);
        //  }
        //}

        // Methods
        public CGM2SVG(string CGMPath)
        {
            this.elementlist = new ArrayList();
            this.mycgmpath = CGMPath;
            this.options = new TranslateOptions();
        }

        public CGM2SVG(string CGMPath, TranslateOptions options)
        {
            this.elementlist = new ArrayList();
            this.mycgmpath = CGMPath;
            this.options = options;
        }

        public CGM2SVG(string CGMPath, PointF TopLeft, PointF BottomRight)
        {
            this.elementlist = new ArrayList();
            this.mycgmpath = CGMPath;
            this.options = new TranslateOptions(TopLeft, BottomRight);
        }

        private void _handler_ElementProcessed(CGMElement el)
        {
            if (this.ProgressChangeEvent != null)
            {
                this.ProgressChangeEvent((int)Math.Round((((double)(el.Offset + el.Data.Length)) / ((double)this.currentFileSize)) * 100.0));
            }
        }

        private void _handler_ExceptionOccured(CGMElement el, Exception ex)
        {
            if (this.options.StrictMode)
            {
                throw new CGM2SVGException("CGM2SVG conversion exception at " + (int)el.Offset + " --> " + ex.Message, (long)el.Offset, ex);
            }
            Trace.WriteLine("Error at " + el.Offset.ToString("X8"));
            Trace.WriteLine(ex);
        }

        public void close()
        {
            this.@ref.close();
        }

        private void CopySvgDocument(XmlTextReader xr, XmlTextWriter xw)
        {
            xw.WriteStartDocument();
            xw.WriteDocType("svg", "-//W3C//DTD SVG 1.0//EN", "http://www.w3.org/TR/SVG/DTD/svg10.dtd", null);
            //xw.WriteComment("This file was converted from CGM by Quick.SVG 2005 (demo) - http://www.quicksvg.com");
            xw.WriteStartElement("svg", "http://www.w3.org/2000/svg");
            if (this.options.SpecifySize)
            {
                Size vdcsize = this.@ref.vdcsize;
                xw.WriteAttributeString("width", vdcsize.Width * this.options.SizeScale + this.options.SizeUnits);
                xw.WriteAttributeString("height", vdcsize.Height * this.options.SizeScale + this.options.SizeUnits);
            }
            else
            {
                xw.WriteAttributeString("width", "100%");
                xw.WriteAttributeString("height", "100%");
            }
            xw.WriteAttributeString("viewBox", string.Format("{0} {1} {2} {3}", new object[] { this.@ref.vdcpoint.X, this.@ref.vdcpoint.Y, this.@ref.vdcsize.Width, this.@ref.vdcsize.Height }));
            xw.WriteAttributeString("xmlns", "xlink", "http://www.w3.org/2000/xmlns/", "http://www.w3.org/1999/xlink");
            xw.WriteAttributeString("xmlns", "qsvg", "http://www.w3.org/2000/xmlns/", "http://www.docsoft.com/qsvg");
            xw.WriteAttributeString("qsvg", "scale", "http://www.docsoft.com/qsvg", this.@ref.scale.ToString());
            while (xr.Read())
            {
                bool isEmptyElement;
                switch (xr.NodeType)
                {
                    case XmlNodeType.Element:
                        isEmptyElement = xr.IsEmptyElement;
                        if (!String.IsNullOrEmpty(xr.Prefix))//if (StringType.StrCmp(xr.Prefix, "", false) != 0)
                        {
                            break;
                        }
                        xw.WriteStartElement(xr.LocalName, "http://www.w3.org/2000/svg");
                        goto Label_0286;

                    case XmlNodeType.Attribute:
                    case XmlNodeType.EntityReference:
                    case XmlNodeType.Entity:
                    case XmlNodeType.ProcessingInstruction:
                    case XmlNodeType.Document:
                    case XmlNodeType.DocumentType:
                    case XmlNodeType.DocumentFragment:
                    case XmlNodeType.Notation:
                    case XmlNodeType.Whitespace:
                        {
                            continue;
                        }
                    case XmlNodeType.Text:
                        {
                            xw.WriteString(xr.Value);
                            continue;
                        }
                    case XmlNodeType.CDATA:
                        {
                            xw.WriteCData(xr.Value);
                            continue;
                        }
                    case XmlNodeType.Comment:
                        {
                            xw.WriteComment(xr.Value);
                            continue;
                        }
                    case XmlNodeType.SignificantWhitespace:
                        {
                            xw.WriteWhitespace(xr.Value);
                            continue;
                        }
                    case XmlNodeType.EndElement:
                        {
                            xw.WriteFullEndElement();
                            continue;
                        }
                    default:
                        {
                            continue;
                        }
                }
                if (xr.Prefix == "qsvg")
                {
                    xw.WriteStartElement(xr.Prefix, xr.LocalName, "http://www.docsoft.com/qsvg");
                }
                else
                {
                    xw.WriteStartElement(xr.Prefix, xr.LocalName, xr.NamespaceURI);
                }
                Label_0286:
                if (xr.HasAttributes)
                {
                    if (xr.MoveToFirstAttribute())
                    {
                        do
                        {
                            if (xr.Prefix == "qsvg")
                            {
                                xw.WriteAttributeString(xr.Prefix, xr.LocalName, "http://www.docsoft.com/qsvg", xr.Value);
                            }
                            else if (xr.NamespaceURI == "http://www.w3.org/1999/xlink")
                            {
                                xw.WriteAttributeString("xlink", xr.LocalName, xr.NamespaceURI, xr.Value);
                            }
                            //else if (StringType.StrCmp(xr.NamespaceURI, "http://www.w3.org/2000/xmlns/", false) != 0)
                            else if (xr.NamespaceURI != "http://www.w3.org/2000/xmlns/")
                            {
                                xw.WriteAttributeString(xr.Prefix, xr.LocalName, xr.NamespaceURI, xr.Value);
                            }
                        }
                        while (xr.MoveToNextAttribute());
                    }
                    xr.MoveToElement();
                }
                if (isEmptyElement)
                {
                    xw.WriteEndElement();
                }
            }
            xw.WriteEndElement();
            xw.WriteEndDocument();
        }

        public ArrayList GetCGMElementList()
        {

            return this.elementlist;
        }

        public ArrayList GetColorTable()
        {
            return this.@ref.mycontext.ExportColorTable();
        }

        public XmlElement GetSVGElement()
        {
            XmlDocument document = new XmlDocument();
            this.@ref.mystream.Position = 0L; //this.@ref.mystream.set_Position(0L);
            XmlNamespaceManager nsMgr = new XmlNamespaceManager(new NameTable());
            nsMgr.AddNamespace("xlink", "http://www.w3.org/1999/xlink");
            nsMgr.AddNamespace("qsvg", "http://www.docsoft.com/qsvg");
            XmlParserContext context = new XmlParserContext(null, nsMgr, "", XmlSpace.Default);
            XmlTextReader reader = new XmlTextReader(this.@ref.mystream, XmlNodeType.Document, context);
            
            var lines = Encoding.ASCII.GetString(@ref.mystream.ToArray());

            try
            {
                //document.Load((XmlReader)reader);
            }
            finally
            {
                reader.Close();
            }
            document.DocumentElement.SetAttribute("stroke-miterlimit", "32767");
            return document.DocumentElement;
        }

        public string GetSVGString()
        {
            return this.GetSVGElement().OuterXml;
        }

        public bool MakeSVGFile(string filepath)
        {
            bool flag;
            try
            {
                if (this.@ref.mystream.Length == 0L)
                {
                    return false;
                }
                XmlTextWriter xw = new XmlTextWriter(filepath, Encoding.UTF8);
                xw.Formatting = Formatting.Indented;
                try
                {
                    this.@ref.mystream.Position = 0L; //this.@ref.mystream.set_Position(0L);
                    XmlNamespaceManager nsMgr = new XmlNamespaceManager(new NameTable());
                    nsMgr.AddNamespace("xlink", "http://www.w3.org/1999/xlink");
                    //nsMgr.AddNamespace("qsvg", "http://www.docsoft.com/qsvg");
                    XmlParserContext context = new XmlParserContext(null, nsMgr, "", XmlSpace.Default);
                    XmlTextReader xr = new XmlTextReader(this.@ref.mystream, XmlNodeType.Document, context);
                    try
                    {
                        this.CopySvgDocument(xr, xw);
                    }
                    finally
                    {
                        xr.Close();
                    }
                }
                finally
                {
                    xw.Close();
                }
                flag = true;
            }
            catch (Exception exception1)
            {
                //ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                Trace.WriteLine(exception);
                flag = false;
                //ProjectData.ClearProjectError();
            }
            return flag;
        }

        public void Run()
        {
            //FileStream r = new FileStream(this.mycgmpath, 3, 1);
            FileStream r = new FileStream(this.mycgmpath, FileMode.Open, FileAccess.Read);
            try
            {
                this.currentFileSize = r.Length;
                this._handler = new CGM2SVGhandler();
                this.@ref = new Reference(this.options);
                try
                {
                    this._handler.@ref = this.@ref;
                    this._handler.elementlist = this.elementlist;
                    CGMScanner.ReadFile(r, this._handler);
                    this._handler = null;
                }
                finally
                {
                    this.@ref.Complete();
                }
            }
            finally
            {
                r.Close();
            }
            if (this.ConvertCompleteEvent != null)
            {
                this.ConvertCompleteEvent(this.mycgmpath);
            }
        }

        //// Properties
        //private CGM2SVGhandler _handler {
        //  get {
        //    return this.__handler;
        //  }
        //  [MethodImpl(MethodImplOptions.Synchronized)]
        //  set {
        //    if (this.__handler != null)
        //    //{
        //    //  this.__handler.ExceptionOccured -= new CGM2SVGhandler.ExceptionOccuredEventHandler(this._handler_ExceptionOccured);
        //    //  this.__handler.ElementProcessed -= new CGM2SVGhandler.ElementProcessedEventHandler(this._handler_ElementProcessed);
        //    //}
        //    this.__handler = value;
        //    //if (this.__handler != null)
        //    //{
        //    //  this.__handler.ExceptionOccured += new CGM2SVGhandler.ExceptionOccuredEventHandler(this._handler_ExceptionOccured);
        //    //  this.__handler.ElementProcessed += new CGM2SVGhandler.ElementProcessedEventHandler(this._handler_ElementProcessed);
        //    //}
        //  }
        //}

        public Size OutputSize
        {
            get
            {
                return new Size(this.@ref.mycontext.rw, this.@ref.mycontext.rh);
            }
        }

        [Obsolete("Included In TraslateOptions", false)]
        public bool StrictMode
        {
            get
            {
                return this.options.StrictMode;
            }
            set
            {
                this.options.StrictMode = value;
            }
        }

        // Nested Types
        public delegate void ConvertCompleteEventHandler(string CGMPath);

        public delegate void ProgressChangeEventHandler(int PercentComplete);
    }


}
