namespace PIE_BE.Excel
{
    public class ExcelApplication
    {
        private static ExcelApplication instance;
        private Microsoft.Office.Interop.Excel.Application application;

        private ExcelApplication()
        {
            application = new Microsoft.Office.Interop.Excel.Application();
            application.Visible = false;
        }

        public static ExcelApplication Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ExcelApplication();
                }
                return instance;
            }
        }

        public Microsoft.Office.Interop.Excel.Application Application
        {
            get
            {
                return application;
            }
        }
    }
}
