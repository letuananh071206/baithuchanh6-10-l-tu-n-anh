using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Bai5._3
{
    public partial class Form1 : Form
    {
        List<Product> products;
        BindingSource bindingSource;

        public Form1()
        {
            InitializeComponent();

            products = new List<Product>();

            bindingSource = new BindingSource();
            bindingSource.DataSource = products;

            dgvProducts.DataSource = bindingSource;

            dgvProducts.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvProducts.MultiSelect = false;
            dgvProducts.AutoGenerateColumns = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            decimal price;
            int quantity;

            if (txtProductId.Text.Trim() == "" ||
                txtProductName.Text.Trim() == "")
            {
                MessageBox.Show(
                    "Vui long nhap ma SP va ten SP!");

                return;
            }

            if (!decimal.TryParse(
                txtUnitPrice.Text, out price))
            {
                MessageBox.Show(
                    "Don gia khong hop le!");

                return;
            }

            if (!int.TryParse(
                txtQuantity.Text, out quantity))
            {
                MessageBox.Show(
                    "So luong khong hop le!");

                return;
            }

            Product product = new Product();

            product.ProductId =
                txtProductId.Text.Trim();

            product.ProductName =
                txtProductName.Text.Trim();

            product.UnitPrice = price;
            product.Quantity = quantity;

            product.Category =
                txtCategory.Text.Trim();

            products.Add(product);

            bindingSource.ResetBindings(false);

            ClearInput();
        }

        private void dgvProducts_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            Product product =
                dgvProducts.Rows[e.RowIndex]
                .DataBoundItem as Product;

            if (product != null)
            {
                txtProductId.Text =
                    product.ProductId;

                txtProductName.Text =
                    product.ProductName;

                txtUnitPrice.Text =
                    product.UnitPrice.ToString();

                txtQuantity.Text =
                    product.Quantity.ToString();

                txtCategory.Text =
                    product.Category;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null)
            {
                return;
            }

            Product product =
                dgvProducts.CurrentRow.DataBoundItem
                as Product;

            if (product == null)
            {
                return;
            }

            decimal price;
            int quantity;

            if (!decimal.TryParse(
                txtUnitPrice.Text, out price))
            {
                MessageBox.Show(
                    "Don gia khong hop le!");

                return;
            }

            if (!int.TryParse(
                txtQuantity.Text, out quantity))
            {
                MessageBox.Show(
                    "So luong khong hop le!");

                return;
            }

            product.ProductId =
                txtProductId.Text.Trim();

            product.ProductName =
                txtProductName.Text.Trim();

            product.UnitPrice = price;
            product.Quantity = quantity;

            product.Category =
                txtCategory.Text.Trim();

            bindingSource.ResetBindings(false);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null)
            {
                return;
            }

            Product product =
                dgvProducts.CurrentRow.DataBoundItem
                as Product;

            if (product == null)
            {
                return;
            }

            DialogResult result =
                MessageBox.Show(
                    "Ban co chac muon xoa san pham nay?",
                    "Xac nhan",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                products.Remove(product);

                bindingSource.ResetBindings(false);

                ClearInput();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword =
                txtSearch.Text.Trim().ToLower();

            if (keyword == "")
            {
                dgvProducts.DataSource =
                    bindingSource;

                return;
            }

            List<Product> result =
                new List<Product>();

            foreach (Product product in products)
            {
                if (product.ProductName
                    .ToLower()
                    .Contains(keyword))
                {
                    result.Add(product);
                }
            }

            dgvProducts.DataSource = result;
        }

        private void ClearInput()
        {
            txtProductId.Clear();
            txtProductName.Clear();
            txtUnitPrice.Clear();
            txtQuantity.Clear();
            txtCategory.Clear();
        }
    }

    public class Product
    {
        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public string Category { get; set; }
    }
}