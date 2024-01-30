using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB.Electrical;
using System;
using System.Collections.Generic;
using System.Windows.Documents;


namespace SWE_Toolbox

{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class CommandUpper : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                Document doc = commandData.Application.ActiveUIDocument.Document;


                using (Transaction tx = new Transaction(doc))
                {
                    tx.Start("Capitalize Load Names");

                    FilteredElementCollector collector = new FilteredElementCollector(doc);
                    collector.OfClass(typeof(ElectricalSystem));
                    List<string> capitalizedloadNames = new List<string>();
                    foreach (ElectricalSystem system in collector)
                    {
                        Parameter loadNameParam = system.get_Parameter(BuiltInParameter.RBS_ELEC_CIRCUIT_NAME);
                        if (loadNameParam != null && loadNameParam.StorageType == StorageType.String)
                        {
                            string currentLoadName = loadNameParam.AsString();
                            if (!string.IsNullOrEmpty(currentLoadName))
                            {
                                // Capitalize the LoadName
                                string capitalizedLoadName = currentLoadName.ToUpper();

                                // Set the new capitalized load name
                                loadNameParam.Set(capitalizedLoadName);
                                capitalizedloadNames.Add(capitalizedLoadName);

                            }
                        }

                    }


                    // Commit the transaction
                    tx.Commit();
                }


                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Result.Failed;
            }
        }
    }
}