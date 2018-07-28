using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CGM.Scanner
{
  public class ElementDictionary
  {

    private static readonly Lazy<ElementDictionary> lazy = new Lazy<ElementDictionary>(() => new ElementDictionary());

    private Hashtable d = new Hashtable();

    public static ElementDictionary GetInstance()
    {
      return lazy.Value;
    }

    public ElementDictionary()
    {
      this.InitDictionary();
    }

    public string FindElement(int classid, int id)
    {
      ElementId id2 = new ElementId(classid, id);
      return this.d[id2].ToString();
    }
    private void InitDictionary()
    {
      ElementId id = new ElementId(0, 1);
      this.d.Add(id, "BEGIN METAFILE");
      id = new ElementId(0, 13);
      this.d.Add(id, "BEGIN PROTECTION REGION");
      id = new ElementId(0, 14);
      this.d.Add(id, "END PROTECTION REGION");
      id = new ElementId(0, 15);
      this.d.Add(id, "BEGIN COMPOUND LINE");
      id = new ElementId(0, 0x10);
      this.d.Add(id, "END COMPOUND LINE");
      id = new ElementId(0, 0x11);
      this.d.Add(id, "BEGIN COMPOUND TEXT PATH");
      id = new ElementId(0, 0x12);
      this.d.Add(id, "END COMPOUND TEXT PATH");
      id = new ElementId(0, 0x13);
      this.d.Add(id, "BEGIN TILE ARRAY");
      id = new ElementId(0, 2);
      this.d.Add(id, "END METAFILE");
      id = new ElementId(0, 20);
      this.d.Add(id, "ENDTILEARRAY");
      id = new ElementId(0, 0x15);
      this.d.Add(id, "BEGIN APPLICATION STRUCTURE");
      id = new ElementId(0, 0x16);
      this.d.Add(id, "BEGIN APPLICATION STRUCTURE BODY");
      id = new ElementId(0, 0x17);
      this.d.Add(id, "END APPLICATION STRUCTURE");
      id = new ElementId(0, 3);
      this.d.Add(id, "BEGIN PICTURE");
      id = new ElementId(0, 4);
      this.d.Add(id, "BEGIN PICTURE BODY");
      id = new ElementId(0, 5);
      this.d.Add(id, "ENDPICTURE");
      id = new ElementId(0, 6);
      this.d.Add(id, "BEGIN SEGMENT");
      id = new ElementId(0, 7);
      this.d.Add(id, "END SEGMENT");
      id = new ElementId(0, 8);
      this.d.Add(id, "BEGIN FIGURE");
      id = new ElementId(0, 9);
      this.d.Add(id, "ENDFIGURE");
      id = new ElementId(1, 1);
      this.d.Add(id, "METAFILEVERSION");
      id = new ElementId(1, 10);
      this.d.Add(id, "COLOUR VALUE EXTENT");
      id = new ElementId(1, 11);
      this.d.Add(id, "METAFILE ELEMENT LIST");
      id = new ElementId(1, 12);
      this.d.Add(id, "METAFILE DEFAULTS REPLACEMENT");
      id = new ElementId(1, 13);
      this.d.Add(id, "FONTLIST");
      id = new ElementId(1, 14);
      this.d.Add(id, "CHARACTER SET LIST");
      id = new ElementId(1, 15);
      this.d.Add(id, "CHARACTER CODING ANNOUNCER");
      id = new ElementId(1, 0x10);
      this.d.Add(id, "NAME PRECISION");
      id = new ElementId(1, 0x11);
      this.d.Add(id, "MAXIMUM VDC EXTENT");
      id = new ElementId(1, 0x12);
      this.d.Add(id, "SEGMENT PRIORITY EXTENT");
      id = new ElementId(1, 0x13);
      this.d.Add(id, "COLOUR MODEL");
      id = new ElementId(1, 2);
      this.d.Add(id, "METAFILE DESCRIPTION");
      id = new ElementId(1, 20);
      this.d.Add(id, "COLOUR CALIBRATION");
      id = new ElementId(1, 0x15);
      this.d.Add(id, "FONT PROPERTIES");
      id = new ElementId(1, 0x16);
      this.d.Add(id, "GLYPH MAPPING");
      id = new ElementId(1, 0x17);
      this.d.Add(id, "SYMBOL LIBRARY LIST");
      id = new ElementId(1, 0x18);
      this.d.Add(id, "PICTURE DIRECTORY");
      id = new ElementId(1, 3);
      this.d.Add(id, "VDC TYPE");
      id = new ElementId(1, 4);
      this.d.Add(id, "INTEGER PRECISION");
      id = new ElementId(1, 5);
      this.d.Add(id, "REAL PRECISION");
      id = new ElementId(1, 6);
      this.d.Add(id, "INDEX PRECISION");
      id = new ElementId(1, 7);
      this.d.Add(id, "COLOUR PRECISION");
      id = new ElementId(1, 8);
      this.d.Add(id, "COLOUR INDEX PRECISION");
      id = new ElementId(1, 9);
      this.d.Add(id, "MAXIMUM COLOUR INDEX");
      id = new ElementId(2, 1);
      this.d.Add(id, "SCALINGMODE");
      id = new ElementId(2, 10);
      this.d.Add(id, "DEVICE VIEWPORT MAPPING");
      id = new ElementId(2, 11);
      this.d.Add(id, "LINE REPRESENTATION");
      id = new ElementId(2, 12);
      this.d.Add(id, "MARKER REPRESENTATION");
      id = new ElementId(2, 13);
      this.d.Add(id, "TEXT REPRESENTATION");
      id = new ElementId(2, 14);
      this.d.Add(id, "FILL REPRESENTATION");
      id = new ElementId(2, 15);
      this.d.Add(id, "EDGE REPRESENTATION");
      id = new ElementId(2, 0x10);
      this.d.Add(id, "INTERIOR STYLE SPECIFICATION MODE");
      id = new ElementId(2, 0x11);
      this.d.Add(id, "LINE AND EDGE TYPE DEFINITION");
      id = new ElementId(2, 0x12);
      this.d.Add(id, "HATCH STYLE DEFINITION");
      id = new ElementId(2, 0x13);
      this.d.Add(id, "GEOMETRIC PATTERN DEFINITION");
      id = new ElementId(2, 2);
      this.d.Add(id, "COLOUR SELECTION MODE");
      id = new ElementId(2, 20);
      this.d.Add(id, "APPLICATION STRUCTURE DIRECTORY");
      id = new ElementId(2, 3);
      this.d.Add(id, "LINE WIDTH SPECIFICATION MODE");
      id = new ElementId(2, 4);
      this.d.Add(id, "MARKER SIZE SPECIFICATION MODE");
      id = new ElementId(2, 5);
      this.d.Add(id, "EDGE WIDTH SPECIFICATION MODE");
      id = new ElementId(2, 6);
      this.d.Add(id, "VDC EXTENT");
      id = new ElementId(2, 7);
      this.d.Add(id, "BACKGROUND COLOUR");
      id = new ElementId(2, 8);
      this.d.Add(id, "DEVICE VIEWPORT");
      id = new ElementId(2, 9);
      this.d.Add(id, "DEVICE VIEWPORT SPECIFICATION MODE");
      id = new ElementId(3, 1);
      this.d.Add(id, "VDC INTEGER PRECISION");
      id = new ElementId(3, 10);
      this.d.Add(id, "NEW REGION");
      id = new ElementId(3, 11);
      this.d.Add(id, "SAVE PRIMITIVE CONTEXT");
      id = new ElementId(3, 12);
      this.d.Add(id, "RESTORE PRIMITIVE CONTEXT");
      id = new ElementId(3, 0x11);
      this.d.Add(id, "PROTECTION REGION INDICATOR");
      id = new ElementId(3, 0x12);
      this.d.Add(id, "GENERALIZED TEXT PATH MODE");
      id = new ElementId(3, 0x13);
      this.d.Add(id, "MITRELIMIT");
      id = new ElementId(3, 2);
      this.d.Add(id, "VDC REAL PRECISION");
      id = new ElementId(3, 20);
      this.d.Add(id, "TRANSPARENT CELL COLOUR");
      id = new ElementId(3, 3);
      this.d.Add(id, "AUXILIARY COLOUR");
      id = new ElementId(3, 4);
      this.d.Add(id, "TRANSPARENCY");
      id = new ElementId(3, 5);
      this.d.Add(id, "CLIP RECTANGLE");
      id = new ElementId(3, 6);
      this.d.Add(id, "CLIP INDICATOR");
      id = new ElementId(3, 7);
      this.d.Add(id, "LINE CLIPPING MODE");
      id = new ElementId(3, 8);
      this.d.Add(id, "MARKER CLIPPING MODE");
      id = new ElementId(3, 9);
      this.d.Add(id, "EDGE CLIPPING MODE");
      id = new ElementId(4, 1);
      this.d.Add(id, "POLYLINE");
      id = new ElementId(4, 10);
      this.d.Add(id, "GENERALIZED DRAWING PRIMITIVE");
      id = new ElementId(4, 11);
      this.d.Add(id, "RECTANGLE");
      id = new ElementId(4, 12);
      this.d.Add(id, "CIRCLE");
      id = new ElementId(4, 13);
      this.d.Add(id, "CIRCULAR ARC 3 POINT");
      id = new ElementId(4, 14);
      this.d.Add(id, "CIRCULAR ARC 3 POINT CLOSE");
      id = new ElementId(4, 15);
      this.d.Add(id, "CIRCULAR ARC CENTRE");
      id = new ElementId(4, 0x10);
      this.d.Add(id, "CIRCULAR ARC CENTRE CLOSE");
      id = new ElementId(4, 0x11);
      this.d.Add(id, "ELLIPSE");
      id = new ElementId(4, 0x12);
      this.d.Add(id, "ELLIPTICAL ARC");
      id = new ElementId(4, 0x13);
      this.d.Add(id, "ELLIPTICAL ARC CLOSE");
      id = new ElementId(4, 2);
      this.d.Add(id, "DISJOINT POLYLINE");
      id = new ElementId(4, 20);
      this.d.Add(id, "CIRCULAR ARC CENTRE REVERSED");
      id = new ElementId(4, 0x15);
      this.d.Add(id, "CONNECTING EDGE");
      id = new ElementId(4, 0x16);
      this.d.Add(id, "HYPERBOLIC ARC");
      id = new ElementId(4, 0x17);
      this.d.Add(id, "PARABOLIC ARC");
      id = new ElementId(4, 0x18);
      this.d.Add(id, "NON-UNIFORM B-SPLINE");
      id = new ElementId(4, 0x19);
      this.d.Add(id, "NON-UNIFORM RATIONAL B-SPLINE");
      id = new ElementId(4, 0x1a);
      this.d.Add(id, "POLYBEZIER");
      id = new ElementId(4, 0x1b);
      this.d.Add(id, "POLYSYMBOL");
      id = new ElementId(4, 0x1c);
      this.d.Add(id, "BITONALTILE");
      id = new ElementId(4, 0x1d);
      this.d.Add(id, "TILE");
      id = new ElementId(4, 3);
      this.d.Add(id, "POLYMARKER");
      id = new ElementId(4, 4);
      this.d.Add(id, "TEXT");
      id = new ElementId(4, 5);
      this.d.Add(id, "RESTRICTED TEXT");
      id = new ElementId(4, 6);
      this.d.Add(id, "APPEND TEXT");
      id = new ElementId(4, 7);
      this.d.Add(id, "POLYGON");
      id = new ElementId(4, 8);
      this.d.Add(id, "POLYGON SET");
      id = new ElementId(4, 9);
      this.d.Add(id, "CELL ARRAY");
      id = new ElementId(5, 1);
      this.d.Add(id, "LINE BUNDLE INDEX");
      id = new ElementId(5, 10);
      this.d.Add(id, "TEXT FONT INDEX");
      id = new ElementId(5, 11);
      this.d.Add(id, "TEXT PRECISION");
      id = new ElementId(5, 12);
      this.d.Add(id, "CHARACTER EXPANSION FACTOR");
      id = new ElementId(5, 13);
      this.d.Add(id, "CHARACTER SPACING");
      id = new ElementId(5, 14);
      this.d.Add(id, "TEXT COLOUR");
      id = new ElementId(5, 15);
      this.d.Add(id, "CHARACTER HEIGHT");
      id = new ElementId(5, 0x10);
      this.d.Add(id, "CHARACTER ORIENTATION");
      id = new ElementId(5, 0x11);
      this.d.Add(id, "TEXT PATH");
      id = new ElementId(5, 0x12);
      this.d.Add(id, "TEXT ALIGNMENT");
      id = new ElementId(5, 0x13);
      this.d.Add(id, "CHARACTER SET INDEX");
      id = new ElementId(5, 2);
      this.d.Add(id, "LINETYPE");
      id = new ElementId(5, 20);
      this.d.Add(id, "ALTERNATE CHARACTER SET INDEX");
      id = new ElementId(5, 0x15);
      this.d.Add(id, "FILL BUNDLE INDEX");
      id = new ElementId(5, 0x16);
      this.d.Add(id, "INTERIORSTYLE");
      id = new ElementId(5, 0x17);
      this.d.Add(id, "FILL COLOUR");
      id = new ElementId(5, 0x18);
      this.d.Add(id, "HATCH INDEX");
      id = new ElementId(5, 0x19);
      this.d.Add(id, "PATTERN INDEX");
      id = new ElementId(5, 0x1a);
      this.d.Add(id, "EDGE BUNDLE INDEX");
      id = new ElementId(5, 0x1b);
      this.d.Add(id, "EDGETYPE");
      id = new ElementId(5, 0x1c);
      this.d.Add(id, "EDGEWIDTH");
      id = new ElementId(5, 0x1d);
      this.d.Add(id, "EDGE COLOUR");
      id = new ElementId(5, 3);
      this.d.Add(id, "LINE WIDTH");
      id = new ElementId(5, 30);
      this.d.Add(id, "EDGE VISIBILITY");
      id = new ElementId(5, 0x1f);
      this.d.Add(id, "FILL REFERENCE POINT");
      id = new ElementId(5, 0x20);
      this.d.Add(id, "PATTERN TABLE");
      id = new ElementId(5, 0x21);
      this.d.Add(id, "PATTERN SIZE");
      id = new ElementId(5, 0x22);
      this.d.Add(id, "COLOUR TABLE");
      id = new ElementId(5, 0x23);
      this.d.Add(id, "ASPECT SOURCE FLAGS");
      id = new ElementId(5, 0x24);
      this.d.Add(id, "PICK IDENTIFIER");
      id = new ElementId(5, 0x25);
      this.d.Add(id, "LINECAP");
      id = new ElementId(5, 0x26);
      this.d.Add(id, "LINEJOIN");
      id = new ElementId(5, 0x27);
      this.d.Add(id, "LINE TYPE CONTINUATION");
      id = new ElementId(5, 4);
      this.d.Add(id, "LINE COLOUR");
      id = new ElementId(5, 40);
      this.d.Add(id, "LINE TYPE INITIAL OFFSET");
      id = new ElementId(5, 0x29);
      this.d.Add(id, "TEXT SCORE TYPE");
      id = new ElementId(5, 0x2a);
      this.d.Add(id, "RESTRICTED TEXT TYPE");
      id = new ElementId(5, 0x2b);
      this.d.Add(id, "INTERPOLATED INTERIOR");
      id = new ElementId(5, 0x2c);
      this.d.Add(id, "EDGECAP");
      id = new ElementId(5, 0x2d);
      this.d.Add(id, "EDGEJOIN");
      id = new ElementId(5, 0x2e);
      this.d.Add(id, "EDGE TYPE CONTINUATION");
      id = new ElementId(5, 0x2f);
      this.d.Add(id, "EDGE TYPE INITIAL OFFSET");
      id = new ElementId(5, 0x30);
      this.d.Add(id, "SYMBOL LIBRARY INDEX");
      id = new ElementId(5, 0x31);
      this.d.Add(id, "SYMBOL COLOUR");
      id = new ElementId(5, 5);
      this.d.Add(id, "MARKER BUNDLE INDEX");
      id = new ElementId(5, 50);
      this.d.Add(id, "SYMBOLSIZE");
      id = new ElementId(5, 0x33);
      this.d.Add(id, "SYMBOL ORIENTATION");
      id = new ElementId(5, 6);
      this.d.Add(id, "MARKERTYPE");
      id = new ElementId(5, 7);
      this.d.Add(id, "MARKERSIZE");
      id = new ElementId(5, 8);
      this.d.Add(id, "MARKER COLOUR");
      id = new ElementId(5, 9);
      this.d.Add(id, "TEXT BUNDLE INDEX");
      id = new ElementId(6, 1);
      this.d.Add(id, "ESCAPE");
      id = new ElementId(7, 1);
      this.d.Add(id, "MESSAGE");
      id = new ElementId(7, 2);
      this.d.Add(id, "APPLICATION DATA");
      id = new ElementId(8, 1);
      this.d.Add(id, "COPY SEGMENT");
      id = new ElementId(8, 2);
      this.d.Add(id, "INHERITANCE FILTER");
      id = new ElementId(8, 3);
      this.d.Add(id, "CLIP INHERITANCE");
      id = new ElementId(8, 4);
      this.d.Add(id, "SEGMENT TRANSFORMATION");
      id = new ElementId(8, 5);
      this.d.Add(id, "SEGMENT HIGHLIGHTING");
      id = new ElementId(8, 6);
      this.d.Add(id, "SEGMENT DISPLAY PRIORITY");
      id = new ElementId(8, 7);
      this.d.Add(id, "SEGMENT PICK PRIORITY");
      id = new ElementId(9, 1);
      this.d.Add(id, "APPLICATION STRUCTURE ATTRIBUTE");
    }

    // Nested Types
    [StructLayout(LayoutKind.Sequential)]
    private struct ElementId : IComparable
    {
      public readonly int ClassId;
      public readonly int Id;
      public ElementId(int ClassId, int Id)
      {
        this = new ElementId();
        this.ClassId = ClassId;
        this.Id = Id;
      }

      public override int GetHashCode()
      {
        return ((this.ClassId * 0x3e8) + this.Id);
      }

      public int CompareTo(object obj)
      {
        ElementId id;
        ElementId id2 = (ElementId)obj;//TODO: cast
        if (this.ClassId == id2.ClassId)
        {
          id = (ElementId)obj;
          return (this.Id - id.Id);
        }
        id = (ElementId)obj;
        return (this.ClassId - id.ClassId);
      }
    }
  }



}
