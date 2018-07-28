using CGM.Scanner;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CGM2SVG
{
  internal class CGM2SVGhandler : ElementHandlerBase
  {
    // Fields
    public ArrayList elementlist;
    private ElementProcessedEventHandler ElementProcessedEvent;
    private ExceptionOccuredEventHandler ExceptionOccuredEvent;
    internal Reference @ref;

    //// Events
    //public event ElementProcessedEventHandler ElementProcessed {
    //  [MethodImpl(MethodImplOptions.Synchronized)]
    //  add {
    //    this.ElementProcessedEvent = Delegate.Combine(this.ElementProcessedEvent, obj);
    //  }
    //  [MethodImpl(MethodImplOptions.Synchronized)]
    //  remove {
    //    this.ElementProcessedEvent = Delegate.Remove(this.ElementProcessedEvent, obj);
    //  }
    //}

    //public event ExceptionOccuredEventHandler ExceptionOccured {
    //  [MethodImpl(MethodImplOptions.Synchronized)]
    //  add {
    //    this.ExceptionOccuredEvent = Delegate.Combine(this.ExceptionOccuredEvent, obj);
    //  }
    //  [MethodImpl(MethodImplOptions.Synchronized)]
    //  remove {
    //    this.ExceptionOccuredEvent = Delegate.Remove(this.ExceptionOccuredEvent, obj);
    //  }
    //}

    // Methods
    protected override void OnElement(CGMElement el)
    {
      try
      {
        if ((el.ElementClass == 1) && (el.ElementId == 12))
        {
          MemoryStream r = new MemoryStream(el.Data, false);
          CGMScanner.ReadFile(r, this);
        }
        else
        {
          using (CGMBinaryReader reader = this.GetReader(el))
          {
            this.@ref.mycontext.Reader = reader;
            this.@ref.ElementWrite(el);
            this.elementlist.Add(ElementDictionary.GetInstance().FindElement(el.ElementClass, el.ElementId) + " " + el.ElementId.ToString() + " : " + el.ElementClass.ToString());
            if (this.ElementProcessedEvent != null)
            {
              this.ElementProcessedEvent(el);
            }
          }
        }
      }
      catch (Exception exception1)
      {
        //ProjectData.SetProjectError(exception1);
        Exception ex = exception1;
        if (this.ExceptionOccuredEvent != null)
        {
          this.ExceptionOccuredEvent(el, ex);
        }
        //ProjectData.ClearProjectError();
      }
    }

    // Nested Types
    public delegate void ElementProcessedEventHandler(CGMElement el);

    public delegate void ExceptionOccuredEventHandler(CGMElement el, Exception ex);
  }



}
