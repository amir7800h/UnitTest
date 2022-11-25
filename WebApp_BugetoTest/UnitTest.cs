using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp_Bugeto.Controllers;
using WebApp_Bugeto.Services;
using Xunit;

namespace WebApp_BugetoTest
{
    public class UnitTest
    {
        [Fact]
        public void Test_MockBehavior()
        {
            var moq = new Mock<IProductService>(MockBehavior.Strict);
            moq.Setup(p => p.GetProductPrice(1)).Returns(15000);
            var result = moq.Object.GetProductPrice(1);
        }

        [Fact]
        public void Test_PropertySetupTest()
        {
            var moq = new Mock<IProductService>();
            moq.Setup(p => p.ProductCount).Returns(25);
        }
        [Fact]
        public void Test_Add_Product_SaveState()
        {
            var moq = new Mock<IProductService>();
            Product product = new Product { Id = 1, Name = "test" };
            moq.Setup(p => p.AddProduct(product)).Returns(product);
            // moq.SetupProperty(p => p.ProductCount);
            moq.SetupAllProperties();
            HomeController controller = new HomeController(moq.Object,null);
            controller.AddProduct(product);

            Assert.Equal(1, moq.Object.ProductCount);
        }



        [Fact]
        public void Test_SetupSequence()
        {
            var moq = new Mock<IProductService>(MockBehavior.Strict);
            moq.SetupSequence(p => p.GetProductPrice(1)).Returns(15000).Returns(20000).Returns(50000);
            var result = moq.Object.GetProductPrice(1);
        }

        [Fact]
        public void Test_ProtectedMock()
        {
            var moq = new Mock<CarService>();

            moq.Protected().Setup<int>("GetCarPrice").Returns(50000);
        }

        [Fact]
        public void Test_It()
        {
            var moq = new Mock<IProductService>();

            int[] ids = new int[] { 5, 85, 45, 745, 86, 896569, 56, 1 };
            moq.Setup(p => p.GetProductPrice(It.IsAny<int>())).Returns(15000);
            moq.Setup(p => p.GetProductPrice(It.IsInRange(1,1000, Moq.Range.Inclusive))).Returns(25000);
            moq.Setup(p => p.GetProductPrice(It.IsIn(ids))).Returns(35000);
            moq.Setup(p => p.GetProductPrice(It.IsNotIn(ids))).Returns(16000);
        }


        [Fact]
        public void Test_OutParam()
        {
            var moq = new Mock<IProductService>();
            int price = 0;
            moq.Setup(p => p.GetProductPrice(1, out price ));
        }


        [Fact]
        public void Test_ChainMock()
        {
            var moq = new Mock<IProductService>();
            moq.Setup(p => p.IbrandProxy.Brand.BrandId).Returns(1);
        }
        [Fact]
        public void Test_Behavior_SendMessageWithEmail()
        {

            var moq = new Mock<IMessage>();
            var moqProduct = new Mock<IProductService>();

            HomeController controller = new HomeController(moqProduct.Object,moq.Object );

            controller.SendMessage("Salam", 1, Messagetype.Email);

            //moq.Verify(p => p.Sms(It.IsAny<string>(), It.IsAny<int>()),"اس ام اس ارسال نشد");
            moq.Verify(p => p.Email(It.IsAny<string>(), It.IsAny<int>()));
            
        }
    }
}
