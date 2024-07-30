using InventoryMgmt.Model;
using InventoryMgmt.Service;
using InventoryMgmt.Interface;
using System.ComponentModel.DataAnnotations;
using System.Text;

// guide: https://www.aligrant.com/web/blog/2020-07-20_capturing_console_outputs_with_microsoft_test_framework

namespace InventoryMgmtQA.Service
{
    [TestClass]
    public class InventoryManagerTest
    {
        private IInventoryManager _inventoryManager = new InventoryManager();

        [TestMethod]
        public void TestAddProduct()
        {
            using (StringWriter sw = new StringWriter())
            {
                // capture console output
                Console.SetOut(sw);

                // create a new product with valid attribute values
                _inventoryManager.AddNewProduct(
                    "TestProduct",
                    1,
                    1.23M
                );

                // console output should contains 'success'
                Assert.IsTrue(sw.ToString().Contains("success"));
            }
        }

        [TestMethod]
        public void TestAddProductPriceNegative()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                _inventoryManager.AddNewProduct(
                    "TestProduct",
                    1,
                    -1.0M
                );
                Assert.IsFalse(sw.ToString().Contains("success"));
            }
        }

        [TestMethod]
        public void TestRemovedExistingProduct()
        {
            // add product method
            TestAddProduct();
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                // remove product with valid Product ID
                _inventoryManager.RemoveProduct(
                    1
                );

                // console output should contains 'success'
                Assert.IsTrue(sw.ToString().Contains("success"));
            }
        }

        [TestMethod]
        public void TestRemovedProductNegativeProductId()
        {
            // add product method
            TestAddProduct();
            using (StringWriter sw = new StringWriter())
            {
                // capture console output
                Console.SetOut(sw);
                // remove product with invalid Product ID
                _inventoryManager.RemoveProduct(
                    -1
                );

                // console output should contains 'Product not found'
                Assert.IsTrue(sw.ToString().Contains("Product not found"));
            }
        }

        [TestMethod]
        public void TestRemovedNonExistingProduct()
        {
            // add product method
            TestAddProduct();
            using (StringWriter sw = new StringWriter())
            {
                // capture console output
                Console.SetOut(sw);
                // remove product with invalid Product ID
                _inventoryManager.RemoveProduct(
                    2
                );

                // console output should contains 'Product not found'
                Assert.IsTrue(sw.ToString().Contains("Product not found"));
            }
        }

        [TestMethod]
        public void TestUpdateExistingProduct()
        {
            // add product method
            TestAddProduct();
            using (StringWriter sw = new StringWriter())
            {
                // capture console output
                Console.SetOut(sw);
                // update product quantity of existing product
                _inventoryManager.UpdateProduct(
                    1,
                    15
                );

                // console output should contains 'Product not found'
                Assert.IsTrue(sw.ToString().Contains("updated"));
            }
        }

        [TestMethod]
        public void TestUpdateExistingProductNegativeQuantity()
        {
            // add product method
            TestAddProduct();
            using (StringWriter sw = new StringWriter())
            {
                // capture console output
                Console.SetOut(sw);
                // update product quantity of existing product but negative quantity
                _inventoryManager.UpdateProduct(
                    1,
                    -15
                );

                // console output should contains 'Please try again.'
                Assert.IsTrue(sw.ToString().Contains("Please try again."));
            }
        }

        [TestMethod]
        public void TestUpdateNonExistingProduct()
        {
            // add product method
            TestAddProduct();
            using (StringWriter sw = new StringWriter())
            {
                // capture console output
                Console.SetOut(sw);
                // update product quantity of non existing product
                _inventoryManager.UpdateProduct(
                    2,
                    15
                );

                // console output should contains 'Product not found'
                Assert.IsTrue(sw.ToString().Contains("Product not found"));
            }
        }

        [TestMethod]
        public void TestGetTotalValue()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                // create a new product with valid attribute values
                _inventoryManager.AddNewProduct(
                    "TestProduct",
                    1,
                    2.56M
                );
                // get product total value
                _inventoryManager.GetTotalValue();
                // console output should contains '2.56'
                Assert.IsTrue(sw.ToString().Contains("2.56"));
            }
        }

        [TestMethod]
        public void TestGetTotalValueNoProduct()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                // get product total value
                _inventoryManager.GetTotalValue();
                // console output should contains '0.00'
                Assert.IsTrue(sw.ToString().Contains("0.00"));
            }
        }

        [TestMethod]
        public void TestProductList()
        {
            // add product method
            TestAddProduct();
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                // list products
                _inventoryManager.ListProducts();
                // console output should contains 'TestProduct'
                Assert.IsTrue(sw.ToString().Contains("TestProduct"));
            }
        }

        [TestMethod]
        public void TestNoProductList()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                // list products
                _inventoryManager.ListProducts();
                // console output should contains 'No products in here.'
                Assert.IsTrue(sw.ToString().Contains("No products in here."));
            }
        }

    }
}