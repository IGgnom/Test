using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GlazkiSaveAfanavicheva.Data
{
    public partial class Agent
    {
        public int CountYear
        {
            get
            {
                return ProductSale.Where(i => i.SaleDate.AddYears(1) >= DateTime.Today).Count();
            }
        }

        public int Percent
        {
            get
            {
                if (ProductSale.Count >= 0 && ProductSale.Count < 10000)
                {
                    return 0;
                }
                else if (ProductSale.Count >= 10000 && ProductSale.Count < 50000)
                {
                    return 5;
                }
                else if (ProductSale.Count >= 50000 && ProductSale.Count < 150000)
                {
                    return 10;
                }
                else if (ProductSale.Count >= 150000 && ProductSale.Count < 500000)
                {
                    return 20;
                }
                else return 25;
            }
        }

        public ImageSource ImageSourc
        {
            get
            {
                if (string.IsNullOrEmpty(Logo))
                {
                    return null;
                }
                string workDir = Environment.CurrentDirectory;
                string projectDirectory = Directory.GetParent(workDir).Parent.FullName + "\\Resources" +Logo.Trim();
                return new BitmapImage(new Uri(projectDirectory));
            }
        }
    }
}
