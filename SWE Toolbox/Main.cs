namespace SWE_Toolbox
{

    #region Namespaces
    using Autodesk.Revit.ApplicationServices;
    using Autodesk.Revit.Attributes;
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Windows.Media.Imaging;
    #endregion
    /// <summary>
    /// Plugin's main entry point.
    /// </summary>
    /// <seealso cref="Autodesk.Revit.UI.IExternalApplication"/>

    public class Main : IExternalApplication
    #region external application public methods
    {
        /// <summary>
        /// Called when Revit starts up.
        /// </summary>
        /// <param name="application">The application.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Result OnStartup(UIControlledApplication application)
        {
            //Plugin's main tab name.
            string tabName = "SWE Toolbox";

            //Panel name hosted on ribbion tab.
            string panelMechanical = "Mechanical";
            string panelElectrical = "Electrial";
            string panelPlumbing = "Plumbing";
            string panelBIM = "BIM";

            //Create Tab on Revit UI.
            application.CreateRibbonTab(tabName);

            //Create first panel on ribbion tab
            var panelMech = application.CreateRibbonPanel(tabName, panelMechanical);
            var panelElec = application.CreateRibbonPanel(tabName, panelElectrical);
            var panelPlum = application.CreateRibbonPanel(tabName, panelPlumbing);
            var panel_BIM = application.CreateRibbonPanel(tabName, panelBIM);

            //Create push buttondata and populate it with information
            var CapitalizeLoadNames = new PushButtonData("Capitalize Load Names", "Capitalize\nLoadName", Assembly.GetExecutingAssembly().Location, "SWE_Toolbox.CommandUpper")
            {
                ToolTipImage = new BitmapImage(new Uri(@"C: \Users\jms\source\repos\SWE Toolbox\SWE Toolbox\SWE CAP.png")),
                ToolTip = "Capitalizes all cells that contain the parameter Load Name"

            };

            var CapButton = panelElec.AddItem(CapitalizeLoadNames) as PushButton;

            CapButton.LargeImage = new BitmapImage(new Uri(@"C: \Users\jms\source\repos\SWE Toolbox\SWE Toolbox\SWE CAP 32.png"));
            return Result.Succeeded;
        }


        /// <summary>
        /// Called when Revit shutsdown
        /// </summary>
        /// <param name="application">The application.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Result OnShutdown(UIControlledApplication applicaiton)
        {
            return Result.Succeeded;
        }
        #endregion
    }
}