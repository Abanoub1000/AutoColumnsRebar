using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;


namespace AutoColumnsRebar
{
  [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
    public Result Execute (ExternalCommandData commandData, ref string message, ElementSet elements)
    { 
      UserControl userControl = new UserControl();
    UIDocument uidoc = commandData.Application.ActiveUIDocument;
      userControl.ShowDialog();
    Document doc = uidoc.Document;
      StringBuilder prompt = new StringBuilder ();
    try
    {
      IList<Element> elems = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_StructuralColumns).OfClass(typeof(FamilyInstance)).ToElements();
      foreach (Element elem in elems) 
      {
        prompt.Append(elem.Name.ToString() + Environment.NewLine +
          elem.Id + Environment.NewLine +
          elem.get_Parameter(BuiltInParameter.KEYNOTE_PARAM) + Environment.NewLine + Environment.NewLine);
      }
      TaskDialog.Show("Test", prompt.ToString());
      return Result.Succeeded;
    }catch( Exception e)
      {
        message = e.Message;
        TaskDialog.Show(" Result Failed", message);
        return Result.Failed;
      }
    }

    }
}
