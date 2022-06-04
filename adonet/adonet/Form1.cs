using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;

namespace adonet
{
    public partial class Form1 : Form
    {
        ProductsEntities _context;


        public Form1()
        {
            InitializeComponent();
        }

        

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _context = new ProductsEntities();
            _context.Categories.Load();



            this.categoriesBindingSource.DataSource =
                _context.Categories.Local.ToBindingList();

           
        }


        

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            this._context.Dispose();
        }

        private void categoriesBindingNavigatorSaveItem_Click_1(object sender, EventArgs e)
        {
            this.Validate();
            foreach (var product in _context.Products.Local.ToList())
            {

                
#pragma warning disable CS0472 // Результат значения всегда одинаковый, так как значение этого типа никогда не равно NULL
                if (product.CategoryId == null)
                {
                    continue;
                }
#pragma warning restore CS0472 // Результат значения всегда одинаковый, так как значение этого типа никогда не равно NULL

                _context.Products.Remove(product);
            }

            
            this._context.SaveChanges();

            
            this.categoriesDataGridView.Refresh();
            this.productsDataGridView.Refresh();
        }
    }
}
