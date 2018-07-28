using CGM.Scanner;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CGM2SVG
{
    internal class Reference : IDisposable
    {
        // Fields
        public SVGContext mycontext = new SVGContext();
        public MemoryStream mystream = new MemoryStream();
        private XmlTextWriter myxml;

        // Methods
        public Reference(TranslateOptions options)
        {
            this.mycontext.options = options;
            this.myxml = new XmlTextWriter(this.mystream, null);
            this.myxml.WriteStartElement("g");
            this.myxml.WriteAttributeString("stroke-linejoin", "miter");
            this.myxml.WriteAttributeString("stroke-miterlimit", "32767");
        }

        public void close()
        {
            this.myxml.Close();
            this.mystream.Close();
        }

        public void Complete()
        {
            this.myxml.WriteEndElement();
            this.myxml.Flush();
        }

        public void ElementWrite(CGMElement element)
        {
            bool flag = false;
            try
            {
                switch (element.ElementClass)
                {
                    case 0:
                        flag = EClass0.class0(element, this.mycontext, this.myxml);
                        return;

                    case 1:
                        flag = EClass1.class1(element, this.mycontext, this.myxml);
                        return;

                    case 2:
                        flag = EClass2.class2(element, this.mycontext, this.myxml);
                        return;

                    case 3:
                        flag = EClass3.class3(element, this.mycontext, this.myxml);
                        return;

                    case 4:
                        flag = EClass4.class4(element, this.mycontext, this.myxml);
                        return;

                    case 5:
                        flag = EClass5.class5(element, this.mycontext, this.myxml);
                        return;

                    case 6:
                    case 8:
                        return;

                    case 7:
                        flag = EClass6.class7(element, this.mycontext, this.myxml);
                        return;

                    case 9:
                        break;

                    default:
                        return;
                }
                flag = EClass6.class9(element, this.mycontext, this.myxml);
            }
            catch (Exception exception1)
            {
                //ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                if (exception.Message == "Invalid Use.")//if (StringType.StrCmp(exception.Message, "Invalid Use.", false) == 0)
                {
                    throw new Exception("This conversion is being called from an invalid source, Please contract Docsoft Inc. if the probem continues.");
                }
                throw new Exception(element.ElementClass.ToString() + " " + element.ElementId.ToString() + " - " + ElementDictionary.GetInstance().FindElement(element.ElementClass, element.ElementId) + " - " + exception.ToString());
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        ~Reference()
        {
        }

        // Properties
        public double scale
        {
            get
            {
                return this.mycontext.scale;
            }
        }

        public CGM.Scanner.Point vdcpoint
        {
            get
            {
                int x = Convert.ToInt32(this.mycontext.rtl.X);
                return new CGM.Scanner.Point(x, Convert.ToInt32(this.mycontext.rtl.Y));
            }
        }

        public Size vdcsize
        {
            get
            {
                return new Size(this.mycontext.rw, this.mycontext.rh);
            }
        }
    }



}
